using System;
using System.Threading;

class Program
{
    static Mutex mutex = new Mutex(); // М'ютекс для синхронізації потоків
    static bool firstThreadDone = false;

    static void Main()
    {
        Thread firstThread = new Thread(FirstThreadMethod);
        Thread secondThread = new Thread(SecondThreadMethod);

        firstThread.Start();
        secondThread.Start();

        firstThread.Join();
        secondThread.Join();
    }

    static void FirstThreadMethod()
    {
        mutex.WaitOne();

        try
        {
            for (int i = 0; i <= 20; i++)
            {
                Console.WriteLine($"Thread 1: {i}");
                Thread.Sleep(200); // Додайте затримку для емуляції реальної роботи
            }

            firstThreadDone = true;
        }
        finally
        {
            mutex.ReleaseMutex();
        }
    }

    static void SecondThreadMethod()
    {
        while (!firstThreadDone)
        {
            // Очікуємо завершення першого потоку
            Thread.Sleep(100);
        }

        mutex.WaitOne();

        try
        {
            for (int i = 10; i >= 0; i--)
            {
                Console.WriteLine($"Thread 2: {i}");
                Thread.Sleep(200); // Додайте затримку для емуляції реальної роботи
            }
        }
        finally
        {
            mutex.ReleaseMutex();
        }
    }
}
