using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab_2
{
    public class University : Organization, IStaff
    {
        protected List<Faculty> Faculties = new List<Faculty>();

        public University() : base() { }

        public University(University university) : base(university.Name, university.ShortName, university.Address)
        {
            TimeStamp = university.TimeStamp;

            Faculties = new List<Faculty>(university.Faculties);

            Vacancies = new List<JobVacancy>(university.Vacancies);
            Titles = new List<JobTitle>(university.Titles);
            Employees = new List<Employee>(university.Employees);
        }

        public University(string name, string shortName, string address) : base(name, shortName, address) { }

        public List<Faculty> GetFaculties() => Faculties;

        public int AddFaculty(Faculty faculty)
        {
            Faculties.Add(faculty);

            return Faculties.Count;
        }

        public bool DeleteFaculty(int index)
        {
            if (index < 0 || index >= Faculties.Count - 1)
            {
                return false;
            }

            Faculties.RemoveAt(index);

            return true;
        }

        public bool UpdateFaculty(Faculty faculty)
        {
            if (!Faculties.Contains(faculty))
            {
                return false;
            }

            Faculties.Remove(faculty);
            Faculties.Add(faculty);

            return true;
        }

        public bool VerifyFaculty(int index)
        {
            if (index < 0 || index >= Faculties.Count - 1)
            {
                return false;
            }

            return true;
        }

        public override void PrintInfo()
        {
            base.PrintInfo();

            Console.WriteLine($"Faculties: {Faculties.Count}");
        }
    }
}
