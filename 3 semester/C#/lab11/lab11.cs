
namespace Lab_11;

class Program
{
    static void Main(string[] args)
    {
        User user = new("Meow", "Nyan", 20);

        Reflector.GetTypeInfo(user.GetType());
    }
}