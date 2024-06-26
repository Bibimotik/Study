using System;
namespace lab01
{
    public class C3
    {
        public string pubStr = "public строка";
        private string prStr= "private строка";
        public string Show_pr_string
        {
            get { return prStr; }
        }
        protected string protected_string = "protected строка";
        public void Method()
        {
            Console.WriteLine("метод из C3");
        }
    }
}