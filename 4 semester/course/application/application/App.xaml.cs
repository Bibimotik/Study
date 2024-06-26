using System.Configuration;
using System.Data;
using System.Windows;
using application.UsersWindow;

namespace application;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private void Application_Startup(object sender, StartupEventArgs e)
    {
        ManagerWindow managerWindow = new ManagerWindow();
        managerWindow.Show();

        UserWindow userWindow = new UserWindow();
        userWindow.Show();
        
        MechanicWindow mechanicWindow = new MechanicWindow();
        mechanicWindow.Show();
        
        AdminWindow adminWindow = new AdminWindow();
        adminWindow.Show();
    }
}