using lab9.EntityFramework;
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
using System.Windows.Shapes;

namespace lab9.Views
{
    /// <summary>
    /// Логика взаимодействия для NewTeamWindow.xaml
    /// </summary>
    public partial class NewTeamWindow : Window
    {
        private Repository<Team> teamRepository;
        public NewTeamWindow()
        {
            InitializeComponent();

            //Console.WriteLine(MainViewModel.Instance.TeamsRepository.dbSet.GetHashCode());
            //teamRepository = new Repository<Team>(new Lab9DbContext()); // ЭТА СТРОЧКА УБИЛА МЕНЯ НА ТРОЕ СУТОК
            //Console.WriteLine(MainViewModel.Instance.TeamsRepository.dbSet.GetHashCode());
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            //teamRepository.Create(new Team()
            //{
            //    Name = TeamName.ToString(),
            //    Region = TeamRegion.ToString()
            //});
            if(TeamName.Text != "" && TeamRegion.Text != "")
            {
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Заполните данные");
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
