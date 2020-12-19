using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace lab12
{
    interface IStudying
    {
        void Study();
    }
    class Student : IComparable<Student>, IStudying
    {
        public string id = null;
        public string Name { get; set; }
        public string Spec { get; set; }
        public short Course { get; set; }
        public double AverageGrade { get; set; }
        public Student(string name, string spec, short course, double avGrade)
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
        private string GetPassword()
        {
            return "777";
        }
        public int CompareTo(Student student)
        {
            return this.Name.CompareTo(student.Name);
        }
        public void Study()
        {
            Console.Write(this.Name + ": ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("I am studying!");
            Console.ResetColor();
        }
        public Game WhatGame(Game g)
        {
            return g;
        }
    }
    class Game
    {
        public int version = 0;
        public string Name { get; set; }
        public string Genre { set; get; }
        public Game(string name, string genre)
        {
            Name = name;
            Genre = genre;
        }
        private string GetRootData()
        {
            return "42";
        }
        public void Info(int a, string str, bool isSomething)
        {
            Console.WriteLine($"Название игры: {Name}, жанр: {Genre}");
        }
        public Student WhoPlays(Student stud)
        {
            return stud;
        }
        public void IsLaunched(bool b)
        {
            if(b) Console.WriteLine("launched");
        }
    }
    class Reflection
    {
        public void InputClassInfo(Type type)
        {
            string fileName = "info about class";
            MemberInfo[] members = type.GetMembers();//Получаем все члены класса
            try
            {
                StreamWriter Writer = new StreamWriter($"{fileName}.txt");
                foreach (MemberInfo member in members)
                {
                    Writer.WriteLine($"Тип: {member.DeclaringType} -> {member.MemberType} {member.Name}");
                }
                Writer.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine("Error");
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.WriteLine("Success");
            }
        }
        public void GetPublicMethods(Type type)
        {
            Console.WriteLine("\nИнформация об общедоступных методах типа:");
            MethodInfo[] methods = type.GetMethods();
            foreach (MethodInfo method in methods)
            {
                Console.WriteLine($"Метод {method.ReturnType} {method.Name}");
            }
        }
        public void GetFieldsAndProps(Type type)
        {
            Console.WriteLine("\nИнформация о полях и свойствах:");
            FieldInfo[] fields = type.GetFields();
            foreach (FieldInfo field in fields)
            {
                Console.WriteLine($"Поле {field.Name} типа {field.FieldType}");
            }
            PropertyInfo[] props = type.GetProperties();
            foreach (PropertyInfo prop in props)
            {
                Console.WriteLine($"Свойство {prop.Name} типа {prop.PropertyType}");
            }
        }
        public void GetInterfaces(Type type)
        {
            Console.WriteLine("\nИнформация об интерфейсах:");
            Type[] interfaces = type.GetInterfaces();
            foreach (var i in interfaces)
            {
                Console.WriteLine($"Интерфейс класса {type}: {i.GetType()} {i.Name}");
            }
        }
        public void OutputMethods(Type type, Type typeToCompare)
        {
            MethodInfo[] methods = type.GetMethods();
            foreach (var method in methods)
            {
                ParameterInfo[] parameters = method.GetParameters();
                foreach (var parameter in parameters)
                {
                    if (parameter.ParameterType == typeToCompare)
                    {
                        Console.WriteLine($"Метод {method.ReturnType} {method.Name}");
                        break;
                    }
                }
            }
        }
        public void GetMethodsWithParameter(Type type)
        {
            Console.WriteLine("Выберите тип параметра:");
            Console.WriteLine("1 -- int");
            Console.WriteLine("2 -- string");
            Console.WriteLine("3 -- bool");
            Console.WriteLine("4 -- Student");
            Console.WriteLine("5 -- Game");
            short choice = Convert.ToInt16(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    OutputMethods(type, typeof(int));
                    break;
                case 2:
                    OutputMethods(type, typeof(string));
                    break;
                case 3:
                    OutputMethods(type, typeof(bool));
                    break;
                case 4:
                    OutputMethods(type, typeof(Student));
                    break;
                case 5:
                    OutputMethods(type, typeof(Game));
                    break;
                default:
                    Console.WriteLine("Nothing founded...");
                    break;
            }
        }
        public void ExecuteMethod(Type type, string methodName)
        {
            Type t = null;
            try
            {
                //Подгружаем сбокру по пути с помощью класса Assembly
                Assembly asm = Assembly.LoadFrom("../../../Game Class/bin/Debug/Game Class.dll");

                //Определяем интересующий тип из сборки
                t = asm.GetType("Game_Class.Game", true, true);

            }
            catch(Exception e)
            {
                Console.WriteLine("Type is not founded");
                Console.WriteLine(e.Message);
            }

            //Создаем экземпляр класса t с помощью класса Activator
            object GameInstance = Activator.CreateInstance(t);
            
            //Получаем метод класса по имени из строки аргумента
            MethodInfo method = t.GetMethod(methodName);
            //Параметры метода
            //foreach(ParameterInfo parameter in method.GetParameters())
            //{
            //    Console.WriteLine(parameter.Name);
            //}

            //Считываем параметры для метода из файла
            try
            {
                StreamReader Reader = new StreamReader("method params.txt");
                Queue<string> methodParams = new Queue<string>();
                string line;
                while((line = Reader.ReadLine()) != null)
                {
                    methodParams.Enqueue(line);
                }
                Reader.Close();
                //Объект параметров из очереди
                object[] parameters = methodParams.ToArray();

                //Вызываем метод
                method.Invoke(GameInstance, parameters);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error");
                Console.WriteLine(e.Message);
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Reflection reflection = new Reflection();

            reflection.InputClassInfo(typeof(Student));//typeof(Game)

            reflection.GetPublicMethods(typeof(Game));

            reflection.GetFieldsAndProps(typeof(Student));

            //Тестовый лист объектов
            List<Student> students = new List<Student>() {
                new Student("Nikita", "DEIVI", 2, 10),
                new Student("Lera", "DEIVI", 1, 6),
                new Student("Yura", "POIT", 4, 2),
                new Student("Den", "POIBMS", 1, 6),
                new Student("Liza", "DEIVI", 3, 5),
            };
            //foreach (Student s in students)
            //{
            //    s.Info();
            //}
            //Проверка работы реализации интерфейса IComparable
            //students.Sort();
            //foreach (Student s in students)
            //{
            //    s.Info();
            //}

            Console.WriteLine();
            students[0].Study();

            Console.WriteLine();
            reflection.GetInterfaces(typeof(Student));

            Console.WriteLine();
            reflection.GetMethodsWithParameter(typeof(Student));

            Console.WriteLine("\nПозднее связывание и чтение параметров из файла");
            reflection.ExecuteMethod(typeof(Game), "Login");

            Console.Read();
        }
    }
}
