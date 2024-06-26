using System.Configuration;
using System.Data;
using System.Windows;
using Npgsql;

namespace lab08;

public partial class Select : Window
{
    private DataTable studentDataTable;
    
    private string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
    
    public int id;
    public string name;
    
    public Select()
    {
        InitializeComponent();
    }
    
    private void Get_Click(object sender, RoutedEventArgs e)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();

            using (NpgsqlTransaction transaction = connection.BeginTransaction())
            {
                string sqlQuery = "SELECT * FROM STUDENT";
                bool whereClauseAdded = false;

                if (!string.IsNullOrEmpty(idsearch.Text))
                {
                    int id;
                    if (int.TryParse(idsearch.Text, out id))
                    {
                        sqlQuery += $" WHERE StudentId = {id}";
                        whereClauseAdded = true;
                    }
                    else
                    {
                        MessageBox.Show("Некорректное значение ID");
                        return;
                    }
                }

                if (!string.IsNullOrEmpty(namesearch.Text))
                {
                    if (whereClauseAdded)
                        sqlQuery += " AND";
                    else
                        sqlQuery += " WHERE";

                    sqlQuery += $" FullName ILIKE '%{namesearch.Text}%'";
                    whereClauseAdded = true;
                }

                using (NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        var studentDataTable = new DataTable();
                        studentDataTable.Load(reader);

                        dataGrid.ItemsSource = studentDataTable.DefaultView;
                    }
                }
            }
        }
    }
}