using System.Windows.Controls;

namespace lab10
{
    /// <summary>
    /// Логика взаимодействия для SpecItemControl.xaml
    /// </summary>
    public partial class SpecItemControl : UserControl
    {
        public SpecItemControl(Spec s)
        {
            InitializeComponent();

            DataContext = new SpecItemViewModel(s);
        }
    }
}
