using System;

public static class StatisticOperation
{
    public static int Sum(Array array)
    {
        int sum = 0;
        for (int i = 0; i < array.Length; i++)
        {
            sum += array[i];
        }
        return sum;
    }

    public static int Difference(Array array)
    {
        int max = int.MinValue;
        int min = int.MaxValue;

        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] > max)
            {
                max = array[i];
            }

            if (array[i] < min)
            {
                min = array[i];
            }
        }

        return max - min;
    }

    public static int CountElements(Array array)
    {
        return array.Length;
    }

    public static bool ContainsCharacter(this string str, char character)
    {
        return str.IndexOf(character) >= 0;
    }

    public static void RemoveNegativeElements(this Array array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] < 0)
            {
                array[i] = 0;
            }
        }
    }
}

public class Array
{
    private int[] array;

    public int this[int index]
    {
        get { return array[index]; }
        set { array[index] = value; }
    }

    public int Length
    {
        get { return array.Length; }
    }

    public Array(int[] array)
    {
        this.array = array;
    }

    public static Array operator *(Array array1, Array array2)
    {
        if (array1.Length != array2.Length)
        {
            throw new ArgumentException("Массивы должны иметь одинаковую длину!");
        }

        int[] result = new int[array1.Length];
        for (int i = 0; i < array1.Length; i++)
        {
            result[i] = array1[i] * array2[i];
        }

        return new Array(result);
    }

    public static bool operator true(Array array)
    {
        foreach (int element in array.array)
        {
            if (element < 0)
            {
                return false;
            }
        }

        return true;
    }

    public static bool operator false(Array array)
    {
        foreach (int element in array.array)
        {
            if (element >= 0)
            {
                return false;
            }
        }

        return true;
    }

    public static explicit operator int(Array array)
    {
        return array.Length;
    }

    public static bool operator ==(Array array1, Array array2)
    {
        if (ReferenceEquals(array1, array2))
        {
            return true;
        }

        if (ReferenceEquals(array1, null) || ReferenceEquals(array2, null))
        {
            return false;
        }

        if (array1.Length != array2.Length)
        {
            return false;
        }

        for (int i = 0; i < array1.Length; i++)
        {
            if (array1[i] != array2[i])
            {
                return false;
            }
        }

        return true;
    }

    public static bool operator !=(Array array1, Array array2)
    {
        return !(array1 == array2);
    }

    public static bool operator <(Array array1, Array array2)
    {
        if (array1.Length < array2.Length)
        {
            return true;
        }

        return false;
    }

    public static bool operator >(Array array1, Array array2)
    {
        if (array1.Length > array2.Length)
        {
            return true;
        }

        return false;
    }


    public class Production
    {
        public int Id { get; set; }
        public string OrganizationName { get; set; }
    }

    public class Developer
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Department { get; set; }
    }
}

class Program
{
    static void Main(string[] args)
    {
        int[] values1 = { 1, 2, 3, -1 };
        int[] values2 = { 4, 5, 6, -1 };
        int[] values3 = { 4, 5, 6 };

        Array array1 = new Array(values1);
        Array array2 = new Array(values2);
        Array array3 = new Array(values3);

        Array resultArray = array1 * array2;
        Console.WriteLine("Результат умножения:");
        for (int i = 0; i < resultArray.Length; i++)
        {
            Console.Write(" " + resultArray[i]);
        }
        Console.WriteLine();

        bool isEqual = array1 == array2;
        Console.WriteLine("Массивы равны: " + isEqual);

        bool isNotEqual = array1 != array2;
        Console.WriteLine("Массивы не равны: " + isNotEqual);

        bool isLessThan = array1 < array3;
        Console.WriteLine("Первый массив меньше третьего: " + isLessThan);

        bool isGreaterThan = array1 > array3;
        Console.WriteLine("Первый массив больше третьего: " + isGreaterThan);

        int arrayLength = (int)array1;
        Console.WriteLine("Длина массива: " + arrayLength);

        bool isPositive = true;
        if (array1)
        {
            Console.WriteLine("Все элементы массива положительны");
        }
        else
        {
            Console.WriteLine("Есть отрицательные элементы в массиве");
            isPositive = false;
        }

        string str = "Hello, world!";
        bool containsCharacter = str.ContainsCharacter('o');
        Console.WriteLine("Строка содержит символ 'o': " + containsCharacter);

        array1.RemoveNegativeElements();
        Console.WriteLine("Массив 1 после удаления отрицательных элементов:");
        for (int i = 0; i < array1.Length; i++)
        {
            Console.Write(" " + array1[i]);
        }
        Console.WriteLine();

        Array.Production product = new Array.Production()
        {
            Id = 1,
            OrganizationName = "ABC Company"
        };

        Array.Developer developer = new Array.Developer()
        {
            Id = 1,
            FullName = "John Doe",
            Department = "Software Development"
        };

        Console.WriteLine("Product: Id = {0}, Organization Name = {1}", product.Id, product.OrganizationName);
        Console.WriteLine("Developer: Id = {0}, Full Name = {1}, Department = {2}", developer.Id, developer.FullName, developer.Department);
    }
}