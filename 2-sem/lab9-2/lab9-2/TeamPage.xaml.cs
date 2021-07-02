using lab9.EntityFramework;
using lab9.Views;
using System.Windows.Controls;

namespace lab9
{
    /// <summary>
    /// Логика взаимодействия для Team.xaml
    /// </summary>
    public partial class TeamPage : Page
    {
        public TeamPage(Team t)
        {
            InitializeComponent();

            DataContext = new TeamViewModel(t);
        }
    }
}
