using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Npgsql;
using NpgsqlTypes;

namespace application.InteractWindow;

public partial class AddCar : Window
{
    public AddCar()
    {
        InitializeComponent();
        
        using (NpgsqlConnection connection = new NpgsqlConnection(SQLclass.manager_string))
        {
            connection.Open();
        
            string sql1 = "SELECT * FROM get_all_label_model()";
            using (NpgsqlCommand command = new NpgsqlCommand(sql1, connection))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string label_model_label = reader.GetString(1);
                        string label_model_model = reader.GetString(2);

                        ComboBoxItem item = new ComboBoxItem();
                        item.Content = label_model_label + ' ' + label_model_model;
                        item.Name = "id" + id;
                        labelModelBox.Items.Add(item);
                    }
                }
            }
        }
    }
    private bool IsPositiveNumber(string value)
    {
        if (decimal.TryParse(value, out decimal number))
        {
            return number > 0;
        }
        return false;
    }
    
    private bool IsPositiveIntNumber(string value)
    {
        if (int.TryParse(value, out int number))
        {
            return number > 0;
        }
        return false;
    }

    private bool IsWithinMaxLength(string value, int maxLength)
    {
        return value.Length <= maxLength;
    }
    private bool SaveCar()
    {
        ComboBoxItem itemLabelModelId = (ComboBoxItem)labelModelBox.SelectedItem;
        string labelModelId = itemLabelModelId != null ? itemLabelModelId.Name.ToString() : null;
        string yearText = year_text.Text;
        string mileageText = mileage_text.Text;
        string engineType = enginetype_text.Text;
        string engineCapacityText = enginecapacity_text.Text;
        string powerText = power_text.Text;
        string priceText = price_text.Text;
        string descriptionText = description_text.Text;
        bool statusChecked = status_text.IsChecked ?? false;

        if (string.IsNullOrEmpty(labelModelId) || string.IsNullOrEmpty(yearText) || string.IsNullOrEmpty(mileageText) ||
            string.IsNullOrEmpty(engineType) || string.IsNullOrEmpty(engineCapacityText) || string.IsNullOrEmpty(powerText) ||
            string.IsNullOrEmpty(priceText) || string.IsNullOrEmpty(descriptionText))
        {
            MessageBox.Show("Пожалуйста, заполните все поля.");
            return false;
        }

        if (!IsPositiveNumber(mileageText) || !IsPositiveNumber(engineCapacityText) || !IsPositiveNumber(priceText))
        {
            MessageBox.Show("Пожалуйста, введите положительное число в поля числовых значений.");
            return false;
        }
        
        if (!IsPositiveIntNumber(yearText) || !IsPositiveIntNumber(powerText))
        {
            MessageBox.Show("Пожалуйста, введите положительное число в поля числовых значений.");
            return false;
        }

        if (!IsWithinMaxLength(engineType, 20))
        {
            MessageBox.Show("Поле 'Тип двигателя' не может содержать более 20 символов.");
            return false;
        }

        if (!IsWithinMaxLength(descriptionText, 1000))
        {
            MessageBox.Show("Поле 'Описание' не может содержать более 1000 символов.");
            return false;
        }

        int currentYear = DateTime.Now.Year;
        int enteredYear;
        if (!int.TryParse(yearText, out enteredYear))
        {
            MessageBox.Show("Пожалуйста, введите год в правильном формате.");
            return false;
        }

        if (enteredYear > currentYear)
        {
            MessageBox.Show("Неверный год. Пожалуйста, введите год, который не превышает текущий.");
            return false;
        }

        if (enteredYear < 1900)
        {
            MessageBox.Show("Неверный год. Пожалуйста, введите год, который не меньше 1900.");
            return false;
        }
        
        return true;
    }
    private void add_car(object sender, RoutedEventArgs e)
    {
        if (SaveCar())
        {
            ComboBoxItem itemLabelModelId = (ComboBoxItem)labelModelBox.SelectedItem;
            int newLabelModelId = int.Parse(itemLabelModelId.Name.Substring(2));
            int newYear = int.Parse(year_text.Text);
            int newMileage = int.Parse(mileage_text.Text);
            string newEngineType = enginetype_text.Text;
            decimal newEngineCapacity = decimal.Parse(enginecapacity_text.Text);
            int newPower = int.Parse(power_text.Text);
            decimal newPrice = decimal.Parse(price_text.Text);
            string newDescription = description_text.Text;
            bool newStatus = status_text.IsChecked ?? false;
            
            using (NpgsqlConnection connection = new NpgsqlConnection(SQLclass.manager_string))
            {
                connection.Open();

                using (NpgsqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (NpgsqlCommand command = new NpgsqlCommand("add_car", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("new_label_model_id", newLabelModelId);
                            command.Parameters.AddWithValue("new_year", newYear);
                            command.Parameters.AddWithValue("new_mileage", newMileage);
                            command.Parameters.Add("new_enginetype", NpgsqlDbType.Varchar).Value = newEngineType;
                            command.Parameters.AddWithValue("new_enginecapacity", newEngineCapacity);
                            command.Parameters.AddWithValue("new_power", newPower);
                            command.Parameters.Add("new_price",NpgsqlDbType.Money).Value = newPrice;
                            command.Parameters.Add("new_description",NpgsqlDbType.Varchar).Value = newDescription;
                            command.Parameters.AddWithValue("new_status", newStatus);

                            command.ExecuteNonQuery();

                            transaction.Commit();

                            MessageBox.Show("Машина добавлена успешно");
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Ошибка при добавлении машины: {ex.Message}");
                    }
                }
            }
        }
    }
}