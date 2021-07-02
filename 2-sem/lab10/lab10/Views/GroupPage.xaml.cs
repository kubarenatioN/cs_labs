using System.Windows.Controls;

namespace lab10.Views
{
    /// <summary>
    /// Логика взаимодействия для GroupPage.xaml
    /// </summary>
    public partial class GroupPage : Page
    {
        public GroupPage(Group g)
        {
            InitializeComponent();

            DataContext = new GroupPageViewModel(g);
        }
    }
}
