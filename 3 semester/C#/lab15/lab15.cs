using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Collections.Concurrent;

namespace Lab_16
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = Convert.ToInt32(Console.ReadLine());
            switch(a)
            {
                case 1:
                    First();
                    Console.WriteLine();
                    Main(args);
                    break;
                case 2:
                    Second();
                    Console.WriteLine();
                    Main(args);
                    break;
                case 3:
                    Third();
                    Console.WriteLine();
                    Main(args);
                    break;
                case 4:
                    Forth();
                    Console.WriteLine();
                    Main(args);
                    break;
                case 5:
                    Fifth();
                    Console.WriteLine();
                    Main(args);
                    break;
                case 6:
                    Sixth();
                    Console.WriteLine();
                    Main(args);
                    break;
                case 7:
                    Seventh();
                    Console.WriteLine();
                    Main(args);
                    break;
                case 8:
                    Eighth();
                    Console.WriteLine();
                    Main(args);
                    break;
            }
        }


        static void First()     
        {
            for (int i = 0; i < 5; i++)
            {
                Stopwatch sw = new Stopwatch(); 
                sw.Start();
                Task task = new Task(() => MulByVector(100000));
                task.Start();
                Console.WriteLine($"ID: {task.Id}, статус: {task.Status}");        
                task.Wait();
                Console.WriteLine($"ID: {task.Id}, статус: {task.Status}");              
                sw.Stop();
                Console.WriteLine($"Производительность: {sw.ElapsedMilliseconds}ms");  
                Console.WriteLine();
            }
        }
        static void MulByVector(int k, object ob = null)   
        {
            Random random = new Random();
            List<int> vector = new List<int>();
            for (int i = 0; i < k; i++)
            {
                vector.Add(random.Next(1, 10));
            }
            vector = vector.Select(x => x * 10).ToList();
        }


        static void Second()
        {
            CancellationTokenSource cancellation = new CancellationTokenSource();  
            Task task = Task.Run(() => MulByVector(1000, cancellation), cancellation.Token);
            try
            {
                cancellation.Cancel();
                task.Wait();
            }
            catch (Exception)
            {
                if (task.IsCanceled)
                    Console.WriteLine("Задача отменена");
            }
        }


        static void Third()     
        {
            Task<int> first = new Task<int>(() => new Random().Next(1, 9) * 100);
            Task<int> second = new Task<int>(() => new Random().Next(0, 9) * 10);
            Task<int> third = new Task<int>(() => new Random().Next(0, 9));

            first.Start();
            second.Start();
            third.Start();
            first.Wait();
            second.Wait();
            third.Wait();

            Task<int> number = new Task<int>(() => first.Result + second.Result + third.Result);
            number.Start();
            Console.WriteLine($"Number: {number.Result}");
        }


        static void Forth()      
        {
            Task<int> task4 = new Task<int>(() => 100 + 10 + 1);
            Task show = task4.ContinueWith(sum => Console.WriteLine($"Sum = {sum.Result}"));
            task4.Start();
            show.Wait();

            Task<double> sqrt = new Task<double>(() => Math.Sqrt(49));
            TaskAwaiter<double> awaiter = sqrt.GetAwaiter();   
            awaiter.OnCompleted(() => Console.WriteLine($"Result is: {sqrt.Result}"));
            sqrt.Start();
            sqrt.Wait();
            awaiter.GetResult();
            Thread.Sleep(10);
        }


        static void Factorial(int num)
        {
            int result = 1;
            for (int i = 1; i <= num; i++)
                result *= i;

            Console.WriteLine($"Факториал числа {num} равен {result}");
        }

        static void Fifth()
        {
            int[] arr1 = new int[1000000];
            int[] arr2 = new int[1000000];
            Stopwatch stopwatch2 = new Stopwatch();
            stopwatch2.Start();
            Parallel.For(0, arr1.Length, t => arr1[t] = t);
            stopwatch2.Stop();
            Console.WriteLine($"Parallel.For: {stopwatch2.Elapsed}"); 

            Stopwatch stopwatch3 = new Stopwatch();
            stopwatch3.Start();
            for (int i = 0; i < arr2.Length; i++)
                arr2[i] = i + 1;

            stopwatch3.Stop();
            Console.WriteLine("foreach: " + stopwatch3.Elapsed);  
            Stopwatch stopwatch4 = new Stopwatch();
            stopwatch4.Start();
            Parallel.ForEach<int>(new List<int>() { 1, 2, 3, 4 }, Factorial);
            stopwatch4.Stop();
            Console.WriteLine("Parallel.Foreach: " + stopwatch4.Elapsed);

            Stopwatch stopwatch5 = new Stopwatch();
            stopwatch5.Start();
            foreach (var m in new List<int>() { 1, 2, 3, 4 })
            {
                Factorial(m);
            }
            stopwatch5.Stop();
            Console.WriteLine("foreach: " + stopwatch5.Elapsed);   
            Console.WriteLine();
        }


        static void Sixth()  
        {
            int count = 0;
            int maxCount = 100;
            Parallel.Invoke(() =>     
            {
                while (count < maxCount)
                {
                    count++;
                    Console.WriteLine($"1: {count}");
                }
            }, () =>
            {
                while (count < maxCount)     
                {
                    count++;
                    Console.WriteLine($"2: {count}");
                }
            });
        }


        static void Seventh()
        {
            BlockingCollection<string> bc = new BlockingCollection<string>(5);    

            Task[] sellers = new Task[10]
            {
                new Task(() => { while (true) { Thread.Sleep(700); bc.Add("Стол"); } }),
                new Task(() => { while (true) { Thread.Sleep(700); bc.Add("Шкаф"); } }),
                new Task(() => { while (true) { Thread.Sleep(700); bc.Add("Зеркало"); } }),
                new Task(() => { while (true) { Thread.Sleep(700); bc.Add("Бра"); } }),
                new Task(() => { while (true) { Thread.Sleep(700); bc.Add("Подоконник"); } }),
                new Task(() => { while (true) { Thread.Sleep(700); bc.Add("Микроволновка"); } }),
                new Task(() => { while (true) { Thread.Sleep(700); bc.Add("Кровать"); } }),
                new Task(() => { while (true) { Thread.Sleep(700); bc.Add("Дверь"); } }),
                new Task(() => { while (true) { Thread.Sleep(700); bc.Add("Вазон"); } }),
                new Task(() => { while (true) { Thread.Sleep(700); bc.Add("Стул"); } })
            };

            Task[] consumers = new Task[5]
            {
                new Task(() => { while (true) { Thread.Sleep(300); bc.Take(); } }),
                new Task(() => { while (true) { Thread.Sleep(500); bc.Take(); } }),
                new Task(() => { while (true) { Thread.Sleep(500); bc.Take(); } }),
                new Task(() => { while (true) { Thread.Sleep(400); bc.Take(); } }),
                new Task(() => { while (true) { Thread.Sleep(250); bc.Take(); } })
            };

            foreach (var i in sellers)
                if (i.Status != TaskStatus.Running)
                    i.Start();

            foreach (var i in consumers)
                if (i.Status != TaskStatus.Running)
                    i.Start();

            int count = 1;
            while (true)
            {
                if (bc.Count != count && bc.Count != 0)
                {
                    count = bc.Count;
                    Thread.Sleep(500);
                    Console.Clear();
                    Console.WriteLine("--- СКЛАД ---");

                    foreach (var i in bc)
                        Console.WriteLine(i);
                }
            }
        }

        static void Eighth() 
        {
            void Factorial()
            {
                int result = 1;
                for (int i = 1; i <= 6; i++)
                {
                    result *= i;
                }
                Thread.Sleep(1000);
                Console.WriteLine($"Факториал равен {result}");
            }

            async void FactorialAsync()   
            {
                Console.WriteLine("Начало метода FactorialAsync");
                await Task.Run(() => Factorial());               
                Console.WriteLine("Конец метода FactorialAsync");
            }
            FactorialAsync();
        }
    }
}