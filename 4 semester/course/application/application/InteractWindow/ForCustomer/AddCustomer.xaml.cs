using System.Data;
using System.Windows;
using Npgsql;
using NpgsqlTypes;

namespace application.InteractWindow.ForCustomer;

public partial class AddCustomer : Window
{
    public AddCustomer()
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
                        using (NpgsqlCommand command = new NpgsqlCommand("add_customer", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;

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

                            MessageBox.Show("Клиент добавлен успешно");
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Ошибка при добавлении клиента: {ex.Message}");
                    }
                }
            }
        }
    }
}