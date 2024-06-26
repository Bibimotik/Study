using System.Data;
using System.Windows;
using System.Windows.Controls;
using Npgsql;
using NpgsqlTypes;

namespace application.Administration;

public partial class ChangePriceCarSparePart : Window
{
    public int car_id;
    public int sparepart_id;
    
    public ChangePriceCarSparePart()
    {
        InitializeComponent();
        
        LoadDataCar();
        LoadDataSparePart();
    }
    
    private void LoadDataCar()
    {
        saveCar.IsEnabled = false;
        car_price.IsEnabled = false;
        cars.Items.Clear();
        
        using (NpgsqlConnection connection = new NpgsqlConnection(SQLclass.admin_string))
        {
            connection.Open();

            using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM get_all_car()", connection))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string label = reader.GetString(1);
                        string model = reader.GetString(2);
                        int year = reader.GetInt32(3);
                        decimal thirdname = reader.GetDecimal(5);

                        ComboBoxItem item = new ComboBoxItem();
                        item.Content = label + ' ' + model + ' ' + year.ToString();
                        item.Name = "id" + id;
                        cars.Items.Add(item);
                    }
                }
            }
        }
    }
    
    private void LoadDataSparePart()
    {
        saveSparePart.IsEnabled = false;
        sparepart_price.IsEnabled = false;
        spareparts.Items.Clear();
        
        using (NpgsqlConnection connection = new NpgsqlConnection(SQLclass.admin_string))
        {
            connection.Open();

            using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM get_all_sparepart()", connection))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string slabel = reader.GetString(1);
                        string label = reader.GetString(2);
                        string model = reader.GetString(3);

                        ComboBoxItem item = new ComboBoxItem();
                        item.Content = slabel + ' ' + label + ' ' + model;
                        item.Name = "id" + id;
                        spareparts.Items.Add(item);
                    }
                }
            }
        }
    }

    private void changeCar_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            ComboBoxItem itemManagerId = (ComboBoxItem)cars.SelectedItem;
            car_id = int.Parse(itemManagerId.Name.Substring(2));

            using (NpgsqlConnection connection = new NpgsqlConnection(SQLclass.admin_string))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand($"SELECT * FROM get_all_car() WHERE car_id = {car_id}", connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            decimal price = reader.GetDecimal(5);
                            car_price.Text = price.ToString();

                            car_price.IsEnabled = true;
                            saveCar.IsEnabled = true;
                        }
                    }
                }
            }
        }
        catch (Exception exception)
        {
            cars.SelectedIndex = -1;
        }
    }
    
    private void changeSparePart_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            ComboBoxItem itemManagerId = (ComboBoxItem)spareparts.SelectedItem;
            sparepart_id = int.Parse(itemManagerId.Name.Substring(2));

            using (NpgsqlConnection connection = new NpgsqlConnection(SQLclass.admin_string))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand($"SELECT * FROM get_all_sparepart() WHERE sparepart_id = {sparepart_id}", connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            decimal price = reader.GetDecimal(8);
                            sparepart_price.Text = price.ToString();

                            sparepart_price.IsEnabled = true;
                            saveSparePart.IsEnabled = true;
                        }
                    }
                }
            }
        }
        catch (Exception exception)
        {
            spareparts.SelectedIndex = -1;
        }
    }

    private void save_car(object sender, RoutedEventArgs e)
    {
        if(!decimal.TryParse(car_price.Text, out decimal price) || price < 0)
        {
            MessageBox.Show("Введите правильную цену");
        }
        else
        {
            decimal newPrice = price;
            
            using (NpgsqlConnection connection = new NpgsqlConnection(SQLclass.admin_string))
            {
                connection.Open();

                using (NpgsqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (NpgsqlCommand command = new NpgsqlCommand("change_car_price", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("car_id", car_id);
                            command.Parameters.Add("car_price", NpgsqlDbType.Money).Value = newPrice;

                            command.ExecuteNonQuery();

                            transaction.Commit();

                            MessageBox.Show("Цена машины обновлена успешно");
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Ошибка при обновлении цены машины: {ex.Message}");
                    }
                }
            }
        }
    }

    private void save_sparepart(object sender, RoutedEventArgs e)
    {
        if(!decimal.TryParse(sparepart_price.Text, out decimal price) || price < 0)
        {
            MessageBox.Show("Введите правильную цену");
        }
        else
        {
            decimal newPrice = price;
            
            using (NpgsqlConnection connection = new NpgsqlConnection(SQLclass.admin_string))
            {
                connection.Open();

                using (NpgsqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (NpgsqlCommand command = new NpgsqlCommand("change_sparepart_price", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("sparepart_id", sparepart_id);
                            command.Parameters.Add("sparepart_price", NpgsqlDbType.Money).Value = newPrice;

                            command.ExecuteNonQuery();

                            transaction.Commit();

                            MessageBox.Show("Цена запчасти обновлена успешно");
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Ошибка при обновлении цены запчасти: {ex.Message}");
                    }
                }
            }
        }
    }
}