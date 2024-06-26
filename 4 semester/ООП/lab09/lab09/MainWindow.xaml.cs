using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace lab09
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LastMaintenanceDatePicker.SelectedDate = DateTime.Now;
        }
        
        private void WriteAirplane(object sender, RoutedEventArgs e)
        {
            string airplaneIDText = AirplaneIDTextBox.Text;
            int id;
            if (string.IsNullOrEmpty(airplaneIDText))
            {
                MessageBox.Show("Поле ID самолета не может быть пустым.", "Ошибка");
                return;
            }

            if (!int.TryParse(airplaneIDText, out id) || id < 0)
            {
                MessageBox.Show("Некорректное значение в поле ID самолета.", "Ошибка");
                return;
            }

            string type = ((ComboBoxItem)AirplaneTypeComboBox.SelectedItem).Content.ToString();
            if (string.IsNullOrEmpty(type))
            {
                MessageBox.Show("Поле 'Тип' должно быть заполнено.");
                return;
            }
            if (!decimal.TryParse(CargoCapacityTextBox.Text, out decimal cargoCapacity) || cargoCapacity < 0)
            {
                MessageBox.Show("Неверный формат значения в поле 'Грузоподъемность'. Пожалуйста, введите числовое значение.");
                return;
            }

            if (!int.TryParse(YearOfManufactureTextBox.Text, out int yearOfManufacture) || yearOfManufacture < 0)
            {
                MessageBox.Show("Неверный формат значения в поле 'Год'. Пожалуйста, введите целое число.");
                return;
            }

            if (!int.TryParse(PassengerSeatsTextBox.Text, out int passengerSeats) || passengerSeats < 0)
            {
                MessageBox.Show("Неверный формат значения в поле 'Количество пассажирских мест'. Пожалуйста, введите целое число.");
                return;
            }
            string model = ModelTextBox.Text;
            if (string.IsNullOrEmpty(model))
            {
                MessageBox.Show("Поле 'Модель' должно быть заполнено.");
                return;
            }
            DateTime lastMaintenanceDate = LastMaintenanceDatePicker.SelectedDate.Value;

            if (lastMaintenanceDate > DateTime.Now)
            {
                MessageBox.Show("Дата последнего то не может быть больше текущего момента.");
                return;
            }

            if (yearOfManufacture > DateTime.Now.Year)
            {
                MessageBox.Show("Год выпуска не может быть больше текущего года.");
                return;
            }

            if (lastMaintenanceDate.Year < yearOfManufacture)
            {
                MessageBox.Show("Год то не может раньше больше текущего года.");
                return;
            }
            
            if (LastMaintenanceDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Дата ТО не может пустой");
                return;
            }

            Airplane airplane = new Airplane
            {
                ID = id,
                Type = type,
                CargoCapacity = cargoCapacity,
                YearOfManufacture = yearOfManufacture,
                PassengerSeats = passengerSeats,
                Model = model,
                LastMaintenanceDate = lastMaintenanceDate
            };

            using (Model1 dbContext = new Model1())
            {
                var existingAirplane = dbContext.Airplanes.FirstOrDefault(a => a.ID == id);

                if (existingAirplane != null)
                {
                    MessageBox.Show("Самолет с указанным ID уже существует в базе данных.", "Ошибка");
                    return;
                }
                dbContext.Airplanes.Add(airplane);
                dbContext.SaveChanges();
            }

            MessageBox.Show("Самолёт успешно записан.", "Успех");
        }

        private void DeleteAirplane(object sender, RoutedEventArgs e)
        {
            using (Model1 dbContext = new Model1())
            {
                int airplaneID;

                if (int.TryParse(AirplaneIDTextBox.Text, out airplaneID))
                {
                    var airplane = dbContext.Airplanes.FirstOrDefault(a => a.ID == airplaneID);

                    if (airplane != null)
                    {
                        var crewMembers = dbContext.CrewMembers.Where(c => c.AirplaneID == airplaneID);
                        dbContext.CrewMembers.RemoveRange(crewMembers);

                        dbContext.Airplanes.Remove(airplane);
                        dbContext.SaveChanges();

                        MessageBox.Show("Самолет и связанные с ним члены экипажа успешно удалены.", "Успех");
                    }
                    else
                    {
                        MessageBox.Show("Самолет с указанным ID не найден.", "Ошибка");
                    }
                }
                else
                {
                    MessageBox.Show("Некорректный ID самолета.", "Ошибка");
                }
            }
        }

        private void GetInfoAirplane(object sender, RoutedEventArgs e)
        {
            int airplaneID;

            if (int.TryParse(AirplaneIDTextBox.Text, out airplaneID))
            {
                using (Model1 dbContext = new Model1())
                {
                    var airplane = dbContext.Airplanes.FirstOrDefault(a => a.ID == airplaneID);

                    if (airplane != null)
                    {
                        AirplaneTypeComboBox.SelectedItem = AirplaneTypeComboBox.Items
                            .OfType<ComboBoxItem>()
                            .FirstOrDefault(item => item.Content.ToString() == airplane.Type);

                        CargoCapacityTextBox.Text = airplane.CargoCapacity.ToString();
                        YearOfManufactureTextBox.Text = airplane.YearOfManufacture.ToString();
                        PassengerSeatsTextBox.Text = airplane.PassengerSeats.ToString();
                        ModelTextBox.Text = airplane.Model;
                        LastMaintenanceDatePicker.SelectedDate = airplane.LastMaintenanceDate;
                    }
                    else
                    {
                        MessageBox.Show("Самолет с указанным ID не найден.", "Ошибка");
                    }
                }
            }
            else
            {
                MessageBox.Show("Некорректный ID самолета.", "Ошибка");
            }
        }

        private void UpdateAirplane(object sender, RoutedEventArgs e)
        {
            string airplaneIDText = AirplaneIDTextBox.Text;
            int id;
            if (string.IsNullOrEmpty(airplaneIDText))
            {
                MessageBox.Show("Поле ID самолета не может быть пустым.", "Ошибка");
                return;
            }

            if (!int.TryParse(airplaneIDText, out id) || id < 0)
            {
                MessageBox.Show("Некорректное значение в поле ID самолета.", "Ошибка");
                return;
            }

            string type = ((ComboBoxItem)AirplaneTypeComboBox.SelectedItem).Content.ToString();
            if (string.IsNullOrEmpty(type))
            {
                MessageBox.Show("Поле 'Тип' должно быть заполнено.");
                return;
            }
            if (!decimal.TryParse(CargoCapacityTextBox.Text, out decimal cargoCapacity) || cargoCapacity < 0)
            {
                MessageBox.Show("Неверный формат значения в поле 'Грузоподъемность'. Пожалуйста, введите числовое значение.");
                return;
            }

            if (!int.TryParse(YearOfManufactureTextBox.Text, out int yearOfManufacture) || yearOfManufacture < 0)
            {
                MessageBox.Show("Неверный формат значения в поле 'год выпуска'. Пожалуйста, введите целое число.");
                return;
            }

            if (!int.TryParse(PassengerSeatsTextBox.Text, out int passengerSeats) || passengerSeats < 0)
            {
                MessageBox.Show("Неверный формат значения в поле 'Количество пассажирских мест'. Пожалуйста, введите целое число.");
                return;
            }
            string model = ModelTextBox.Text;
            if (string.IsNullOrEmpty(model))
            {
                MessageBox.Show("Поле 'Модель' должно быть заполнено.");
                return;
            }
            DateTime lastMaintenanceDate = LastMaintenanceDatePicker.SelectedDate.Value;

            if (lastMaintenanceDate > DateTime.Now)
            {
                MessageBox.Show("Дата последнего то не может быть больше текущего момента.");
                return;
            }

            if (yearOfManufacture > DateTime.Now.Year)
            {
                MessageBox.Show("Год выпуска не может быть больше текущего года.");
                return;
            }

            if (lastMaintenanceDate.Year < yearOfManufacture)
            {
                MessageBox.Show("Год ТО не может раньше больше текущего года.");
                return;
            }
            
            if (LastMaintenanceDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Дата ТО не может пустой");
                return;
            }

            using (Model1 dbContext = new Model1())
            {
                var airplane = dbContext.Airplanes.FirstOrDefault(a => a.ID == id);

                if (airplane != null)
                {
                    airplane.Type = type;
                    airplane.CargoCapacity = cargoCapacity;
                    airplane.YearOfManufacture = yearOfManufacture;
                    airplane.PassengerSeats = passengerSeats;
                    airplane.Model = model;
                    airplane.LastMaintenanceDate = lastMaintenanceDate;

                    dbContext.SaveChanges();
                    MessageBox.Show("Информация о самолете успешно обновлена.", "Успех");
                }
                else
                {
                    MessageBox.Show("Самолет с указанным ID не найден.", "Ошибка");
                }
            }
        }


        public bool CheckNamePattern(string name)
        {
            string pattern = @"^[A-Za-zА-Яа-яЁё\s-]+$";
            return Regex.IsMatch(name, pattern);
        }

        private void WriteCrewMember(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(CrewId.Text, out int crewID) || crewID < 0)
            {
                MessageBox.Show("Неверный формат значения в поле 'ID'. Пожалуйста, введите целое число.");
                return;
            }

            int airplaneID;
            if (!int.TryParse(IdAirplain.Text, out airplaneID) || airplaneID < 0)
            {
                MessageBox.Show("Некорректный ID самолета.", "Ошибка");
                return;
            }

            string fullName = CrewName.Text;
            string position = ((ComboBoxItem)PositionComboBox.SelectedItem).Content.ToString();
            int age;

            if (!CheckNamePattern(fullName))
            {
                MessageBox.Show("Неверный формат значения в поле 'ФИО'.");
                return;
            }

            if (!int.TryParse(CrewAge.Text, out age) || age < 0)
            {
                MessageBox.Show("Некорректный возраст.", "Ошибка");
                return;
            }

            if (age < 21 || age > 100)
            {
                MessageBox.Show("Неверное значение в поле 'Age'. Пожалуйста, введите возраст от 21 до 100 лет.");
                return;
            }

            if (!int.TryParse(CrewExp.Text, out int experience) || experience < 0)
            {
                MessageBox.Show("Неверный формат значения в поле 'Experience'. Пожалуйста, введите целое число.");
                return;
            }

            if (age >= 21 && experience > (age - 21))
            {
                MessageBox.Show("Неверное значение в поле 'Experience'. Стаж не может быть больше " + (age - 21) + " лет, если возраст составляет " + age + " лет.");
                return;
            }

            using (Model1 dbContext = new Model1())
            {
                var airplane = dbContext.Airplanes.FirstOrDefault(a => a.ID == airplaneID);
                if (airplane != null)
                {
                    MessageBox.Show("Член экипажа с указанным ID уже существует в базе данных.", "Ошибка");
                    return;
                }

                CrewMember crewMember = new CrewMember
                {
                    ID = crewID,
                    FullName = fullName,
                    Position = position,
                    Age = age,
                    Experience = experience,
                    AirplaneID = airplaneID
                };

                dbContext.CrewMembers.Add(crewMember);
                dbContext.SaveChanges();

                MessageBox.Show("Новый член экипажа успешно добавлен.", "Успех");
            }
        }

        private void DeleteCrewMember(object sender, RoutedEventArgs e)
        {
            using (Model1 dbContext = new Model1())
            {
                int crewMemberId;

                if (int.TryParse(CrewId.Text, out crewMemberId))
                {
                    using (var transaction = dbContext.Database.BeginTransaction())
                    {
                        try
                        {
                            var crewMember = dbContext.CrewMembers.FirstOrDefault(c => c.ID == crewMemberId);

                            if (crewMember != null)
                            {
                                dbContext.CrewMembers.Remove(crewMember);
                                dbContext.SaveChanges();

                                transaction.Commit();

                                MessageBox.Show("Член экипажа успешно удален.", "Успех");
                            }
                            else
                            {
                                MessageBox.Show("Член экипажа с указанным ID не найден.", "Ошибка");
                            }
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Ошибка при удалении члена экипажа: " + ex.Message, "Ошибка");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Некорректный ID члена экипажа.", "Ошибка");
                }
            }
        }

        private void UpdateCrewMember(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(CrewId.Text, out int crewID) || crewID < 0)
            {
                MessageBox.Show("Неверный формат значения в поле 'ID'. Пожалуйста, введите целое число.");
                return;
            }

            int airplaneID;
            if (!int.TryParse(IdAirplain.Text, out airplaneID) || airplaneID < 0)
            {
                MessageBox.Show("Некорректный ID самолета.", "Ошибка");
                return;
            }

            string fullName = CrewName.Text;
            string position = ((ComboBoxItem)PositionComboBox.SelectedItem).Content.ToString();
            int age;

            if (!CheckNamePattern(fullName))
            {
                MessageBox.Show("Неверный формат значения в поле 'ФИО'.");
                return;
            }

            if (!int.TryParse(CrewAge.Text, out age) || age < 0)
            {
                MessageBox.Show("Некорректный возраст.", "Ошибка");
                return;
            }

            if (age < 21 || age > 100)
            {
                MessageBox.Show("Неверное значение в поле 'Age'. Пожалуйста, введите возраст от 21 до 100 лет.");
                return;
            }

            if (!int.TryParse(CrewExp.Text, out int experience) || experience < 0)
            {
                MessageBox.Show("Неверный формат значения в поле 'Experience'. Пожалуйста, введите целое число.");
                return;
            }

            if (age >= 21 && experience > (age - 21))
            {
                MessageBox.Show("Неверное значение в поле 'Experience'. Стаж не может быть больше " + (age - 21) + " лет, если возраст составляет " + age + " лет.");
                return;
            }


            using (Model1 dbContext = new Model1())
            {
                var crewMember = dbContext.CrewMembers.FirstOrDefault(c => c.ID == crewID);

                if (crewMember == null)
                {
                    MessageBox.Show("Член экипажа с указанным ID не найден.", "Ошибка");
                    return;
                }

                crewMember.FullName = fullName;
                crewMember.Position = position;
                crewMember.Age = age;
                crewMember.Experience = experience;
                crewMember.AirplaneID = airplaneID;

                dbContext.SaveChanges();
            }

            MessageBox.Show("Информация о члене экипажа успешно обновлена.", "Успех");
        }
        
        private void GetInfoCrewMember(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(CrewId.Text);

            using (Model1 dbContext = new Model1())
            {
                var query = "SELECT * FROM CrewMembers WHERE ID = @id";
                var crewMember = dbContext.Database.SqlQuery<CrewMember>(query, new SqlParameter("id", id)).FirstOrDefault();

                if (crewMember == null)
                {
                    MessageBox.Show("Член экипажа с указанным ID не найден.", "Ошибка");
                    return;
                }

                CrewName.Text = crewMember.FullName;
                CrewAge.Text = crewMember.Age.ToString();
                CrewExp.Text = crewMember.Experience.ToString();

                foreach (ComboBoxItem item in PositionComboBox.Items)
                {
                    if (item.Content.ToString() == crewMember.Position)
                    {
                        item.IsSelected = true;
                        break;
                    }
                }

                IdAirplain.Text = crewMember.AirplaneID.ToString();
            }
        }

        private void load_Crew(object sender, RoutedEventArgs e)
        {
            using (Model1 dbContext = new Model1())
            {
                var crewMembers = dbContext.CrewMembers.ToList();
                CrewMembersGrid.ItemsSource = crewMembers;
            }
        }

        private void load_Airplanes(object sender, RoutedEventArgs e)
        {
            using (Model1 dbContext = new Model1())
            {
                var airplanes = dbContext.Airplanes.ToList();
                AirplaneGrid.ItemsSource = airplanes;
            }
        }

        private void sort_Crew_Up(object sender, RoutedEventArgs e)
        {
            using (Model1 dbContext = new Model1())
            {
                var sortedByAge = dbContext.CrewMembers.OrderBy(c => c.Age).ToList();
                CrewMembersGrid.ItemsSource = sortedByAge;
            }
        }

        private void sort_Crew_Down(object sender, RoutedEventArgs e)
        {
            using (Model1 dbContext = new Model1())
            {
                var sortedByExperience = dbContext.CrewMembers.OrderByDescending(c => c.Experience).ToList();
                CrewMembersGrid.ItemsSource = sortedByExperience;
            }
        }

        private async void sort_Airplanes(object sender, RoutedEventArgs e)
        {
            using (Model1 dbContext = new Model1())
            {
                var sortedByPassengerSeats = dbContext.Airplanes.OrderBy(a => a.PassengerSeats).ToList();

                await Task.Delay(5000);

                AirplaneGrid.ItemsSource = sortedByPassengerSeats;
            }
        }

        private void search(object sender, RoutedEventArgs e)
        {
            using (Model1 dbContext = new Model1())
            {
                string searchName = EnterName.Text;
                string searchAgeText = EnterAge.Text;

                var crewMembersQuery = dbContext.CrewMembers.AsQueryable();

                if (!string.IsNullOrEmpty(searchName) && !string.IsNullOrEmpty(searchAgeText))
                {
                    int searchAge = Convert.ToInt32(searchAgeText);
                    crewMembersQuery = crewMembersQuery.Where(c => c.FullName.Contains(searchName) && c.Age == searchAge);
                }
                else if (!string.IsNullOrEmpty(searchName))
                {
                    crewMembersQuery = crewMembersQuery.Where(c => c.FullName.Contains(searchName));
                }
                else if (!string.IsNullOrEmpty(searchAgeText))
                {
                    int searchAge = Convert.ToInt32(searchAgeText);
                    crewMembersQuery = crewMembersQuery.Where(c => c.Age == searchAge);
                }

                var crewMembersByNameAndAge = crewMembersQuery.ToList();

                CrewMembersGrid.ItemsSource = crewMembersByNameAndAge;
            }
        }
    }
}
