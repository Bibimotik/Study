using System.ComponentModel.DataAnnotations;

namespace lab03
{
    public class Adres
    {
        [Required(ErrorMessage = "Поле 'Город' обязательно для заполнения.")]
        public string City { get; set; }
        [Required(ErrorMessage = "Поле 'Индекс' обязательно для заполнения.")]
        public string Index { get; set; }
        [Required(ErrorMessage = "Поле 'Улица' обязательно для заполнения.")]
        public string Street { get; set; }
        [Required(ErrorMessage = "Поле 'Дом' обязательно для заполнения.")]
        public string House { get; set; }
        [Required(ErrorMessage = "Поле 'Квартира' обязательно для заполнения.")]
        public string Flat { get; set; }
        public Adres()
        {
            
        }
        public Adres(string City,string Index,string Street,string House,string Flat)
        {
            this.City = City;
            this.Index = Index;
            this.Street = Street;
            this.House = House;
            this.Flat = Flat;
        }
        public override string ToString()
        {
            string str = "";
            str = $"Город:{City}\nУлица:{Street}\nИндекс:{Index}\nДом:{House}\nКвартира:{Flat}";
            return str;
        }
    }
}