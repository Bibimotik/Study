namespace Ekzamen
{
    public class NTDate : NDate
    {
        public int Year
        {
            get { return base.Year; }
            set { base.Year = value; }
        }
    
        public NTDate(int day, int month, int year) : base(day, month)
        {
            Year = year;
        }
    
        public override void NextDay()
        {
            Day++;
            if (Day == 1)
            {
                Month++;
                if (Month == 1)
                {
                    Year++;
                }
            }
        }
    
        public static bool operator >(NTDate date1, NTDate date2)
        {
            if (date1.Year > date2.Year)
            {
                return true;
            }
            else if (date1.Year == date2.Year)
            {
                if (date1.Month > date2.Month)
                {
                    return true;
                }
                else if (date1.Month == date2.Month)
                {
                    if (date1.Day > date2.Day)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    
        public static bool operator <(NTDate date1, NTDate date2)
        {
            return !(date1 > date2);
        }
    }
}