using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace lab03
{
    public partial class Person : Form
    {
        private Student student = new Student();

        public List<Student> search;
        public List<Student> sort;

        private System.Windows.Forms.StatusBar status = new System.Windows.Forms.StatusBar();
        private Timer timer;
        private const int timerInterval = 1000;

        private string lastAction = "";

        public Person()
        {
            InitializeComponent();
            InitializeStatusBar();
            InitializeTimer();
        }

        private void InitializeStatusBar()
        {
            this.status = new System.Windows.Forms.StatusBar();
            this.Controls.Add(this.status);
            this.status.Dock = DockStyle.Bottom;
        }

        private void InitializeTimer()
        {
            this.timer = new Timer();
            this.timer.Interval = timerInterval;
            this.timer.Tick += TimerTick;
            this.timer.Start();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            UpdateStatusBarTime();
        }

        private void UpdateStatusBarTime()
        {
            string json = File.ReadAllText("student.json");
            List<Student> students = JsonConvert.DeserializeObject<List<Student>>(json);
            if (string.IsNullOrEmpty(json))
            {
                int count = 0;
                this.status.Text = $"Объектов: {count}  |  Дата и время: {DateTime.Now.ToString()} | Последнее действие: {lastAction}";
            }
            else
            {
                int count = students.Count;
                this.status.Text = $"Объектов: {count}  |  Дата и время: {DateTime.Now.ToString()} | Последнее действие: {lastAction}";
            }
        }
        public void ClearJobLabel()
        {
            labelJob.Text = "";
        }
        private void adres_Click(object sender, EventArgs e)
        {
            Live adresForm = new Live();
            DialogResult result = adresForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                student.Adres = new Adres();
                student.Adres = Live.GetAdres();
                labelAdres.Text = student.Adres.ToString();
            }
            lastAction = "Добавление адреса!";
        }
        private void job_Click(object sender, EventArgs e)
        {
            Work workForm = new Work();
            DialogResult result = workForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                student.Job = new Job();
                student.Job = Work.GetWork();
                labelJob.Text = student.Job.ToString();
            }
            lastAction = "Добавление работы!";
        }
        private void Birthday_ValueChanged(object sender, EventArgs e)
        {
            DateTime currentDate = DateTime.Now;
            DateTime birthDate = this.birthday.Value;

            int age = currentDate.Year - birthDate.Year;

            if (currentDate < birthDate.AddYears(age))
            {
                age--;
            }

            this.age.Value = age;
        }
        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(firstName.Text) || string.IsNullOrWhiteSpace(secondName.Text) || string.IsNullOrWhiteSpace(patronymic.Text) || string.IsNullOrWhiteSpace(birthday.Text) || string.IsNullOrWhiteSpace(spec.Text) || string.IsNullOrWhiteSpace(curs.Text) || string.IsNullOrWhiteSpace(group.Text) || string.IsNullOrWhiteSpace(mark.Text))
            {
                errorMessage.Text = "Заполните все поля.";
            }
            else
            {
                errorMessage.Text = "";
                student.SecondName = secondName.Text;
                student.ThirdName = patronymic.Text;
                student.FirstName = firstName.Text;
                student.Age = birthday.Text;

                if (!Regex.IsMatch(spec.Text, @"^(ПОИТ|ИСиТ|ПОИБМС|ДЭВИ)$"))
                {
                    errorMessage.Text = "Выберите специальность из списка.";
                }
                else
                {
                    errorMessage.Text = "";
                    student.Specialinost = spec.Text;

                    int cursValue;
                    if (!int.TryParse(curs.Text, out cursValue))
                    {
                        errorMessage.Text = "Курс должен быть числом.";
                    }
                    else
                    {
                        errorMessage.Text = "";
                        student.Curs = cursValue;

                        if (!int.TryParse(group.Text, out cursValue))
                        {
                            errorMessage.Text = "Группа должна быть числом.";
                        }
                        else
                        {
                            errorMessage.Text = "";
                            student.Group = group.Text;

                            student.Ocenka = (double)mark.Value;

                            if (man.Checked)
                            {
                                student.Sex = "Мужской";
                            }
                            else if (woman.Checked)
                            {
                                student.Sex = "Женский";
                            }

                            DateTime currentDate = DateTime.Now;
                            DateTime birthDate = this.birthday.Value;

                            int age = currentDate.Year - birthDate.Year;
                            if (age >= 0)
                            {
                                errorMessage.Text = "";
                                student.BirthDate = birthday.Value.ToString("yyyy-MM-dd");
                                try
                                {
                                    List<Student> saveStudent = new List<Student>();

                                    JsonSerializerSettings options = new JsonSerializerSettings
                                    {
                                        Formatting = Formatting.Indented,
                                        NullValueHandling = NullValueHandling.Ignore
                                    };

                                    if (File.Exists("student.json"))
                                    {
                                        string json = File.ReadAllText("student.json");
                                        saveStudent = JsonConvert.DeserializeObject<List<Student>>(json, options);
                                    }

                                    saveStudent.Add(student);

                                    string updatedJson = JsonConvert.SerializeObject(saveStudent, options);

                                    File.WriteAllText("student.json", updatedJson);
                                    lastAction = "Сохранение данных в файл!";
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                    throw;
                                }
                            }
                            else
                            {
                                errorMessage.Text = "Некорректная дата.";
                            }
                        }
                    }
                }
            }
        }
        private void print_Click_1(object sender, EventArgs e)
        {
            List<Student> p1;

            string json = File.ReadAllText("student.json");
            p1 = JsonConvert.DeserializeObject<List<Student>>(json);

            studentGrid.DataSource = p1;
            lastAction = "Вывод данных!";
        }
        
        //lab03
        // Обработчик события для кнопки "Поиск" в меню
        private void SearchButton_Click(object sender, EventArgs e)
        {
            SearchResultForm searchResultForm = new SearchResultForm();
            searchResultForm.ShowDialog();
            search = searchResultForm.SearchResults;
            lastAction = "Поиск студентов!";
        }
        
        // Обработчик события для кнопки "Сортировка" в меню
        private void sortButton_Click(object sender, EventArgs e)
        {
            SortedResultForm sortedResultForm = new SortedResultForm(search);
            sortedResultForm.ShowDialog();
            sort = sortedResultForm.SortResults;
            lastAction = "Сортировка студентов!";
        }
        
        // Обработчик события для кнопки "Сохранить" в меню
        private void saveMenu_Click(object sender, EventArgs e)
        {
            List<Student> saveListSearch = new List<Student>(search);
            List<Student> saveListSort = new List<Student>(sort);

            SaveListToJson(saveListSearch, "search"); 
            SaveListToJson(saveListSort, "sort");

            lastAction = "Созранение результатов поиска и сортировки!";
        }

        private void SaveListToJson(List<Student> saveList, string listName)
        {
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            string fileName = $"student_{listName}_{timestamp}.json";

            string json = JsonConvert.SerializeObject(saveList, Formatting.Indented);
            File.WriteAllText(fileName, json);
        }
        // Обработчик события для кнопки "О программе" в меню
        private void programMenu_Click(object sender, EventArgs e)
        {
            string version = "1.2";
            string developerName = "Банкузов Михаил Олегович";
    
            string message = $"Версия программы: {version}\nРазработчик: {developerName}";

            MessageBox.Show(message, "О программе", MessageBoxButtons.OK, MessageBoxIcon.Information);

            lastAction = "Отображение оинформации о программе!";
        }
    }
}