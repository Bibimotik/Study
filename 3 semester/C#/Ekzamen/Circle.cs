namespace Ekzamen
{
    public class Circle : IComparable
    {
        public int Radius { get; set; }
        public string Color { get; set; }

        public Circle(int radius, string color)
        {
            this.Radius = radius;
            this.Color = color;
        }

        public double Area(int radius)
        {
            double area = Math.Pow(radius, 2) * Math.PI; 
            return area;
        }
        
        public override string ToString()
        {
            return $"Круг с радиусом {Radius} и цветом {Color}";
        }

        public bool Equals(Circle circle)
        {
            if (circle == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        
        public int CompareTo(object obj)
        {
            if (obj == null || GetType()!= obj.GetType())
            {
                return 1;
            }

            Circle other = (Circle)obj;
            return Radius.CompareTo(other.Radius);
        }

        public int CompareTo(Circle circle)
        {
            return Radius.CompareTo(circle.Radius);
        }
    }
}