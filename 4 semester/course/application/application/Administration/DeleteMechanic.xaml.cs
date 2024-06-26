using System.Data;
using System.Windows;
using System.Windows.Controls;
using Npgsql;

namespace application.Administration;

public partial class DeleteMechanic : Window
{
    public int mechanic_id;
    
    public DeleteMechanic()
    {
        InitializeComponent();
        
        LoadData();
    }
    
    private void LoadData()
    {
        delete.IsEnabled = false;
        mechanics.Items.Clear();
        
        using (NpgsqlConnection connection = new NpgsqlConnection(SQLclass.admin_string))
        {
            connection.Open();

            using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM get_all_mechanic()", connection))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string secondname = reader.GetString(1);
                        string firstname = reader.GetString(2);
                        string thirdname = reader.GetString(3);

                        ComboBoxItem item = new ComboBoxItem();
                        item.Content = secondname + ' ' + firstname + ' ' + thirdname;
                        item.Name = "id" + id;
                        mechanics.Items.Add(item);
                    }
                }
            }
        }
    }

    private void change_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            ComboBoxItem itemManagerId = (ComboBoxItem)mechanics.SelectedItem;
            mechanic_id = int.Parse(itemManagerId.Name.Substring(2));

            delete.IsEnabled = true;
        }
        catch (Exception exception)
        {
            mechanics.SelectedIndex = -1;
        }
    }

    private void delete_Click(object sender, RoutedEventArgs e)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(SQLclass.admin_string))
        {
            connection.Open();

            using (NpgsqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    using (NpgsqlCommand command = new NpgsqlCommand("delete_mechanic", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                            
                        command.Parameters.AddWithValue("mechanic_id", mechanic_id);

                        command.ExecuteNonQuery();

                        transaction.Commit();

                        MessageBox.Show("Механик удален");
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show($"Ошибка при удалении механика: {ex.Message}");
                }
            }
        }
        
        LoadData();
    }
}