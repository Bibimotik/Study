using System;
namespace lab01
{
    public class C2 : C1, I1
    {
        public event EventHandler I1_event;
        public int I1_property { get; set; }

        public int this[int index] { get { return pi3; } set { pi3 = 7; } }

        public C2()
        {
            Console.WriteLine("Конструктор по умолчанию");
        }
        public C2(C2 c)
        {
            Console.WriteLine("Конструктор копирования");
            pubIntProperty = c.pi2;
            pubStrProperty = c.ps2;
        }
        public C2(int pi2, string ps2)
        {
            Console.WriteLine("Конструктор с параметрами");
            this.pi2 = pi2;
            this.ps2 = ps2;
        }

        public void I1_method()
        {
            Console.WriteLine("метож I1 в C2");
        }
    }
}