using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab11
{
    class Vector
    {

        public int[] Arr;//массив
        static Random rnd = new Random();
        public Vector()
        {
            Arr = new int[7];
            foreach (var i in Arr)
            {
                Arr[i] = 7;
            }
        }
        public Vector(int a)
        {
            Arr = new int[a];
            for (int i = 0; i < Arr.Length; i++)
            {
                Arr[i] = rnd.Next(-1, 10);
            }
        }
        public int Sum()
        {
            int sum = 0;
            foreach(var i in Arr)
            {
                sum += i;
            }
            return sum;
        }
        public void Print()
        {
            Console.Write("[ ");
            foreach (var i in Arr)
            {
                Console.Write(i + "  ");
            }
            Console.WriteLine("]");
        }
        public void PrintWithModule()
        {
            Console.Write("[ ");
            foreach (var i in Arr)
            {
                Console.Write(i + "  ");
            }
            Console.Write("]");
            Console.WriteLine($" модуль: {this.CountModule()} ");
        }
        public bool HasNull()
        {
            foreach (var i in Arr)
            {
                if (i == 0)
                {
                    return true;
                }
            }
            return false;
        }
        public double CountModule()
        {
            double module = 0;
            foreach (var i in Arr)
            {
                module += Math.Pow(i, 2);
            }
            return Math.Round(Math.Sqrt(module), 2);
        }
    }
    public enum Specs
    {
        ISIT = 1,
        POIT,
        POIBMS,
        DEIVI
    }
    class Student
    {
        public string Name { get; set; }
        public Specs Spec { get; set; }
        public short Course { get; set; }
        public double AverageGrade { get; set; }
        public Student(string name, Specs spec, short course, double avGrade)
        {
            Name = name;
            Spec = spec;
            Course = course;
            AverageGrade = Math.Round(avGrade, 2);
        }
        public void Info()
        {
            Console.WriteLine($"Студент {Name}\t спец.: {Spec}\t курс: {Course}\t ср.балл: {AverageGrade}");
        }
    }
    class Subject
    {
        public Specs Spec { get; set; }
        public string Name { get; set; }
        public Subject(Specs spec, string name)
        {
            Spec = spec;
            Name = name;
        }
    }
    class Program
    {
        //Делегат типа Func WinterSeason содержит предикат (лямбда выражение) на зимние месяцы
        public static Func<string, bool> WinterSeason = month =>
        {
            if (month == "December" || month == "January" || month == "February")
            {
                return true;
            }
            return false;
        };
        public static Func<string, bool> SpringSeason = 
            month => (month == "March" || 
            month == "April" || 
            month == "May") ? (true) : (false);
        public static Func<string, bool> SummerSeason =
            month => (month == "June" ||
            month == "July" ||
            month == "August") ? (true) : (false);
        public static Func<string, bool> AutumnSeason =
            month => (month == "September" ||
            month == "November" ||
            month == "October") ? (true) : (false);
        public enum Months
        {
            Jan,
            Feb,
            Mar,
            Apr,
            May,
            Jun,
            Jul,
            Aug,
            Sept,
            Oct,
            Nov,
            Dec
        }
        static void Main(string[] args)
        {
            string[] months = new string[] { "January", "February", "March", "April", "May", "June",
            "July", "August", "September", "October", "November", "December" };
            var query1 = from month in months
                         where month.Length < 5 //4 for example...
                         select month;

            foreach (var month in query1)
            {
                Console.Write(month + " -> ");
            }
            Console.WriteLine();
            //Через перечисление
            //var query2 = months.Where((month, index) => index == (int)Months.Jan ||
            //index == (int)Months.Feb || index == (int)Months.Dec);
            //Через предикаты
            //WinterSeason or SpringSeason or...
            var query2 = from month in months
                         where SummerSeason(month)
                         select (month);
            foreach (var month in query2)
            {
                Console.Write(month + " -> ");
            }
            Console.WriteLine();

            var query3 = months.OrderBy(month => month);
            foreach (var month in query3)
            {
                Console.Write(month + " -> ");
            }
            Console.WriteLine();

            var query4 = months.Where(month => month.Contains('u') && month.Length > 4);
            foreach (var month in query4)
            {
                Console.Write(month + " -> ");
            }

            Console.WriteLine("\n------ Задание №2");

            List<Vector> Vectors = new List<Vector>();
            Vectors.Add(new Vector(5));
            Vectors.Add(new Vector(3));
            Vectors.Add(new Vector(3));
            Vectors.Add(new Vector(3));
            Vectors.Add(new Vector(4));
            Vectors.Add(new Vector(7));
            Console.WriteLine("*** Исходные векторы:");
            foreach(var vector in Vectors)
            {
                vector.Print();
            }
            var vectorsQuery1 = Vectors.Where(vector => vector.HasNull()).Count();
            Console.WriteLine("\nКоличество векторов содержащих 0: " + vectorsQuery1);

            var vectorsQuery2 = Vectors.OrderBy(vector => vector.CountModule()).Take(3);
            Console.WriteLine("\nСписок векторов с наименьшим модулем: ");
            foreach (var vector in vectorsQuery2)
            {
                vector.PrintWithModule();
            }

            Console.WriteLine("\nСписок векторов длины 3: ");
            var vectorsQuery3 = from vector in Vectors
                                where vector.Arr.Length == 3
                                select vector;
            foreach (var vector in vectorsQuery3)
            {
                vector.Print();
            }

            var vectorsQuery4 = Vectors.OrderByDescending(vector => vector.CountModule()).First();
            Console.WriteLine("\nМаксимальный вектор: ");
            vectorsQuery4.PrintWithModule();


            Console.WriteLine("\nПервый вектор с отрицательными элементами: ");
            if (Vectors.Any(vector => vector.Arr.Any(item => item < 0)))//Есть ли векторы с отрицательными значениями?
            {
                var vectorsQuery5 = Vectors.First(vector => vector.Arr.Any(item => item < 0));
                vectorsQuery5.Print();
            }
            else
            {
                Console.WriteLine("Ни один из векторов не содержит отрицательные числа.");
            }


            Console.WriteLine("\nУпорядоченный список векторов по размеру: ");
            var vectorsQuery6 = Vectors.OrderBy(vector => vector.Arr.Length);
            foreach (var vector in vectorsQuery6)
            {
                vector.Print();
            }


            Console.WriteLine("\n------ Задание №3");
            List<Student> FitStudents = new List<Student>();
            FitStudents.Add(new Student("Kubarko Nikita", Specs.DEIVI, 2, 42.234/5));
            FitStudents.Add(new Student("Kubarko Nikita", Specs.DEIVI, 3, 44.234/5));
            FitStudents.Add(new Student("Shereshik Ilya", Specs.DEIVI, 2, 43/4.61));
            FitStudents.Add(new Student("Karlenok Yura", Specs.POIT, 4, 43.4352/4.12));
            FitStudents.Add(new Student("Drugov Anton", Specs.POIBMS, 2, 41.435/3.356));
            FitStudents.Add(new Student("Pisarik Andrei", Specs.ISIT, 2, 42/4.45));
            FitStudents.Add(new Student("Bich Pavel", Specs.POIBMS, 1, 42/4.672));
            FitStudents.Add(new Student("Hramava Liza", Specs.DEIVI, 3, 41/4.456));
            FitStudents.Add(new Student("Kravchenko Anna", Specs.DEIVI, 2, 42.34/4.345));
            FitStudents.Add(new Student("Kravtsova Diana", Specs.POIT, 3, 44.786/5));
            FitStudents.Add(new Student("Stashevskaya Irina", Specs.ISIT, 4, 44.23/5));
            FitStudents.Add(new Student("Sveklo Anna", Specs.DEIVI, 4, 42.75/5));
            FitStudents.Add(new Student("Borodina Liza", Specs.POIT, 4, 43.54/5));

            List<Subject> FitSubjects = new List<Subject>();
            FitSubjects.Add(new Subject(Specs.POIT, "Java"));
            FitSubjects.Add(new Subject(Specs.DEIVI, "3D Modeling"));

            var takeDeiviHigherCourses = FitStudents
                .Where(student =>
                (student.Spec == Specs.DEIVI ||
                student.Spec == Specs.ISIT) &&
                student.Course >= 2)
                .Except(FitStudents.Where(student => student.Spec == Specs.ISIT))
                .Union(FitStudents.Where(student => student.Spec == Specs.POIT))
                .Join(FitSubjects,
                student => student.Spec,
                subject => subject.Spec,
                (student, subject) => new {
                    student.Name,
                    student.Spec,
                    student.Course,
                    student.AverageGrade,
                    Subject = subject.Name,//Добавляем поле в объекты выборки
                })
                .OrderBy(student => student.Name)
                .ThenBy(student => student.AverageGrade);

            foreach (var student in takeDeiviHigherCourses)
            {
                Console.WriteLine($"Студент {student.Name}\t" +
                    $" спец.: {student.Spec}\t " +
                    $"курс: {student.Course}\t " +
                    $"ср.балл: {student.AverageGrade}\t " +
                    $"предмет: {student.Subject}");
            }


            Console.Read();
        }
    }
}
