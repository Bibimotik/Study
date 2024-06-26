using System.Data;
using System.Windows;
using System.Windows.Controls;
using Npgsql;
using NpgsqlTypes;

namespace application.ServiceSheet
{
    public partial class OrderSpareParts : Window
    {
        public int sparepart_id;
        
        public OrderSpareParts()
        {
            InitializeComponent();

            quantity_text.IsEnabled = false;
            get.IsEnabled = false;

            using (NpgsqlConnection connection = new NpgsqlConnection(SQLclass.mechanic_string))
            {
                connection.Open();

                string sql1 = "SELECT * FROM get_all_sparepart()";
                using (NpgsqlCommand command = new NpgsqlCommand(sql1, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string sparepart_label = reader.GetString(1);
                            string label_model_label = reader.GetString(2);
                            string label_model_model = reader.GetString(3);
                            int quantity = reader.GetInt32(4);

                            ComboBoxItem item = new ComboBoxItem();
                            item.Content = sparepart_label + ' ' + label_model_label + ' ' + label_model_model + ' ' + quantity;
                            item.Name = "id" + id;
                            spareparts.Items.Add(item);
                        }
                    }
                }
            }
        }

        private void change_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem itemSparePartsId = (ComboBoxItem)spareparts.SelectedItem;
            sparepart_id = int.Parse(itemSparePartsId.Name.Substring(2));
            
            quantity_text.IsEnabled = true;
            get.IsEnabled = true;
        }
        
        private void get_Click(object sender, RoutedEventArgs e)
        {
            int newQuantity = int.Parse(quantity_text.Text);
            
            using (NpgsqlConnection connection = new NpgsqlConnection(SQLclass.mechanic_string))
            {
                connection.Open();

                using (NpgsqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string sqlQuery = $"update_sparepart_quantity";

                        using (NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("part_id", sparepart_id);
                            command.Parameters.AddWithValue("reduce_quantity", newQuantity);

                            command.ExecuteNonQuery();

                            transaction.Commit();

                            MessageBox.Show("Запчаси изъяты успешно");
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Ошибка при изъятии запчастей: {ex.Message}");
                    }
                }
            }
        }
    }
}
