using System;
namespace lab01
{
    public class C4 : C3, I2
    {
        public string C4_string = "public строка C4";

        public void I2_method()
        {
            Console.WriteLine("метод I2 в C4");
        }
    }
}