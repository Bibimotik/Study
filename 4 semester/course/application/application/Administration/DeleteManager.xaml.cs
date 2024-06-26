using System.Data;
using System.Windows;
using System.Windows.Controls;
using Npgsql;
using NpgsqlTypes;

namespace application.Administration;

public partial class DeleteManager : Window
{
    public int manager_id;
    
    public DeleteManager()
    {
        InitializeComponent();
        
        LoadData();
    }

    private void LoadData()
    {
        delete.IsEnabled = false;
        managers.Items.Clear();
        
        using (NpgsqlConnection connection = new NpgsqlConnection(SQLclass.admin_string))
        {
            connection.Open();

            using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM get_all_manager()", connection))
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
                        managers.Items.Add(item);
                    }
                }
            }
        }
    }

    private void change_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            ComboBoxItem itemManagerId = (ComboBoxItem)managers.SelectedItem;
            manager_id = int.Parse(itemManagerId.Name.Substring(2));

            delete.IsEnabled = true;
        }
        catch (Exception exception)
        {
            managers.SelectedIndex = -1;
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
                    using (NpgsqlCommand command = new NpgsqlCommand("delete_manager", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                            
                        command.Parameters.AddWithValue("manager_id", manager_id);

                        command.ExecuteNonQuery();

                        transaction.Commit();

                        MessageBox.Show("Менеджер удален");
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show($"Ошибка при удалении менеджера: {ex.Message}");
                }
            }
        }
        
        LoadData();
    }
}