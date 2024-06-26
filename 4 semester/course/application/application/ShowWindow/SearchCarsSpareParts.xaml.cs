using System.Data;
using System.Windows;
using System.Windows.Controls;
using Npgsql;

namespace application.ShowWindow;

public partial class SearchCarsSpareParts : Window
{
    public int? label_id = null;
    public int? label_model_id = null;
    
    public int? label_ids = null;
    public int? label_model_ids = null;
    
    public SearchCarsSpareParts()
    {
        InitializeComponent();

        modelsBox.IsEnabled = false;
        modelsBox1.IsEnabled = false;
        
        using (NpgsqlConnection connection = new NpgsqlConnection(SQLclass.user_string))
        {
            connection.Open();

            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand($"SELECT * FROM get_labels()", connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string label = reader.GetString(1);

                            ComboBoxItem item = new ComboBoxItem();
                            item.Content = label;
                            item.Name = "id" + id;
                            labelsBox.Items.Add(item);
                        
                            ComboBoxItem item1 = new ComboBoxItem();
                            item1.Content = label;
                            item1.Name = "id" + id;
                            labelsBox1.Items.Add(item1);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Возникла проблема: {ex}");
            }
        }
    }

    private void label_Click(object sender, RoutedEventArgs e)
    {
        modelsBox.Items.Clear();
        modelsBox.IsEnabled = false;
        label_model_id = null;
        
        try
        {
            ComboBoxItem itemLabelId = (ComboBoxItem)labelsBox.SelectedItem;
            label_id = int.Parse(itemLabelId.Name.Substring(2));

            using (NpgsqlConnection connection = new NpgsqlConnection(SQLclass.user_string))
            {
                connection.Open();

                try
                {
                    using (NpgsqlCommand command =
                           new NpgsqlCommand($"SELECT * FROM get_all_models_by_label({label_id})", connection))
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                string model = reader.GetString(2);

                                ComboBoxItem item = new ComboBoxItem();
                                item.Content = model;
                                item.Name = "id" + id;
                                modelsBox.Items.Add(item);

                                modelsBox.IsEnabled = true;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Возникла проблема: {ex}");
                }
            }
        }
        catch (Exception exception)
        {
            labelsBox.SelectedIndex = -1;
        }
    }
    
    private void model_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            ComboBoxItem itemModelId = (ComboBoxItem)modelsBox.SelectedItem;
            label_model_id = int.Parse(itemModelId.Name.Substring(2));
        }
        catch (Exception exception)
        {
            modelsBox.SelectedIndex = -1;
        }
    }

    private void search_Click(object sender, RoutedEventArgs e)
    {
        int? searchYear = null;
        int? searchLabelId = null;
        int? searchLabelModelId = null;

        if (!string.IsNullOrWhiteSpace(year_text.Text))
        {
            if (int.TryParse(year_text.Text, out int year))
            {
                searchYear = year;
            }
            else
            {
                MessageBox.Show("Некорректный формат года");
                return;
            }
        }

        if (labelsBox.SelectedItem != null)
        {
            ComboBoxItem itemLabelId = (ComboBoxItem)labelsBox.SelectedItem;
            searchLabelId = int.Parse(itemLabelId.Name.Substring(2));
        }

        if (modelsBox.SelectedItem != null)
        {
            ComboBoxItem itemModelId = (ComboBoxItem)modelsBox.SelectedItem;
            searchLabelModelId = int.Parse(itemModelId.Name.Substring(2));
        }

        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SQLclass.user_string))
            {
                connection.Open();

                using (NpgsqlCommand command =
                       new NpgsqlCommand("SELECT * FROM search_cars(@searchYear, @searchLabelId, @searchLabelModelId)", connection))
                {
                    command.Parameters.AddWithValue("searchYear", searchYear ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("searchLabelId", searchLabelId ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("searchLabelModelId", searchLabelModelId ?? (object)DBNull.Value);

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);
                        searchGrid.ItemsSource = dataTable.DefaultView;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Возникла проблема: {ex}");
        }
    }

    
    
    private void label_Clicks(object sender, RoutedEventArgs e)
    {
        modelsBox1.Items.Clear();
        modelsBox1.IsEnabled = false;
        label_model_ids = null;
        
        try
        {
            ComboBoxItem itemLabelId = (ComboBoxItem)labelsBox1.SelectedItem;
            label_ids = int.Parse(itemLabelId.Name.Substring(2));

            using (NpgsqlConnection connection = new NpgsqlConnection(SQLclass.user_string))
            {
                connection.Open();

                try
                {
                    using (NpgsqlCommand command =
                           new NpgsqlCommand($"SELECT * FROM get_all_models_by_label({label_ids})", connection))
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                string model = reader.GetString(2);

                                ComboBoxItem item = new ComboBoxItem();
                                item.Content = model;
                                item.Name = "id" + id;
                                modelsBox1.Items.Add(item);

                                modelsBox1.IsEnabled = true;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Возникла проблема: {ex}");
                }
            }
        }
        catch (Exception exception)
        {
            labelsBox1.SelectedIndex = -1;
        }
    }
    
    private void model_Clicks(object sender, RoutedEventArgs e)
    {
        try
        {
            ComboBoxItem itemModelId = (ComboBoxItem)modelsBox1.SelectedItem;
            label_model_ids = int.Parse(itemModelId.Name.Substring(2));
        }
        catch (Exception exception)
        {
            modelsBox1.SelectedIndex = -1;
        }
    }

    private void search_Clicks(object sender, RoutedEventArgs e)
    {
        int? searchLabelId = null;
        int? searchLabelModelId = null;

        if (labelsBox1.SelectedItem != null)
        {
            ComboBoxItem itemLabelId = (ComboBoxItem)labelsBox1.SelectedItem;
            searchLabelId = int.Parse(itemLabelId.Name.Substring(2));
        }

        if (modelsBox1.SelectedItem != null)
        {
            ComboBoxItem itemModelId = (ComboBoxItem)modelsBox1.SelectedItem;
            searchLabelModelId = int.Parse(itemModelId.Name.Substring(2));
        }

        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SQLclass.user_string))
            {
                connection.Open();

                using (NpgsqlCommand command =
                       new NpgsqlCommand("SELECT * FROM search_spareparts(@search_label_id, @search_label_model_id)", connection))
                {
                    command.Parameters.AddWithValue("search_label_id", searchLabelId ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("search_label_model_id", searchLabelModelId ?? (object)DBNull.Value);

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);
                        searchGrids.ItemsSource = dataTable.DefaultView;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Возникла проблема: {ex}");
        }
    }
}