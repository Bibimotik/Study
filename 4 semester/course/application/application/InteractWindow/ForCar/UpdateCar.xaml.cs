using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Npgsql;
using NpgsqlTypes;

namespace application.InteractWindow;

public partial class UpdateCar : Window
{
    public int id;
    
    public UpdateCar()
    {
        InitializeComponent();
        
        labelModelBox.IsEnabled = false;
        year_text.IsEnabled = false;
        mileage_text.IsEnabled = false;
        enginetype_text.IsEnabled = false;
        enginecapacity_text.IsEnabled = false;
        power_text.IsEnabled = false;
        description_text.IsEnabled = false;
        status_text.IsEnabled = false;
        save.IsEnabled = false;

        using (NpgsqlConnection connection = new NpgsqlConnection(SQLclass.manager_string))
        {
            connection.Open();

            string sql1 = "SELECT * FROM get_all_car()";
            using (NpgsqlCommand command = new NpgsqlCommand(sql1, connection))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string label_model_label = reader.GetString(1);
                        string label_model_model = reader.GetString(2);
                        int label_model_year = reader.GetInt32(3);

                        ComboBoxItem item = new ComboBoxItem();
                        item.Content = label_model_label + ' ' + label_model_model + ' ' + label_model_year;
                        item.Name = "id" + id;
                        carsBox.Items.Add(item);
                    }
                }
            }
            string sql2 = "SELECT * FROM get_all_label_model()";
            using (NpgsqlCommand command = new NpgsqlCommand(sql2, connection))
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

    private void getID_Click(object sender, RoutedEventArgs e)
    {
        ComboBoxItem itemCarsId = (ComboBoxItem)carsBox.SelectedItem;
        id = int.Parse(itemCarsId.Name.Substring(2));
        
        using (NpgsqlConnection connection = new NpgsqlConnection(SQLclass.admin_string))
        {
            connection.Open();

            using (NpgsqlTransaction transaction = connection.BeginTransaction())
            {
                string sqlQuery = $"SELECT * FROM CARS WHERE ID = {id};";

                using (NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int label_model_id = reader.GetInt32(1);
                            int year = reader.GetInt32(2);
                            int mileage = reader.GetInt32(3);
                            string enginetype = reader.GetString(4);
                            decimal enginecapasity = reader.GetDecimal(5);
                            int power = reader.GetInt32(6);
                            decimal price = reader.GetDecimal(7);
                            string description = reader.GetString(8);
                            bool status = reader.GetBoolean(9);
                            
                            labelModelBox.Text = label_model_id.ToString();
                            year_text.Text = year.ToString();
                            mileage_text.Text = mileage.ToString();
                            enginetype_text.Text = enginetype;
                            enginecapacity_text.Text = enginecapasity.ToString();
                            power_text.Text = power.ToString();
                            description_text.Text = description;
                            status_text.IsChecked = status;
                            
                            labelModelBox.IsEnabled = true;
                            year_text.IsEnabled = true;
                            mileage_text.IsEnabled = true;
                            enginetype_text.IsEnabled = true;
                            enginecapacity_text.IsEnabled = true;
                            power_text.IsEnabled = true;
                            description_text.IsEnabled = true;
                            status_text.IsEnabled = true;
                            save.IsEnabled = true;
                        }
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
        string descriptionText = description_text.Text;
        bool statusChecked = status_text.IsChecked ?? false;

        if (string.IsNullOrEmpty(labelModelId) || string.IsNullOrEmpty(yearText) || string.IsNullOrEmpty(mileageText) ||
            string.IsNullOrEmpty(engineType) || string.IsNullOrEmpty(engineCapacityText) || string.IsNullOrEmpty(powerText) ||
            string.IsNullOrEmpty(descriptionText))
        {
            MessageBox.Show("Пожалуйста, заполните все поля.");
            return false;
        }

        if (!IsPositiveNumber(mileageText) || !IsPositiveNumber(engineCapacityText))
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

        if (!IsWithinMaxLength(descriptionText, 200))
        {
            MessageBox.Show("Поле 'Описание' не может содержать более 200 символов.");
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
    
    private void update_car(object sender, RoutedEventArgs e)
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
            string newDescription = description_text.Text;
            bool newStatus = status_text.IsChecked ?? false;

            using (NpgsqlConnection connection = new NpgsqlConnection(SQLclass.manager_string))
            {
                connection.Open();

                using (NpgsqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (NpgsqlCommand command = new NpgsqlCommand("update_car", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("car_id", id);
                            command.Parameters.AddWithValue("new_label_model_id", newLabelModelId);
                            command.Parameters.AddWithValue("new_year", newYear);
                            command.Parameters.AddWithValue("new_mileage", newMileage);
                            command.Parameters.Add("new_enginetype", NpgsqlDbType.Varchar).Value = newEngineType;
                            command.Parameters.AddWithValue("new_enginecapacity", newEngineCapacity);
                            command.Parameters.AddWithValue("new_power", newPower);
                            command.Parameters.Add("new_description", NpgsqlDbType.Varchar).Value = newDescription;
                            command.Parameters.AddWithValue("new_status", newStatus);

                            command.ExecuteNonQuery();

                            transaction.Commit();

                            MessageBox.Show("Машина обновлена успешно");
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Ошибка при обновлении машины: {ex.Message}");
                    }
                }
            }
        }
    }
}