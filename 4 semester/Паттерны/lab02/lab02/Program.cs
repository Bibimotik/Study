using System;
namespace Lab_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Organization organization = new Organization();
            University university = new University();
            
            Faculty faculty = new Faculty("Информационные Технологии", "ИТ", "Свердлова 13а");
            
            faculty.PrintInfo();
            Console.WriteLine(faculty.Name);
            Console.WriteLine(faculty.ID);
            Console.WriteLine(faculty);
            Console.WriteLine(faculty.GetHashCode());
            Console.WriteLine(faculty.Equals(faculty));
            Console.WriteLine();
            
            faculty.OpenJobVacancy(new JobVacancy("ЗамДекана"));
            faculty.OpenJobVacancy(new JobVacancy("Преподаватель-стажер"));
            faculty.PrintJobVacancies();
            Console.WriteLine();
            faculty.CloseJobVacancy(0);
            faculty.PrintJobVacancies();
            Console.WriteLine();
            
            faculty.AddJobTitle(new JobTitle("Преподаватель"));
            Console.WriteLine(faculty.GetJobTitles()[0]);
            faculty.Recruit(faculty.GetJobVacancies()[0], new Person("Банкузов Михаил"));
            Console.WriteLine(faculty.GetEmployees()[0]);
            faculty.Dismiss(0, "Сокращение");
            Console.WriteLine(faculty.GetEmployees().Count);
            Console.WriteLine();
            
            faculty.DeleteJobTitle(0);
            Console.WriteLine(faculty.GetJobTitles().Count);
        }
    }
}
