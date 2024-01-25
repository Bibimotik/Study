using System;
using System.Linq;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Ekzamen
{
    public static class Program
    {
        static void Main()
        {
            List<Circle> circles = new List<Circle>()
            {
                new Circle(2, "red"),
                new Circle(3, "green"),
                new Circle(3, "yellow"),
                new Circle(4, "purple"),
                new Circle(5, "green"),
                new Circle(6, "red")
            };
            var sortedCircles = circles
                .Skip(3)
                .OrderBy(c => c.Color)
                .GroupBy(c => c.Color);

            foreach (var group in sortedCircles)
            {
                Console.WriteLine("Цвет: " + group.Key);
                foreach (var circle in group)
                {
                    Console.WriteLine("Радиус: " + circle.Radius);
                }
                Console.WriteLine();
            }
            
            /*XmlSerializer serializer = new XmlSerializer(typeof(List<Circle>));

            using (StreamWriter writer = new StreamWriter("circles.xml"))
            {
                serializer.Serialize(writer, sortedCircles);
            }*/
            
            string json = JsonConvert.SerializeObject(sortedCircles, Formatting.Indented);
            Console.WriteLine(json);
            File.WriteAllText("rectangle.json", json);
            
            
            NDate date = new NDate(15, 10);
            Console.WriteLine("День: " + date.Day);
            Console.WriteLine("Месяц: " + date.Month);
            date.NextDay();
            Console.WriteLine("Следующий день: " + date.Day + "." + date.Month);
            
            NTDate date1 = new NTDate(15, 10, 2023);
            NTDate date2 = new NTDate(20, 10, 2023);

            Console.WriteLine("Дата 1: " + date1.Day + "." + date1.Month + "." + date1.Year);
            Console.WriteLine("Дата 2: " + date2.Day + "." + date2.Month + "." + date2.Year);

            if (date1 > date2)
            {
                Console.WriteLine("Дата 1 позже Дата 2");
            }
            else if (date1 < date2)
            {
                Console.WriteLine("Дата 1 раньше Дата 2");
            }
            else
            {
                Console.WriteLine("Дата 1 и Дата 2 одинаковы");
            }
        }
    }
}

