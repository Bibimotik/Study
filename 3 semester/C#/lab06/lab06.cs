using System;
using System.Diagnostics;
using System.Xml.Linq;

public enum ProductType
{
    Software,
    OperationSet,
    TextProcessor,
    Virus,
    Conficker,
    Word,
    Toy,
    Minesweeper
}

public struct DeveloperInfo
{
    public string Experience;
    public string Company;
}

public class PriceException : ArgumentException
{
    public PriceException(string message)
        : base(message) { }
}

public class QuantityException : ArgumentOutOfRangeException
{
    public QuantityException(string message)
        : base(message) { }
}

public class VersionException : Exception
{
    public string Value { get; }
    public VersionException(string message, string val)
        : base(message)
    {
        Value = val;
    }
}

public abstract class Product
{
    public string Name { get; set; }
    public ProductType Type { get; set; }

    public Product(string name, ProductType type)
    {
        Name = name;
        Type = type;
    }

    public abstract void DoPlay();

    public override string ToString()
    {
        return $"Product: {Name} ({Type})";
    }
}

public interface IPlayable
{
    void Play();
}

public class Software : Product
{
    public string Company;

    public Software(string name, string company) : base(name, ProductType.Software)
    {
        Company = company;
    }

    public override void DoPlay()
    {
        Console.WriteLine($"Playing software: {Name}");
    }

    public override string ToString()
    {
        return $"Software: {Name} ({Type})";
    }
}

public class OperationSet : Software
{
    private string Type;
    private double quantity;
    public double Quantity
    {
        get => quantity;
        set
        {
            if (value < 0)
                throw new QuantityException("Количество должно быть больше 0!");
            else
                quantity = value;
        }
    }

    public OperationSet(string name, string type, double quantity) : base(name, "")
    {
        Type = type;
        Quantity = quantity;
    }

    public override void DoPlay()
    {
        Console.WriteLine($"Playing operation set: {Name}");
    }

    public override string ToString()
    {
        return $"OperationSet: {Name} ({Type})";
    }
}

public class TextProcessor : Software
{
    private double price;
    public string Version;
    public string Developer;
    public double Price
    {
        get => price;
        set
        {
            if (value < 0)
                throw new PriceException("Цена должна быть больше 0!");
            else
                price = value;
        }
    }

    public TextProcessor(string name, string version, string developer, double price) : base(name, "")
    {
        Developer = developer;
        Version = version;
        Price = price;
    }

    public override void DoPlay()
    {
        Console.WriteLine($"Playing text processor: {Name}");
    }

    public override string ToString()
    {
        return $"TextProcessor: {Name} ({Type})";
    }
}

public class Virus : TextProcessor
{
    private string version;
    private string Type;
    public string Version
    {
        get => version;
        set
        {
            if (value == "X6")
                throw new VersionException("Версия слишком старая!", value);
            else
                version = value;
        }
    }
    private string Profilactin;
    public Virus(string name, string type, string version, string profilactin) : base(name, version, "", 0 )
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
        return $"Virus: {Name} ({Type})";
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
        return $"Conficker: {Name} ({Type})";
    }
}

public class Word : TextProcessor
{
    private string Version;
    public Word(string name, string version) : base(name, version, "", 0 )
    {
        Version = version;
    }

    public override void DoPlay()
    {
        Console.WriteLine($"Playing Word text processor: {Name}");
    }

    public override string ToString()
    {
        return $"Word: {Name} ({Type})";
    }
}

public class Toy : Software, IPlayable
{
    public string Type;
    public Toy(string name, string type) : base(name, ProductType.Toy.ToString())
    {
        Type = type;
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
        return $"Toy: {Name} ({Type})";
    }
}

public class Minesweeper : Toy, IPlayable
{
    public Minesweeper(string name) : base(name, "")
    {
    }

    public override void DoPlay()
    {
        Console.WriteLine($"Playing Minesweeper toy: {Name}");
    }

    public override string ToString()
    {
        return $"Minesweeper: {Name} ({Type})";
    }
}

public sealed class Developer
{
    public DeveloperInfo Info { get; set; }
    public string Name { get; set; }

