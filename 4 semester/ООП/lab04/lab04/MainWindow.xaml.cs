using System;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace lab04
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string Lang = "English";

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

            ResourceDictionary resourceDict = new ResourceDictionary();
            resourceDict.Source = new System.Uri("D:/Учеба/4_семестр/ООП/lab04/lab04/Lang/English.xaml", System.UriKind.Absolute);
            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);

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
                    ShowAllCars showAllCars = new ShowAllCars();
                    showAllCars.Show();
                }
                else if (radioButton.Content.ToString() == "Add product" || radioButton.Content.ToString() == "Добавить продукт")
                {
                    AddProduct addProduct = new AddProduct();
                    addProduct.Show();
                }
                else if (radioButton.Content.ToString() == "Show all spare parts" || radioButton.Content.ToString() == "Показать все запчасти")
                {
                    ShowAllSpareParts showAllSpareParts = new ShowAllSpareParts();
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
                resourceDict.Source = new System.Uri("D:/Учеба/4_семестр/ООП/lab04/lab04/Lang/English.xaml", System.UriKind.Absolute);
                Application.Current.Resources.MergedDictionaries.Clear();
                Application.Current.Resources.MergedDictionaries.Add(resourceDict);
            }
            else if (Lang == "Russian")
            {
                ResourceDictionary resourceDict = new ResourceDictionary();
                resourceDict.Source = new System.Uri("D:/Учеба/4_семестр/ООП/lab04/lab04/Lang/Russian.xaml", System.UriKind.Absolute);
                Application.Current.Resources.MergedDictionaries.Clear();
                Application.Current.Resources.MergedDictionaries.Add(resourceDict);
            }
        }
    }
}