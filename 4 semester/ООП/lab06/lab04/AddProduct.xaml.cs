using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using lab04;
using Microsoft.Win32;

namespace lab04
{
    public partial class AddProduct
    {
        private Product _product;

        public string Theme;

        public History History = new History();

        public AddProduct(string theme)
        {
            InitializeComponent();

            Theme = theme;
            
            ResourceDictionary resourceDict = new ResourceDictionary();
            resourceDict.Source = new System.Uri($"D:/Учеба/4_семестр/ООП/test/test/Theme/{Theme}.xaml", System.UriKind.Absolute);
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);
            
            _product = new Product();
            
            string cursorFilePath = "D:\\Учеба\\4_семестр\\ООП\\lab04\\lab04\\Fonts\\cursor.cur";

            try
            {
                if (File.Exists(cursorFilePath))
                {
                    Cursor customCursor = new Cursor(cursorFilePath);

                    Cursor = customCursor;
                }
                else
                {
                    MessageBox.Show("Файл скачанного курсора не найден.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки курсора: " + ex.Message);
            }
        }

        private void SelectFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PNG Files (*.png)|*.png";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFileName = openFileDialog.FileName;
                string fileName = Path.GetFileName(selectedFileName);

                SelectFileButton.Content = fileName;

                _product.Photo = selectedFileName;
            }
        }
        
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(NameTextBox.Text)
                || string.IsNullOrEmpty(ShortNameTextBox.Text)
                || CategoryComboBox.SelectedItem == null
                || string.IsNullOrEmpty(_product.Photo)
                || string.IsNullOrEmpty(PriceTextBox.Text)
                || string.IsNullOrEmpty(ColorTextBox.Text)
                || DescriptionRichTextBox.Document == null)
            {
                MessageBox.Show("All fields must be filled!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _product.Name = NameTextBox.Text;
            _product.Short_Name = ShortNameTextBox.Text;
            _product.Category = ((ComboBoxItem)CategoryComboBox.SelectedItem).Content.ToString();

            if (!int.TryParse(PriceTextBox.Text, out int price) || price < 0)
            {
                MessageBox.Show("Price must be a number > 0!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            _product.Price = price;

            _product.Rating = (int)RatingSlider.Value;
            _product.Color = ColorTextBox.Text;
            _product.Description = new TextRange(DescriptionRichTextBox.Document.ContentStart,
                DescriptionRichTextBox.Document.ContentEnd).Text;
            _product.In_Stock = InStockCheckBox.IsChecked == true ? 1 : 0;

            try
            {
                List<Product> addcar = new List<Product>();

                JsonSerializerSettings options = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Ignore
                };

                string filename = _product.Category == "Запчасти" ? "spare_parts.json" : "cars.json";

                if (File.Exists(filename))
                {
                    string json = File.ReadAllText(filename);
                    addcar = JsonConvert.DeserializeObject<List<Product>>(json, options);
                }

                addcar.Add(_product);

                if (filename == "cars.json")
                {
                    History.AddProductToHistory(_product);
                }
                
                //Console.WriteLine(History.HistoryList);

                string updatedJson = JsonConvert.SerializeObject(addcar, options);

                File.WriteAllText(filename, updatedJson);

                MessageBox.Show($"{_product.Category} data saved successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }
    }
}