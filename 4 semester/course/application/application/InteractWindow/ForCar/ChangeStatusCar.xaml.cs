using System.Data;
using System.Windows;
using System.Windows.Controls;
using Npgsql;
using NpgsqlTypes;

namespace application.InteractWindow;

public partial class ChangeStatusCar : Window
{
    public int id;
    
    public ChangeStatusCar()
    {
        InitializeComponent();
        
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
        }
    }
    
    private void getID_Click(object sender, RoutedEventArgs e)
    {
        ComboBoxItem itemCarsId = (ComboBoxItem)carsBox.SelectedItem;
        id = int.Parse(itemCarsId.Name.Substring(2));
        
        using (NpgsqlConnection connection = new NpgsqlConnection(SQLclass.manager_string))
        {
            connection.Open();

            using (NpgsqlTransaction transaction = connection.BeginTransaction())
            {
                string sqlQuery = $"SELECT car_status FROM get_all_car() WHERE car_id = {id};";

                using (NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            bool status = reader.GetBoolean(0);
                            
                            status_text.IsChecked = status;
                            
                            status_text.IsEnabled = true;
                            save.IsEnabled = true;
                        }
                    }
                }
            }
        }
    }

    private void save_Click(object sender, RoutedEventArgs e)
    {
        bool newStatus = status_text.IsChecked ?? false;
        
        using (NpgsqlConnection connection = new NpgsqlConnection(SQLclass.manager_string))
        {
            connection.Open();

            using (NpgsqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    using (NpgsqlCommand command = new NpgsqlCommand("delete_car", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        
                        command.Parameters.AddWithValue("car_id", id);
                        command.Parameters.AddWithValue("new_status", newStatus);

                        command.ExecuteNonQuery();

                        transaction.Commit();

                        MessageBox.Show("Статус машины обновлен успешно");
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show($"Ошибка при обновлении статуса машины: {ex.Message}");
                }
            }
        }
    }
}