using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace lab02
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
            else
            {
                errorMessage.Text = "";
                job.Company = company.Text;
                job.Post = post.Text;
                int exValue;
                    
                if (!int.TryParse(experience.Text, out exValue))
                {
                    errorMessage.Text = "Стаж должен быть числом.";
                }
                else
                {
                    errorMessage.Text = "";
                    job.Experience = experience.Text;
                    this.Close();
                }
            }
        }
        public static Job GetWork()
        {
            return job;
        }
    }
}