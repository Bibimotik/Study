using System.Data;
using System.Windows;
using System.Windows.Controls;
using Npgsql;

namespace application.InteractWindow.ForOrders;

public partial class ChangeStatusOrderSparePart : Window
{
    public int id;
    public int order_id;
    
    public ChangeStatusOrderSparePart()
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

            string sql1 = "SELECT * FROM get_available_customers()";
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
                        customerBox.Items.Add(item);
                    }
                }
            }
        }
    }
    
    private void getIdCustomer_Click(object sender, RoutedEventArgs e)
    {
        ComboBoxItem itemCustomerId = (ComboBoxItem)customerBox.SelectedItem;
        id = int.Parse(itemCustomerId.Name.Substring(2));
        
        ordersBox.Items.Clear();
        ordersBox.SelectedIndex = -1;
        
        using (NpgsqlConnection connection = new NpgsqlConnection(SQLclass.admin_string))
        {
            connection.Open();
            string sql1 = $"SELECT * FROM get_orders_spare_parts_by_customer_id({id})";
            using (NpgsqlCommand command = new NpgsqlCommand(sql1, connection))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int order_id = reader.GetInt32(0);
                        string secondname_customer = reader.GetString(1);
                        string sparepart_label = reader.GetString(2);
                        string label = reader.GetString(3);
                        string model = reader.GetString(4);
                        bool status = reader.GetBoolean(5);
                        
                        Console.WriteLine(status);

                        ComboBoxItem item = new ComboBoxItem();
                        item.Content = sparepart_label + ' ' + label + ' ' + model + ' ' + ' ' + secondname_customer + ' ' + status.ToString();
                        item.Name = "id" + order_id;
                        ordersBox.Items.Add(item);
                        
                        ordersBox.IsEnabled = true;
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
                    string sqlQuery =
                        $"SELECT * FROM get_orders_spare_parts_by_customer_id({id}) WHERE order_id = {order_id}";

                    using (NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection))
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string secondname_customer = reader.GetString(1);
                                string sparepart_label = reader.GetString(2);
                                string label = reader.GetString(3);
                                string model = reader.GetString(4);
                                bool status = reader.GetBoolean(5);

                                secondnameCustomer_text.Text = secondname_customer;
                                car_text.Text = sparepart_label + ' ' + label + ' ' + model;
                                status_text.IsChecked = status;

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
                    string sqlQuery = "change_status_orderspareparts";

                    using (NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("ordersparepart_id", order_id);
                        command.Parameters.AddWithValue("new_status", newStatus);

                        command.ExecuteNonQuery();

                        transaction.Commit();

                        MessageBox.Show("Статус заказа запчасти обновлен успешно");
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show($"Ошибка при обновлении статуса заказа запчасти: {ex.Message}");
                }
            }
        }
    }
}