using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.XPath;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization;

public interface ISerializer
{
    void Serialize<T>(T obj, string fileName);
    T Deserialize<T>(string fileName);
}
public class BinaryDataSerializer : ISerializer
{
    public void Serialize<T>(T obj, string fileName)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream fileStream = new FileStream(fileName, FileMode.Create))
        {
            formatter.Serialize(fileStream, obj);
        }
    }
    public T Deserialize<T>(string fileName)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
        {
            return (T)formatter.Deserialize(fileStream);
        }
    }
}
public class JsonDataSerializer : ISerializer
{
    public void Serialize<T>(T obj, string fileName)
    {
        string json = JsonSerializer.Serialize(obj);
        File.WriteAllText(fileName, json);
    }

    public T Deserialize<T>(string fileName)
    {
        string json = File.ReadAllText(fileName);
        return JsonSerializer.Deserialize<T>(json);
    }
}
public class XmlDataSerializer : ISerializer
{
    public void Serialize<T>(T obj, string fileName)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        using (FileStream fileStream = new FileStream(fileName, FileMode.Create))
        {
            serializer.Serialize(fileStream, obj);
        }
    }
    public T Deserialize<T>(string fileName)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
        {
            return (T)serializer.Deserialize(fileStream);
        }
    }
}
public class SoapDataSerializer : ISerializer
{
    public void Serialize<T>(T obj, string fileName)
    {
        using (FileStream fileStream = new FileStream(fileName, FileMode.Create))
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(T));
            serializer.WriteObject(fileStream, obj);
        }
    }
    public T Deserialize<T>(string fileName)
    {
        using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(T));
            return (T)serializer.ReadObject(fileStream);
        }
    }
}

[Serializable]
public abstract class Software
{
    public string Company { get; set; }

    public abstract void ConductSoftware();

    public virtual void PrintInformation()
    {
        Console.WriteLine("Компания: " + Company);
    }

    public override string ToString()
    {
        return $"Type: {GetType().Name}\n Company: {Company}";
    }
}

[Serializable]
public sealed class OperationSet : Software
{
    public override void ConductSoftware()
    {
        Console.WriteLine("Набор операций");
    }

    public override void PrintInformation()
    {
        Console.WriteLine("Тип ПО: Набор операций");
        Console.WriteLine("Название: " + Company);
    }
    public override string ToString()
    {
        return base.ToString() + "\nOperationSet";
    }
}


public class Program
{
    public static void Main(string[] args)
    {
        OperationSet[] tests = new OperationSet[]
        {
            new OperationSet { Company = "Компания 1" },
            new OperationSet { Company = "Компания 2" },
            new OperationSet { Company = "Компания 3" }
        };
        ISerializer binarySerializer = new BinaryDataSerializer();
        binarySerializer.Serialize(tests, "tests.bin");

        ISerializer jsonSerializer = new JsonDataSerializer();
        jsonSerializer.Serialize(tests, "tests.json");

        ISerializer xmlSerializer = new XmlDataSerializer();
        xmlSerializer.Serialize(tests, "tests.xml");

        ISerializer soapSerializer = new SoapDataSerializer();
        soapSerializer.Serialize(tests, "tests.soap");

        OperationSet[] binaryDeserialized = binarySerializer.Deserialize<OperationSet[]>("tests.bin");
        OperationSet[] jsonDeserialized = jsonSerializer.Deserialize<OperationSet[]>("tests.json");
        OperationSet[] xmlDeserialized = xmlSerializer.Deserialize<OperationSet[]>("tests.xml");
        OperationSet[] soapDeserialized = soapSerializer.Deserialize<OperationSet[]>("tests.soap");

        Console.WriteLine("Binary Deserialized:");
        foreach (var test in binaryDeserialized)
        {
            Console.WriteLine(test);
        }

        Console.WriteLine("JSON Deserialized:");
        foreach (var test in jsonDeserialized)
        {
            Console.WriteLine(test);
        }

        Console.WriteLine("XML Deserialized:");
        foreach (var test in xmlDeserialized)
        {
            Console.WriteLine("Название: " + test.Company);
        }

        Console.WriteLine("SOAP Deserialized:");
        foreach (var test in soapDeserialized)
        {
            Console.WriteLine("Название: " + test.Company);
        }
        Console.WriteLine("--------------------------------------------");
        OperationSet[] newTests = new OperationSet[]
        {
            new OperationSet { Company = "Новый тест 1" },
            new OperationSet { Company = "Новый тест 2" },
            new OperationSet { Company = "Новый тест 3" }
        };
        ISerializer BinarySerializer = new BinaryDataSerializer();
        BinarySerializer.Serialize(newTests, "newTests.bin");
        OperationSet[] BinaryDeserialized = BinarySerializer.Deserialize<OperationSet[]>("newTests.bin");
        foreach (var test in BinaryDeserialized)
        {
            Console.WriteLine(test);
        }
        Console.WriteLine("--------------------------------------------");

        XDocument document = new XDocument(
            new XElement("Root",
                new XElement("Element1", "Value1"),
                new XElement("Element2", "Value2"),
                new XElement("Element3", "Value3")
            )
        );
        document.Save("newDocument.xml");
        XDocument loadedDocument = XDocument.Load("newDocument.xml");
        XElement element1 = loadedDocument.XPathSelectElement("//Element1");
        XElement element2 = loadedDocument.XPathSelectElement("//Element2");
        var elements = loadedDocument.XPathSelectElements("//Root/*");

        Console.WriteLine("Element1: " + element1?.Value);
        Console.WriteLine("Element2: " + element2?.Value);
        Console.WriteLine("All Elements:");
        foreach (var element in elements)
        {
            Console.WriteLine(element.Name + ": " + element.Value);
        }
    }
}