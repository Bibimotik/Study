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
    public partial class SearchResultForm : Form
    {
        public List<Student> SearchResults { get; set; }
        public SearchResultForm()
        {
            InitializeComponent();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            /*string searchFio = fio.Text;
            string searchSpec = spec.Text;
            string searchCours = cours.Text;
            string searchBall = ball.Text;*/

            SearchResults = PerformSearch();
            
            searchGrid.DataSource = SearchResults;
        }

        private List<Student> PerformSearch()
        {
            List<Student> searchResults = new List<Student>();

            List<Student> dataList;

            string json = File.ReadAllText("student.json");
            dataList = JsonConvert.DeserializeObject<List<Student>>(json);

            string fioSearchText = this.fio.Text;
            string specSearchText = this.spec.Text;
            string coursSearchText = this.cours.Text;
            string ballSearchText = this.ball.Text;

            searchResults = dataList.Where(data =>
                    (string.IsNullOrEmpty(fioSearchText) || IsFioMatch(data, fioSearchText)) &&
                    (string.IsNullOrEmpty(specSearchText) || IsSpecMatch(data, specSearchText)) &&
                    (string.IsNullOrEmpty(coursSearchText) || IsCoursMatch(data, coursSearchText)) &&
                    (string.IsNullOrEmpty(ballSearchText) || IsBallMatch(data, ballSearchText)))
                .ToList();

            return searchResults;
        }

        private bool IsFioMatch(Student student, string searchText)
        {
            return Regex.IsMatch(student.SecondName, searchText, RegexOptions.IgnoreCase) ||
                   Regex.IsMatch(student.FirstName, searchText, RegexOptions.IgnoreCase) ||
                   Regex.IsMatch(student.ThirdName, searchText, RegexOptions.IgnoreCase);
        }

        private bool IsSpecMatch(Student student, string searchText)
        {
            return Regex.IsMatch(student.Specialinost, searchText, RegexOptions.IgnoreCase);
        }

        private bool IsCoursMatch(Student student, string searchText)
        {
            int cours;
            return int.TryParse(searchText, out cours) && student.Curs == cours;
        }

        private bool IsBallMatch(Student student, string searchText)
        {
            double ball;
            return double.TryParse(searchText, out ball) && student.Ocenka > ball;
        }
    }
}