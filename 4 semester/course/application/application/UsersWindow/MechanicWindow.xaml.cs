using System.Windows;
using application.ServiceSheet;

namespace application;

public partial class MechanicWindow : Window
{
    public MechanicWindow()
    {
        InitializeComponent();
    }
    
    private void open_ShowHistoryServiceSheet(object sender, RoutedEventArgs e)
    {
        ShowHistoryServiceSheet showHistoryServiceSheet = new ShowHistoryServiceSheet();
        showHistoryServiceSheet.Show();
    }
    
    private void open_WorkWithServiceSheets(object sender, RoutedEventArgs e)
    {
        WorkWithServiceSheets workWithServiceSheets = new WorkWithServiceSheets();
        workWithServiceSheets.Show();
    }

    private void open_OrderSpareParts(object sender, RoutedEventArgs e)
    {
        OrderSpareParts orderSpareParts = new OrderSpareParts();
        orderSpareParts.Show();
    }
}