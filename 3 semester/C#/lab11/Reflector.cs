using Microsoft.VisualBasic;
using System.Data.Common;
using System.Reflection.Metadata;

namespace Lab_11;

static class Reflector
{
    private static string _path = @"D:\Учеба\C#\lab11\";
    private static string _fileName = "Reflector.txt";
    private static string Info { get; set; } = "";

    private static string GetAssembly(Type type)
    {
        string? assembly = "Assembly Name: ";
        assembly += type.Assembly.FullName;
        return (assembly ?? "undenfined") + '\n';
    }
    private static string HasPublicConstructors(Type type)
    {
        string? constructors = "Has public constructors: ";
        constructors += type.GetConstructors().Length > 0 ? "true" : "false";
        return (constructors ?? "undenfined") + '\n';
    }

    private static string GetPublicMethods(Type type)
    {
        string? methods = "Public methods: \n";
        foreach (var method in type.GetMethods())
        {
            methods += method.Name + "\n";
        }
        return (methods ?? "undenfined") + '\n';
    }
    private static string GetFieldsAndProperties(Type type)
    {
        string? fields = "Fields: \n";
        foreach (var field in type.GetFields())
        {
            fields += field.Name + "\n";
        }
        string? props = "Properieties: \n";
        foreach (var prop in type.GetProperties())
        {
            props += prop.Name + "\n";
        }
        return ((fields ?? "undenfined") + "\n" + (props ?? "undenfined")) + "\n";
    }

    private static string GetInterfaces(Type type)
    {
        string? interfaces = "Interfaces: \n";
        foreach (var intreface in type.GetInterfaces())
        {
            interfaces += intreface.Name + "\n";
        }
        return (interfaces ?? "undenfined") + "\n";
    }

    private static string GetMethodsByParameter(Type type, Type? parameter)
    {
        string parameterType = parameter == null ? "undenfined" : parameter.ToString();
        string? methodsByParametrs = $"Methods by parameter - {parameterType}: \n";
        foreach (var method in type.GetMethods())
        {
            if (parameter != null)
            {
                foreach (var parameterInfo in method.GetParameters())
                {
                    if (parameterInfo.ParameterType == parameter)
                    {
                        methodsByParametrs += "method" + '\n';
                    }
                }
            }
        }
        return methodsByParametrs;
    }

    public static void Invoke(object? obj, string methodName)
    {
        var method = obj?.GetType().GetMethod(methodName);
        if (method == null)
        {
            return;
        }
        var parameters = method.GetParameters();
        var values = new object[parameters.Length];
        int i = 0;
        foreach (var parameterFromFile in File.ReadAllLines("parameters.txt"))
        {

            if (i < parameters.Length)
            {
                values[i] = parameterFromFile;
            }
            else
            {
                break;
            }
            i++;
        }
        method.Invoke(obj, values);
    }

    public static void Invoke(object? obj, string methodName, object[] parameters)
    {
        var method = obj?.GetType().GetMethod(methodName);
        if (method == null)
        {
            return;
        }
        method.Invoke(obj, parameters);
    }

    public static void Invoke(Type type,
                              object? obj,
                              string methodName,
                              params string[] parameters)
    {
        var method = type.GetMethod(methodName);

        if (method == null)
        {
            Console.WriteLine("Метод не найден");
            return;
        }

        var methodParameters = method.GetParameters();

        var values = new object[methodParameters.Length];

        for (var i = 0; i < methodParameters.Length; i++)
        {
            values[i] = Convert.ChangeType(parameters[i],
                                           methodParameters[i].ParameterType);
        }

        method?.Invoke(obj, values);
    }
    public static string GetTypeInfo(Type type, Type? methodsParametrs = null)
    {
        Info = "";
        DeleteFile();
        Info += GetAssembly(type);
        Info += HasPublicConstructors(type);
        Info += GetPublicMethods(type);
        Info += GetFieldsAndProperties(type);
        Info += GetInterfaces(type);
        Info += GetMethodsByParameter(type, methodsParametrs);
        ToFile();
        return Info;
    }

    private static void ToFile() => File.AppendAllText(_fileName, Info);
    private static void DeleteFile() => File.Delete(_fileName);

    public static T? Create<T>(object[] parameters)
    {
        var type = typeof(T);
        var constructor = type.GetConstructor(parameters.Select(p => p.GetType()).ToArray());
        if (constructor == null) { return default; }
        else { return (T)constructor.Invoke(parameters); }
    }
}