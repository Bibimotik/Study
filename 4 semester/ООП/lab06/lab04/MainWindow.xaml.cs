using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Newtonsoft.Json;

namespace lab04
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string Lang = "English";
        public string Theme = "BlackTheme";

        public History MainHistory = new History();
        
        private AddProduct addProductWindow;

        public int index;

        public MainWindow()
        {
            InitializeComponent();
            
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
            
            LanguageBox.SelectionChanged += LanguageBox_SelectionChanged;
            
            ThemeBox.SelectionChanged += ThemeBox_SelectionChanged;
            
            ResourceDictionary resourceDict = new ResourceDictionary();
            resourceDict.Source = new System.Uri("D:/Учеба/4_семестр/ООП/lab04/lab04/Lang/English.xaml", System.UriKind.Absolute);
            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);
            
            ResourceDictionary blackTheme = new ResourceDictionary();
            blackTheme.Source = new Uri("D:/Учеба/4_семестр/ООП/test/test/Theme/BlackTheme.xaml", UriKind.RelativeOrAbsolute);
            Resources.MergedDictionaries.Add(blackTheme);
            
            foreach (ComboBoxItem item in LanguageBox.Items)
            {
                if (item.Content.ToString() == "English")
                {
                    item.IsSelected = true;
                    break;
                }
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;

            if (radioButton != null)
            {
                if (radioButton.Content.ToString() == "Show all cars" || radioButton.Content.ToString() == "Показать все машины")
                {
                    ShowAllCars showAllCars = new ShowAllCars(Theme);
                    showAllCars.Show();
                }
                else if (radioButton.Content.ToString() == "Add product" || radioButton.Content.ToString() == "Добавить продукт")
                {
                    addProductWindow = new AddProduct(Theme);
                    addProductWindow.Closed += AddProductWindow_Closed;
                    addProductWindow.Show();
                }
                else if (radioButton.Content.ToString() == "Show all spare parts" || radioButton.Content.ToString() == "Показать все запчасти")
                {
                    ShowAllSpareParts showAllSpareParts = new ShowAllSpareParts(Theme);
                    showAllSpareParts.Show();
                }

                radioButton.IsChecked = false;
            }
        }
        
        private void LanguageBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedItem = (ComboBoxItem)LanguageBox.SelectedItem;
            Lang = selectedItem.Content.ToString();

            if (Lang == "English")
            {
                ResourceDictionary resourceDict = new ResourceDictionary();
                resourceDict.Source = new System.Uri("D:/Учеба/4_семестр/ООП/lab06/lab04/Lang/English.xaml", System.UriKind.Absolute);
                Application.Current.Resources.MergedDictionaries.Clear();
                Application.Current.Resources.MergedDictionaries.Add(resourceDict);
            }
            else if (Lang == "Russian")
            {
                ResourceDictionary resourceDict = new ResourceDictionary();
                resourceDict.Source = new System.Uri("D:/Учеба/4_семестр/ООП/lab06/lab04/Lang/Russian.xaml", System.UriKind.Absolute);
                Application.Current.Resources.MergedDictionaries.Clear();
                Application.Current.Resources.MergedDictionaries.Add(resourceDict);
            }
        }
        
        private void ThemeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ResourceDictionary blackTheme = new ResourceDictionary();
            blackTheme.Source = new Uri("D:/Учеба/4_семестр/ООП/test/test/Theme/BlackTheme.xaml", UriKind.RelativeOrAbsolute);
            ResourceDictionary whiteTheme = new ResourceDictionary();
            whiteTheme.Source = new Uri("D:/Учеба/4_семестр/ООП/test/test/Theme/WhiteTheme.xaml", UriKind.RelativeOrAbsolute);
            if (ThemeBox.SelectedItem is ComboBoxItem selectedItem)
            {
                if (selectedItem.Content.ToString() == "Black")
                {
                    Theme = "BlackTheme";
                    Resources.MergedDictionaries.Remove(whiteTheme);
                    Resources.MergedDictionaries.Add(blackTheme);
                }
                else if (selectedItem.Content.ToString() == "White")
                {
                    Theme = "WhiteTheme";
                    Resources.MergedDictionaries.Remove(blackTheme);
                    Resources.MergedDictionaries.Add(whiteTheme);
                }
            }
        }
        
        private void AddProductWindow_Closed(object sender, EventArgs e)
        {
            History closedWindowHistory = addProductWindow.History;

            MainHistory.HistoryList.AddRange(closedWindowHistory.HistoryList);
            
            index = MainHistory.HistoryList.Count - 1;
        }
        
        private void Undo_OnClick(object sender, RoutedEventArgs e)
        {
            if (MainHistory.HistoryList.Count > 0)
            {
                Product lastProduct = MainHistory.HistoryList[index];
                
                try
                {
                    string fileName = "cars.json";

                    List<Product> deletecar = new List<Product>();

                    JsonSerializerSettings options = new JsonSerializerSettings
                    {
                        Formatting = Formatting.Indented,
                        NullValueHandling = NullValueHandling.Ignore
                    };

                    if (File.Exists("cars.json"))
                    {
                        string json = File.ReadAllText("cars.json");
                        deletecar = JsonConvert.DeserializeObject<List<Product>>(json, options);
                    }

                    Product carToDelete = deletecar.FirstOrDefault(c => c.Name == lastProduct.Name && c.Short_Name == lastProduct.Short_Name && c.Price == lastProduct.Price && c.Color == lastProduct.Color);

                    if (carToDelete != null)
                    {
                        deletecar.Remove(carToDelete);

                        string updatedJson = JsonConvert.SerializeObject(deletecar, options);
                        File.WriteAllText(fileName, updatedJson);

                        MessageBox.Show("Car deleted successfully!");
                        index -= 1;
                    }
                    else
                    {
                        MessageBox.Show("Car not found in the file.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while deleting the car: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("History is empty.");
            }
        }

        private void Redo_OnClick(object sender, RoutedEventArgs e)
        {
            if (MainHistory.HistoryList.Count > 0)
            {
                Product lastProduct = MainHistory.HistoryList[index];

                try
                {
                    string fileName = "cars.json";

                    List<Product> addcar = new List<Product>();

                    JsonSerializerSettings options = new JsonSerializerSettings
                    {
                        Formatting = Formatting.Indented,
                        NullValueHandling = NullValueHandling.Ignore
                    };

                    if (File.Exists("cars.json"))
                    {
                        string json = File.ReadAllText("cars.json");
                        addcar = JsonConvert.DeserializeObject<List<Product>>(json, options);
                    }

                    addcar.Add(lastProduct);
                    
                    string updatedJson = JsonConvert.SerializeObject(addcar, options);

                    File.WriteAllText("cars.json", updatedJson);

                    MessageBox.Show("Car add successfully!");
                    
                    index += 1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while add the car: {ex.Message}");
                }
            }
        }
    }
}