namespace Ekzamen
{
    public class NDate
    {
        private int day;
        private int month;

        public int Day
        {
            get { return this.day; }
            set
            {
                if (IsValidDay(value))
                {
                    this.day = value;
                }
                else
                {
                    Console.WriteLine("Не правильный день (1-31)");
                }
            }
        }
        
        private bool IsValidDay(int day)
        {
            if (day >= 1 && day <= 31)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int Month
        {
            get { return this.month; }
            set
            {
                if (IsValidMonth(value))
                {
                    this.month = value;
                }
                else
                {
                    Console.WriteLine("Не правильный месяц (1-12)");
                }
            }
        }

        public int Year { get; set; }

        public NDate(int day, int month)
        {
            this.day = day;
            this.month = month;
        }

        private NDate()
        {
        }

        public virtual void NextDay()
        {
            day++;
            if (day > 31)
            {
                day = 1;
                month++;
                if (month > 12)
                {
                    month = 1;
                }
            }
        }

        private bool IsValidMonth(int month)
        {
            if (month >= 1 && month <= 12)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}