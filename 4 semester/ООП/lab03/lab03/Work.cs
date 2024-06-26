using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace lab03
{
    public partial class Work : Form
    {
        static Job job = new Job();
        
        public Work()
        {
            InitializeComponent();
        }
        
        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(company.Text) || string.IsNullOrWhiteSpace(post.Text) || string.IsNullOrWhiteSpace(experience.Text))
            {
                errorMessage.Text = "Заполните все поля.";
            }
            else if (!Regex.IsMatch(company.Text, @"^[\p{L}0-9\s]+$"))
            {
                errorMessage.Text = "Некорректное название компании. Используйте только буквы, цифры и пробелы.";
            }
            else if (!Regex.IsMatch(post.Text, @"^[\p{L}\s]+$"))
            {
                errorMessage.Text = "Некорректное название должности. Используйте только буквы и пробелы.";
            }
            else if (!Regex.IsMatch(experience.Text, @"^\d+$"))
            {
                errorMessage.Text = "Некорректный стаж. Стаж должен быть положительным целым числом.";
            }
            else
            {
                errorMessage.Text = "";
                job.Company = company.Text;
                job.Post = post.Text;
                job.Experience = experience.Text;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        
        public static Job GetWork()
        {
            return job;
        }
    }
}