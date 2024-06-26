using System.Data;
using System.Windows;
using System.Windows.Controls;
using Npgsql;
using NpgsqlTypes;

namespace application.InteractWindow.ForSparePart;

public partial class AddSparePart : Window
{
    public AddSparePart()
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
    
    private bool IsPositiveNumberInt(string value)
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

    private bool SaveSparePart()
    {
        string labelText = label_text.Text;
        ComboBoxItem itemLabelModelId = (ComboBoxItem)labelModelBox.SelectedItem;
        string labelModelId = itemLabelModelId != null ? itemLabelModelId.Name.ToString() : null;
        string quantityText = quantity_text.Text;
        string priceText = price_text.Text;
        string descriptionText = description_text.Text;
        bool statusChecked = status_text.IsChecked ?? false;

        if (string.IsNullOrEmpty(labelModelId) || string.IsNullOrEmpty(labelText) || string.IsNullOrEmpty(quantityText)
            || string.IsNullOrEmpty(priceText) || string.IsNullOrEmpty(descriptionText))
        {
            MessageBox.Show("Пожалуйста, заполните все поля.");
            return false;
        }
        
        if (!IsPositiveNumberInt(quantityText))
        {
            MessageBox.Show("Пожалуйста, введите положительное число в поля числовых значений.");
            return false;
        }

        if (!IsPositiveNumber(priceText))
        {
            MessageBox.Show("Пожалуйста, введите положительное число в поля числовых значений.");
            return false;
        }
        
        if (!IsWithinMaxLength(labelText, 20))
        {
            MessageBox.Show("Поле 'Название' не может содержать более 20 символов.");
            return false;
        }

        if (!IsWithinMaxLength(descriptionText, 200))
        {
            MessageBox.Show("Поле 'Описание' не может содержать более 200 символов.");
            return false;
        }
        
        return true;
    }
    private void add_car(object sender, RoutedEventArgs e)
    {
        if (SaveSparePart())
        {
            string newLabel = label_text.Text;
            ComboBoxItem itemLabelModelId = (ComboBoxItem)labelModelBox.SelectedItem;
            int newLabelModelId = int.Parse(itemLabelModelId.Name.Substring(2));
            string newDescription = description_text.Text;
            int newQuantity = int.Parse(quantity_text.Text);
            decimal newPrice = decimal.Parse(price_text.Text);
            bool newStatus = status_text.IsChecked ?? false;
            
            using (NpgsqlConnection connection = new NpgsqlConnection(SQLclass.manager_string))
            {
                connection.Open();

                using (NpgsqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (NpgsqlCommand command = new NpgsqlCommand("add_sparepart", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.Add("new_label", NpgsqlDbType.Varchar).Value = newLabel;
                            command.Parameters.AddWithValue("new_label_model_id", newLabelModelId);
                            command.Parameters.Add("new_description", NpgsqlDbType.Varchar).Value = newDescription;
                            command.Parameters.AddWithValue("new_quantity", newQuantity);
                            command.Parameters.Add("new_price", NpgsqlDbType.Money).Value = newPrice;
                            command.Parameters.AddWithValue("new_status", newStatus);

                            command.ExecuteNonQuery();

                            transaction.Commit();

                            MessageBox.Show("Запчасть добавлена успешно");
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Ошибка при добавлении запчасти: {ex.Message}");
                    }
                }
            }
        }
    }
}