    public Developer(string name, string experience, string company)
    {
        Name = name;
        Info = new DeveloperInfo
        {
            Experience = experience,
            Company = company
        };
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


public class ProductContainer
{
    private List<Product> products;

    public ProductContainer()
    {
        products = new List<Product>();
    }

    public void AddProduct(Product product)
    {
        products.Add(product);
    }

    public void RemoveProduct(Product product)
    {
        products.Remove(product);
    }

    public Toy FindToysByType(string type)
    {
        foreach (Product product in products)
        {
            if (product is Toy toy && toy.Type == type)
            {
                return toy;
            }
        }

        return null;
    }

    public TextProcessor FindTextProcessorByVersion(string version)
    {
        foreach (Product product in products)
        {
            if (product is TextProcessor textProcessor && textProcessor.Version == version)
            {
                return textProcessor;
            }
        }

        return null;
    }

    public void PrintSoftwareInAlphabeticalOrder()
    {
        var softwareProducts = products.OfType<Software>().OrderBy(s => s.Name);

        foreach (var software in softwareProducts)
        {
            Console.WriteLine(software);
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {

        ProductContainer container = new ProductContainer();

        container.AddProduct(new Software("MacOS", "Apple"));
        container.AddProduct(new Toy("Generic Toy", "Arcada"));
        container.AddProduct(new TextProcessor("WordPerfect", "X9", "Corel", 135.1));
        container.AddProduct(new Software("Windows", "Microsoft"));
        container.AddProduct(new Toy("Concept Toy", "Sniper"));
        container.AddProduct(new TextProcessor("Pages", "X5", "Apple", 34.5));

        Toy toy = container.FindToysByType("Arcada");
        if (toy != null)
        {
            Console.WriteLine(toy);
        }

        TextProcessor textProcessor = container.FindTextProcessorByVersion("X9");
        if (textProcessor != null)
        {
            Console.WriteLine(textProcessor);
        }

        Console.WriteLine("----------------------------------------");
        
        container.PrintSoftwareInAlphabeticalOrder();

        Console.WriteLine("----------------------------------------");

        try
        {
            container.AddProduct(new TextProcessor("Pages", "c67", "Apple", -34.4));


            try
            {
                container.AddProduct(new TextProcessor("Pages", "c67", "Apple", -5656.4));
            }
            catch (PriceException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                Console.WriteLine($"Место: {ex.StackTrace}");
                Console.WriteLine($"Причина: {ex.InnerException?.Message ?? "Неизвестно"}");

                throw;
            }
        }
        catch (PriceException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
            Console.WriteLine($"Место: {ex.StackTrace}");
            Console.WriteLine($"Причина: {ex.InnerException?.Message ?? "Неизвестно"}");
        }
        finally
        {
            Console.WriteLine("Исправьте значение Price!\n");
        }

        try
        {
            container.AddProduct(new Virus("Trouan23", "Agrassiv", "X6", "Rebut"));
        }
        catch (VersionException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
            Console.WriteLine($"Некорректное значение: {ex.Value}");
            Console.WriteLine($"Место: {ex.StackTrace}");
            Console.WriteLine($"Причина: {ex.InnerException?.Message ?? "Неизвестно"}");
        }
        finally
        {
            Console.WriteLine("Исправьте значение Version!\n");
        }

        try
        {
            container.AddProduct(new OperationSet("GGgf", "dfnn", -5));
        }
        catch (QuantityException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
            Console.WriteLine($"Место: {ex.StackTrace}");
            Console.WriteLine($"Причина: {ex.InnerException?.Message ?? "Неизвестно"}");
        }
        finally
        {
            Console.WriteLine("Исправьте значение Quantity!\n");
        }

        try
        {
            container.AddProduct(new OperationSet("GGgf", "dfnn", -5));
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
            Console.WriteLine($"Место: {ex.StackTrace}");
            Console.WriteLine($"Причина: {ex.InnerException?.Message ?? "Неизвестно"}");
        }
        finally
        {
            Console.WriteLine("Исправьте значение Quantity!\n");
        }

        Console.WriteLine("----------------------------------------");
        int a = 5;

        Debug.Assert(a >= 5, "Check");
        Console.WriteLine("GOOD");
    }
}