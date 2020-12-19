using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kontra
{
    interface IBank
    {
        double GetValue(int sum);
    }
    class Bank : IBank
    {
        public virtual double GetValue(int sum)
        {
            return (double)sum * 0.9;
        }
    }
    class Deposit : Bank, IBank
    {
        public override double GetValue(int sum)
        {
            return base.GetValue(sum) * 0.1;
        }
    }
    class Point3D : IComparable<Point3D>
    {
        int x;
        int y;
        int z;
        public Point3D(int a, int b, int c)
        {
            x = a;
            y = b;
            z = c;
        }
        public void Print()
        {
            Console.WriteLine($"({x}, {y}, {z})");
        }
        public int CompareTo(Point3D obj)
        {
            if (this.x > obj.x)
            {
                return 1;
            }
            else if (this.x == obj.x)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            long l1 = 110;
            object obj = l1;//Упаковка
            long l2 = (long)obj;//Распаковка в long
            //Console.WriteLine($"object: {obj}");
            //Console.WriteLine($"long: {l2}");
            Console.WriteLine("Введите строку");
            char[] userStr = (Console.ReadLine()).ToCharArray();
            char[] str1;
            char[] str2;
            if (userStr.Length % 2 == 1)
            {
                str1 = new char[(userStr.Length - 1) / 2];
                str2 = new char[(userStr.Length + 1) / 2];
                for (int i = 0; i < (userStr.Length - 1) / 2; i++)
                {
                    str1[i] = userStr[i];
                }
                int j = 0;
                for (int i = (userStr.Length - 1) / 2; i < userStr.Length; i++)
                {
                    str2[j] = userStr[i];
                    j++;
                }
            }
            else
            {
                str1 = new char[userStr.Length / 2];
                str2 = new char[userStr.Length / 2];
                for (int i = 0; i < userStr.Length / 2 ; i++)
                {
                    str1[i] = userStr[i];
                }
                int j = 0;
                for (int i = userStr.Length / 2; i < userStr.Length; i++)
                {
                    str2[j] = userStr[i];
                    j++;
                }
            }
            Console.WriteLine(new string(str1));
            Console.WriteLine(new string(str2));
            Console.WriteLine(String.Concat(new string(str1), new string(str2)));

            
            int?[,] mas = new int?[,]
            {
                { 1,2,3 },
                { 4,null,6}
            };
            int counter = 0;
            foreach (int? m1 in mas)
            {
                if(m1 == null)
                {
                    counter++;
                }
            }
            Console.WriteLine(counter);

            Console.WriteLine("#########");
            Point3D point1 = new Point3D(1, 2, 3); 
            Point3D point2 = new Point3D(5, 4, -1);
            Point3D point3 = new Point3D(1, -7, 9);
            point1.Print();
            point2.Print();
            Console.WriteLine($"Сравниваем point1 и point2");
            if (point1.CompareTo(point2) == 1)
            {
                Console.WriteLine("Больше");
            }
            else if (point1.CompareTo(point2) == 0)
            {
                Console.WriteLine("Равен");
            }
            else
            {
                Console.WriteLine("Меньше");
            }

            Console.WriteLine($"Сравниваем point1 и point3");
            if (point1.CompareTo(point3) == 1)
            {
                Console.WriteLine("Больше");
            }
            else if (point1.CompareTo(point3) == 0)
            {
                Console.WriteLine("Равен");
            }
            else
            {
                Console.WriteLine("Меньше");
            }

            Bank bank1 = new Bank();
            Deposit deposit1 = new Deposit();
            Console.WriteLine(bank1.GetValue(100));
            Console.WriteLine(deposit1.GetValue(100));

            Console.Read();
        }
    }
}
