using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using Newtonsoft.Json;

namespace lab04
{
    public partial class EditCar : Window
    {
        public Product Car { get; set; }
        public Product UpdatedCar { get; set; }
        public Product OldCar { get; set; }
        public string Theme;

        public EditCar(Product car, string theme)
        {
            InitializeComponent();
            Car = car;
            UpdatedCar = new Product
            {
                Name = car.Name,
                Short_Name = car.Short_Name,
                Photo = car.Photo,
                Category = car.Category,
                Price = car.Price,
                Rating = car.Rating,
                Color = car.Color,
                Description = car.Description,
                In_Stock = car.In_Stock
            };
            OldCar = new Product
            {
                Name = car.Name,
                Short_Name = car.Short_Name,
                Photo = car.Photo,
                Category = car.Category,
                Price = car.Price,
                Rating = car.Rating,
                Color = car.Color,
                Description = car.Description,
                In_Stock = car.In_Stock
            };
            DataContext = UpdatedCar;
            
            NameTextBox.Text = car.Name;
            ShortNameTextBox.Text = car.Short_Name;
            PriceTextBox.Text = car.Price.ToString();
            RatingSlider.Value = car.Rating;
            ColorTextBox.Text = car.Color;
            DescriptionRichTextBox.Document.Blocks.Clear();
            DescriptionRichTextBox.Document.Blocks.Add(new Paragraph(new Run(car.Description)));
            InStockCheckBox.IsChecked = car.In_Stock == 1;
            
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

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(NameTextBox.Text)
                || string.IsNullOrEmpty(ShortNameTextBox.Text)
                || string.IsNullOrEmpty(PriceTextBox.Text)
                || string.IsNullOrEmpty(ColorTextBox.Text)
                || DescriptionRichTextBox.Document == null)
            {
                MessageBox.Show("All fields must be filled!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            UpdatedCar.Name = NameTextBox.Text;
            UpdatedCar.Short_Name = ShortNameTextBox.Text;

            if (!int.TryParse(PriceTextBox.Text, out int price) || price < 0)
            {
                MessageBox.Show("Price must be a number > 0!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            UpdatedCar.Price = price;

            UpdatedCar.Rating = (int)RatingSlider.Value;
            UpdatedCar.Color = ColorTextBox.Text;
            UpdatedCar.Description = new TextRange(DescriptionRichTextBox.Document.ContentStart,
                DescriptionRichTextBox.Document.ContentEnd).Text;
            UpdatedCar.In_Stock = InStockCheckBox.IsChecked == true ? 1 : 0;

            try
            {
                List<Product> addcar = new List<Product>();

                JsonSerializerSettings options = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Ignore
                };
                
                try
                {
                    string fileName = "cars.json";
                    
                    List<Product> deletecar = new List<Product>();

                    if (File.Exists("cars.json"))
                    {
                        string json = File.ReadAllText("cars.json");
                        deletecar = JsonConvert.DeserializeObject<List<Product>>(json, options);
                    }

                    Product carToDelete = deletecar.FirstOrDefault(c => c.Name == Car.Name && c.Short_Name == Car.Short_Name && c.Price == Car.Price && c.Color == Car.Color);

                    if (carToDelete != null)
                    {
                        deletecar.Remove(carToDelete);

                        string updatedJson1 = JsonConvert.SerializeObject(deletecar, options);
                        File.WriteAllText(fileName, updatedJson1);
                    }
                    else
                    {
                        MessageBox.Show("Car not found in the file.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message}");
                }

                if (File.Exists("cars.json"))
                {
                    string json = File.ReadAllText("cars.json");
                    addcar = JsonConvert.DeserializeObject<List<Product>>(json, options);
                }
                
                addcar.Add(UpdatedCar);

                string updatedJson = JsonConvert.SerializeObject(addcar, options);

                File.WriteAllText("cars.json", updatedJson);
                
                MessageBox.Show($"{UpdatedCar.Category} data edited successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }
    }
}