using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2
{
    public class Organization : IStaff
    {
        protected List<JobVacancy> Vacancies = new List<JobVacancy>();
        protected List<JobTitle> Titles = new List<JobTitle>();
        protected List<Employee> Employees = new List<Employee>();

        private int _ID;
        public int ID
        {
            get { return _ID; }
            private set { _ID = value; }
        }
        private string _name;
        public string Name
        { 
            get { return _name; }
            protected set { _name = value; }
        }
        private string _shortName;
        public string ShortName
        {
            get { return _shortName; }
            protected set 
            { 
                _shortName = value;
                ID = value.GetHashCode();
            }
        }
        private string _address;
        public string Address
        {
            get { return _address; }
            protected set { _address = value; }
        }
        private DateTime _timeStamp;
        public DateTime TimeStamp
        {
            get { return _timeStamp; }
            protected set { _timeStamp = value; }
        }

        public Organization()
        {
            Name = "";
            ShortName = "";
            Address = "";
            TimeStamp = DateTime.Now;
        }

        public Organization(Organization organization)
        {
            Name = organization.Name;
            ShortName = organization.ShortName;
            Address = organization.Address;
            TimeStamp = organization.TimeStamp;
        }

        public Organization(string name, string shortName, string address)
        {
            Name = name;
            ShortName = shortName;
            Address = address;
            TimeStamp = DateTime.Now;
        }

        public List<JobVacancy> GetJobVacancies() => Vacancies;
        public List<JobTitle> GetJobTitles() => Titles;
        public List<Employee> GetEmployees() => Employees;

        public void PrintJobVacancies()
        {
            foreach (var vacancy in Vacancies)
            {
                Console.WriteLine(vacancy);
            }
        }

        public int OpenJobVacancy(JobVacancy vacancy)
        {
            Vacancies.Add(vacancy);

            return Vacancies.Count;
        }

        public bool CloseJobVacancy(int index)
        {
            if (index < 0 || index > Vacancies.Count - 1)
            {
                return false;
            }

            Vacancies.RemoveAt(index);

            return true;
        }

        public int AddJobTitle(JobTitle title)
        {
            Titles.Add(title);

            return Titles.Count;
        }

        public bool DeleteJobTitle(int index)
        {
            if (index < 0 || index > Titles.Count - 1)
            {
                return false;
            }

            Titles.RemoveAt(index);

            return true;
        }

        public Employee Recruit(JobVacancy vacancy, Person person)
        {
            Employee employee = new Employee(person, vacancy);

            Employees.Add(employee);

            return employee;
        }

        public bool Dismiss(int index, string reason)
        {
            if (index < 0 || index > Employees.Count - 1)
            {
                return false;
            }

            Employees.RemoveAt(index);
            Console.WriteLine($"Employee dismissed, reason: {reason}");

            return true;
        }

        public virtual void PrintInfo()
        {
            Console.WriteLine($"Name: {Name}({ShortName}), address: {Address}, registration time: {TimeStamp}, ID: {ID}");
            Console.WriteLine($"Employees: {Employees.Count}({Titles.Count} titles), vacancies: {Vacancies.Count}");
        }

        public override int GetHashCode()
        {
            return ShortName.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            if (obj is Organization organiztion)
            {
                return organiztion.ID == ID;
            }

            return false;
        }
    }
}
