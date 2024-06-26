using System.Data;
using System.Windows;
using Npgsql;
using NpgsqlTypes;

namespace application.Administration;

public partial class AddMechanic : Window
{
    public AddMechanic()
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

        if (string.IsNullOrEmpty(secondText) || string.IsNullOrEmpty(firstnameText) || string.IsNullOrEmpty(thirdnameText) ||
            string.IsNullOrEmpty(mailText) || string.IsNullOrEmpty(phoneText))
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
        
        if (!IsPositiveNumber(phoneText))
        {
            MessageBox.Show("Пожалуйста, введите положительное число в поля числовых значений.");
            return false;
        }
        
        if (!IsWithinMaxLength(phoneText, 16))
        {
            MessageBox.Show("Поле 'Телефон' не может содержать более 16 символов.");
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
            
            using (NpgsqlConnection connection = new NpgsqlConnection(SQLclass.admin_string))
            {
                connection.Open();

                using (NpgsqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (NpgsqlCommand command = new NpgsqlCommand("add_mechanic", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.Add("new_secondname", NpgsqlDbType.Varchar).Value = secondText;
                            command.Parameters.Add("new_firstname", NpgsqlDbType.Varchar).Value = firstnameText;
                            command.Parameters.Add("new_thirdname", NpgsqlDbType.Varchar).Value = thirdnameText;
                            command.Parameters.Add("new_mail", NpgsqlDbType.Varchar).Value = mailText;
                            command.Parameters.Add("new_phone", NpgsqlDbType.Varchar).Value = phoneText;

                            command.ExecuteNonQuery();

                            transaction.Commit();

                            MessageBox.Show("Механик добавлен успешно");
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Ошибка при добавлении механика: {ex.Message}");
                    }
                }
            }
        }
    }
}