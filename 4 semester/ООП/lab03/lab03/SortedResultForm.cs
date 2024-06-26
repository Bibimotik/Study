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
    public partial class SortedResultForm : Form
    {
        public List<Student> SortResults { get; set; }
        public List<Student> SearchResultsFromSearchForm { get; set; }
        public SortedResultForm(List<Student> searchResults)
        {
            InitializeComponent();
            SearchResultsFromSearchForm = searchResults;
        }
        private void sortButton_Click(object sender, EventArgs e)
        {
            string sortBy;
            if (sortedComboBox.Text != "курс" && sortedComboBox.Text != "группа" && sortedComboBox.Text != "год" &&
                sortedComboBox.Text != "специальность" && sortedComboBox.Text != "фамилия" &&
                sortedComboBox.Text != "стаж")
            {
                errorLabel.Text = "Неверная сортровка по!";
            }
            else
            {
                errorLabel.Text = "";
                sortBy = sortedComboBox.SelectedItem.ToString();
                SortResults = PerformSort(sortBy);
            }

            sortedGrid.DataSource = SortResults;
        }

        private List<Student> PerformSort(string sortBy)
        {
            List<Student> sortedList = new List<Student>();

            List<Student> dataList = new List<Student>();
            
            if (allData.Checked)
            {
                string json = File.ReadAllText("student.json");
                dataList = JsonConvert.DeserializeObject<List<Student>>(json);
            }
            else if (searchData.Checked)
            {
                dataList = SearchResultsFromSearchForm;
            }
            else
            {
                errorLabel.Text = "Выберите какие данные сортировать!";
            }
            switch (sortBy)
            {
                case "курс":
                    sortedList = dataList.OrderBy(student => student.Curs).ToList();
                    break;
                case "группа":
                    sortedList = dataList.OrderBy(student => student.Group).ToList();
                    break;
                case "год":
                    sortedList = dataList.OrderBy(student => student.BirthDate).ToList();
                    break;
                case "специальность":
                    sortedList = dataList.OrderBy(student => student.Specialinost).ToList();
                    break;
                case "фамилия":
                    sortedList = dataList.OrderBy(student => student.SecondName).ToList();
                    break;
                case "стаж":
                    sortedList = dataList.OrderBy(student =>
                        student.Job != null
                            ? (student.Job.Experience != null ? int.Parse(student.Job.Experience) : 0)
                            : 0).ToList();
                    break;
                default:
                    break;
            }
            

            return sortedList;
        }
    }
}