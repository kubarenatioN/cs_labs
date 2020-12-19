using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace lab10
{
    class Program
    {
        //По дефолту принимает два параметра - объект отправитель и объект с информацией о событии
        static void GamesListChangeHandler(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)//Узнаем тип события
            {
                case NotifyCollectionChangedAction.Add:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Добавлен объект: ");
                    Game gNew = e.NewItems[0] as Game;
                    gNew.Info();
                    Console.ResetColor();
                    break;
                case NotifyCollectionChangedAction.Remove:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Удален объект: ");
                    Game gOld = e.OldItems[0] as Game;
                    gOld.Info();
                    Console.ResetColor();
                    break;
                default:
                    Console.WriteLine("Changes occured!" + e.Action);
                    break;
            }
        }
        static void PrintStack<T>(Stack<T> stack)
        {
            Console.WriteLine("Вывод стека:");
            foreach (T item in stack)
            {
                Console.WriteLine(item);
            }
        }
        static void PrintList<T>(List<T> list)
        {
            Console.WriteLine("Вывод списка:");
            foreach (T item in list)
            {
                Console.WriteLine(item);
            }
        }
        static Random rnd = new Random();
        class Student
        {
            public string Name { get; set; }
            public int Age{ get; set; }
            public Student(string name, int age)
            {
                Name = name;
                Age = age;
            }
            public void Info()
            {
                Console.WriteLine($"Name: {Name}\nAge: {Age}");
            }
        }
        //класс, реализующий интерфейс IComparer для сравнения объектов по жанру
        class GameGenreComparer : IComparer<Game>
        {
            public int Compare(Game g1, Game g2)
            {
                return g1.Genre.CompareTo(g2.Genre);
            }
        }
        //класс, реализующий интерфейс IComparer для сравнения объектов по названию
        class GameNameComparer : IComparer<Game>
        {
            public int Compare(Game g1, Game g2)
            {
                return g1.Name.CompareTo(g2.Name);
            }
        }
        class Game : IComparable<Game>//Нужно реализовать интерфейс, чтобы иметь возможность использовать метод .Sort() у списка
        {
            public string Name { get; set; }
            public string Genre { set; get; }
            public Game(string name, string genre)
            {
                Name = name;
                Genre = genre;
            }
            public void Info()
            {
                Console.WriteLine($"Name: {Name} | Genre: {Genre}");
            }
            public int CompareTo(Game g)//Реализация обычного сравнения по жанру
            {
                return this.Genre.CompareTo(g.Genre);
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("-------- Работа с необобщенной коллекцией:");
            ArrayList myArrList = new ArrayList() {
                rnd.Next(-50, 50),
                rnd.Next(-50, 50),
                rnd.Next(-50, 50),
                rnd.Next(-50, 50),
                rnd.Next(-50, 50)
            };
            myArrList.Insert(2, "Nikita");
            myArrList.Add(new Student("Nikita", 18));
            myArrList.RemoveAt(1);
            Console.WriteLine("Количество элементов в коллекции: " + myArrList.Count);
            foreach (object obj in myArrList)
            {
                Console.WriteLine(obj);
            }
            try
            {
                Console.WriteLine("Find: " + myArrList[myArrList.IndexOf("Nikita")]);
            }
            catch(ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
            }


            Console.WriteLine("-------- Работа с обобщенной коллекцией №1:");

            Stack<char> myCharStack = new Stack<char>();
            char[] symbols = ("ytrewqdsaw").ToCharArray();//Сделали массив символов

            Console.WriteLine("###  Заполняем элементами");
            //Заполнил стек символами автоматически
            foreach (char s in symbols)
            {
                myCharStack.Push(s);
            }

            PrintStack(myCharStack);

            Console.WriteLine("###  Последовательно удаляем");
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine(myCharStack.Pop());
            }

            PrintStack(myCharStack);

            Console.WriteLine("###  Добавляем элементы всеми способами :)");
            myCharStack.Push('a');
            myCharStack.Push('t');
            myCharStack.Push('i');
            myCharStack.Push('k');
            myCharStack.Push('i');
            myCharStack.Push('N');

            PrintStack(myCharStack);



            Console.WriteLine("-------- Работа с обобщенной коллекцией №2:");

            List<char> myCharList = new List<char>();

            Console.WriteLine("###  Заполняем элементами");
            foreach (char s in symbols)
            {
                myCharList.Add(myCharStack.Pop());//Берем элементы из стека и сразу добавляем в список
            }

            PrintList(myCharList);

            Console.WriteLine("Найденный элемент: " + myCharList.Find(x => x == 'q'));//Поиск по предикату


            Console.WriteLine("-------- Работа с коллекцией пользовательского типа");

            List<Game> GameList = new List<Game>();
            //Заполнение
            GameList.AddRange(new Game[]{
                new Game("Dota 2", "MMORPG"),
                new Game("Counter-Strike", "Shooter"),
                new Game("Among Us", "Battle Royale"),
                new Game("Crusader Kings 3", "Strategy"),
                new Game("Skyrim", "RPG"),
            });

            Console.WriteLine("\n  ### Вывод коллекции пользовательского типа");
            foreach (Game g in GameList)
            {
                g.Info();
            }

            Console.WriteLine("  ### Удаление n элементов");
            GameList.RemoveRange(2, 2);

            Console.WriteLine("\n  ### Вывод коллекции пользовательского типа");
            foreach (Game g in GameList)
            {
                g.Info();
            }

            Console.WriteLine("\n  ### Методы добавления в коллекцию");
            GameList.Add(new Game("PUBG", "Battle Royale"));
            Console.WriteLine("\n  ### Вывод коллекции пользовательского типа");
            foreach (Game g in GameList)
            {
                g.Info();
            }

            Console.WriteLine("\n  ### Упорядочить коллекцию по жанрам игр(с помощью IComparable):");
            GameList.Sort();
            Console.WriteLine("\n  ### Вывод коллекции пользовательского типа");
            foreach (Game g in GameList)
            {
                g.Info();
            }

            Console.WriteLine("\n  ### Упорядочить коллекцию по названиям игр(с помощью ICompare):");
            GameList.Sort(new GameNameComparer());//Передаем объект класса, который реализует Comparer
            Console.WriteLine("\n  ### Вывод коллекции пользовательского типа");
            foreach (Game g in GameList)
            {
                g.Info();
            }


            Console.WriteLine("\n-------- Работа с наблюдаемой коллекцией");

            ObservableCollection<Game> observableGames = new ObservableCollection<Game>
            {
                new Game("Crusader Kings 3", "Strategy"),
                new Game("Among Us", "Battle Royale"),
            };
            foreach (Game g in GameList)
            {
                observableGames.Add(g);
            }

            Console.WriteLine("\n  ### Вывод наблюдаемой коллекции");
            foreach (Game item in observableGames)
            {
                item.Info();
            }

            observableGames.CollectionChanged += GamesListChangeHandler;//Стандартное событие CollectionChanged, добавляем обработчик GamesListChangeHandler

            observableGames.RemoveAt(2);
            observableGames.Add(new Game("Mario", "Platformer"));
            observableGames.RemoveAt(0);
            observableGames.Add(new Game("Warcraft", "Strategy"));
            observableGames.RemoveAt(observableGames.Count - 1);

            Console.WriteLine("\n  ### Вывод наблюдаемой коллекции");
            foreach (Game item in observableGames)
            {
                item.Info();
            }

            Console.Read();
        }
    }
}
