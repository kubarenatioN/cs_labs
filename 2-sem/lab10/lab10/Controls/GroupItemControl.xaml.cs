using System.Windows.Controls;

namespace lab10
{
    /// <summary>
    /// Логика взаимодействия для GroupItemControl.xaml
    /// </summary>
    public partial class GroupItemControl : UserControl
    {
        public GroupItemControl(Group g)
        {
            InitializeComponent();

            DataContext = new GroupItemViewModel(g);
        }
    }
}
