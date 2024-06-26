namespace lab02
{
    public class Adres
    {
        public string City { get; set; }
        public string Index { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
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