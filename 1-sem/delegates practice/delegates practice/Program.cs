using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace delegates_practice
{
    class Program
    {
        class AccountEventArgs
        {
            public string Message { get; }
            public int Sum { get; }
            public AccountEventArgs(string mes, int sum)
            {
                Message = mes;
                Sum = sum;
            }
            class Account
            {
                public delegate void AccHandler(object sender, AccountEventArgs e);
                public event AccHandler Print;
                public int Sum { get; private set; }
                public Account(int sum)
                {
                    Sum = sum;
                }
                public void Put(int sum)
                {
                    Sum += sum;
                    Print?.Invoke(this, new AccountEventArgs($"На счет поступило {sum}", sum));
                }
                public void Take(int sum)
                {
                    if (sum <= Sum)
                    {
                        Sum -= sum;
                        Print?.Invoke(this, new AccountEventArgs($"Со счета снято {sum}", sum));
                    }
                    else
                    {
                        Print?.Invoke(this, new AccountEventArgs($"На счете недостаточно средств.", Sum));
                    }
                }
            }
            private static void DisplayMessage(object sender, AccountEventArgs e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine($"На счете {e.Sum}");
            }
            private static void DisplayRedMessage(string message)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(message);
                Console.ResetColor();
            }
            static void Main(string[] args)
            {
                Account acc = new Account(100);
                acc.Print += DisplayMessage;
                acc.Put(20);
                acc.Take(50);
                acc.Take(200);




                Console.Read();
            }
        }
    }
}
