using System;
using System.Linq;

class Program
{
    public partial class Vector
    {
        public int[] elements;
        private int size;
        private int errorCode;
        private static int instanceCount;
        private readonly int ID;
        private const int MAX_SIZE = 100;

        static Vector()
        {
            instanceCount = 0;
        }

        public Vector(int size = 5)
        {
            this.size = size;
            elements = new int[size];
            errorCode = 0;
            instanceCount++;
            ID = GetHashCode();
        }

        private Vector() { }

        public Vector(int[] elements)
        {
            this.elements = elements;
            size = elements.Length;
            errorCode = 0;
            instanceCount++;
            ID = GetHashCode();
        }

        public int this[int index]
        {
            get
            {
                if (index >= 0 && index < size)
                {
                    return elements[index];
                }
                else
                {
                    errorCode = 1;
                    return elements[0];
                }
            }
            set
            {
                if (index >= 0 && index < size)
                {
                    elements[index] = value;
                }
                else
                {
                    errorCode = 1;
                }
            }
        }

        public int Magnitude()
        {
            int sumOfSquares = 0;
            foreach (int element in elements)
            {
                sumOfSquares += element * element;
            }
            return (int)Math.Sqrt(sumOfSquares);
        }

        public void Add(ref Vector vector, int num, out Vector result)
        {
            int[] newElements = new int[vector.Size];
            for (int i = 0; i < vector.Size; i++)
            {
                newElements[i] = vector[i] + num;
            }
            result = new Vector(newElements);
        }

        public Vector Multiply(int num)
        {
            int[] result = new int[size];
            for (int i = 0; i < size; i++)
            {
                result[i] = elements[i] * num;
            }
            return new Vector(result);
        }

        public int ErrorCode
        {
            get { return errorCode; }
            private set { errorCode = value; }
        }

        public int Size
        {
            get { return size; }
            private set { size = value; }
        }

        public static int InstanceCount
        {
            get { return instanceCount; }
        }

        public static Vector CreateVector(int size)
        {
            if (size > MAX_SIZE)
            {
                throw new ArgumentException("Размер больше положенного.");
            }
            return new Vector(size);
        }

        public int GetID()
        {
            return ID;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Vector other = (Vector)obj;

            if (size != other.size)
            {
                return false;
            }

            for (int i = 0; i < size; i++)
            {
                if (elements[i] != other.elements[i])
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * size.GetHashCode();
            for (int i = 0; i < size; i++)
            {
                hash = hash - elements[i].GetHashCode();
            }
            return hash;
        }

        public override string ToString()
        {
            string result = "Vector [";
            for (int i = 0; i < size; i++)
            {
                result += elements[i];
                if (i < size - 1)
                {
                    result += ", ";
                }
            }
            result += "]";
            return result;
        }
    }

