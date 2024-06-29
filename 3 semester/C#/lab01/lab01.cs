using System;
using System.Linq;


public class Product
{
    private string name;
    private string upc;
    private string manufacturer;
    private decimal price;
    private int shelfLife;
    private int quantity;

    public int Id { get; private set; }

    public string Name
    {
        get { return name; }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Наименование продукта не может быть пустым.");
            }
            name = value;
        }
    }

    public string UPC
    {
        get { return upc; }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("UPC продукта не может быть пустым.");
            }
            upc = value;
        }
    }

    public string Manufacturer
    {
        get { return manufacturer; }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Производитель продукта не может быть пустым.");
            }
            manufacturer = value;
        }
    }

    public decimal Price
    {
        get { return price; }
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Цена продукта не может быть отрицательной.");
            }
            price = value;
        }
    }

    public int ShelfLife
    {
        get { return shelfLife; }
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Срок хранения продукта не может быть отрицательным.");
            }
            shelfLife = value;
        }
    }

    public int Quantity
    {
        get { return quantity; }
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Количество продукта не может быть отрицательным.");
            }
            quantity = value;
        }
    }

    public Product(int id, string name, string upc, string manufacturer, decimal price, int shelfLife, int quantity)
    {
        Id = id;
        Name = name;
        UPC = upc;
        Manufacturer = manufacturer;
        Price = price;
        ShelfLife = shelfLife;
        Quantity = quantity;
    }

    public decimal CalculateTotalPrice()
    {
        return Price * Quantity;
    }
}
public class ProductManager
{
    private List<Product> productList;

    public ProductManager(List<Product> products)
    {
        productList = products;
    }

    public List<Product> GetProductsByName(string name)
    {
        return productList.Where(p => p.Name == name).ToList();
    }

    public List<Product> GetProductsByNameAndPrice(string name, decimal maxPrice)
    {
        return productList.Where(p => p.Name == name && p.Price <= maxPrice).ToList();
    }

    public int GetCountOfExpensiveProducts(int priceThreshold)
    {
        return productList.Count(p => p.Price > priceThreshold);
    }

    public Product GetMaxProduct()
    {
        return productList.OrderByDescending(p => p.Price).FirstOrDefault();
    }

    public List<Product> GetOrderedProductsByManufacturerAndQuantity()
    {
        return productList.OrderBy(p => p.Manufacturer).ThenBy(p => p.Quantity).ToList();
    }
}


class Program
{
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

        List<Product> products = new List<Product>
        {
            new Product(1, "Товар1", "UPC1", "Производитель1", 50, 10, 10),
            new Product(2, "Товар2", "UPC2", "Производитель2", 100, 10, 5),
            new Product(3, "Товар3", "UPC3", "Производитель1", 150, 10, 8),
        };

        ProductManager manager = new ProductManager(products);

        List<Product> productsByName = manager.GetProductsByName("Товар1");
        Console.WriteLine("Результаты поиска по имени 'Товар1':");
        foreach (var product in productsByName)
        {
            Console.WriteLine(product.Name);
        }

        Console.WriteLine();

        List<Product> productsByNameAndPrice = manager.GetProductsByNameAndPrice("Товар2", 120);
        Console.WriteLine("Результаты поиска по имени 'Товар2' и цене не более 120:");
        foreach (var product in productsByNameAndPrice)
        {
            Console.WriteLine(product.Name);
        }

        Console.WriteLine();

        int countOfExpensiveProducts = manager.GetCountOfExpensiveProducts(100);
        Console.WriteLine("Количество товаров с ценой выше 100: " + countOfExpensiveProducts);

        Console.WriteLine();

        Product maxProduct = manager.GetMaxProduct();
        Console.WriteLine("Самый дорогой товар:");
        Console.WriteLine("Название: " + maxProduct.Name);
        Console.WriteLine("Цена: " + maxProduct.Price);
        Console.WriteLine("Производитель: " + maxProduct.Manufacturer);
        Console.WriteLine("Количество: " + maxProduct.Quantity);

        Console.WriteLine();

        List<Product> orderedProducts = manager.GetOrderedProductsByManufacturerAndQuantity();
        Console.WriteLine("Упорядоченные товары по производителю и количеству:");
        foreach (var product in orderedProducts)
        {
            Console.WriteLine("Название: " + product.Name);
            Console.WriteLine("Производитель: " + product.Manufacturer);
            Console.WriteLine("Количество: " + product.Quantity);
            Console.WriteLine();
        }

        Console.WriteLine("----------------------------------------");

        List<int> numbers = new List<int> { 1, 5, 2, 8, 3, 6, 9, 4, 7 };

        var query = from num in numbers
                    where num % 2 == 0
                    orderby num descending
                    group num by num % 3 into numGroup
                    let sum = numGroup.Sum()
                    where sum > 10
                    select numGroup;

        var result1 = products
        .Join(
            monthsWithLengthN,
            product => product.Name.Length,
            month => month.Length,
            (product, month) => new { Product = product, Month = month }
        );
    }
}