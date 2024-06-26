using System.Data;
using System.Windows;
using System.Windows.Controls;
using Npgsql;
using NpgsqlTypes;

namespace application.ShowWindow;

public partial class CreateReview : Window
{
    public int customer_id;
    
    public CreateReview()
    {
        InitializeComponent();
        
        review_text.IsEnabled = false;
        save.IsEnabled = false;
    }
    
    private bool IsPositiveNumber(string value)
    {
        if (long.TryParse(value, out long number))
        {
            return number > 0;
        }
        return false;
    }

    private bool IsWithinMaxLength(string value, int maxLength)
    {
        return value.Length <= maxLength;
    }

    private void login_Click(object sender, RoutedEventArgs e)
    {
        review_text.IsEnabled = false;
        save.IsEnabled = false;
        
        if (IsPositiveNumber(phone_text.Text) && IsWithinMaxLength(phone_text.Text, 16))
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SQLclass.user_string))
            {
                connection.Open();

                try
                {
                    string phone = phone_text.Text;
                    using (NpgsqlCommand command = new NpgsqlCommand($"SELECT * FROM get_customer_by_phone(@phone_number)", connection))
                    {
                        command.Parameters.Add("phone_number", NpgsqlDbType.Varchar).Value = phone;
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                customer_id = id;
                                
                                review_text.IsEnabled = true;
                                save.IsEnabled = true;
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show($"Возникла проблема: {ex}");
                }
            }
        }
        else
        {
            MessageBox.Show("Введите правильный номер телефона: только цифры, длина меньше 16 символов");
        }
    }

    private void save_Click(object sender, RoutedEventArgs e)
    {
        string newReview = review_text.Text;
        DateTime currentDateTime = DateTime.Now;
        int rating = (int)rating_slider.Value;
        
        using (NpgsqlConnection connection = new NpgsqlConnection(SQLclass.user_string))
        {
            connection.Open();

            using (NpgsqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    using (NpgsqlCommand command = new NpgsqlCommand("add_review", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        
                        command.Parameters.AddWithValue("cust_id", customer_id);
                        command.Parameters.Add("review_date", NpgsqlDbType.Date).Value = currentDateTime;
                        command.Parameters.AddWithValue("review_rating", rating);
                        command.Parameters.Add("review_text", NpgsqlDbType.Varchar).Value = newReview;

                        command.ExecuteNonQuery();

                        transaction.Commit();

                        MessageBox.Show("Отзыв оставлен успешно");
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show($"Ошибка при оставлении отзыва: {ex.Message}");
                }
            }
        }
    }
}