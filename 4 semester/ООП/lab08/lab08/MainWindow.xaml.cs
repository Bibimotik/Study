using System.ComponentModel;
using Npgsql;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Windows;

namespace lab08
{
    public partial class MainWindow : Window
    {
        private DataTable studentDataTable;

        public MainWindow()
        {
            InitializeComponent();
            SQLClass.OpenConnection();
            LoadStudentData();
        }

        private void LoadStudentData()
        {
            var command = new NpgsqlCommand("SELECT * FROM Student", SQLClass.conn);

            var dataAdapter = new NpgsqlDataAdapter(command);
            studentDataTable = new DataTable();
            dataAdapter.Fill(studentDataTable);

            dataGrid.ItemsSource = studentDataTable.DefaultView;
        }

        private void MainWindow_OnClosing(object? sender, CancelEventArgs e)
        {
            SQLClass.CloseConnection();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Add add = new Add();
            add.Closed += Refresh;
            add.Show();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Edit edit = new Edit();
            edit.Closed += Refresh;
            edit.Show();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Delete delete = new Delete();
            delete.Closed += Refresh;
            delete.Show();
        }
        
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            Select select = new Select();
            select.Closed += Refresh;
            select.Show();
        }
        
        private void Refresh(object sender, EventArgs e)
        {
            LoadStudentData();
        }
        
        private void up(object sender, EventArgs e)
        {
            var command = new NpgsqlCommand("SELECT * FROM Student", SQLClass.conn);

            var dataAdapter = new NpgsqlDataAdapter(command);
            studentDataTable = new DataTable();
            dataAdapter.Fill(studentDataTable);

            dataGrid.ItemsSource = studentDataTable.DefaultView;
        }
        
        private void down(object sender, EventArgs e)
        {
            var command = new NpgsqlCommand("SELECT * FROM ADDRESS", SQLClass.conn);

            var dataAdapter = new NpgsqlDataAdapter(command);
            studentDataTable = new DataTable();
            dataAdapter.Fill(studentDataTable);

            dataGrid.ItemsSource = studentDataTable.DefaultView;
        }
    }
}