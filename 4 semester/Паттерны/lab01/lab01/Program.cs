using System;

namespace lab01
{
    class Program
    {
        static void Main(string[] args)
        {
            C1 first = new C1();
            C1 second = new C1(2005, "HELLO");
            C1 third = new C1(second);
            Console.WriteLine($"{first.pi2}\t {first.ps2}");
            Console.WriteLine($"{second.pi2}\t {second.ps2}");
            Console.WriteLine($"{third.pubIntProperty}\t {third.pubStrProperty}");
            first.PubMethod();

            C2 first_c2 = new C2();
            C2 second_c2 = new C2(3005, "WORLD");
            C2 third_c2 = new C2(second_c2);
            first_c2.I1_method();
            first_c2.I1_property = 123456;
            Console.WriteLine($"Свойство: {first_c2.I1_property} Поле: {first_c2.ps2}");
            
            C4 first_c4 = new C4();
            string pr_str = first_c4.Show_pr_string;
            first_c4.Method();
            Console.WriteLine($"Наследуем от C3 приватную строку {pr_str}");
            Console.ReadKey();
        }
    }
}