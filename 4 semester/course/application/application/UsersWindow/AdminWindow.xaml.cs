using System.Data;
using System.Windows;
using application.Administration;
using Npgsql;
using NpgsqlTypes;

namespace application.UsersWindow;

public partial class AdminWindow : Window
{
    public AdminWindow()
    {
        InitializeComponent();
    }
    
    private void open_AddManager(object sender, RoutedEventArgs e)
    {
        AddManager addManager = new AddManager();
        addManager.Show();
    }

    private void open_DeleteManager(object sender, RoutedEventArgs e)
    {
        DeleteManager deleteManager = new DeleteManager();
        deleteManager.Show();
    }

    private void open_AddMechanic(object sender, RoutedEventArgs e)
    {
        AddMechanic addMechanic = new AddMechanic();
        addMechanic.Show();
    }

    private void open_DeleteMechanic(object sender, RoutedEventArgs e)
    {
        DeleteMechanic deleteMechanic = new DeleteMechanic();
        deleteMechanic.Show();
    }

    private void open_ChangePriceCarSparePart(object sender, RoutedEventArgs e)
    {
        ChangePriceCarSparePart changePriceCarSparePart = new ChangePriceCarSparePart();
        changePriceCarSparePart.Show();
    }
    
    private void create_extract_car(object sender, RoutedEventArgs e)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SQLclass.admin_string))
            {
                connection.Open();

                using (NpgsqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM generate_sales_report_sparepart()", connection))
                        {
                            command.ExecuteNonQuery();

                            transaction.Commit();

                            MessageBox.Show("Отчет о продажах машин создан");
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Ошибка при создании отчета: {ex.Message}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка: {ex}");
        }
    }

    private void create_extract_sparepart(object sender, RoutedEventArgs e)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SQLclass.admin_string))
            {
                connection.Open();

                using (NpgsqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM generate_sales_report_sparepart()", connection))
                        {
                            command.ExecuteNonQuery();

                            transaction.Commit();

                            MessageBox.Show("Отчет о продажах запчастей создан");
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Ошибка при создании отчета: {ex.Message}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка: {ex}");
        }
    }
    private void add_ReviewJson(object sender, RoutedEventArgs e)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SQLclass.admin_string))
            {
                connection.Open();

                using (NpgsqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (NpgsqlCommand command = new NpgsqlCommand("add_review_from_json", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            
                            command.ExecuteNonQuery();

                            transaction.Commit();

                            MessageBox.Show("Отзывы загружены из json");
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Ошибка при загрузке отзывов: {ex.Message}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка: {ex}");
        }
    }
}