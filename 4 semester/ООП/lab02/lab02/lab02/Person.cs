using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;


namespace lab02
{
    public partial class Person : Form
    {
        private Student student = new Student();
        
        public Person()
        {
            InitializeComponent();
        }
        
        private void adres_Click(object sender, EventArgs e)
        {
            Live adresForm = new Live();
            adresForm.FormClosing += new FormClosingEventHandler(this.AdresForm_OnClousing);
            adresForm.Show();
        }
        private void job_Click(object sender, EventArgs e)
        {
            Work workForm = new Work();
            workForm.FormClosing += new FormClosingEventHandler(this.WorkForm_OnClousing);
            workForm.Show();
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
                if (spec.Text != "ПОИТ" && spec.Text != "ИСиТ" && spec.Text != "ПОИБМС" && spec.Text != "ДЭВИ")
                {
                    errorMessage.Text = "Выберите специальность из списка.";
                }
                else
                {
                    errorMessage.Text = "";
                    student.Specialinost = spec.Text;
                    student.Curs = curs.Text;
                    int cursValue;
                    
                    if (!int.TryParse(group.Text, out cursValue))
                    {
                        errorMessage.Text = "Группа должна быть числом.";
                    }
                    else
                    {
                        errorMessage.Text = "";
                        student.Group = group.Text;
                        student.Ocenka = mark.Text;
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
                            student.BirtDate = birthday.Value.ToString("yyyy-MM-dd");
                            try
                            {
                                List<Student> st = new List<Student>();

                                JsonSerializerSettings options = new JsonSerializerSettings
                                {
                                    Formatting = Formatting.Indented,
                                    NullValueHandling = NullValueHandling.Ignore
                                };

                                if (File.Exists("student.json"))
                                {
                                    string json = File.ReadAllText("student.json");
                                    st = JsonConvert.DeserializeObject<List<Student>>(json, options);
                                }

                                st.Add(student);

                                string updatedJson = JsonConvert.SerializeObject(st, options);

                                File.WriteAllText("student.json", updatedJson);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                                throw;
                            }
                        }
                        else
                        {
                            errorMessage.Text = "Некорректрная дата.";
                        }
                    }
                }
            }
        }
        private void AdresForm_OnClousing(object sender, EventArgs e)
        {
            student.adres = new Adres();
            student.adres = Live.GetAddress();
            labelAdres.Text= student.adres.ToString();
        }
        private void WorkForm_OnClousing(object sender, EventArgs e)
        {
            student.job = new Job();
            student.job = Work.GetWork();
            labelJob.Text= student.job.ToString();
        }
        private void print_Click_1(object sender, EventArgs e)
        {
            List<Student> p1;

            string json = File.ReadAllText("student.json");
            p1 = JsonConvert.DeserializeObject<List<Student>>(json);

            studentGrid.DataSource = p1;
        }
    }
}