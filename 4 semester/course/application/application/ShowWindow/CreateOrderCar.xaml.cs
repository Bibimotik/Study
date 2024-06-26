using System.Data;
using System.Windows;
using System.Windows.Controls;
using Npgsql;
using NpgsqlTypes;

namespace application.ShowWindow;

public partial class CreateOrderCar : Window
{
    public int customer_id;
    public int manager_id;
    public int car_id;
    
    public CreateOrderCar()
    {
        InitializeComponent();

        managerBox.IsEnabled = false;
        carBox.IsEnabled = false;
        comment_text.IsEnabled = false;
        save.IsEnabled = false;
        
        using (NpgsqlConnection connection = new NpgsqlConnection(SQLclass.user_string))
        {
            connection.Open();

            using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM get_all_manager()", connection))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string secondname = reader.GetString(1);
                        string firstname = reader.GetString(2);
                        string thirdname = reader.GetString(3);

                        ComboBoxItem item = new ComboBoxItem();
                        item.Content = secondname + ' ' + firstname + ' ' + thirdname;
                        item.Name = "id" + id;
                        managerBox.Items.Add(item);
                    }
                }
            }
        
            using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM get_available_cars()", connection))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string label = reader.GetString(1);
                        string model = reader.GetString(2);
                        int year = reader.GetInt32(3);
                        int mileage = reader.GetInt32(4);

                        ComboBoxItem item = new ComboBoxItem();
                        item.Content = label + ' ' + model + ' ' + year.ToString() + ' ' + mileage.ToString();
                        item.Name = "id" + id;
                        carBox.Items.Add(item);
                    }
                }
            }
        }
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
                                
                                managerBox.IsEnabled = true;
                                carBox.IsEnabled = true;
                                comment_text.IsEnabled = true;
                                save.IsEnabled = true;
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show($"Возникла проблема со входом: {ex}");
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
        if (IsWithinMaxLength(comment_text.Text, 1000))
        {
            ComboBoxItem itemManagerId = (ComboBoxItem)managerBox.SelectedItem;
            int newManagerId = int.Parse(itemManagerId.Name.Substring(2));
            ComboBoxItem itemCarId = (ComboBoxItem)carBox.SelectedItem;
            int newCarId = int.Parse(itemCarId.Name.Substring(2));
            string newComment = comment_text.Text;
            bool newStatus = false;
            DateTime newDate = DateTime.Today;
            
            using (NpgsqlConnection connection = new NpgsqlConnection(SQLclass.user_string))
            {
                connection.Open();

                using (NpgsqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (NpgsqlCommand command = new NpgsqlCommand("add_ordercar", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            
                            command.Parameters.AddWithValue("order_customer_id", customer_id);
                            command.Parameters.AddWithValue("order_manager_id", newManagerId);
                            command.Parameters.Add("order_date", NpgsqlDbType.Date).Value = newDate;
                            command.Parameters.AddWithValue("order_car_id", newCarId);
                            command.Parameters.AddWithValue("order_status", newStatus);
                            command.Parameters.Add("order_comment", NpgsqlDbType.Varchar).Value = newComment;

                            command.ExecuteNonQuery();

                            transaction.Commit();

                            MessageBox.Show("Заказ машины создан успешно");
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Ошибка при создании заказа машины: {ex.Message}");
                    }
                }
            }
        }
        else
        {
            MessageBox.Show("Комментарий не может буть больше 1000 символов.");
        }
    }
}