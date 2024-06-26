using System;
using System.ComponentModel.DataAnnotations;

namespace lab03
{
    public class Job
    {
        [Required(ErrorMessage = "Поле 'Компания' обязательно для заполнения.")]
        public string Company { get; set; }

        [Required(ErrorMessage = "Поле 'Должность' обязательно для заполнения.")]
        public string Post { get; set; }

        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Поле 'Опыт' должно содержать только цифры.")]
        public string Experience { get; set; }

        public Job()
        {

        }

        public Job(string Company, string Post, string Experience)
        {
            this.Company = Company;
            this.Post = Post;
            this.Experience = Experience;
        }

        public override string ToString()
        {
            string str = "";
            str = $"Компания: {Company}\nДолжность: {Post}\nОпыт: {Experience}";
            return str;
        }
    }
}