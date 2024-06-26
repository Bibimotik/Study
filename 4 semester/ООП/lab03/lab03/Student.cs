using System.ComponentModel.DataAnnotations;

namespace lab03
{
    public class Student
    {
        [Required(ErrorMessage = "Поле 'Фамилия' обязательно для заполнения.")]
        public string SecondName { get; set; }

        [Required(ErrorMessage = "Поле 'Имя' обязательно для заполнения.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Поле 'Отчество' обязательно для заполнения.")]
        public string ThirdName { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Поле 'Возраст' должно содержать только цифры.")]
        public string Age { get; set; }

        [Required(ErrorMessage = "Поле 'Специальность' обязательно для заполнения.")]
        public string Specialinost { get; set; }

        [Required(ErrorMessage = "Поле 'Дата рождения' обязательно для заполнения.")]
        public string BirthDate { get; set; }

        [Range(1, 4, ErrorMessage = "Поле 'Курс' должно быть в диапазоне от 1 до 4.")]
        public int Curs { get; set; }

        [RegularExpression(@"^\d+$", ErrorMessage = "Поле 'Группа' должно содержать только цифры.")]
        public string Group { get; set; }

        [Range(0.1, 10, ErrorMessage = "Поле 'Оценка' должно быть в диапазоне от 0.1 до 10.")]
        public double Ocenka { get; set; }

        [Required(ErrorMessage = "Поле 'Пол' обязательно для заполнения.")]
        public string Sex { get; set; }
        
        public Adres Adres { get; set; }
        public Job Job { get; set; }
    }
}