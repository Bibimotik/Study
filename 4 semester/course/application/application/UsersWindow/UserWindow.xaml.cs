using System.Windows;
using application.ShowWindow;

namespace application;

public partial class UserWindow : Window
{
    public UserWindow()
    {
        InitializeComponent();
    }
    
    private void open_ShowCarsSpareParts(object sender, RoutedEventArgs e)
    {
        ShowCarsSpareParts showCarsSpareParts = new ShowCarsSpareParts();
        showCarsSpareParts.Show();
    }
    
    private void open_CreateOrderCar(object sender, RoutedEventArgs e)
    {
        CreateOrderCar createOrderCar = new CreateOrderCar();
        createOrderCar.Show();
    }

    private void open_CreateOrderSparePart(object sender, RoutedEventArgs e)
    {
        CreateOrderSparePart createOrderSparePart = new CreateOrderSparePart();
        createOrderSparePart.Show();
    }

    private void open_CreateReview(object sender, RoutedEventArgs e)
    {
        CreateReview createReview = new CreateReview();
        createReview.Show();
    }

    private void open_ShowDataStatusOrders(object sender, RoutedEventArgs e)
    {
        ShowDataStatusOrders showDataStatusOrders = new ShowDataStatusOrders();
        showDataStatusOrders.Show();
    }

    private void open_ShowHistory(object sender, RoutedEventArgs e)
    {
        ShowHistory showHistory = new ShowHistory();
        showHistory.Show();
    }

    private void open_CreateServiceSheet(object sender, RoutedEventArgs e)
    {
        CreateServiceSheet createServiceSheet = new CreateServiceSheet();
        createServiceSheet.Show();
    }

    private void open_ShowServiceSheet(object sender, RoutedEventArgs e)
    {
        ShowServiceSheet showServiceSheet = new ShowServiceSheet();
        showServiceSheet.Show();
    }

    private void open_ShowAllReviews(object sender, RoutedEventArgs e)
    {
        ShowAllReviews showAllReviews = new ShowAllReviews();
        showAllReviews.Show();
    }
    
    
    private void open_SearchCarsSpareParts(object sender, RoutedEventArgs e)
    {
        SearchCarsSpareParts searchCarsSpareParts = new SearchCarsSpareParts();
        searchCarsSpareParts.Show();
    }

}