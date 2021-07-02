using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Delicious
{
    /// <summary>
    /// Логика взаимодействия для LoginWindowPage.xaml
    /// </summary>
    public partial class LoginWindowPage : Page
    {
        public LoginWindow LoginWindow { get; set; }

        public LoginWindowPage(LoginWindow w)
        {
            InitializeComponent();

            LoginWindow = w;
        }

        private void OpenLoginPage(object sender, RoutedEventArgs e)
        {
            LoginWindow.loginFrame.Content = new LoginPage(LoginWindow);
        }

        private void OpenSignPage(object sender, RoutedEventArgs e)
        {
            LoginWindow.loginFrame.Content = new SignPage(LoginWindow);
        }
    }
}
