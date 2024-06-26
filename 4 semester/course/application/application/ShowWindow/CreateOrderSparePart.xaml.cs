using System.Data;
using System.Windows;
using System.Windows.Controls;
using Npgsql;
using NpgsqlTypes;

namespace application.ShowWindow;

public partial class CreateOrderSparePart : Window
{
    public int customer_id;
    
    public CreateOrderSparePart()
    {
        InitializeComponent();
        
        quantity_text.IsEnabled = false;
        sparePartBox.IsEnabled = false;
        save.IsEnabled = false;
        
        using (NpgsqlConnection connection = new NpgsqlConnection(SQLclass.admin_string))
        {
            connection.Open();

            using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM get_available_spareparts()", connection))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string label_sparepart = reader.GetString(1);
                        string label = reader.GetString(2);
                        string model = reader.GetString(3);
                        int quantity = reader.GetInt32(5);

                        ComboBoxItem item = new ComboBoxItem();
                        item.Content = label_sparepart + ' ' + label + ' ' + model + ' ' + quantity.ToString();
                        item.Name = "id" + id;
                        sparePartBox.Items.Add(item);
                    }
                }
            }
        }
    }
    
    private bool IsPositiveNumber(string value)
    {
        if (long.TryParse(value, out long number))
        {
            return number > 0;
        }
        return false;
    }

    private bool IsWithinMaxLength(string value, int maxLength)
    {
        return value.Length <= maxLength;
    }

    private void login_Click(object sender, RoutedEventArgs e)
    {
        quantity_text.IsEnabled = false;
        sparePartBox.IsEnabled = false;
        save.IsEnabled = false;
        
        if (IsPositiveNumber(phone_text.Text) && IsWithinMaxLength(phone_text.Text, 16))
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SQLclass.user_string))
            {
                connection.Open();

                try
                {
                    string phone = phone_text.Text;
                    using (NpgsqlCommand command = new NpgsqlCommand($"SELECT * FROM get_customer_by_phone(@phone_number)", connection))
                    {
                        command.Parameters.Add("phone_number", NpgsqlDbType.Varchar).Value = phone;
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                customer_id = id;
                                
                                quantity_text.IsEnabled = true;
                                sparePartBox.IsEnabled = true;
                                save.IsEnabled = true;
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show($"Возникла проблема: {ex}");
                }
            }
        }
        else
        {
            MessageBox.Show("Введите правильный номер телефона: только цифры, длина меньше 16 символов");
        }
    }

    private void save_Click(object sender, RoutedEventArgs e)
    {
        ComboBoxItem itemSparePartId = (ComboBoxItem)sparePartBox.SelectedItem;
        int newSparePartId = int.Parse(itemSparePartId.Name.Substring(2));
        int newQuantity = int.Parse(quantity_text.Text);
        bool newStatus = false;
        DateTime newDate = DateTime.Today;

        using (NpgsqlConnection connection = new NpgsqlConnection(SQLclass.user_string))
        {
            connection.Open();

            using (NpgsqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    using (NpgsqlCommand checkQuantityCommand =
                           new NpgsqlCommand("SELECT * FROM check_quantity_spareparts(@order_id)", connection))
                    {
                        checkQuantityCommand.Parameters.AddWithValue("order_id", newSparePartId);
                        int availableQuantity = (int)checkQuantityCommand.ExecuteScalar();

                        if (newQuantity > availableQuantity || newQuantity < 0)
                        {
                            MessageBox.Show(
                                "Введенное количество запчастей превышает доступное количество или меньше нуля");
                            return;
                        }
                    }

                    using (NpgsqlCommand command = new NpgsqlCommand("add_ordersparepart", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("order_customer_id", customer_id);
                        command.Parameters.AddWithValue("order_sparepart_id", newSparePartId);
                        command.Parameters.Add("order_date", NpgsqlDbType.Date).Value = newDate;
                        command.Parameters.AddWithValue("order_quantity", newQuantity);
                        command.Parameters.AddWithValue("order_status", newStatus);

                        command.ExecuteNonQuery();

                        transaction.Commit();

                        MessageBox.Show("Заказ запчасти создан успешно");
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show($"Ошибка при создании заказа запчасти: {ex.Message}");
                }
            }
        }
    }
}