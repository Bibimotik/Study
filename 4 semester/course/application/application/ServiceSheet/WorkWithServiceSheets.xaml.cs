using System.Data;
using System.Windows;
using System.Windows.Controls;
using Npgsql;
using NpgsqlTypes;

namespace application.ServiceSheet;

public partial class WorkWithServiceSheets : Window
{
    public int order_id;
    public int mechanic_id;
    public int customer_id;
    
    public WorkWithServiceSheets()
    {
        InitializeComponent();

        customers.IsEnabled = false;
        orders.IsEnabled = false;
        description_text.IsEnabled = false;
        price_text.IsEnabled = false;
        status_text.IsEnabled = false;
        save.IsEnabled = false;
        
        using (NpgsqlConnection connection = new NpgsqlConnection(SQLclass.mechanic_string))
        {
            connection.Open();

            string sql1 = "SELECT * FROM get_all_mechanic()";
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
                        mechanic.Items.Add(item);
                    }
                }
            }
        }
    }
    
    private void getMechanic_Click(object sender, RoutedEventArgs e)
    {
        ComboBoxItem itemMechanicId = (ComboBoxItem)mechanic.SelectedItem;
        mechanic_id = int.Parse(itemMechanicId.Name.Substring(2));
        customers.Items.Clear();
        customers.SelectedIndex = -1;
        orders.Items.Clear();
        orders.SelectedIndex = -1;
        description_text.Text = "";
        price_text.Text = "";
        status_text.IsChecked = false;
        
        using (NpgsqlConnection connection = new NpgsqlConnection(SQLclass.mechanic_string))
        {
            connection.Open();
            string sql1 = $"SELECT * FROM get_customer_full_names({mechanic_id})";
            using (NpgsqlCommand command = new NpgsqlCommand(sql1, connection))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    HashSet<int> addedCustomerIds = new HashSet<int>();

                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string secondname = reader.GetString(1);
                        string firstname = reader.GetString(2);
                        string thirdname = reader.GetString(3);

                        if (!addedCustomerIds.Contains(id))
                        {
                            ComboBoxItem item = new ComboBoxItem();
                            item.Content = secondname + ' ' + firstname + ' ' + thirdname;
                            item.Name = "id" + id;
                            customers.Items.Add(item);

                            addedCustomerIds.Add(id);

                            customers.IsEnabled = true;
                        }
                    }
                }
            }
        }
    }
    
    private void getCustomer_Click(object sender, RoutedEventArgs e)
    {
        orders.Items.Clear();
        orders.SelectedIndex = -1;
        description_text.Text = "";
        price_text.Text = "";
        status_text.IsChecked = false;
        try
        {
            ComboBoxItem itemCustomerId = (ComboBoxItem)customers.SelectedItem;
            customer_id = int.Parse(itemCustomerId.Name.Substring(2));
            
            using (NpgsqlConnection connection = new NpgsqlConnection(SQLclass.mechanic_string))
            {
                connection.Open();

                using (NpgsqlTransaction transaction = connection.BeginTransaction())
                {
                    string sqlQuery = $"SELECT * FROM get_service_sheet({mechanic_id}) WHERE customer_id = {customer_id}";

                    using (NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection))
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                string label_car = reader.GetString(1);
                                string model_car = reader.GetString(2);

                                ComboBoxItem item = new ComboBoxItem();
                                item.Content = label_car + ' ' + model_car;
                                item.Name = "id" + id;
                                orders.Items.Add(item);

                                orders.IsEnabled = true;
                            }
                        }
                    }
                }
            }
        }
        catch (Exception exception)
        {
            customers.SelectedIndex = -1;
        }
    }
    
    private void getOrder_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            ComboBoxItem itemOrderId = (ComboBoxItem)orders.SelectedItem;
            order_id = int.Parse(itemOrderId.Name.Substring(2));

            using (NpgsqlConnection connection = new NpgsqlConnection(SQLclass.mechanic_string))
            {
                connection.Open();

                using (NpgsqlTransaction transaction = connection.BeginTransaction())
                {
                    string sqlQuery =
                        $"SELECT * FROM get_service_sheet({mechanic_id}) WHERE customer_id = {customer_id} AND id = {order_id}";

                    using (NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection))
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (!reader.IsDBNull(6))
                                {
                                    decimal price = reader.GetDecimal(6);
                                    price_text.Text = price.ToString();
                                }
                                else
                                {
                                    price_text.Text = "";
                                }

                                string description = reader.GetString(10);
                                bool status_car = reader.GetBoolean(11);

                                description_text.Text = description;
                                status_text.IsChecked = status_car;

                                price_text.IsEnabled = true;
                                description_text.IsEnabled = true;
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
            orders.SelectedIndex = -1;
        }
    }

    private void save_Click(object sender, RoutedEventArgs e)
    {
        if (description_text.Text.Length > 1000 || string.IsNullOrEmpty(description_text.Text))
        {
            MessageBox.Show("Описание не может превышать 1000 символов и обязательно для заполнения.");
            return;
        }
        
        if (!decimal.TryParse(price_text.Text, out _))
        {
            MessageBox.Show("Пожалуйста, введите корректное значение цены (число с плавающей точкой).");
            return;
        }

        string newDescription = description_text.Text;
        decimal? newPrice = decimal.Parse(price_text.Text);
        bool newStatus = status_text.IsChecked ?? false;
        
        using (NpgsqlConnection connection = new NpgsqlConnection(SQLclass.mechanic_string))
        {
            connection.Open();

            using (NpgsqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    string sqlQuery = $"update_service_sheet";

                    using (NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("service_sheet_id", order_id);
                        command.Parameters.Add("service_sheet_description", NpgsqlDbType.Varchar).Value = newDescription;
                        command.Parameters.Add("service_sheet_price", NpgsqlDbType.Money).Value = newPrice;
                        command.Parameters.AddWithValue("service_sheet_status", newStatus);

                        command.ExecuteNonQuery();

                        transaction.Commit();

                        MessageBox.Show("Обслуживание обновлено успешно");
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show($"Ошибка при обновлении обслуживания: {ex.Message}");
                }
            }
        }
    }
}