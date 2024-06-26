using System.Data;
using System.Windows;
using System.Windows.Controls;
using Npgsql;
using NpgsqlTypes;

namespace application.InteractWindow.ForOrders;

public partial class ChangeStatusOrderCar : Window
{
    public int id;
    public int order_id;
    
    public ChangeStatusOrderCar()
    {
        InitializeComponent();

        ordersBox.IsEnabled = false;
        secondnameCustomer_text.IsEnabled = false;
        car_text.IsEnabled = false;
        status_text.IsEnabled = false;
        save.IsEnabled = false;
        
        using (NpgsqlConnection connection = new NpgsqlConnection(SQLclass.manager_string))
        {
            connection.Open();

            string sql1 = "SELECT * FROM get_all_manager()";
            using (NpgsqlCommand command = new NpgsqlCommand(sql1, connection))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string secondname = reader.GetString(1);
                        string firstname = reader.GetString(2);
                        string thirdname = reader.GetString(3);

                        ComboBoxItem item = new ComboBoxItem();
                        item.Content = secondname + ' ' + firstname + ' ' + thirdname;
                        item.Name = "id" + id;
                        managerBox.Items.Add(item);
                    }
                }
            }
        }
    }
    
    private void getIdManager_Click(object sender, RoutedEventArgs e)
    {
        ComboBoxItem itemManagerId = (ComboBoxItem)managerBox.SelectedItem;
        id = int.Parse(itemManagerId.Name.Substring(2));
        ordersBox.Items.Clear();
        ordersBox.SelectedIndex = -1;
        ordersBox.IsEnabled = true;
        
        using (NpgsqlConnection connection = new NpgsqlConnection(SQLclass.manager_string))
        {
            connection.Open();
            string sql1 = $"SELECT * FROM get_orders_cars_by_manager_id({id})";
            using (NpgsqlCommand command = new NpgsqlCommand(sql1, connection))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int order_id = reader.GetInt32(0);
                        string label_car = reader.GetString(2);
                        string model_car = reader.GetString(3);
                        int year = reader.GetInt32(4);
                        string secondname_customer = reader.GetString(5);
                        bool status_car = reader.GetBoolean(6);

                        ComboBoxItem item = new ComboBoxItem();
                        item.Content = label_car + ' ' + model_car + ' ' + year.ToString() + ' ' + secondname_customer + ' ' + status_car.ToString();
                        item.Name = "id" + order_id;
                        ordersBox.Items.Add(item);
                    }
                }
            }
        }
    }
    
    private void getIdOrder_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            ComboBoxItem itemOrderId = (ComboBoxItem)ordersBox.SelectedItem;
            order_id = int.Parse(itemOrderId.Name.Substring(2));
            
            using (NpgsqlConnection connection = new NpgsqlConnection(SQLclass.manager_string))
            {
                connection.Open();

                using (NpgsqlTransaction transaction = connection.BeginTransaction())
                {
                    string sqlQuery = $"SELECT * FROM get_orders_cars_by_manager_id({id}) WHERE order_id = {order_id}";

                    using (NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection))
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string secondname_manager = reader.GetString(1);
                                string label_car = reader.GetString(2);
                                string model_car = reader.GetString(3);
                                int year = reader.GetInt32(4);
                                string secondname_customer = reader.GetString(5);
                                bool status_car = reader.GetBoolean(6);

                                secondnameCustomer_text.Text = secondname_customer;
                                car_text.Text = label_car + ' ' + model_car + ' ' + year.ToString();
                                status_text.IsChecked = status_car;

                                status_text.IsEnabled = true;
                                save.IsEnabled = true;
                            }
                        }
                    }
                }
            }
        }
        catch (Exception exception)
        {
            ordersBox.SelectedIndex = -1;
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
                    string sqlQuery = $"change_status_ordercar";

                    using (NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("ordercar_id", order_id);
                        command.Parameters.AddWithValue("new_status", newStatus);

                        command.ExecuteNonQuery();

                        transaction.Commit();

                        MessageBox.Show("Статус заказа машины обновлен успешно");
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show($"Ошибка при обновлении статуса заказа машины: {ex.Message}");
                }
            }
        }
    }
}