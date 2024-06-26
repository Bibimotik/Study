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
    public partial class ShowOneCar : Window
    {
        public Product Car { get; set; }

        public ICommand DeleteCommand { get; }
        public ICommand EditCommand { get; }

        public ShowOneCar(Product car)
        {
            InitializeComponent();
            
            Car = car;
            
            DataContext = this;

            Closing += ShowOneCar_Closing;

            DeleteCommand = new RelayCommand(DeleteCar);
            EditCommand = new RelayCommand(EditCar);
            
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

        private void DeleteCar(object parameter)
        {
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

                Product carToDelete = deletecar.FirstOrDefault(c => c.Name == Car.Name && c.Short_Name == Car.Short_Name && c.Price == Car.Price && c.Color == Car.Color);

                if (carToDelete != null)
                {
                    deletecar.Remove(carToDelete);

                    string updatedJson = JsonConvert.SerializeObject(deletecar, options);
                    File.WriteAllText(fileName, updatedJson);

                    MessageBox.Show("Car deleted successfully!");
                    ShowOneCar_Closing(null, null);
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

        private void EditCar(object parameter)
        {
            EditCar editWindow = new EditCar(Car);
            editWindow.ShowDialog();
            Close();

            //Car = editWindow.UpdatedCar;
        }
        
        private void ShowOneCar_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ShowAllCars showAllCarsWindow = Application.Current.Windows.OfType<ShowAllCars>().FirstOrDefault();
            if (showAllCarsWindow != null)
            {
                showAllCarsWindow.LoadAndDrawCars(null, null);
            }
        }
        
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double windowWidth = e.NewSize.Width;

            if (windowWidth < 600)
            {
                MainGrid.ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Star);
                CarImage.Width = 170;
                CarImage.Height = 70;
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
                CarImage.Width = 200;
                CarImage.Height = 100;
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