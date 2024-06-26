using System;
namespace lab01
{
    public class C1
    {
        private const int pc1 = 1;
        public const int pc2 = 2;
        protected const int pc3 = 3;
        private int pi1 = 4;
        private string ps1 = "Приватная строка";
        public int pi2 = 5;
        public string ps2 = "Публичная строка";
        protected int pi3 = 6;
        protected string ps3 = "Защищённая строка";
        private int prIntProperty { get; set; }
        public int pubIntProperty { get; set; }
        public string pubStrProperty { get; set; }
        protected int protIntProperty { get; set; }
        public C1()
        {
            Console.WriteLine("Конструктор по умолчанию");
        }
        public C1(C1 c)
        {
            Console.WriteLine("Копирующий конструктор");
            pubIntProperty = c.pi2;
            pubStrProperty = c.ps2;
        }
        public C1(int pi2, string ps2)
        {
            Console.WriteLine("Конструктор с параметрами");
            this.pi2 = pi2;
            this.ps2 = ps2;
        }
        private void PrMethod()
        {
            Console.WriteLine("Приватный метод.");
        }
        public void PubMethod()
        {
            Console.WriteLine("Публичный метод.");
            PrMethod();
        }
        protected void ProtMethod()
        {
            Console.WriteLine("Защищенный метод.");
        }
    }
}