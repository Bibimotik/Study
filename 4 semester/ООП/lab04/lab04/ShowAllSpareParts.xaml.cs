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
    public partial class ShowAllSpareParts : Window
    {
        private const int SquareSize = 200;
        private const int Margin = 10;

        public List<Product> SearchResults { get; set; }
        public List<Product> SortResults { get; set; }
        private List<Product> objects;

        public ShowAllSpareParts()
        {
            InitializeComponent();
            
            SizeChanged += LoadAndDrawSpareParts;
            
            CategoryComboBox.SelectionChanged += sortSpareParts;
            
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

        public void LoadAndDrawSpareParts(object sender, SizeChangedEventArgs e)
        {
            string jsonFilePath = "spare_parts.json";

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
                            Height = 200,
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
                            Product selectedSpare_Part = objects[rectangleIndex];
                            //Close();

                            ShowOneSparePart showOneSparePartWindow = new ShowOneSparePart(selectedSpare_Part);
                            showOneSparePartWindow.ShowDialog();
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
                        grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(220) });

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
                MessageBox.Show("The spare_parts.json file does not exist.");
            }
        }
        
        private void searchButton_Click(object sender, EventArgs e)
        {
            SearchResults = PerformSearch();
            LoadAndDrawSpareParts(null, null);
        }

        private List<Product> PerformSearch()
        {
            List<Product> searchResults = new List<Product>();

            List<Product> dataList;

            string json = File.ReadAllText("spare_parts.json");
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

        private bool IsNameMatch(Product spare_part, string searchText)
        {
            return Regex.IsMatch(spare_part.Name, searchText, RegexOptions.IgnoreCase) ||
                   Regex.IsMatch(spare_part.Short_Name, searchText, RegexOptions.IgnoreCase);
        }
        
        private void sortSpareParts(object sender, EventArgs e)
        {
            if (CategoryComboBox.SelectedItem != null)
            {
                string sortBy = ((ComboBoxItem)CategoryComboBox.SelectedItem).Content.ToString();
                SortResults = PerformSort(sortBy);
                LoadAndDrawSpareParts(null, null);
            }
        }
        
        private List<Product> PerformSort(string sortBy)
        {
            List<Product> sortedList = new List<Product>();

            List<Product> dataList = new List<Product>();
            
            if (SearchResults == null)
            {
                string json = File.ReadAllText("spare_parts.json");
                dataList = JsonConvert.DeserializeObject<List<Product>>(json);
            }
            else
            {
                dataList = SearchResults;
            }
            switch (sortBy)
            {
                case "Price up":
                    sortedList = dataList.OrderBy(spare_part => spare_part.Price).ToList();
                    break;
                case "Цена выс":
                    sortedList = dataList.OrderBy(spare_part => spare_part.Price).ToList();
                    break;
                case "Price down":
                    sortedList = dataList.OrderByDescending(spare_part => spare_part.Price).ToList();
                    break;
                case "Цена низ":
                    sortedList = dataList.OrderByDescending(spare_part => spare_part.Price).ToList();
                    break;
                case "In stock":
                    sortedList = dataList.Where(spare_part => spare_part.In_Stock == 1).ToList();
                    break;
                case "В наличии":
                    sortedList = dataList.Where(spare_part => spare_part.In_Stock == 1).ToList();
                    break;
                case "Not in stock":
                    sortedList = dataList.Where(spare_part => spare_part.In_Stock == 0).ToList();
                    break;
                case "Не в наличии":
                    sortedList = dataList.Where(spare_part => spare_part.In_Stock == 0).ToList();
                    break;
                default:
                    sortedList = dataList;
                    break;
            }

            return sortedList;
        }
        
        /*private List<Product> FilterCarsByPrice(List<Product> cars)
        {
            string startPriceText = StartPrice.Text;
            string endPriceText = EndPrice.Text;

            if (string.IsNullOrEmpty(startPriceText) && string.IsNullOrEmpty(endPriceText))
            {
                return cars;
            }

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

            return cars.Where(car => car.Price >= startPrice && car.Price <= endPrice).ToList();
        }
        
        private void filterButton_Click(object sender, EventArgs e)
        {
            SearchResults = PerformSearch();
            SearchResults = FilterCarsByPrice(SearchResults);
            LoadAndDrawCars(null, null);
        }*/
    }
}