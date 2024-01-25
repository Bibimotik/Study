using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Methods
{
    public static void EvenNumbers()
    {
        for (int i = 0; i <= 20; i++)
        {
            if (i % 2 == 0)
            {
                Console.Write($"{i} ");
                Thread.Sleep(100);
            }
        }
    }

    public static void OddNumbers()
    {
        for (int i = 0; i <= 20; i++)
        {
            if (i % 2 != 0)
            {
                Console.Write($"{i} ");
                Thread.Sleep(100);
            }
        }
    }
}

public class Program
{
    static void Main(string[] args)
    {
        First();
        Second();
        Third();
        Fourth();
        Fifth();
    }
    private static void First()
    {
        var allProcesses = Process.GetProcesses();
        Console.WriteLine("Information about processes:");
        Console.Write("{0,-20}", "ID:");
        Console.Write("{0,-70}", "Process Name:");
        Console.Write("{0,-20}", "Priority:\n");
        foreach (var process in allProcesses)
        {
            Console.Write("{0,-20}", $"{process.Id}");
            Console.Write("{0,-70}", $"{process.ProcessName}");
            Console.Write("{0,-20}", $"{process.BasePriority}");
            Console.WriteLine();
        }
    }
    private static void Second()
    {
        var domain = AppDomain.CurrentDomain;
        Console.WriteLine("Information about current domain:");
        Console.WriteLine("\n\nCurrent domain:\t" + domain.FriendlyName);
        Console.WriteLine("Base directory:\t" + domain.BaseDirectory);
        Console.WriteLine("Configuration Details:\t" + domain.SetupInformation);
        Console.WriteLine("All assemblies in the domain:\n");

        foreach (var assembly in domain.GetAssemblies())
        {
            Console.WriteLine(assembly.GetName().Name);
        }
    }

    private static void Third()
    {
        Mutex mutex = new Mutex();
        Thread NumbersThread = new Thread(new ParameterizedThreadStart(WriteNums));
        NumbersThread.Start(7);

        Thread.Sleep(2000);
        mutex.WaitOne();

        Console.WriteLine("\n--------------------");
        Console.WriteLine("Priority:   " + NumbersThread.Priority);
        Thread.Sleep(100);
        Console.WriteLine("Name tread:  " + NumbersThread.Name);
        Thread.Sleep(100);
        Console.WriteLine("ID tread:   " + NumbersThread.ManagedThreadId);
        Console.WriteLine("---------------------");
        Thread.Sleep(1000);

        mutex.ReleaseMutex();
        Thread.Sleep(2000);

        void WriteNums(object number)
        {
            int num = (int)number;
            for (int i = 0; i < num; i++)
            {
                Console.WriteLine(i);
                Thread.Sleep(500);
            }
        }
    }
    private static void Fourth()
    {
        Console.WriteLine("\n\n\nthead evan and odd numbers");
        Thread evenThread = new Thread(Methods.EvenNumbers);
        evenThread.Priority = ThreadPriority.Normal;
        evenThread.Start();
        evenThread.Join();

        Console.WriteLine();
        Thread oddThread = new Thread(Methods.OddNumbers);
        oddThread.Priority = ThreadPriority.Normal;
        oddThread.Start();
        oddThread.Join();
        Console.WriteLine("\n");
    }
    private static void Fifth()
    {
        TimerCallback timerCallback = new TimerCallback(WhatTimeIsIt);
        Timer timer = new Timer(timerCallback, null, 500, 1000);
        Thread.Sleep(5000);
        timer.Change(Timeout.Infinite, 2000);

        void WhatTimeIsIt(object obj)
        {
            Console.WriteLine($"It's {DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}");
        }
        Console.ReadLine();
    }
}