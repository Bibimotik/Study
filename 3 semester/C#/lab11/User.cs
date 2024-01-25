namespace Lab_11;

internal class User
{
    public string name { get; set; }
    public string surname { get; set; }
    public int age { get; set; } = -1;

    public User(string name, string surname, int age)
    {
        this.name = name;
        this.surname = surname;
        this.age = age;
    }
}