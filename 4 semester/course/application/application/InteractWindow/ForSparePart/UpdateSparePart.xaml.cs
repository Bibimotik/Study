using System.Data;
using System.Windows;
using System.Windows.Controls;
using Npgsql;
using NpgsqlTypes;

namespace application.InteractWindow.ForSparePart;

public partial class UpdateSparePart : Window
{
    public int id;
    
    public UpdateSparePart()
    {
        InitializeComponent();
        
        label_text.IsEnabled = false;
        labelModelBox.IsEnabled = false;
        description_text.IsEnabled = false;
        status_text.IsEnabled = false;
        save.IsEnabled = false;
        
        using (NpgsqlConnection connection = new NpgsqlConnection(SQLclass.manager_string))
        {
            connection.Open();

            string sql1 = "SELECT * FROM get_all_sparepart()";
            using (NpgsqlCommand command = new NpgsqlCommand(sql1, connection))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string sparepart_label = reader.GetString(1);
                        string label_model_label = reader.GetString(2);
                        string label_model_model = reader.GetString(3);

                        ComboBoxItem item = new ComboBoxItem();
                        item.Content = sparepart_label + ' ' + label_model_label + ' ' + label_model_model;
                        item.Name = "id" + id;
                        sparepartsBox.Items.Add(item);
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
        ComboBoxItem itemSparePartsId = (ComboBoxItem)sparepartsBox.SelectedItem;
        id = int.Parse(itemSparePartsId.Name.Substring(2));
        
        using (NpgsqlConnection connection = new NpgsqlConnection(SQLclass.manager_string))
        {
            connection.Open();

            using (NpgsqlTransaction transaction = connection.BeginTransaction())
            {
                string sqlQuery = $"SELECT * FROM get_all_sparepart() WHERE sparepart_id = {id};";

                using (NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string label = reader.GetString(1);
                            int quantity = reader.GetInt32(4);
                            bool status = reader.GetBoolean(5);
                            string description = reader.GetString(6);
                            int label_model_id = reader.GetInt32(7);
                            
                            label_text.Text = label;
                            quantity_text.Text = quantity.ToString();
                            status_text.IsChecked = status;
                            description_text.Text = description;
                            
                            label_text.IsEnabled = true;
                            labelModelBox.IsEnabled = true;
                            description_text.IsEnabled = true;
                            status_text.IsEnabled = true;
                            save.IsEnabled = true;
                        }
                    }
                }
            }
        }
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

    private bool SaveCar()
    {
        string label = label_text.Text;
        ComboBoxItem itemLabelModelId = (ComboBoxItem)labelModelBox.SelectedItem;
        string labelModelId = itemLabelModelId != null ? itemLabelModelId.Name.ToString() : null;
        string quantityText = quantity_text.Text;
        string descriptionText = description_text.Text;
        bool statusChecked = status_text.IsChecked ?? false;

        if (string.IsNullOrEmpty(labelModelId) || string.IsNullOrEmpty(label) || 
            string.IsNullOrEmpty(quantityText) || string.IsNullOrEmpty(descriptionText))
        {
            MessageBox.Show("Пожалуйста, заполните все поля.");
            return false;
        }

        if (!IsWithinMaxLength(label, 20))
        {
            MessageBox.Show("Поле 'Тип двигателя' не может содержать более 20 символов.");
            return false;
        }

        if (!IsPositiveNumberInt(quantityText))
        {
            MessageBox.Show("Пожалуйста, введите положительное число в поля числовых значений.");
            return false;
        }

        if (!IsWithinMaxLength(descriptionText, 200))
        {
            MessageBox.Show("Поле 'Описание' не может содержать более 200 символов.");
            return false;
        }
        
        return true;
    }
    
    private void update_car(object sender, RoutedEventArgs e)
    {
        if (SaveCar())
        {
            string newLabel = label_text.Text;
            ComboBoxItem itemLabelModelId = (ComboBoxItem)labelModelBox.SelectedItem;
            int newLabelModelId = int.Parse(itemLabelModelId.Name.Substring(2));
            string newDescription = description_text.Text;
            int newQuantity = int.Parse(quantity_text.Text);
            bool newStatus = status_text.IsChecked ?? false;

            using (NpgsqlConnection connection = new NpgsqlConnection(SQLclass.manager_string))
            {
                connection.Open();

                using (NpgsqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (NpgsqlCommand command = new NpgsqlCommand("update_sparepart", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            
                            command.Parameters.AddWithValue("sparepart_id", id);
                            command.Parameters.Add("new_label", NpgsqlDbType.Varchar).Value = newLabel;
                            command.Parameters.AddWithValue("new_label_model_id", newLabelModelId);
                            command.Parameters.AddWithValue("new_quantity", newQuantity);
                            command.Parameters.Add("new_description", NpgsqlDbType.Varchar).Value = newDescription;
                            command.Parameters.AddWithValue("new_status", newStatus);

                            command.ExecuteNonQuery();

                            transaction.Commit();

                            MessageBox.Show("Запчасть обновлена успешно");
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Ошибка при обновлении запчасти: {ex.Message}");
                    }
                }
            }
        }
    }
}