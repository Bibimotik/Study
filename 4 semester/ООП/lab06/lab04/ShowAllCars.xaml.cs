using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Newtonsoft.Json;
using System.Windows.Media.Imaging;

namespace lab04
{
    public partial class ShowAllCars : Window
    {
        private const int SquareSize = 200;
        private const int Margin = 10;

        public string Theme;
        public List<Product> SearchResults { get; set; }
        public List<Product> SortResults { get; set; }
        private List<Product> objects;

        //public History History = new History();
        
        public ShowAllCars(string theme)
        {
            InitializeComponent();

            //History.HistoryList = history;
            
            Theme = theme;
            
            ResourceDictionary resourceDict = new ResourceDictionary();
            resourceDict.Source = new System.Uri($"D:/Учеба/4_семестр/ООП/test/test/Theme/{Theme}.xaml", System.UriKind.Absolute);
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);
            
            SizeChanged += LoadAndDrawCars;
            
            CategoryComboBox.SelectionChanged += sortCars;
            
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

        public void LoadAndDrawCars(object sender, SizeChangedEventArgs e)
        {
            string jsonFilePath = "cars.json";

            if (File.Exists(jsonFilePath))
            {
                string jsonContent = File.ReadAllText(jsonFilePath);
                if (SearchResults != null)
                {
                    if(SortResults != null)
                    {
                        objects = SortResults;
                    }
                    else
                    {
                        objects = SearchResults;
                    }
                }
                else if(SearchResults == null)
                {
                    if(SortResults != null)
                    {
                        objects = SortResults;
                    }
                    else
                    {
                        objects = JsonConvert.DeserializeObject<List<Product>>(jsonContent);
                    }
                }
                
                if (objects != null && objects.Count > 0)
                {
                    int availableWidth = (int)YourLayoutGrid.ActualWidth;
                    int gridColumns = Math.Max(1, availableWidth / (SquareSize + Margin * 2));

                    Grid grid = new Grid();
                    grid.Margin = new Thickness(Margin);

                    for (int i = 0; i < objects.Count; i++)
                    {
                        int copyOfI = i;

                        Grid innerGrid = new Grid();

                        Rectangle rectangle = new Rectangle
                        {
                            Name = $"r{copyOfI}",
                            VerticalAlignment = VerticalAlignment.Top,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            Width = 200,
                            Height = 100,
                            RadiusX = 10,
                            RadiusY = 10,
                            Margin = new Thickness(Margin)
                        };

                        string imagePath = objects[i].Photo;
                        if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                        {
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri(imagePath, UriKind.Relative));
                            rectangle.Fill = imageBrush;
                        }
                        else
                        {
                            rectangle.Fill = new SolidColorBrush(Colors.Blue);
                        }

                        rectangle.MouseLeftButtonDown += (s, args) =>
                        {
                            string newIndex = rectangle.Name;
                            newIndex = newIndex.Substring(1);
                            int rectangleIndex = Convert.ToInt32(newIndex);
                            Product selectedCar = objects[rectangleIndex];
                            //Close();

                            ShowOneCar showOneCarWindow = new ShowOneCar(selectedCar, Theme);
                            showOneCarWindow.ShowDialog();
                        };

                        Label label = new Label
                        {
                            Content = $"{objects[i].Name} {objects[i].Short_Name}",
                            Foreground = new SolidColorBrush(Colors.White),
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Bottom,
                            Margin = new Thickness(0, 0, 0, 10)
                        };

                        innerGrid.Children.Add(rectangle);
                        innerGrid.Children.Add(label);

                        grid.ColumnDefinitions.Add(new ColumnDefinition());
                        grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(170) });

                        Grid.SetColumn(innerGrid, i % gridColumns);
                        Grid.SetRow(innerGrid, i / gridColumns);

                        grid.Children.Add(innerGrid);
                    }

                    StackPanelLayout.Children.Clear();
                    StackPanelLayout.Children.Add(grid);
                }
            }
            else
            {
                MessageBox.Show("The cars.json file does not exist.");
            }
        }
        
        private void searchButton_Click(object sender, EventArgs e)
        {
            CategoryComboBox.SelectedItem = null;
            SortResults = null;
            SearchResults = PerformSearch();
            LoadAndDrawCars(null, null);
        }

        private List<Product> PerformSearch()
        {
            List<Product> searchResults = new List<Product>();

            List<Product> dataList;

            string json = File.ReadAllText("cars.json");
            dataList = JsonConvert.DeserializeObject<List<Product>>(json);

            string nameSearchText = SearchText.Text;

            string startPriceText = StartPrice.Text;
            string endPriceText = EndPrice.Text;

            decimal startPrice = 0;
            decimal endPrice = decimal.MaxValue;

            if (!string.IsNullOrEmpty(startPriceText))
            {
                decimal.TryParse(startPriceText, out startPrice);
            }

            if (!string.IsNullOrEmpty(endPriceText))
            {
                decimal.TryParse(endPriceText, out endPrice);
            }

            searchResults = dataList.Where(data =>
                    (string.IsNullOrEmpty(nameSearchText) || IsNameMatch(data, nameSearchText)) &&
                    data.Price >= startPrice && data.Price <= endPrice)
                .ToList();
            
            startPrice = 0;
            endPrice = decimal.MaxValue;

            return searchResults;
        }

        private bool IsNameMatch(Product car, string searchText)
        {
            return Regex.IsMatch(car.Name, searchText, RegexOptions.IgnoreCase) ||
                   Regex.IsMatch(car.Short_Name, searchText, RegexOptions.IgnoreCase);
        }
        
        private void sortCars(object sender, EventArgs e)
        {
            if (CategoryComboBox.SelectedItem != null)
            {
                string sortBy = ((ComboBoxItem)CategoryComboBox.SelectedItem).Content.ToString();
                SortResults = PerformSort(sortBy);
                LoadAndDrawCars(null, null);
            }
        }
        
        private List<Product> PerformSort(string sortBy)
        {
            List<Product> sortedList = new List<Product>();

            List<Product> dataList = new List<Product>();
            
            if (SearchResults == null)
            {
                string json = File.ReadAllText("cars.json");
                dataList = JsonConvert.DeserializeObject<List<Product>>(json);
            }
            else
            {
                dataList = SearchResults;
            }
            switch (sortBy)
            {
                case "Price up":
                    sortedList = dataList.OrderBy(car => car.Price).ToList();
                    break;
                case "Цена выс":
                    sortedList = dataList.OrderBy(car => car.Price).ToList();
                    break;
                case "Price down":
                    sortedList = dataList.OrderByDescending(car => car.Price).ToList();
                    break;
                case "Цена низ":
                    sortedList = dataList.OrderByDescending(car => car.Price).ToList();
                    break;
                case "In stock":
                    sortedList = dataList.Where(car => car.In_Stock == 1).ToList();
                    break;
                case "В наличии":
                    sortedList = dataList.Where(car => car.In_Stock == 1).ToList();
                    break;
                case "Not in stock":
                    sortedList = dataList.Where(car => car.In_Stock == 0).ToList();
                    break;
                case "Не в наличии":
                    sortedList = dataList.Where(car => car.In_Stock == 0).ToList();
                    break;
                default:
                    sortedList = dataList;
                    break;
            }

            return sortedList;
        }


        /*private void Undo_OnClick(object sender, RoutedEventArgs e)
        {
            if (History.HistoryList.Count > 0)
            {
                Product lastProduct = History.HistoryList[History.HistoryList.Count - 1];
                
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
                        Close();
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

        }*/
    }
}