using System.Data;
using System.Windows;
using Npgsql;

namespace application.InteractWindow.ForDalances;

public partial class CustomerBalances : Window
{
    public CustomerBalances()
    {
        InitializeComponent();
        
        using (NpgsqlConnection connection = new NpgsqlConnection(SQLclass.manager_string))
        {
            connection.Open();

            using (NpgsqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    int totalAvailableCars = 0;
                    using (NpgsqlCommand countCommand = new NpgsqlCommand("SELECT COUNT(*) FROM get_available_customers()", connection))
                    {
                        totalAvailableCars = Convert.ToInt32(countCommand.ExecuteScalar());
                    }

                    available.Text = totalAvailableCars.ToString();
            
                    using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM get_available_customers()", connection))
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            DataTable table = new DataTable();
                            table.Load(reader);
                            customersGrid.ItemsSource = table.DefaultView;
                        }

                        transaction.Commit();
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show($"Ошибка при получении списка клиентов: {ex.Message}");
                }
            }
        }
    }
}