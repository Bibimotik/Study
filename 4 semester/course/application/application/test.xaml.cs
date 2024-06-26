using System.Data;
using System.Windows;
using System.Windows.Controls;
using Npgsql;
using NpgsqlTypes;

namespace application;

public partial class test : Window
{
    public test()
    {
        InitializeComponent();
        
        using (NpgsqlConnection connection = new NpgsqlConnection(SQLclass.admin_string))
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
                        string secondName = reader.GetString(1);
                        string firstName = reader.GetString(2);
                        string thirdName = reader.GetString(3);
                
                        ComboBoxItem item = new ComboBoxItem();
                        item.Content = secondName + ' ' + firstName + ' ' + thirdName;
                        item.Name = "id" + id;
                        custumerBox.Items.Add(item);
                    }
                }
            }
            string sql2 = "SELECT * FROM get_all_mechanic()";
            using (NpgsqlCommand command = new NpgsqlCommand(sql2, connection))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string secondName = reader.GetString(1);
                        string firstName = reader.GetString(2);
                        string thirdName = reader.GetString(3);
            
                        ComboBoxItem item = new ComboBoxItem();
                        item.Content = secondName + ' ' + firstName + ' ' + thirdName;
                        item.Name = "id" + id;
                        mechanicBox.Items.Add(item);
                    }
                }
            }
            string sql3 = "SELECT * FROM get_all_label_model()";
            using (NpgsqlCommand command = new NpgsqlCommand(sql3, connection))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int labelModelId = reader.GetInt32(0);
                        string label = reader.GetString(1);
                        string model = reader.GetString(2);
                
                        ComboBoxItem item = new ComboBoxItem();
                        item.Content = label + " - " + model;
                        item.Name = "id" + labelModelId;
                        labelModelBox.Items.Add(item);
                    }
                }
            }
        }
    }
    
    private void save_Click(object sender, RoutedEventArgs e)
    {
        ComboBoxItem itemCustomerId = (ComboBoxItem)custumerBox.SelectedItem;
        int customerId = int.Parse(itemCustomerId.Name.Substring(2));
        ComboBoxItem itemMechanicId = (ComboBoxItem)mechanicBox.SelectedItem;
        int mechanicId = int.Parse(itemMechanicId.Name.Substring(2));
        ComboBoxItem itemLabelModelId = (ComboBoxItem)labelModelBox.SelectedItem;
        int labelModelId = int.Parse(itemLabelModelId.Name.Substring(2));
        string new_problem = newProblem.Text;
        DateTime? startDate = newDate.SelectedDate;
        
        using (NpgsqlConnection connection = new NpgsqlConnection(SQLclass.admin_string))
        {
            connection.Open();

            using (NpgsqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    using (NpgsqlCommand command = new NpgsqlCommand("add_service", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("cust_id", customerId);
                        command.Parameters.AddWithValue("mech_id", mechanicId);
                        command.Parameters.Add("new_date", NpgsqlDbType.Date).Value = startDate;
                        command.Parameters.AddWithValue("new_label_model_id", labelModelId);
                        command.Parameters.Add("new_problem", NpgsqlDbType.Varchar).Value = new_problem;

                        command.ExecuteNonQuery();

                        transaction.Commit();

                        MessageBox.Show("Запись на обслуживание добавлена успешно");
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show($"Ошибка при добавлении записи на обслуживание: {ex.Message}");
                }
            }
        }
    }
}