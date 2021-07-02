using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Delicious
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginWindow LoginWindow { get; set; }

        private Regex usernameRegex = new Regex(@"^[A-Za-z]\w{4,20}$");
        private Regex passwordRegex = new Regex(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{8,20}$");
        private Regex userRealNameRegex = new Regex(@"^[A-Za-zА-Яа-я][a-zа-я]{0,20}$");

        public LoginPage(LoginWindow w)
        {
            InitializeComponent();

            LoginWindow = w;
        }

        private void LoginUser(object sender, RoutedEventArgs e)
        {
            string _username = username.Text;
            string _password = password.Password;
            string _userRealName = userRealName.Text;

            username.BorderBrush = (SolidColorBrush)(new BrushConverter()).ConvertFrom("#FFCDEACE");
            password.BorderBrush = (SolidColorBrush)(new BrushConverter()).ConvertFrom("#FFCDEACE");
            userRealName.BorderBrush = (SolidColorBrush)(new BrushConverter()).ConvertFrom("#FFCDEACE");

            bool usernameMatch = usernameRegex.IsMatch(_username);
            bool passwordMatch = passwordRegex.IsMatch(_password);
            bool userRealNameMatch = userRealNameRegex.IsMatch(_userRealName);

            if (!usernameMatch)
            {
                username.BorderBrush = Brushes.Red;
            }
            if (!passwordMatch)
            {
                password.BorderBrush = Brushes.Red;
            }
            if (!userRealNameMatch)
            {
                userRealName.BorderBrush = Brushes.Red;
            }

            if (usernameMatch && passwordMatch && userRealNameMatch)
            {
                List<Users> alreadyUsers = new List<Users>();
                using(DeliciousEntities context = new DeliciousEntities())
                {
                    alreadyUsers = context.Users.Where(user => user.Username == _username && user.Password == _password).ToList();
                }
                if(alreadyUsers.Count != 0)
                {
                    MessageBox.Show("Пользователь с таким логином и паролем уже есть.");
                }
                else
                {
                    Users newUser = new Users()
                    {
                        Username = _username,
                        Password = _password,
                        Name = _userRealName
                    };
                    using (DeliciousEntities context = new DeliciousEntities())
                    {
                        context.Users.Add(newUser);
                        context.SaveChanges();
                    }
                    MainWindow mainWindow = new MainWindow(newUser);
                    mainWindow.Show();
                    LoginWindow.Close();
                }
            }
        }

        private void BackPage(object sender, RoutedEventArgs e)
        {
            LoginWindow.loginFrame.GoBack();
        }
    }
}
