using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2
{
    public interface IStaff
    {
        List<JobVacancy> GetJobVacancies();
        List<Employee> GetEmployees();
        List<JobTitle> GetJobTitles();
        int AddJobTitle(JobTitle jobTitle);
        bool DeleteJobTitle(int index);
        void PrintJobVacancies();
        int OpenJobVacancy(JobVacancy vacancy);
        bool CloseJobVacancy(int index);
        Employee Recruit(JobVacancy vacancy, Person person);
        bool Dismiss(int index, string reason);
    }
}
