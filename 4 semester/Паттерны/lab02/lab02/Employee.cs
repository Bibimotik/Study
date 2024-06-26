namespace Lab_2;

public class Employee : StaffObject
{
    public JobTitle Title;

    public Employee(Person person, JobVacancy vacancy) : base(person.Name)
    {
        Title = new JobTitle(vacancy.Name);
    }
}