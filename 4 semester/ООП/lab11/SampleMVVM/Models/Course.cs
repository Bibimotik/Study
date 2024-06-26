using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace SampleMVVM.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Department { get; set; }
        public string DoctorCategory { get; set; }
        public string Specialization { get; set; }
        public string FIO { get; set; }
        public int Count { get; set; }
        
        public Course()
        {
        }

        public Course(string department, string doctorCategory, string specialization, string fio, int count)
        {
            this.Department = department;
            this.DoctorCategory = doctorCategory;
            this.Specialization = specialization;
            this.FIO = fio;
            this.Count = count;
        }
    }
}