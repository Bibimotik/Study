using System.Configuration;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using Npgsql;

namespace lab08;

public partial class Edit : Window
{
    public Student student;
    public int id;
    
    private string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
    
    public Edit()
    {
        InitializeComponent();
        student = new Student();
    }

    private void Get_Click(object sender, RoutedEventArgs e)
    {
        studentImage.Source = null;
        if (!int.TryParse(idstudent.Text, out int ID) || ID < 0)
        {
            MessageBox.Show("ID студента число больше 0");
        }
        else
        {
            id = ID;
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlTransaction transaction = connection.BeginTransaction())
                {
                    string sqlQuery = $"SELECT * FROM STUDENT WHERE StudentId = {id};";
    
                    using (NpgsqlCommand command = new NpgsqlCommand(sqlQuery, connection))
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string fullname = reader.GetString(1);
                                int age = reader.GetInt32(2);
                                string speciality = reader.GetString(3);
                                int course = reader.GetInt32(4);
                                string groupname = reader.GetString(5);
                                decimal avg = reader.GetDecimal(6);
                                string gender = reader.GetString(7);
                                int addressid = reader.GetInt32(8);
                                byte[] imageBytes = reader.IsDBNull(9) ? null : (byte[])reader["Image"];

                                Student student1 = new Student(fullname, age, speciality, course, groupname, avg, gender, addressid, imageBytes);
                                
                                FullName.Text = student1.FullName;
                                Age.Text = student1.Age.ToString();
                                Specialty.Text = student1.Specialty;
                                Course.Text = student1.Course.ToString();
                                GroupName.Text = student1.GroupName;
                                AverageGrade.Text = student1.AverageGrade.ToString();
                                Gender.Text = student1.Gender;
                                AddressId.Text = student1.AddressId.ToString();
                                
                                if (imageBytes != null)
                                {
                                    using (MemoryStream ms = new MemoryStream(imageBytes))
                                    {
                                        BitmapImage bitmapImage = new BitmapImage();
                                        bitmapImage.BeginInit();
                                        bitmapImage.StreamSource = ms;
                                        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                                        bitmapImage.EndInit();

                                        studentImage.Source = bitmapImage;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    
    private void Update(object sender, RoutedEventArgs e)
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

            if (!int.TryParse(Age.Text, out int age) || age < 0)
            {
                MessageBox.Show("Возраст должен быть числом больше 0");
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

                            using (NpgsqlCommand command =
                                   new NpgsqlCommand("SELECT COUNT(*) FROM Address WHERE AddressId = @addressId",
                                       connection))
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

                                            using (NpgsqlConnection connection1 =
                                                   new NpgsqlConnection(connectionString))
                                            {
                                                connection1.Open();

                                                using (NpgsqlTransaction transaction1 = connection1.BeginTransaction())
                                                {
                                                    string sqlQuery =
                                                        $"UPDATE STUDENT SET FullName = '{student.FullName}', Age = {student.Age}, GroupName = '{student.GroupName}', AverageGrade = {student.AverageGrade}, AddressId = {student.AddressId}, Specialty = '{student.Specialty}', Gender = '{student.Gender}', Course = {student.Course} WHERE StudentId = {id};";

                                                    using (NpgsqlCommand updateCommand = new NpgsqlCommand(
                                                               "UPDATE STUDENT SET FullName = @fullName, Age = @age, GroupName = @groupName, AverageGrade = @averageGrade, AddressId = @addressId, Specialty = @specialty, Gender = @gender, Course = @course, Image = @image WHERE StudentId = @id",
                                                               connection))
                                                    {
                                                        updateCommand.Parameters.AddWithValue("fullName",
                                                            student.FullName);
                                                        updateCommand.Parameters.AddWithValue("age", student.Age);
                                                        updateCommand.Parameters.AddWithValue("groupName",
                                                            student.GroupName);
                                                        updateCommand.Parameters.AddWithValue("averageGrade",
                                                            student.AverageGrade);
                                                        updateCommand.Parameters.AddWithValue("addressId",
                                                            student.AddressId);
                                                        updateCommand.Parameters.AddWithValue("specialty",
                                                            student.Specialty);
                                                        updateCommand.Parameters.AddWithValue("gender", student.Gender);
                                                        updateCommand.Parameters.AddWithValue("course", student.Course);
                                                        updateCommand.Parameters.AddWithValue("image", student.Image);
                                                        updateCommand.Parameters.AddWithValue("id", id);

                                                        int rowsAffected = updateCommand.ExecuteNonQuery();

                                                        if (rowsAffected > 0)
                                                        {
                                                            MessageBox.Show("Студент успешно обновлен");
                                                        }
                                                        else
                                                        {
                                                            MessageBox.Show("Не удалось обновить студента");
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