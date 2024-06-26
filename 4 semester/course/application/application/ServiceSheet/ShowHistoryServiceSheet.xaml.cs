using System.Data;
using System.Windows;
using Npgsql;
using NpgsqlTypes;

namespace application.ServiceSheet;

public partial class ShowHistoryServiceSheet : Window
{
    public int mechanic_id;
    
    public ShowHistoryServiceSheet()
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
            using (NpgsqlConnection connection = new NpgsqlConnection(SQLclass.mechanic_string))
            {
                connection.Open();

                try
                {
                    string phone = phone_text.Text;
                    using (NpgsqlCommand command = new NpgsqlCommand($"SELECT * FROM get_mechanic_by_phone(@phone_number)", connection))
                    {
                        command.Parameters.Add("phone_number", NpgsqlDbType.Varchar).Value = phone;
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);

                                mechanic_id = id;
                            }
                        }
                    }

                    using (NpgsqlCommand command = new NpgsqlCommand($"SELECT * FROM get_service_sheet(@mech_id)", connection))
                    {
                        command.Parameters.AddWithValue("mech_id", mechanic_id);
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            DataTable table = new DataTable();
                            table.Load(reader);
                            serviceSheetGrid.ItemsSource = table.DefaultView;
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
}