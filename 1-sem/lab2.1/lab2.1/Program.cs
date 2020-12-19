using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("---TASK1---");
            int number = 7;
            Object o = number;//o ссылается на number
            float s = (float)(int)o;

            char ch = 'q';
            Object chO = ch;//упаковка
            ch = 'v';
            char str = (char)chO;
            Console.WriteLine(ch);
            Console.WriteLine(str);

            double d = 42.42;
            Object dObj = d;//упаковка
            d = d - 0.42;//упакованный объект все равно не изменится
            float f = (float)(double)dObj;
            Console.WriteLine(d);
            Console.WriteLine(f);

            Console.WriteLine("\n---TASK2---");

            int myNum = 6;
            byte byteNum1 = (byte)myNum;
            float fNum1 = myNum;
            double dNum1 = myNum;
            Console.WriteLine($"From int: {myNum}, {byteNum1}, {fNum1}, {dNum1}.");

            double bigNum = 6.66;
            byte byteNum2 = (byte)myNum;
            float fNum2 = (float)bigNum;
            Console.WriteLine($"From double: {bigNum}, {byteNum2}, {fNum2}.");

            char chNum = 'N';
            int chToInt = chNum;
            float chToFloat = chNum;
            Console.WriteLine($"From char: {chNum}, {chToInt}, {chToFloat}.");

            Console.WriteLine("\n---TASK3---");

            string myName = "Nikita";
            string mySurname = "Kubarko";
            Console.WriteLine(String.Format("My name is {0} {1}", myName, mySurname));
            Console.WriteLine($"My name is {myName} {mySurname}");

            Console.WriteLine("\n---TASK4---");
            float someNumber = (float)9.99;
            bool someBool = true;
            string myStr = $"Моя строка плюс число.ТуСтринг() {someNumber.ToString()}";
            Console.WriteLine(someNumber.ToString());
            Console.WriteLine(myStr);
            Console.WriteLine(someBool.ToString());
            Console.WriteLine(someNumber.GetHashCode());
            Console.WriteLine(someNumber.ToString().GetType());
            Console.WriteLine(someBool.GetType());
            double someObj1 = 5.55;
            double someObj2 = 5.55;
            double someObj3 = 7.77;
            Console.WriteLine(someObj1.Equals(someObj2));
            Console.WriteLine(someObj1.Equals(someObj3));

            Console.WriteLine("\n---TASK5---");
            string myFirstStr = "Mike";
            string mySecondStr = "Peterson";
            Console.WriteLine(String.Compare(myFirstStr, mySecondStr));//return 1 if str1[0] > st2[0] else if str1[0] < str2[0] return -1 else return 0
            Console.WriteLine($"Содержится? {myFirstStr.Contains(mySecondStr)}");
            Console.WriteLine(mySecondStr.Substring(0, mySecondStr.Length - 3));
            Console.WriteLine(myFirstStr.Insert(myFirstStr.Length, mySecondStr));
            Console.WriteLine(myFirstStr.Replace(myFirstStr, "Nick"));

            Console.WriteLine("\n---TASK6---");
            Console.WriteLine($"Пуста ли строка? {String.IsNullOrEmpty(myFirstStr)}");

            Console.WriteLine("\n---TASK7---");
            //неявно типизированная переменная
            var somevar = "this is string";
            //somevar = 17; <- error
            //то же самое, что и string str = 5;

            Console.WriteLine("\n---TASK8---");
            int? nullableNumber = 17;
            if (nullableNumber.HasValue)
            {
                Console.WriteLine($"Nullable var has value {nullableNumber}");
            }
            else
            {
                Console.WriteLine("No Value in var");
            }

            Console.WriteLine("\n---TUPLES---");
            (int a, string b) tuple = (5, "hi there!");
            Console.WriteLine(tuple.a);
            Console.WriteLine(tuple.b);

            Console.WriteLine();
            GetTupleStr((18, "Nick"));

            Console.WriteLine();
            (int age, string about, bool isStudent) tupleAboutMe = CreateDescr(18);
            Console.WriteLine($"{tupleAboutMe.age}\n{tupleAboutMe.about}\n{tupleAboutMe.isStudent}");

            Console.WriteLine();
            var somet = ((float)188.4, "зеленый");
            (float height1, string eyesColor1) = somet;
            var (height2, eyesColor2) = somet;
            float height3 = 0;
            var eyesColor3 = "";
            (height3, eyesColor3) = somet;
            Console.WriteLine($"Мой рост {height1}см, цвет глаз {eyesColor1}");
            Console.WriteLine($"Мой рост {height2}см, цвет глаз {eyesColor2}");
            Console.WriteLine($"Мой рост {height3}см, цвет глаз {eyesColor3}");

            Console.WriteLine();

            try
            {
                // The following line raises an exception because it is checked.
                checkedFunc();
            }
            catch (System.OverflowException e)
            {
                // The following line displays information about the error.
                Console.WriteLine("CHECKED and CAUGHT:  " + e.ToString());
            }
            uncheckedFunc();
        }

        private static void GetTupleStr((int age, string name) tuple)
        {
            Console.WriteLine($"Hello, my name is {tuple.name}, I'am {tuple.age} y.o.");
        }
        private static (int, string, bool) CreateDescr(int age)
        {
            string about = $"Здравствуйте, мне {age} лет";
            bool isStudent = true;
            return (age, about, isStudent);
        }
        private static void checkedFunc()
        {
            checked
            {
                int a = 2147483647;
                Console.WriteLine(a + 10);
            }
        }
        private static void uncheckedFunc()
        {
            int a = 2147483647;
            Console.WriteLine(a + 10);
        }
    }
}
