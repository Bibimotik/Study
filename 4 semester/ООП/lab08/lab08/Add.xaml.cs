using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using Npgsql;
using NpgsqlTypes;

namespace lab08
{
    public partial class Add : Window
    {
        public Student student;
        
        private string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        
        public Add()
        {
            InitializeComponent();
            student = new Student();
        }
        
        private void Insert(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(FullName.Text)
                || string.IsNullOrEmpty(Age.Text)
                || string.IsNullOrEmpty(Specialty.Text)
                || string.IsNullOrEmpty(Course.Text)
                || string.IsNullOrEmpty(GroupName.Text)
                || string.IsNullOrEmpty(AverageGrade.Text)
                || string.IsNullOrEmpty(Gender.Text)
                || string.IsNullOrEmpty(AddressId.Text))
            {
                MessageBox.Show("Заполните поля!");
            }
            else
            {
                student.FullName = FullName.Text;

                if (!int.TryParse(Age.Text, out int age) || age < 18)
                {
                    MessageBox.Show("Возраст должен быть числом больше 18");
                }
                else
                {
                    student.Age = age;
                    
                    student.GroupName = GroupName.Text;

                    if (!decimal.TryParse(AverageGrade.Text, out decimal avg) || avg < 0)
                    {
                        MessageBox.Show("Средняя оценка должна быть числом больше 0 в формате %,%%");
                    }
                    else
                    {
                        student.AverageGrade = avg;
                        
                        if (!int.TryParse(AddressId.Text, out int address) || address < 0)
                        {
                            MessageBox.Show("ID адреса должно быть числом больше 0");
                        }
                        else
                        {
                            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                            {
                                connection.Open();

                                using (NpgsqlCommand command = new NpgsqlCommand("SELECT COUNT(*) FROM Address WHERE AddressId = @addressId", connection))
                                {
                                    command.Parameters.AddWithValue("addressId", address);

                                    int count = Convert.ToInt32(command.ExecuteScalar());

                                    if (count == 0)
                                    {
                                        MessageBox.Show("Указанный ID адреса не существует в таблице Address");
                                    }
                                    else
                                    {
                                        student.AddressId = address;

                                        student.Specialty = Specialty.Text;
                                        student.Gender = Gender.Text;

                                        if (!int.TryParse(Course.Text, out int course) || course < 0)
                                        {
                                            MessageBox.Show("Курс должен быть числом больше 0");
                                        }
                                        else
                                        {
                                            student.Course = course;

                                            OpenFileDialog openFileDialog = new OpenFileDialog();
                                            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                                            if (openFileDialog.ShowDialog() != true)
                                            {
                                                MessageBox.Show("Не удалось выбрать изображение!");
                                                return;
                                            }
                                            else
                                            {
                                                // Преобразование изображения в массив байтов
                                                byte[] imageData = File.ReadAllBytes(openFileDialog.FileName);
                                                student.Image = imageData;

                                                using (NpgsqlConnection connection1 = new NpgsqlConnection(connectionString))
                                                {
                                                    connection1.Open();
                                                    using (var transaction = connection1.BeginTransaction())
                                                    {
                                                        try
                                                        {
                                                            using (NpgsqlCommand command1 = new NpgsqlCommand("add_student", connection1))
                                                            {
                                                                command1.CommandType = CommandType.StoredProcedure;
                
                                                                command1.Parameters.AddWithValue("p_full_name", NpgsqlDbType.Varchar, 100).Value = student.FullName;
                                                                command1.Parameters.AddWithValue("p_age", NpgsqlDbType.Integer).Value = student.Age;
                                                                command1.Parameters.AddWithValue("p_specialty", NpgsqlDbType.Varchar, 50).Value = student.Specialty;
                                                                command1.Parameters.AddWithValue("p_course", NpgsqlDbType.Integer).Value = student.Course;
                                                                command1.Parameters.AddWithValue("p_group_name", NpgsqlDbType.Varchar, 20).Value = student.GroupName;
                                                                command1.Parameters.AddWithValue("p_average_grade", NpgsqlDbType.Numeric).Value = student.AverageGrade;
                                                                command1.Parameters.AddWithValue("p_gender", NpgsqlDbType.Varchar, 10).Value = student.Gender;
                                                                command1.Parameters.AddWithValue("p_address_id", NpgsqlDbType.Integer).Value = student.AddressId;
                                                                command1.Parameters.AddWithValue("p_image", NpgsqlDbType.Integer).Value = student.Image;

                                                                command1.ExecuteNonQuery();
                                                            }

                                                            transaction.Commit();
                                                        }
                                                        catch (Exception exception)
                                                        {
                                                            transaction.Rollback();
                                                            MessageBox.Show($"Error: '{exception}'");
                                                            throw;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}