using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Reflection;
using System.IO;

namespace lab15
{
    class CountPrimes2
    {
        public StreamWriter Writer { get; set; }
        public int n { get; set; }
        public CountPrimes2(StreamWriter writer, object num)
        {
            Writer = writer;
            n = (int)num;
        }
        public void Count()
        {
            Writer.WriteLine(DateTime.Now);
            for (int i = 1; i <= n; i++)
            {
                if ((i % 2 != 0 && i % 3 != 0 && i % 5 != 0 && i % 7 != 0 && i % 9 != 0) || i == 2 || i == 3 || i == 5 || i == 7)
                {
                    Writer.WriteLine(i);
                    Thread.Sleep(200);
                }
            }
            Writer.WriteLine(DateTime.Now);
            Writer.Close();
        }
    }
    class WeatherQuery
    {
        public string Country { get; set; }
        public DateTime Date { get; set; }
        public Random rand { get; set; }
        public Countries cntrs { get; set; }
        public WeatherQuery(string country, DateTime date, Random rnd)
        {
            Country = country;
            Date = date;
            rand = rnd;
        }
        public enum Countries
        {
            Belarus,
            USA,
            Poland,
            Singapure,
            NewZeland
        }
        public void GetWeather(object obj)
        {
            WeatherQuery w = (WeatherQuery)obj;
            int n = rand.Next(0, 100);
            int c = rand.Next(0, 5);
            w.cntrs = (Countries)Enum.GetValues(typeof(Countries)).GetValue(c);
            w.Country = Enum.GetValues(typeof(Countries)).GetValue(c).ToString();
            if (n <= 33)
            {
                Console.WriteLine($"В стране {w.Country} в {DateTime.Now.ToLongTimeString()} солнечно.");
            }
            else if(n > 66)
            {
                Console.WriteLine($"В стране {w.Country} в {DateTime.Now.ToLongTimeString()} выпадают осадки.");
            }
            else
            {
                Console.WriteLine($"В стране {w.Country} в {DateTime.Now.ToLongTimeString()} облачно.");
            }
        }
    }
    class Program
    {
        static public void CountPrime(object num)
        {
            int n = (int)num;
            for (int i = 1; i <= n; i++)
            {
                if((i % 2 != 0 && i%3 != 0 && i%5 != 0 && i%7 != 0 && i%9 != 0) || i == 2 || i==3 || i==5 || i==7)
                {
                    Console.WriteLine(i);
                    Thread.Sleep(200);
                }
            }
            //Console.WriteLine("Thread status: " + Thread.CurrentThread.ThreadState);
        }
        static public void PrintOdd(object num)//Нечетные
        {
            int n = (int)num;
            for (int i = 1; i < n; i++)
            {
                if(i % 2 == 1)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(i + " ");
                    //Thread.Sleep(50);
                    Console.ResetColor();
                }
            }
        }
        static public void PrintEven(object num)//Четные
        {
            int n = (int)num;
            for (int i = 0; i < n; i++)
            {
                if (i % 2 == 0 && i != 1)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(i + " ");
                    //Thread.Sleep(50);
                    Console.ResetColor();
                }
            }
        }
        static object locker1 = new object();
        static bool flagEven = false;
        static bool flagOdd = false;
        static StreamWriter Writer2 = new StreamWriter("evens and odds.txt");
        static public void PrintOdd2(object num)//Нечетные
        {
            Monitor.Enter(locker1, ref flagOdd);
            int n = (int)num;
            for (int i = 1; i < n; i++)
            {
                if (i % 2 == 1)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(i + " ");
                    Writer2.Write(i + " ");
                    Thread.Sleep(20);
                    Console.ResetColor();
                }
            }
            Console.WriteLine();
            Monitor.Exit(locker1);
        }
        static public void PrintEven2(object num)//Четные
        {
            Monitor.Enter(locker1, ref flagEven);
            int n = (int)num;
            for (int i = 0; i < n; i++)
            {
                if (i % 2 == 0 && i != 1)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(i + " ");
                    Writer2.Write(i + " ");
                    Thread.Sleep(20);
                    Console.ResetColor();
                }
            }
            Console.WriteLine();
            Monitor.Exit(locker1);
        }

        static AutoResetEvent waitHandler = new AutoResetEvent(true);
        static StreamWriter Writer3 = new StreamWriter("evens and odds2.txt");
        static public void PrintOdd3(object num)//Нечетные
        {
            int n = (int)num;
            for (int i = 1; i < n; i++)
            {
                if (i % 2 == 1)
                {
                    waitHandler.WaitOne();
                    Thread.Sleep(100);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(i + " ");
                    Writer3.Write(i + " ");
                    Console.ResetColor();
                    waitHandler.Set();
                }
            }
        }
        static public void PrintEven3(object num)//Четные
        {
            int n = (int)num;
            for (int i = 0; i < n; i++)
            {
                if (i % 2 == 0 && i != 1)
                {
                    waitHandler.WaitOne();
                    Thread.Sleep(100);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(i + " ");
                    Writer3.Write(i + " ");
                    Console.ResetColor();
                    waitHandler.Set();
                }
            }
        }

        static Thread ThWhite = new Thread(PrintWhite)
        {
            Priority = ThreadPriority.Highest
        };
        static Thread ThRed = new Thread(PrintRed);
        static object locker = new object();
        static public bool lockerFlagWhite = false;
        static public bool lockerFlagRed = false;
        static public void PrintWhite(object num)
        {
            int n = (int)num;
            Monitor.Enter(locker, ref lockerFlagWhite);
            for (int i = 0; i < n; i++)
            {
                if (i == n / 2)
                {
                    //Console.WriteLine("Middle");
                    Monitor.Exit(locker);
                    ThRed.Join();
                }
                if (i % 2 == 1)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(i);
                    //Thread.Sleep(50);
                    Console.ResetColor();
                }
            }
        }
        static public void PrintRed(object num)
        {
            int n = (int)num;
            Monitor.Enter(locker, ref lockerFlagRed);
            for (int i = 0; i < n; i++)
            {
                if (i % 2 == 0 && i != 1)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.Write(i);
                    //Thread.Sleep(50);
                    Console.ResetColor();
                }
            }
            Monitor.Exit(locker);
        }
        static Random rand = new Random();
        static void Main(string[] args)
        {
            //#1
            var allProcesses = Process.GetProcesses();
            foreach (var proc in allProcesses)
            {
                try
                {
                    Console.WriteLine($"ID: {proc.Id}\nName: {proc.ProcessName}\nPriority: {proc.PriorityClass}\nLaunch Time: {proc.StartTime}\nProcessor Time: {proc.TotalProcessorTime}");
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Message);
                    Console.ResetColor();
                }
                Console.WriteLine();
            }

            //#2
            {
                Console.WriteLine("\n### Работа с доменами ###\n");
                AppDomain appDom = AppDomain.CurrentDomain;
                Console.WriteLine($"Current Domain's Name: {appDom.FriendlyName}\nId: {appDom.Id}");
                Console.WriteLine("\nDomain loaded assemblies:");
                Assembly[] assembs = appDom.GetAssemblies();
                foreach (var assemb in assembs)
                {
                    Console.WriteLine(assemb.ToString());
                }

                AppDomain newDomain = AppDomain.CreateDomain("MyNewDomain");
                newDomain.Load("SayHelloLib");
                Assembly[] loadedDomainAssemb = newDomain.GetAssemblies();
                Console.WriteLine("\nЗагруженные сборки созданного домена:");
                foreach (var assemb in loadedDomainAssemb)
                {
                    Console.WriteLine(assemb.ToString());
                }
                AppDomain.Unload(newDomain);
            }

            //#3
            {

                Console.WriteLine("\n### Работа с отдельным потоком ###\n");

                Thread th = new Thread(CountPrime);
                th.Name = "Поток вывода простых чисел на консоль";
                StreamWriter Writer = new StreamWriter("Primes.txt");
                CountPrimes2 cprimes = new CountPrimes2(Writer, 200);
                Thread th2 = new Thread(cprimes.Count);
                th2.Name = "Поток записи простых чисел в файл";
                th2.Start();
                th.Start(200);
                Thread.Sleep(2000);

                th.Suspend();
                Console.Write("Pausing second thread for a 3 secs ");
                Console.WriteLine(DateTime.Now);
                Console.WriteLine("Thread status: " + th.ThreadState);
                Console.WriteLine("Thread name: " + th.Name);
                Console.WriteLine("Thread priority: " + th.Priority);
                Console.WriteLine("Thread ID: " + th.ManagedThreadId);

                th2.Suspend();
                Writer.Write("Paused input for 3 seconds ");
                Writer.WriteLine(DateTime.Now);
                Writer.WriteLine("Thread status: " + th2.ThreadState);
                Writer.WriteLine("Thread name: " + th2.Name);
                Writer.WriteLine("Thread priority: " + th2.Priority);
                Writer.WriteLine("Thread ID: " + th2.ManagedThreadId);

                Thread.Sleep(3000);

                Console.Write("Continue processing... ");
                Console.WriteLine(DateTime.Now);
                th.Resume();
                Console.WriteLine("Thread status: " + th.ThreadState);

                Writer.Write("Continued input...");
                Writer.WriteLine(DateTime.Now);
                th2.Resume();
                Writer.WriteLine("Thread status: " + th2.ThreadState);

            }

            //#4
            Console.WriteLine("\n### Работа с двумя потоками ###\n");
            Thread ThEven = new Thread(PrintEven);
            Thread ThOdd = new Thread(PrintOdd)
            {
                Priority = ThreadPriority.Lowest
            };
            Console.WriteLine("Различные приоритеты потоков: ");
            ThEven.Start(10000);
            ThOdd.Start(10000);
            while (ThEven.IsAlive || ThOdd.IsAlive)
            {

            }
            Console.WriteLine();

            ThWhite.Start(1198);
            ThRed.Start(598);
            while (ThWhite.IsAlive)
            {

            }
            Console.WriteLine();


            Console.WriteLine("\nСначала четные, потом нечетные.");
            Thread ThEven2 = new Thread(PrintEven2);
            Thread ThOdd2 = new Thread(PrintOdd2);
            ThEven2.Start(200);
            Thread.Sleep(10);
            ThOdd2.Start(200);
            while (ThOdd2.IsAlive)
            {

            }
            Writer2.Close();


            Console.WriteLine("\nОдно четное, одно нечетное.");
            Thread ThEven3 = new Thread(PrintEven3);
            Thread ThOdd3 = new Thread(PrintOdd3);
            ThEven3.Start(100);
            ThOdd3.Start(100);
            while (ThOdd3.IsAlive)
            {

            }
            Console.WriteLine();
            Writer3.Close();


            Console.WriteLine("\nРабота с классом Timer.");
            
            WeatherQuery query = new WeatherQuery("Belarus", new DateTime(), rand);
            TimerCallback timerCallbackDelegate = query.GetWeather;
            //Timer weatherQueryTimer = new Timer(timerCallbackDelegate, query, 1000, 2000);


            Console.ReadLine();
        }
    }
}
