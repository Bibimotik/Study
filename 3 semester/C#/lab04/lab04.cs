using System;
using System.Runtime.ConstrainedExecution;

public abstract class Product
{
    public string Name { get; set; }

    public Product(string name)
    {
        Name = name;
    }

    public abstract void DoPlay();

    public override string ToString()
    {
        return $"Product: {Name}";
    }
}

public interface IPlayable
{
    void Play();
}

public class Software : Product
{
    private string Company;

    public Software(string name, string company) : base(name)
    {
        Company = company;
    }

    public override void DoPlay()
    {
        Console.WriteLine($"Playing software: {Name}");
    }

    public override string ToString()
    {
        return $"Software: {Name}";
    }
}

public class OperationSet : Software
{
    private string Type;
    public OperationSet(string name, string type) : base(name, "")
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
    private string Version;
    private string Developer;
    public TextProcessor(string name, string version, string developer) : base(name, "")
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
    private string Type;
    private string Version;
    private string Profilactin;
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
    private string Version;
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
    public Toy(string name) : base(name, "")
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

public sealed class Developer
{
    private string Experience;
    private string Company;
    public string Name { get; set; }

    public Developer(string name, string experience, string company)
    {
        Name = name;
        Experience = experience;
        Company = company;
    }

    public override string ToString()
    {
        return $"Developer: {Name}";
    }
}

public class Printer
{
    public static void IAmPrinting(Product product)
    {
        Console.WriteLine(product.ToString());
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Product software = new Software("MacOS", "Apple");
        Product operationSet = new OperationSet("OperationSet", "Boolean");
        Product textProcessor = new TextProcessor("WordPerfect", "X9", "Corel");
        Product virus = new Virus("Troyan", "File Virus", "Old", "Reboot");
        Product conficker = new Conficker("Conficker");
        Product word = new Word("Word", "v19.0.2");
        IPlayable toy = new Toy("Generic Toy");
        IPlayable minesweeper = new Minesweeper("Minesweeper");
        Developer developer = new Developer("John Doe", "2 years", "Apple");

        Console.WriteLine(software);
        Console.WriteLine(operationSet);
        Console.WriteLine(textProcessor);
        Console.WriteLine(virus);
        Console.WriteLine(conficker);
        Console.WriteLine(word);
        Console.WriteLine(toy);
        Console.WriteLine(minesweeper);
        Console.WriteLine(developer);

        toy.Play();

        if (toy is IPlayable)
        {
            Console.WriteLine("The toy is playable.");
        }

        software.DoPlay();
        operationSet.DoPlay();
        textProcessor.DoPlay();
        virus.DoPlay();
        conficker.DoPlay();
        word.DoPlay();
        ((Product)toy).DoPlay();
        ((Product)minesweeper).DoPlay();

        Console.WriteLine("--------------------");

        Printer printer = new Printer();
        
        Product[] products = new Product[]
        {
            software,
            operationSet,
            textProcessor,
            virus,
            conficker,
            word,
            (Product)toy,
            (Product)minesweeper
        };

        foreach (Product product in products)
        {
            Printer.IAmPrinting(product);
        }
    }
}