using System;

public partial class Vector
{
    private int[] elements;
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

class Program
{
    static void Main(string[] args)
    {
        //задание варианта
        Vector[] vectors = new Vector[]
        {
            new Vector(new int[] { 1, 2, 3 }),
            new Vector(new int[] { 1, 0, 1 }),
            new Vector(new int[] { -1, 0, 2 }),
            new Vector(new int[] { 1, 1, 1 }),
            new Vector(new int[] { -3, -2, -1 })
        };

        Console.WriteLine("Список векторов, содержащих 0:");
        foreach (Vector vector in vectors)
        {
            bool containsZero = false;
            for (int i = 0; i < vector.Size; i++)
            {
                if (vector[i] == 0)
                {
                    containsZero = true;
                    break;
                }
            }
            if (containsZero)
            {
                for (int i = 0; i < vector.Size; i++)
                {
                    Console.Write(vector[i] + " ");
                }
                Console.WriteLine();
            }
        }

        Vector minMagnitudeVector = vectors[0];
        double minMagnitude = GetMagnitude(vectors[0]);
        for (int i = 1; i < vectors.Length; i++)
        {
            double magnitude = GetMagnitude(vectors[i]);
            if (magnitude < minMagnitude)
            {
                minMagnitude = magnitude;
                minMagnitudeVector = vectors[i];
            }
        }

        Console.WriteLine("Вектор с наименьшим модулем:");
        for (int i = 0; i < minMagnitudeVector.Size; i++)
        {
            Console.Write(minMagnitudeVector[i] + " ");
        }
        Console.WriteLine();

        Vector vector1 = new Vector();
        Vector vector2 = new Vector(new int[] { 1, 2, 3 });
        Vector vector3 = Vector.CreateVector(5);

        int element = vector2[1];
        Console.WriteLine("Элемент с индексом 1: " + element);

        vector3[2] = 10;

        Vector result;
        vector2.Add(ref vector2, 5, out result);
        Console.WriteLine("Сложенный вектор: " + result);

        Vector multiplied = vector2.Multiply(2);
        Console.WriteLine("Умноженный вектор: " + multiplied);

        Console.WriteLine("Размер Vector1: " + vector1.Size);
        Console.WriteLine("Код ошибки Vector2: " + vector2.ErrorCode);

        bool equals = vector2.Equals(vector3);
        Console.WriteLine("Vector2 равен Vector3: " + equals);

        Type type = vector1.GetType();
        Console.WriteLine("Тип vector1: " + type);

        int id = vector1.GetID();
        Console.WriteLine("ID vector1: " + id);

        int instanceCount = Vector.InstanceCount;
        Console.WriteLine("Создано элементов: " + instanceCount);

        Vector.PrintClassInfo();
    }

    static double GetMagnitude(Vector vector)
    {
        double sumOfSquares = 0;
        for (int i = 0; i < vector.Size; i++)
        {
            sumOfSquares += Math.Pow(vector[i], 2);
        }
        return Math.Sqrt(sumOfSquares);
    }
}