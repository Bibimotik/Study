using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using lab04.Core;
using Newtonsoft.Json;

namespace lab04
{
    public partial class ShowOneSparePart : Window
    {
        public Product SparePart { get; set; }

        public ICommand DeleteCommand { get; }
        public ICommand EditCommand { get; }
        public string Theme;

        public ShowOneSparePart(Product sparePart, string theme)
        {
            InitializeComponent();
            SparePart = sparePart;
            DataContext = this;

            Closing += ShowOneSparePart_Closing;

            DeleteCommand = new RelayCommand(DeleteSparePart);
            EditCommand = new RelayCommand(EditSparePart);
            
            Theme = theme;
            
            ResourceDictionary resourceDict = new ResourceDictionary();
            resourceDict.Source = new System.Uri($"D:/Учеба/4_семестр/ООП/test/test/Theme/{Theme}.xaml", System.UriKind.Absolute);
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);
            
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

        private void DeleteSparePart(object parameter)
        {
            try
            {
                string fileName = "spare_parts.json";

                List<Product> deleteSpareParts = new List<Product>();

                JsonSerializerSettings options = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Ignore
                };

                if (File.Exists(fileName))
                {
                    string json = File.ReadAllText(fileName);
                    deleteSpareParts = JsonConvert.DeserializeObject<List<Product>>(json, options);
                }

                Product sparePartToDelete = deleteSpareParts.FirstOrDefault(s => s.Name == SparePart.Name && s.Short_Name == SparePart.Short_Name && s.Price == SparePart.Price);

                if (sparePartToDelete != null)
                {
                    deleteSpareParts.Remove(sparePartToDelete);

                    string updatedJson = JsonConvert.SerializeObject(deleteSpareParts, options);
                    File.WriteAllText(fileName, updatedJson);

                    MessageBox.Show("Spare part deleted successfully!");
                    ShowOneSparePart_Closing(null, null);
                    Close();
                }
                else
                {
                    MessageBox.Show("Spare part not found in the file.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while deleting the spare part: {ex.Message}");
            }
        }

        private void EditSparePart(object parameter)
        {
            EditSparePart editWindow = new EditSparePart(SparePart, Theme);
            editWindow.ShowDialog();
            Close();

            //SparePart = editWindow.UpdatedSparePart;
        }

        private void ShowOneSparePart_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ShowAllSpareParts showAllSparePartsWindow = Application.Current.Windows.OfType<ShowAllSpareParts>().FirstOrDefault();
            if (showAllSparePartsWindow != null)
            {
                showAllSparePartsWindow.LoadAndDrawSpareParts(null, null);
            }
        }
        
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double windowWidth = e.NewSize.Width;

            if (windowWidth < 600)
            {
                MainGrid.ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Star);
                SparePartImage.Width = 170;
                SparePartImage.Height = 70;
                ButtonStackPanel.Orientation = Orientation.Vertical;
                DeleteButton.Width = 70;
                DeleteButton.Height = 30;
                DeleteButton.FontSize = 14;
                EditButton.Width = 70;
                EditButton.Height = 30;
                EditButton.FontSize = 14;
            }
            else
            {
                MainGrid.ColumnDefinitions[1].Width = new GridLength(2, GridUnitType.Star);
                SparePartImage.Width = 200;
                SparePartImage.Height = 100;
                ButtonStackPanel.Orientation = Orientation.Horizontal;
                DeleteButton.Width = 110;
                DeleteButton.Height = 40;
                DeleteButton.FontSize = 20;
                EditButton.Width = 110;
                EditButton.Height = 40;
                EditButton.FontSize = 20;
            }
        }
    }
}