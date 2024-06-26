using Lec03LibN;

namespace Lec03LibN
{
    public interface IBonus
    {
        float cost1hour { get; set; }
        float calc(
            float number_hours
            );
    }

    public interface IFactory
    {
        IBonus getA(
            float cost1hour
            );
        IBonus getB(
            float cost1hour,
            float x
            );
        IBonus getC(
            float cost1hour,
            float x,
            float y
            );
    }

    static public partial class Lec03LibN
    {
        static public IFactory getL1()
        {
            return new Level1();
        }
        static public IFactory getL2(
                                    float a
            )
        {
            return new Level2(a);
        }
        static public IFactory getL3(
                                    float a,
                                    float b)
        {
            return new Level3(a, b);
        }
    }

    public class Employee
    {
        public IBonus bonus { get; protected set; }
        public Employee(IBonus bonus)
        {
            this.bonus = bonus;
        }
        public float calcBonus(float number_hours)
        {
            return bonus.calc(number_hours);
        }
    }
    //      Level 1 IFactory
    public class Level1 : IFactory
    {
        public IBonus getA(float cost1hour)
        {
            return new L1A(cost1hour);
        }

        public IBonus getB(float cost1hour, float x)
        {
            return new L1B(cost1hour, x);
        }

        public IBonus getC(float cost1hour, float x, float y)
        {
            return new L1C(cost1hour, x, y);
        }
    }
    //      Level 2 IFactory
    public class Level2 : IFactory
    {
        public float a { get; set; }
        public Level2(float a)
        {
            this.a = a;
        }
        public IBonus getA(float cost1hour)
        {
            return new L2A(cost1hour, a);
        }

        public IBonus getB(float cost1hour, float x)
        {
            return new L2B(cost1hour, x, a);
        }

        public IBonus getC(float cost1hour, float x, float y)
        {
            return new L2C(cost1hour, x, y, a);
        }
    }
    //      Level 3 IFactory
    public class Level3 : IFactory
    {
        public float a { get; set; }
        public float b { get; set; }
        public Level3(float a, float b)
        {
            this.a = a;
            this.b = b;
        }
        public IBonus getA(float cost1hour)
        {
            return new L3A(cost1hour, a, b);
        }

        public IBonus getB(float cost1hour, float x)
        {
            return new L3B(cost1hour, x, a, b);
        }

        public IBonus getC(float cost1hour, float x, float y)
        {
            return new L3C(cost1hour, x, y, a, b);
        }
    }


    #region Level 1 IBonus
    public class L1A : IBonus
    {
        public float cost1hour { get; set; }
        public L1A(float cost1hour)
        {
            this.cost1hour = cost1hour;
        }
        public float calc(float number_hours)
        {
            return number_hours * cost1hour;
        }
    }
    public class L1B : IBonus
    {
        public float cost1hour { get; set; }
        public float x { get; set; }
        public L1B(float cost1hour, float x)
        {
            this.cost1hour = cost1hour;
            this.x = x;
        }

        public float calc(float number_hours)
        {
            return number_hours * cost1hour * x;
        }
    }
    public class L1C : IBonus
    {
        public float cost1hour { get; set; }
        public float x { get; set; }
        public float y { get; set; }
        public L1C(float cost1hour, float x, float y)
        {
            this.cost1hour = cost1hour;
            this.x = x;
            this.y = y;
        }

        public float calc(float number_hours)
        {
            return number_hours * cost1hour * x + y;
        }
    }
    #endregion
    #region Level 2 IBonus
    public class L2A : IBonus
    {
        public float cost1hour { get; set; }
        public float a { get; set; }
        public L2A(float cost1hour, float a)
        {
            this.cost1hour = cost1hour;
            this.a = a;
        }
        public float calc(float number_hours)
        {
            return (number_hours + a) * cost1hour;
        }
    }
    public class L2B : IBonus
    {
        public float cost1hour { get; set; }
        public float x { get; set; }
        public float a { get; set; }
        public L2B(float cost1hour, float x, float a)
        {
            this.cost1hour = cost1hour;
            this.x = x;
            this.a = a;
        }

        public float calc(float number_hours)
        {
            return (number_hours + a) * cost1hour * x;
        }
    }
    public class L2C : IBonus
    {
        public float cost1hour { get; set; }
        public float x { get; set; }
        public float y { get; set; }
        public float a { get; set; }
        public L2C(float cost1hour, float x, float y, float a)
        {
            this.cost1hour = cost1hour;
            this.x = x;
            this.y = y;
            this.a = a;
        }

        public float calc(float number_hours)
        {
            return (number_hours + a) * cost1hour * x + y;
        }
    }
}
#endregion
#region Level 3 IBonus
public class L3A : IBonus
{
    public float cost1hour { get; set; }
    public float a { get; set; }
    public float b { get; set; }
    public L3A(float cost1hour, float a, float b)
    {
        this.cost1hour = cost1hour;
        this.a = a;
        this.b = b;
    }
    public float calc(float number_hours)
    {
        return (number_hours + a) * (cost1hour + b);
    }
}
public class L3B : IBonus
{
    public float cost1hour { get; set; }
    public float x { get; set; }
    public float a { get; set; }
    public float b { get; set; }
    public L3B(float cost1hour, float x, float a, float b)
    {
        this.cost1hour = cost1hour;
        this.x = x;
        this.a = a;
        this.b = b;
    }

    public float calc(float number_hours)
    {
        return (number_hours + a) * (cost1hour + b) * x;
    }
}
public class L3C : IBonus
{
    public float cost1hour { get; set; }
    public float x { get; set; }
    public float y { get; set; }
    public float a { get; set; }
    public float b { get; set; }
    public L3C(float cost1hour, float x, float y, float a, float b)
    {
        this.cost1hour = cost1hour;
        this.x = x;
        this.y = y;
        this.a = a;
        this.b = b;
    }

    public float calc(float number_hours)
    {
        return (number_hours + a) * (cost1hour + b) * x + y;
    }
    #endregion
}