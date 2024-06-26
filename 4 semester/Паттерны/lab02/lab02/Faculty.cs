using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2
{
    public class Faculty : Organization, IStaff
    {
        protected List<Department> Departments = new List<Department>();

        public Faculty() : base() { }

        public Faculty(Faculty faculty) : base(faculty.Name, faculty.ShortName, faculty.Address)
        {
            TimeStamp = faculty.TimeStamp;

            Departments = new List<Department>(faculty.Departments);

            Vacancies = new List<JobVacancy>(faculty.Vacancies);
            Titles = new List<JobTitle>(faculty.Titles);
            Employees = new List<Employee>(faculty.Employees);
        }

        public Faculty(string name, string shortName, string address) : base(name, shortName, address) { }

        public List<Department> GetDepartments() => Departments;

        public int DeleteDepartment(Department department)
        {
            Departments.Add(department);

            return Departments.Count;
        }

        public bool DeleteDepartment(int index)
        {
            if (index < 0 || index >= Departments.Count - 1)
            {
                return false;
            }

            Departments.RemoveAt(index);

            return true;
        }

        public bool UpdateDepartment(Department department)
        {
            if (!Departments.Contains(department))
            {
                return false;
            }

            Departments.Remove(department);
            Departments.Add(department);

            return true;
        }

        public bool VerifyDepartment(int index)
        {
            if (index < 0 || index >= Departments.Count - 1)
            {
                return false;
            }

            return true;
        }

        public override void PrintInfo()
        {
            base.PrintInfo();

            Console.WriteLine($"Departments: {Departments.Count}");
        }
    }
}
