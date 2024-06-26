using System.Data;
using System.Windows;
using System.Windows.Controls;
using Npgsql;
using NpgsqlTypes;

namespace application.ShowWindow;

public partial class CreateServiceSheet : Window
{
    public int customer_id;
    public int mechanic_id;
    public int car_id;
    
    public CreateServiceSheet()
    {
        InitializeComponent();
        
        mechanicBox.IsEnabled = false;
        carBox.IsEnabled = false;
        comment_text.IsEnabled = false;
        save.IsEnabled = false;
        
        using (NpgsqlConnection connection = new NpgsqlConnection(SQLclass.user_string))
        {
            connection.Open();

            using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM get_all_mechanic()", connection))
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
                        mechanicBox.Items.Add(item);
                    }
                }
            }
        
            using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM get_all_label_model()", connection))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string label = reader.GetString(1);
                        string model = reader.GetString(2);

                        ComboBoxItem item = new ComboBoxItem();
                        item.Content = label + ' ' + model;
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
                                
                                mechanicBox.IsEnabled = true;
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
            ComboBoxItem itemMechanicId = (ComboBoxItem)mechanicBox.SelectedItem;
            int newMechanicId = int.Parse(itemMechanicId.Name.Substring(2));
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
                        using (NpgsqlCommand command = new NpgsqlCommand("add_servicesheet", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            
                            command.Parameters.AddWithValue("p_customer_id", customer_id);
                            command.Parameters.AddWithValue("p_mechanic_id", newMechanicId);
                            command.Parameters.Add("p_date", NpgsqlDbType.Date).Value = newDate;
                            command.Parameters.AddWithValue("p_label_model_id", newCarId);
                            command.Parameters.Add("p_problem_description", NpgsqlDbType.Varchar).Value = newComment;
                            command.Parameters.Add("p_status", NpgsqlDbType.Boolean).Value = newStatus;

                            command.ExecuteNonQuery();

                            transaction.Commit();

                            MessageBox.Show("Запись на обслуживание создана успешно");
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Ошибка при создании записи на обслуживание: {ex.Message}");
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