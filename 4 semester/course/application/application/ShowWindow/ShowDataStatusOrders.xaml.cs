using System.Data;
using System.Windows;
using Npgsql;
using NpgsqlTypes;

namespace application.ShowWindow;

public partial class ShowDataStatusOrders : Window
{
    public int customer_id;
    
    public ShowDataStatusOrders()
    {
        InitializeComponent();
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
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show($"Возникла проблема со входом: {ex}");
                }
                
                using (NpgsqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM get_date_status(@cust_id)", connection))
                        {
                            command.Parameters.Add("cust_id", NpgsqlDbType.Numeric).Value = customer_id;
                            using (NpgsqlDataReader reader = command.ExecuteReader())
                            {
                                DataTable table = new DataTable();
                                table.Load(reader);
                                orderCars.ItemsSource = table.DefaultView;
                            }

                            transaction.Commit();
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Ошибка при получении заказов машин: {ex.Message}");
                    }
                }
                
                using (NpgsqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM get_date_status_spare(@cust_id)", connection))
                        {
                            command.Parameters.Add("cust_id", NpgsqlDbType.Numeric).Value = customer_id;
                            using (NpgsqlDataReader reader = command.ExecuteReader())
                            {
                                DataTable table = new DataTable();
                                table.Load(reader);
                                orderSpareParts.ItemsSource = table.DefaultView;
                            }

                            transaction.Commit();
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Ошибка при получении заказов запчастей: {ex.Message}");
                    }
                }
            }
        }
        else
        {
            MessageBox.Show("Введите правильный номер телефона: только цифры, длина меньше 16 символов");
        }
    }
}