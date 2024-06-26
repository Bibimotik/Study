using System.Data;
using System.Windows;
using Npgsql;

namespace application.ShowWindow;

public partial class ShowCarsSpareParts : Window
{
    public ShowCarsSpareParts()
    {
        InitializeComponent();
        
        using (NpgsqlConnection connection = new NpgsqlConnection(SQLclass.user_string))
        {
            connection.Open();

            using (NpgsqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM get_available_cars()", connection))
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            DataTable table = new DataTable();
                            table.Load(reader);
                            carsGrid.ItemsSource = table.DefaultView;
                        }

                        transaction.Commit();
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show($"Ошибка при получении доступных машин: {ex.Message}");
                }
            }
        
            using (NpgsqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM get_available_spareparts()", connection))
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            DataTable table = new DataTable();
                            table.Load(reader);
                            sparePartsGrid.ItemsSource = table.DefaultView;
                        }

                        transaction.Commit();
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show($"Ошибка при получении доступных запчастей: {ex.Message}");
                }
            }
        }
    }
}