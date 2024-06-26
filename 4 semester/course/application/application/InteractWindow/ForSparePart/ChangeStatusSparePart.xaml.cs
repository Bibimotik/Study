using System.Data;
using System.Windows;
using System.Windows.Controls;
using Npgsql;

namespace application.InteractWindow.ForSparePart;

public partial class ChangeStatusSparePart : Window
{
    public int id;
    
    public ChangeStatusSparePart()
    {
        InitializeComponent();

        quantity_text.IsEnabled = false;
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
        }
    }
    
    private void getID_Click(object sender, RoutedEventArgs e)
    {
        ComboBoxItem itemCarsId = (ComboBoxItem)sparepartsBox.SelectedItem;
        id = int.Parse(itemCarsId.Name.Substring(2));
        
        using (NpgsqlConnection connection = new NpgsqlConnection(SQLclass.admin_string))
        {
            connection.Open();

            using (NpgsqlTransaction transaction = connection.BeginTransaction())
            {
                string sqlQuery = $"SELECT sparepart_status, sparepart_quantity FROM get_all_sparepart() WHERE sparepart_id = {id};";

                using (NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            bool status = reader.GetBoolean(0);
                            int quantity = reader.GetInt32(1);

                            quantity_text.Text = quantity.ToString();
                            status_text.IsChecked = status;
                            
                            quantity_text.IsEnabled = true;
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
    
    

    private void save_Click(object sender, RoutedEventArgs e)
    {
        bool newStatus = status_text.IsChecked ?? false;

        if (!IsPositiveNumberInt(quantity_text.Text))
        {
            MessageBox.Show("Введите корректное значение количества");
            return;
        }
        
        int newQuantity = int.Parse(quantity_text.Text);
        if (newQuantity == 0 && newStatus)
        {
            MessageBox.Show("Статус не может быть true, если количество равно 0");
            return;
        }

        using (NpgsqlConnection connection = new NpgsqlConnection(SQLclass.manager_string))
        {
            connection.Open();

            using (NpgsqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    using (NpgsqlCommand command = new NpgsqlCommand("delete_sparepart", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("sparepart_id", id);
                        command.Parameters.AddWithValue("new_quantity", newQuantity);
                        command.Parameters.AddWithValue("new_status", newStatus);

                        command.ExecuteNonQuery();

                        transaction.Commit();

                        MessageBox.Show("Статус и количество запчасти обновлены успешно");
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show($"Ошибка при обновлении статуса и количества запчасти: {ex.Message}");
                }
            }
        }
    }
}