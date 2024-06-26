using System.Data;
using System.Windows;
using System.Windows.Controls;
using Npgsql;
using NpgsqlTypes;

namespace application.InteractWindow.ForCustomer;

public partial class UpdateCustomer : Window
{
    public int id;
    
    public UpdateCustomer()
    {
        InitializeComponent();
        
        firstName_text.IsEnabled = false;
        secondName_text.IsEnabled = false;
        thirdName_text.IsEnabled = false;
        mail_text.IsEnabled = false;
        phone_text.IsEnabled = false;
        country_text.IsEnabled = false;
        address_text.IsEnabled = false;
        requisites_text.IsEnabled = false;

        using (NpgsqlConnection connection = new NpgsqlConnection(SQLclass.manager_string))
        {
            connection.Open();

            string sql1 = "SELECT * FROM get_all_customer()";
            using (NpgsqlCommand command = new NpgsqlCommand(sql1, connection))
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
                        customersBox.Items.Add(item);
                    }
                }
            }
        }
    }
    
    private void getID_Click(object sender, RoutedEventArgs e)
    {
        ComboBoxItem itemCarsId = (ComboBoxItem)customersBox.SelectedItem;
        id = int.Parse(itemCarsId.Name.Substring(2));
        
        using (NpgsqlConnection connection = new NpgsqlConnection(SQLclass.admin_string))
        {
            connection.Open();

            using (NpgsqlTransaction transaction = connection.BeginTransaction())
            {
                string sqlQuery = $"SELECT * FROM get_all_customer() WHERE customer_id = {id};";

                using (NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string secondName = reader.GetString(1);
                            string firstName = reader.GetString(2);
                            string thirdName = reader.GetString(3);
                            string mail = reader.GetString(4);
                            string phone = reader.GetString(5);
                            string country = reader.GetString(6);
                            string address = reader.GetString(7);
                            string requisites = reader.GetString(8);
                            
                            secondName_text.Text = secondName;
                            firstName_text.Text = firstName;
                            thirdName_text.Text = thirdName;
                            mail_text.Text = mail;
                            phone_text.Text = phone;
                            country_text.Text = country;
                            address_text.Text = address;
                            requisites_text.Text = requisites;
                            
                            firstName_text.IsEnabled = true;
                            secondName_text.IsEnabled = true;
                            thirdName_text.IsEnabled = true;
                            mail_text.IsEnabled = true;
                            phone_text.IsEnabled = true;
                            country_text.IsEnabled = true;
                            address_text.IsEnabled = true;
                            requisites_text.IsEnabled = true;
                        }
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
    
    private bool SaveCustomer()
    {
        string secondText = secondName_text.Text;
        string firstnameText = firstName_text.Text;
        string thirdnameText = thirdName_text.Text;
        string mailText = mail_text.Text;
        string phoneText = phone_text.Text;
        string countryText = country_text.Text;
        string addressText = address_text.Text;
        string requisitesText = requisites_text.Text;

        if (string.IsNullOrEmpty(secondText) || string.IsNullOrEmpty(firstnameText) || string.IsNullOrEmpty(thirdnameText) ||
            string.IsNullOrEmpty(mailText) || string.IsNullOrEmpty(phoneText) || string.IsNullOrEmpty(addressText) ||
            string.IsNullOrEmpty(requisitesText) || string.IsNullOrEmpty(countryText))
        {
            MessageBox.Show("Пожалуйста, заполните все поля.");
            return false;
        }

        if (!IsWithinMaxLength(secondText, 20) || !IsWithinMaxLength(firstnameText, 20) || !IsWithinMaxLength(thirdnameText, 20))
        {
            MessageBox.Show("Поля имени не могут содержать более 20 символов.");
            return false;
        }

        if (!IsWithinMaxLength(mailText, 40))
        {
            MessageBox.Show("Поле 'Почта' не может содержать более 40 символов.");
            return false;
        }
        
        if (!IsPositiveNumber(phoneText) || !IsPositiveNumber(requisitesText))
        {
            MessageBox.Show("Пожалуйста, введите положительное число в поля числовых значений.");
            return false;
        }
        
        if (!IsWithinMaxLength(phoneText, 16))
        {
            MessageBox.Show("Поле 'Телефон' не может содержать более 16 символов.");
            return false;
        }
        
        if (!IsWithinMaxLength(countryText, 50))
        {
            MessageBox.Show("Поле 'Страна' не может содержать более 50 символов.");
            return false;
        }
        
        if (!IsWithinMaxLength(addressText, 1000))
        {
            MessageBox.Show("Поле 'Адрес' не может содержать более 1000 символов.");
            return false;
        }
        
        if (!IsWithinMaxLength(requisitesText, 16))
        {
            MessageBox.Show("Поле 'Реквизиты' не может содержать более 16 символов.");
            return false;
        }
        
        return true;
    }
    
    private void save_Click(object sender, RoutedEventArgs e)
    {
        if (SaveCustomer())
        {
            string secondText = secondName_text.Text;
            string firstnameText = firstName_text.Text;
            string thirdnameText = thirdName_text.Text;
            string mailText = mail_text.Text;
            string phoneText = phone_text.Text;
            string countryText = country_text.Text;
            string addressText = address_text.Text;
            string requisitesText = requisites_text.Text;
            
            using (NpgsqlConnection connection = new NpgsqlConnection(SQLclass.manager_string))
            {
                connection.Open();

                using (NpgsqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (NpgsqlCommand command = new NpgsqlCommand("update_customer", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("customer_id", id);
                            command.Parameters.Add("new_secondname", NpgsqlDbType.Varchar).Value = secondText;
                            command.Parameters.Add("new_firstname", NpgsqlDbType.Varchar).Value = firstnameText;
                            command.Parameters.Add("new_thirdname", NpgsqlDbType.Varchar).Value = thirdnameText;
                            command.Parameters.Add("new_mail", NpgsqlDbType.Varchar).Value = mailText;
                            command.Parameters.Add("new_phone", NpgsqlDbType.Varchar).Value = phoneText;
                            command.Parameters.Add("new_country", NpgsqlDbType.Varchar).Value = countryText;
                            command.Parameters.Add("new_address", NpgsqlDbType.Varchar).Value = addressText;
                            command.Parameters.Add("new_requisites", NpgsqlDbType.Varchar).Value = requisitesText;

                            command.ExecuteNonQuery();

                            transaction.Commit();

                            MessageBox.Show("Данные клиента обновлены успешно");
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Ошибка при добавлении обновлении данных клиента: {ex.Message}");
                    }
                }
            }
        }
    }
}