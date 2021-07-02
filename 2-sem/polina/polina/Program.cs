using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//создать лкаассUser с закрытыми полями логин и пасворд 
//добавте свойства для изменния этих значений.
//переопределить в классе все public методы object. 
//перегрузить метод CompareTo стандартного унаследованого интерфейса IComparable,который сравнивает пользователей по логину.
//Сощдать три  пользователя сравнить их.
//Создать LinkedList<User> с 5ю потзователями.
//испольхуя LINQ найти коллекции пользователей, у который длинная пароля меньше 6 и содержит только циФРЫ

namespace polina
{
    class Program
    {
        static void Main(string[] args)
        {
            User u1 = new User("qwe", "123");
            User u2 = new User("asd", "456");
            User u4 = new User("asd", "654");
            User u3 = new User("zxc", "789");
            User u5 = new User("try", "111qqq");
            Console.WriteLine(u1.CompareTo(u2));
            Console.WriteLine(u1.CompareTo(u2.Login));
            Console.WriteLine(u2.CompareTo(u4.Login));

            LinkedList<User> list = new LinkedList<User>();
            list.AddFirst(u1);
            list.AddFirst(u2);
            list.AddFirst(u3);
            list.AddFirst(u4);
            list.AddFirst(u5);
            List<User> users = list.Where(user => user.Password.Length < 6 && IsDigitsOnly(user.Password)).ToList();
            foreach (User user in users)
            {
                Console.WriteLine(user.Login);
            }

            Console.ReadLine();
        }

        public static bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
    }

    class User : IComparable
    {
        private string login;
        private string password;

        public string Login
            {
            get { return login; }
            set { login = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public User(string l, string p)
        {
            Login = l;
            Password = p;
        }

        public int CompareTo(object obj)
        {
            return Login.CompareTo((object)((User)obj).Login);
        }

        public int CompareTo(string userLogin) // Перегрузка метода CompareTo
        {
            return Login.CompareTo(userLogin);
        }

        public override bool Equals(object obj)
        {
            bool isObjetsEquals = login == ((User)obj).login && password == ((User)obj).password;
            return isObjetsEquals;
        }

        public override int GetHashCode()
        {
            char[] chars = (login + password).ToCharArray();
            return chars[0]; // числовое представление первого символа будет хеш-кодом объекта
        }

        public override string ToString()
        {
            return login + password;
        }
    }
}