    static void Main()
    {
        string[] months = { "June", "July", "May", "December", "January", "August", "February", "September", "November", "April", "October", "March" };

        int n = 4;
        var monthsWithLengthN = months.Where(month => month.Length == n);

        Console.WriteLine("Месяцы с длиной строки равной {0}:", n);
        foreach (var month in monthsWithLengthN)
        {
            Console.WriteLine(month);
        }

        Console.WriteLine();

        var summerAndWinterMonths = months.Where(month => month == "June" || month == "July" || month == "August" || month == "December" || month == "January" || month == "February");

        Console.WriteLine("Летние и зимние месяцы:");
        foreach (var month in summerAndWinterMonths)
        {
            Console.WriteLine(month);
        }

        Console.WriteLine();

        var sortedMonths = months.OrderBy(month => month);

        Console.WriteLine("Месяцы в алфавитном порядке:");
        foreach (var month in sortedMonths)
        {
            Console.WriteLine(month);
        }

        Console.WriteLine();

        var monthsWithUAndLength4Plus = months.Where(month => month.Contains("u") && month.Length >= 4);

        Console.WriteLine("Месяцы, содержащие букву 'u' и имеющие длину имени не менее 4-х:");
        foreach (var month in monthsWithUAndLength4Plus)
        {
            Console.WriteLine(month);
        }
        Console.WriteLine("-----------------------------------------");

        List<Vector> collection = new List<Vector>();

        for (int i = -2; i < 10; i++)
        {
            int[] elements = new int[] { i, i + 1, i + 2 };
            Vector vector = new Vector(elements);
            collection.Add(vector);
        }
        int[] elements3 = { 1, 2, 3 };
        Vector vector3 = new Vector(elements3);

        int[] elements5 = { 10, 20, 30, 40, 50 };
        Vector vector5 = new Vector(elements5);

        int[] elements7 = { 100, 200, 300, 400, 500, 600, 700 };
        Vector vector7 = new Vector(elements7);

        collection.Add(vector3);
        collection.Add(vector5);
        collection.Add(vector7);

        foreach (var vector in collection)
        {
            Console.WriteLine(vector.ToString());
        }
        Console.WriteLine("-----------------------------------------");

        int vectorsWithZeroCount = collection.Count(vector => vector.elements.Contains(0));
        Console.WriteLine("Количество векторов с 0: " + vectorsWithZeroCount);

        var vectorsWithSmallestMagnitude = collection.OrderBy(vector => vector.elements.Select(Math.Abs).Sum()).Take(1);
        Console.WriteLine("Список векторов с наименьшим модулем:");
        foreach (var vector in vectorsWithSmallestMagnitude)
        {
            Console.WriteLine(vector.ToString());
        }

        var vectorsLength3 = collection.Where(vector => vector.Size == 3);
        Console.WriteLine("Векторы длиной 3:");
        foreach (var vector in vectorsLength3)
        {
            Console.WriteLine(vector.ToString());
        }

        var vectorsLength5 = collection.Where(vector => vector.Size == 5);
        Console.WriteLine("Векторы длиной 5:");
        foreach (var vector in vectorsLength5)
        {
            Console.WriteLine(vector.ToString());
        }

        var vectorsLength7 = collection.Where(vector => vector.Size == 7);
        Console.WriteLine("Векторы длиной 7:");
        foreach (var vector in vectorsLength7)
        {
            Console.WriteLine(vector.ToString());
        }

        var maxVector = collection.OrderByDescending(vector => vector.elements.Max()).First();
        Console.WriteLine("Максимальный вектор: " + maxVector);

        var firstVectorWithNegativeValue = collection.FirstOrDefault(vector => vector.elements.Any(element => element < 0));
        Console.WriteLine("Первый массив с отрицательным значением: " + firstVectorWithNegativeValue);

        var sortedVectors = collection.OrderByDescending(vector => vector.Size);
        Console.WriteLine("Упорядоченные векторы по убыванию размера:");
        foreach (var vector in sortedVectors)
        {
            Console.WriteLine(vector.ToString());
        }
        Console.WriteLine("----------------------------------------");

        var result = collection
            .Where(vector => vector.Size > 3)
            .OrderByDescending(vector => vector.Size)
            .GroupBy(vector => vector.Size % 2 == 0)
            .Select(group => new { IsEvenSize = group.Key, Vectors = group.ToList() })
            .Where(group => group.Vectors.Count > 1)
            .Select(group => new { group.IsEvenSize, TotalMagnitude = group.Vectors.Sum(vector => vector.Magnitude()) })
            .OrderByDescending(group => group.TotalMagnitude)
            .Take(2);

        Console.WriteLine("Результат запроса:");
        foreach (var item in result)
        {
            Console.WriteLine($"Четный размер: {item.IsEvenSize}, Суммарная величина: {item.TotalMagnitude}");
        }

        var result1 = collection
        .Join(
            monthsWithLengthN,
            vector => vector.Size,
            month => month.Length,
            (vector, month) => new { Vector = vector, Month = month }
        );
    }
}