namespace lab08
{
    public class Student
    {
        public string FullName { get; set; }
        public int Age { get; set; }
        public string Specialty { get; set; }
        public int Course { get; set; }
        public string GroupName { get; set; }
        public decimal AverageGrade { get; set; }
        public string Gender { get; set; }
        public int AddressId { get; set; }
        public byte[] Image { get; set; }
        
        public Student()
        {
        }
        
        public Student(string fullName, int age, string specialty, int course, string groupName, decimal averageGrade, string gender, int addressId, byte[] image)
        {
            FullName = fullName;
            Age = age;
            Specialty = specialty;
            Course = course;
            GroupName = groupName;
            AverageGrade = averageGrade;
            Gender = gender;
            AddressId = addressId;
            Image = image;
        }
    }
}