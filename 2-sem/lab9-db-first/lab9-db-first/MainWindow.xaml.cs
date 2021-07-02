using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace lab9_db_first
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            using (lab9dbfirstEntities db = new lab9dbfirstEntities())
            {
                List<Player> plrs = db.Players.ToList();
                List<Team> tms = db.Teams.ToList();

                dataGrid.ItemsSource = tms.Join(plrs, 
                    t => t.Id, 
                    p => p.TeamId, 
                    (t, p) => new
                    {
                        Id = t.Id,
                        TName = t.Name,
                        TRegion = t.Region,
                        PName = p.Name,
                        PNickname = p.Nickname,
                        PTeamId = p.TeamId
                    });
            }
        }
    }
}
