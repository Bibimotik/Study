using System.Configuration;
using System.Windows;
using Npgsql;

namespace lab08;

public partial class Delete : Window
{
    public int id;
    
    private string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

    public Delete()
    {
        InitializeComponent();
    }

    private void Delete_Click(object sender, RoutedEventArgs e)
    {
        if (!int.TryParse(deleteid.Text, out int ID) || ID < 0)
        {
            MessageBox.Show("ID должен быть числом больше 0");
        }
        id = int.Parse(deleteid.Text);
        
        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();

            using (NpgsqlTransaction transaction = connection.BeginTransaction())
            {
                string sqlQuery = $"DELETE FROM STUDENT WHERE StudentId = {id};";

                using (NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection))
                {
                    command.Transaction = transaction;

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        transaction.Commit();
                    }
                    else
                    {
                        transaction.Rollback();
                    }
                }
            }
        }
    }
}