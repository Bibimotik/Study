using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public abstract class Software
{
    public string Name { get; set; }

    public abstract void DoPlay();

    public Software(string name)
    {
        Name = name;
    }

    public override string ToString()
    {
        return $"Software: {Name}";
    }
}

public interface IPlayable
{
    void Play();
}

public class OperationSet : Software
{
    public string Type;

    public OperationSet(string name, string type) : base(name)
    {
        Type = type;
    }

    public override void DoPlay()
    {
        Console.WriteLine($"Playing operation set: {Name}");
    }

    public override string ToString()
    {
        return $"OperationSet: {Name}";
    }
}

public class TextProcessor : Software
{
    public string Version;
    public string Developer;

    public TextProcessor(string name, string version, string developer) : base(name)
    {
        Developer = developer;
        Version = version;
    }

    public override void DoPlay()
    {
        Console.WriteLine($"Playing text processor: {Name}");
    }

    public override string ToString()
    {
        return $"TextProcessor: {Name}";
    }
}

public class Virus : TextProcessor
{
    public string Type;
    public string Version;
    public string Profilactin;

    public Virus(string name, string type, string version, string profilactin) : base(name, "", "")
    {
        Type = type;
        Version = version;
        Profilactin = profilactin;
    }

    public override void DoPlay()
    {
        Console.WriteLine($"Playing virus: {Name}");
    }

    public override string ToString()
    {
        return $"Virus: {Name}";
    }
}

public class Conficker : Virus
{
    public Conficker(string name) : base(name, "", "", "")
    {
    }

    public override void DoPlay()
    {
        Console.WriteLine($"Playing Conficker virus: {Name}");
    }

    public override string ToString()
    {
        return $"Conficker: {Name}";
    }
}

public class Word : TextProcessor
{
    public string Version;

    public Word(string name, string version) : base(name, "", "")
    {
        Version = version;
    }

    public override void DoPlay()
    {
        Console.WriteLine($"Playing Word text processor: {Name}");
    }

    public override string ToString()
    {
        return $"Word: {Name}";
    }
}

public class Toy : Software, IPlayable
{
    public Toy(string name) : base(name)
    {
    }

    public void Play()
    {
        Console.WriteLine($"Playing with {Name}");
    }

    public override void DoPlay()
    {
        Console.WriteLine($"Playing with toy: {Name}");
    }

    public override string ToString()
    {
        return $"Toy: {Name}";
    }
}

public class Minesweeper : Toy, IPlayable
{
    public Minesweeper(string name) : base(name)
    {
    }

    public override void DoPlay()
    {
        Console.WriteLine($"Playing Minesweeper toy: {Name}");
    }

    public override string ToString()
    {
        return $"Minesweeper: {Name}";
    }
}

public interface IContainer<T>
{
    void Add(T item);
    void Remove(T item);
    void View();
    List<T> FindAll(Predicate<T> predicate);
}

public class CollectionType<T> : IContainer<T>
{
    public List<T> collection;

    public CollectionType(List<T> collection)
    {
        this.collection = collection;
    }

    public CollectionType()
    {
        collection = new List<T>();
    }

    public void Add(T item)
    {
        try
        {
            collection.Add(item);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка добавления элемента: " + ex.Message);
        }
        finally
        {
            Console.WriteLine("Элемент был добавлен.");
        }
    }

    public void Remove(T item)
    {
        try
        {
            collection.Remove(item);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка удаления элемента: " + ex.Message);
        }
        finally
        {
            Console.WriteLine("Элемент был удален.");
        }
    }

    public void View()
    {
        Console.WriteLine("Элементы коллекции:");
        foreach (T item in collection)
        {
            Console.WriteLine(item);
        }
    }

    public List<T> FindAll(Predicate<T> predicate)
    {
        return collection.FindAll(predicate);
    }   
}

class Program
{
    static void Main(string[] args)
    {
        List<int> a = new List<int>() { 1, 2, 3, 3, 4, 5 };
        CollectionType<int> collectionType = new CollectionType<int>(a);
        collectionType.Add(1);
        collectionType.Add(2);
        collectionType.Add(3);
        collectionType.Add(4);
        collectionType.View();

        List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        List<int> evenNumbers = numbers.FindAll(IsEven);
        List<int> oddNumbers = numbers.FindAll(IsOdd);

        Console.WriteLine("Even numbers:");
        foreach (int number in evenNumbers)
        {
            Console.WriteLine(number);
        }

        Console.WriteLine("Odd numbers:");
        foreach (int number in oddNumbers)
        {
            Console.WriteLine(number);
        }

        CollectionType<Software> collectionType1 = new CollectionType<Software>();
        TextProcessor textProcessor = new TextProcessor("som", "1.2.3", "I");
        TextProcessor textProcessor1 = new TextProcessor("dima", "2.3.1", "Me");
        collectionType1.Add(textProcessor);
        collectionType1.Add(textProcessor1);
        collectionType1.View();

        CollectionType<Software> collectionType2 = new CollectionType<Software>();
        collectionType2.Add(new OperationSet("Operation Set 1", "Type 1"));
        collectionType2.Add(new TextProcessor("Text Processor 1", "Version 1", "Developer 1"));
        collectionType2.Add(new Virus("Virus 1", "Type 1", "Version 1", "Profilactin 1"));

        string json = JsonConvert.SerializeObject(collectionType2);
        File.WriteAllText("output.json", json);
    }

    static bool IsEven(int number)
    {
        return number % 2 == 0;
    }

    static bool IsOdd(int number)
    {
        return number % 2 != 0;
    }
}