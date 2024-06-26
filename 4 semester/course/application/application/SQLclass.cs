using System.Configuration;
using Npgsql;

namespace application;

public class SQLclass
{
    internal static string manager_string = ConfigurationManager.ConnectionStrings["ManagerConnectionString"].ConnectionString;
    internal static string user_string = ConfigurationManager.ConnectionStrings["UserConnectionString"].ConnectionString;
    internal static string mechanic_string = ConfigurationManager.ConnectionStrings["MechanicConnectionString"].ConnectionString;
    internal static string admin_string = ConfigurationManager.ConnectionStrings["AdminConnectionString"].ConnectionString;
    internal static NpgsqlConnection manager = new NpgsqlConnection
    {
        ConnectionString = manager_string
    };
        
    internal static NpgsqlConnection user = new NpgsqlConnection
    {
        ConnectionString = user_string
    };
    
    internal static NpgsqlConnection mechanic = new NpgsqlConnection
    {
        ConnectionString = mechanic_string
    };
    
    internal static NpgsqlConnection admin = new NpgsqlConnection
    {
        ConnectionString = admin_string
    };

    internal static void open_manager()
    {
        manager.Open();
    }
    
    internal static void open_user()
    {
        user.Open();
    }
    
    internal static void open_mechanic()
    {
        mechanic.Open();
    }
    
    internal static void open_admin()
    {
        admin.Open();
    }
    
    public static void CloseConnection()
    {
        manager.Close();
        user.Close();
        mechanic.Close();
        admin.Close();
    }
}