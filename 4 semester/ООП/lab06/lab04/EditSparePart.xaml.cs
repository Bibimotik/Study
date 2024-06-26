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
    public partial class EditSparePart : Window
    {
        public Product SparePart { get; set; }
        public Product UpdatedCar { get; set; }
        public Product OldSparePart { get; set; }
        public string Theme;

        public EditSparePart(Product sparePart, string theme)
        {
            InitializeComponent();
            SparePart = sparePart;
            UpdatedCar = new Product
            {
                Name = sparePart.Name,
                Short_Name = sparePart.Short_Name,
                Photo = sparePart.Photo,
                Category = sparePart.Category,
                Price = sparePart.Price,
                Rating = sparePart.Rating,
                Color = sparePart.Color,
                Description = sparePart.Description,
                In_Stock = sparePart.In_Stock
            };
            OldSparePart = new Product
            {
                Name = sparePart.Name,
                Short_Name = sparePart.Short_Name,
                Photo = sparePart.Photo,
                Category = sparePart.Category,
                Price = sparePart.Price,
                Rating = sparePart.Rating,
                Color = sparePart.Color,
                Description = sparePart.Description,
                In_Stock = sparePart.In_Stock
            };
            DataContext = UpdatedCar;
            
            NameTextBox.Text = sparePart.Name;
            ShortNameTextBox.Text = sparePart.Short_Name;
            PriceTextBox.Text = sparePart.Price.ToString();
            RatingSlider.Value = sparePart.Rating;
            ColorTextBox.Text = sparePart.Color;
            DescriptionRichTextBox.Document.Blocks.Clear();
            DescriptionRichTextBox.Document.Blocks.Add(new Paragraph(new Run(sparePart.Description)));
            InStockCheckBox.IsChecked = sparePart.In_Stock == 1;
            
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
                List<Product> addsparepart = new List<Product>();

                JsonSerializerSettings options = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Ignore
                };
                
                try
                {
                    string fileName = "spare_parts.json";
                    
                    List<Product> deletesparepart = new List<Product>();

                    if (File.Exists("spare_parts.json"))
                    {
                        string json = File.ReadAllText("spare_parts.json");
                        deletesparepart = JsonConvert.DeserializeObject<List<Product>>(json, options);
                    }

                    Product carToDelete = deletesparepart.FirstOrDefault(c => c.Name == SparePart.Name && c.Short_Name == SparePart.Short_Name && c.Price == SparePart.Price && c.Color == SparePart.Color);

                    if (carToDelete != null)
                    {
                        deletesparepart.Remove(carToDelete);

                        string updatedJson1 = JsonConvert.SerializeObject(deletesparepart, options);
                        File.WriteAllText(fileName, updatedJson1);
                    }
                    else
                    {
                        MessageBox.Show("Spare part not found in the file.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message}");
                }

                if (File.Exists("spare_parts.json"))
                {
                    string json = File.ReadAllText("spare_parts.json");
                    addsparepart = JsonConvert.DeserializeObject<List<Product>>(json, options);
                }
                
                addsparepart.Add(UpdatedCar);

                string updatedJson = JsonConvert.SerializeObject(addsparepart, options);

                File.WriteAllText("spare_parts.json", updatedJson);
                
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