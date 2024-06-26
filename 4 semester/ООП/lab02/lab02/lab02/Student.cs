namespace lab02
{
    public class Student
    {
        public string SecondName { get; set; }
        public string FirstName { get; set; }
        public string ThirdName { get; set; }
        public string Age { get; set; }
        public string Specialinost{ get; set; }
        public string BirtDate { get; set; }
        public string Curs { get; set; }
        public string Group { get; set; }
        public string Ocenka { get; set; }

        public string Sex { get; set; }
        
        public Adres adres { get; set; }
        public Job job { get; set; }
    }
}