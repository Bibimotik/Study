using System.ComponentModel;
using System.Configuration;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using application.InteractWindow;
using application.InteractWindow.ForBalances;
using application.InteractWindow.ForCustomer;
using application.InteractWindow.ForDalances;
using application.InteractWindow.ForOrders;
using application.InteractWindow.ForSparePart;
using Npgsql;
using NpgsqlTypes;

namespace application;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class ManagerWindow : Window
{
    public ManagerWindow()
    {
        InitializeComponent();
    }

    private void calculate_total_price(object sender, RoutedEventArgs e)
    {
        DateTime? startDate = start_date.SelectedDate;
        DateTime? endDate = end_date.SelectedDate;

        using (NpgsqlConnection connection = new NpgsqlConnection(SQLclass.manager_string))
        {
            connection.Open();

            using (NpgsqlCommand command = new NpgsqlCommand("SELECT calculate_total_price(@startDate, @endDate);", connection))
            {
                command.Parameters.Add("@startDate", NpgsqlDbType.Date).Value = startDate != null ? (object)startDate.Value : DBNull.Value;
                command.Parameters.Add("@endDate", NpgsqlDbType.Date).Value = endDate != null ? (object)endDate.Value : DBNull.Value;

                object result = command.ExecuteScalar();
                decimal totalPrice = result != DBNull.Value ? (decimal)result : 0;

                MessageBox.Show($"Total Price: {totalPrice}");
            }
        }
    }

    private void open_AddCar(object sender, RoutedEventArgs e)
    {
        AddCar addCar = new AddCar();
        addCar.Show();
    }
    
    private void open_UpdateCar(object sender, RoutedEventArgs e)
    {
        UpdateCar updateCar = new UpdateCar();
        updateCar.Show();
    }
    
    private void open_ChangeStatusCar(object sender, RoutedEventArgs e)
    {
        ChangeStatusCar changeStatusCar = new ChangeStatusCar();
        changeStatusCar.Show();
    }
    
    private void open_AddSparePart(object sender, RoutedEventArgs e)
    {
        AddSparePart addSparePart = new AddSparePart();
        addSparePart.Show();
    }
    
    private void open_UpdateSparePart(object sender, RoutedEventArgs e)
    {
        UpdateSparePart updateSparePart = new UpdateSparePart();
        updateSparePart.Show();
    }
    
    private void open_ChangeStatusSparePart(object sender, RoutedEventArgs e)
    {
        ChangeStatusSparePart changeStatusSparePart = new ChangeStatusSparePart();
        changeStatusSparePart.Show();
    }
    
    private void open_AddCustomer(object sender, RoutedEventArgs e)
    {
        AddCustomer addCustomer = new AddCustomer();
        addCustomer.Show();
    }
    
    private void open_UpdateCustomer(object sender, RoutedEventArgs e)
    {
        UpdateCustomer upadateCustomeer = new UpdateCustomer();
        upadateCustomeer.Show();
    }
    
    private void open_CarBalances(object sender, RoutedEventArgs e)
    {
        CarBalances carBalances = new CarBalances();
        carBalances.Show();
    }
    
    private void open_SparePartsBalances(object sender, RoutedEventArgs e)
    {
        SparePartsBalances sparePartsBalances = new SparePartsBalances();
        sparePartsBalances.Show();
    }
    
    private void open_CustomerBalances(object sender, RoutedEventArgs e)
    {
        CustomerBalances customerBalances = new CustomerBalances();
        customerBalances.Show();
    }
    
    private void open_ChangeStatusOrderCar(object sender, RoutedEventArgs e)
    {
        ChangeStatusOrderCar changeStatusOrderCar = new ChangeStatusOrderCar();
        changeStatusOrderCar.Show();
    }
    
    private void open_ChangeStatusOrderSparePart(object sender, RoutedEventArgs e)
    {
        ChangeStatusOrderSparePart changeStatusOrderSparePart = new ChangeStatusOrderSparePart();
        changeStatusOrderSparePart.Show();
    }
    
    private void open_test(object sender, RoutedEventArgs e)
    {
        test test = new test();
        test.Show();
    }
}