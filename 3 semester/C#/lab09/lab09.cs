using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

public class Car
{
    public string Make { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }

    public Car(string make, string model, int year)
    {
        Make = make;
        Model = model;
        Year = year;
    }

    public void StartEngine()
    {
        Console.WriteLine("Engine started.");
    }

    public void StopEngine()
    {
        Console.WriteLine("Engine stopped.");
    }
}

public interface IList<T> where T : Car
{
    void Add(T item);
    bool Remove(T item);
    void PrintAll();
    T Find(Predicate<T> match);
    IEnumerable<T> GetAll();
}

public class CarCollection<T> : IList<T> where T : Car
{
    private Dictionary<string, Car> cars;

    public CarCollection()
    {
        cars = new Dictionary<string, Car>();
    }

    public void Add(T item)
    {
        cars.Add(item.Make, item);
    }

    public bool Remove(T item)
    {
        return cars.Remove(item.Make);
    }

    public T Find(Predicate<T> match)
    {
        foreach (var car in cars.Values)
        {
            if (match((T)car))
            {
                return (T)car;
            }
        }

        return default(T);
    }

    public void PrintAll()
    {
        foreach (var car in cars.Values)
        {
            Console.WriteLine($"Марка: {car.Make}, Модель: {car.Model}, Год: {car.Year}");
        }
    }

    public IEnumerable<T> GetAll()
    {
        foreach (var car in cars.Values)
        {
            yield return (T)car;
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        CarCollection<Car> carCollection = new CarCollection<Car>();

        Car car1 = new Car("Toyota", "Camry", 2020);
        Car car2 = new Car("Honda", "Civic", 2019);
        Car car3 = new Car("Ford", "Mustang", 2021);

        carCollection.Add(car1);
        carCollection.Add(car2);
        carCollection.Add(car3);

        Console.WriteLine("Все авто:");
        carCollection.PrintAll();

        Console.WriteLine();
        Console.WriteLine("Удаленная car2...");
        carCollection.Remove(car2);

        Console.WriteLine("Все авто после удаления:");
        carCollection.PrintAll();

        Console.WriteLine();
        Console.WriteLine("Найденная car3...");
        Car foundCar = carCollection.Find(car => car == car3);
        if (foundCar != null)
        {
            Console.WriteLine($"Марка: {foundCar.Make}, Модель: {foundCar.Model}, Год: {foundCar.Year}");
        }
        else
        {
            Console.WriteLine("Автомобиль не найден.");
        }

        Console.WriteLine("----------------------------------------------------");

        List<int> collection1 = new List<int>() { 1, 2, 3, 4, 5 };

        Console.WriteLine("Первая коллекция:");
        PrintCollection(collection1);

        int n = 2;
        RemoveSequentialElements(collection1, n);

        Console.WriteLine("\nПервая коллекция после удаления {0} последовательных элементов:", n);
        PrintCollection(collection1);

        collection1.Add(6);
        collection1.Insert(0, 0);
        collection1.AddRange(new List<int>() { 7, 8, 9 });

        Console.WriteLine("\nПервая коллекция после добавления других элементов:");
        PrintCollection(collection1);

        Dictionary<int, int> collection2 = new Dictionary<int, int>();
        FillDictionaryFromList(collection1, collection2);

        Console.WriteLine("\nВторая коллекция (Dictionary<int, int>):");
        PrintDictionary(collection2);

        int searchValue = 5;
        bool found = SearchValueInDictionary(collection2, searchValue);

        Console.WriteLine("\nЗначение {0} найдено во второй коллекции: {1}", searchValue, found);

        ObservableCollection<Car> observableCollection = new ObservableCollection<Car>(carCollection.GetAll());
        observableCollection.CollectionChanged += CollectionChangedHandler;

        Console.WriteLine("Все авто:");
        carCollection.PrintAll();

        Console.WriteLine();
        Console.WriteLine("Удаленная car2...");
        carCollection.Remove(car2);

        Console.WriteLine("Все авто после удаления:");
        carCollection.PrintAll();

        Console.WriteLine("----------------------------------------------------");
        Console.WriteLine("Добавление и удаление элементов из наблюдаемой коллекции:");

        observableCollection.Add(new Car("Nissan", "Altima", 2022));
        observableCollection.RemoveAt(1);

        Console.WriteLine();
        Console.WriteLine("Все авто в наблюдаемойколлекции:");
        foreach (Car car in observableCollection)
        {
            Console.WriteLine($"Марка: {car.Make}, Модель: {car.Model}, Год: {car.Year}");
        }

        Console.ReadLine();
    }

    public static void PrintCollection(IEnumerable<int> collection)
    {
        foreach (int item in collection)
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();
    }

    public static void RemoveSequentialElements(List<int> collection, int n)
    {
        List<int> newCollection = new List<int>();

        int count = collection.Count;
        for (int i = 0; i < count; i++)
        {
            if (i % n != 0)
            {
                newCollection.Add(collection[i]);
            }
        }

        collection.Clear();
        collection.AddRange(newCollection);
    }

    public static void FillDictionaryFromList(List<int> list, Dictionary<int, int> dictionary)
    {
        for (int i = 0; i < list.Count; i++)
        {
            dictionary.Add(i, list[i]);
        }
    }

    public static void PrintDictionary(Dictionary<int, int> dictionary)
    {
        foreach (KeyValuePair<int, int> pair in dictionary)
        {
            Console.WriteLine($"Ключ: {pair.Key}, Значение: {pair.Value}");
        }
    }

    public static bool SearchValueInDictionary(Dictionary<int, int> dictionary, int value)
    {
        return dictionary.ContainsValue(value);
    }

    public static void CollectionChangedHandler(object sender, NotifyCollectionChangedEventArgs e)
    {
        Console.WriteLine("Изменения в наблюдаемой коллекции:");
        if (e.OldItems != null)
        {
            Console.WriteLine("Удаленные элементы:");
            foreach (Car car in e.OldItems)
            {
                Console.WriteLine($"Марка: {car.Make}, Модель: {car.Model}, Год: {car.Year}");
            }
        }
        if (e.NewItems != null)
        {
            Console.WriteLine("Добавленные элементы:");
            foreach (Car car in e.NewItems)
            {
                Console.WriteLine($"Марка: {car.Make}, Модель: {car.Model}, Год: {car.Year}");
            }
        }
        Console.WriteLine();
    }
}