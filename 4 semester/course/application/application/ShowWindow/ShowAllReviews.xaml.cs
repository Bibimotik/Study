using System.Data;
using System.Windows;
using Npgsql;
using NpgsqlTypes;

namespace application.ShowWindow;

public partial class ShowAllReviews : Window
{
    public ShowAllReviews()
    {
        InitializeComponent();
        
        using (NpgsqlConnection connection = new NpgsqlConnection(SQLclass.user_string))
        {
            connection.Open();

            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand($"SELECT * FROM get_all_reviews()", connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable table = new DataTable();
                        table.Load(reader);
                        reviewGrid.ItemsSource = table.DefaultView;
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Возникла проблема: {ex}");
            }
        }
    }
}