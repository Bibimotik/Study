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

        //Добавление самолёта
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

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var airplaneRepository = unitOfWork.Airplanes;

                if (airplaneRepository.GetAirplaneById(id) != null)
                {
                    MessageBox.Show("Самолет с указанным ID уже существует в базе данных.", "Ошибка");
                    return;
                }

                airplaneRepository.AddAirplane(airplane);

                unitOfWork.Commit();
            }

            MessageBox.Show("Самолёт успешно записан.", "Успех");
        }


        //Удаление самолёта
        private void DeleteAirplane(object sender, RoutedEventArgs e)
        {
            int airplaneID;

            if (int.TryParse(AirplaneIDTextBox.Text, out airplaneID))
            {
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    var airplaneRepository = unitOfWork.Airplanes;
                    var crewMemberRepository = unitOfWork.CrewMembers;

                    var airplane = airplaneRepository.GetAirplaneById(airplaneID);

                    if (airplane != null)
                    {
                        var crewMembers = crewMemberRepository.GetAllCrewMembers()
                            .Where(c => c.AirplaneID == airplaneID);
                        foreach (var crewMember in crewMembers)
                        {
                            crewMemberRepository.DeleteCrewMember(crewMember);
                        }

                        airplaneRepository.DeleteAirplane(airplane);

                        unitOfWork.Commit();

                        MessageBox.Show("Самолет и связанные с ним члены экипажа успешно удалены.", "Успех");
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


        //получение информации о самолёте
        private void GetInfoAirplane(object sender, RoutedEventArgs e)
        {
            int airplaneID;

            if (int.TryParse(AirplaneIDTextBox.Text, out airplaneID))
            {
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    var airplaneRepository = unitOfWork.Airplanes;

                    var airplane = airplaneRepository.GetAirplaneById(airplaneID);

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


        //обновление информации о самолёте
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

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var airplaneRepository = unitOfWork.Airplanes;

                var airplane = airplaneRepository.GetAirplaneById(id);

                if (airplane != null)
                {
                    airplane.Type = type;
                    airplane.CargoCapacity = cargoCapacity;
                    airplane.YearOfManufacture = yearOfManufacture;
                    airplane.PassengerSeats = passengerSeats;
                    airplane.Model = model;
                    airplane.LastMaintenanceDate = lastMaintenanceDate;

                    unitOfWork.Commit();

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

        //добавление члена экипажа
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

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var airplaneRepository = unitOfWork.Airplanes;
                var crewMemberRepository = unitOfWork.CrewMembers;

                var airplane = airplaneRepository.GetAirplaneById(airplaneID);
                if (airplane == null)
                {
                    MessageBox.Show("Самолет с указанным ID не найден.", "Ошибка");
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

                crewMemberRepository.AddCrewMember(crewMember);

                unitOfWork.Commit();

                MessageBox.Show("Новый член экипажа успешно добавлен.", "Успех");
            }
        }

        //удаление члена экипажа
        private void DeleteCrewMember(object sender, RoutedEventArgs e)
        {
            int crewMemberId;

            if (int.TryParse(CrewId.Text, out crewMemberId))
            {
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    var crewMemberRepository = unitOfWork.CrewMembers;

                    var crewMember = crewMemberRepository.GetCrewMemberById(crewMemberId);

                    if (crewMember != null)
                    {
                        crewMemberRepository.DeleteCrewMember(crewMember);

                        unitOfWork.Commit();

                        MessageBox.Show("Член экипажа успешно удален.", "Успех");
                    }
                    else
                    {
                        MessageBox.Show("Член экипажа с указанным ID не найден.", "Ошибка");
                    }
                }
            }
            else
            {
                MessageBox.Show("Некорректный ID члена экипажа.", "Ошибка");
            }
        }


        //обновление информации о члене экипажа
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
            
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var crewMemberRepository = unitOfWork.CrewMembers;

                var crewMember = crewMemberRepository.GetCrewMemberById(crewID);

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

                unitOfWork.Commit();

                MessageBox.Show("Информация о члене экипажа успешно обновлена.", "Успех");
            }
        }


        //получение информации о члене экипажа
        private void GetInfoCrewMember(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(CrewId.Text);

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var crewMemberRepository = unitOfWork.CrewMembers;

                var crewMember = crewMemberRepository.GetCrewMemberById(id);

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

        // Загрузка членов экипажа
        private void load_Crew(object sender, RoutedEventArgs e)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var crewMemberRepository = unitOfWork.CrewMembers;

                var crewMembers = crewMemberRepository.GetAllCrewMembers();
                CrewMembersGrid.ItemsSource = crewMembers;
            }
        }

        // Загрузка самолетов
        private void load_Airplanes(object sender, RoutedEventArgs e)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var airplaneRepository = unitOfWork.Airplanes;

                var airplanes = airplaneRepository.GetAllAirplanes();
                AirplaneGrid.ItemsSource = airplanes;
            }
        }

        // Сортировка по возрастанию возраста
        private void sort_Crew_Up(object sender, RoutedEventArgs e)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var crewMemberRepository = unitOfWork.CrewMembers;

                var sortedByAge = crewMemberRepository.GetCrewMembersSortedByAgeAscending();
                CrewMembersGrid.ItemsSource = sortedByAge;
            }
        }

        // Сортировка по убыванию опыта
        private void sort_Crew_Down(object sender, RoutedEventArgs e)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var crewMemberRepository = unitOfWork.CrewMembers;

                var sortedByExperience = crewMemberRepository.GetCrewMembersSortedByExperienceDescending();
                CrewMembersGrid.ItemsSource = sortedByExperience;
            }
        }

        // Сортировка по возрастанию вместимости самолета
        private async void sort_Airplanes(object sender, RoutedEventArgs e)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var airplaneRepository = unitOfWork.Airplanes;

                var sortedByPassengerSeats =
                    await airplaneRepository.GetAirplanesSortedByPassengerSeatsAscendingAsync();

                await Task.Delay(5000);

                AirplaneGrid.ItemsSource = sortedByPassengerSeats;
            }
        }

        // Поиск
        private void search(object sender, RoutedEventArgs e)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                var crewMemberRepository = unitOfWork.CrewMembers;

                string searchName = EnterName.Text;
                string searchAgeText = EnterAge.Text;

                var crewMembersQuery = crewMemberRepository.GetAllCrewMembers();

                if (!string.IsNullOrEmpty(searchName) && !string.IsNullOrEmpty(searchAgeText))
                {
                    int searchAge = Convert.ToInt32(searchAgeText);
                    crewMembersQuery =
                        crewMembersQuery.Where(c => c.FullName.Contains(searchName) && c.Age == searchAge);
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