using lab9.EntityFramework;
using lab9.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace lab9
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private List<Team> teamsCollection;
        private Team selectedTeam;
        private Page currentPage;

        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        public static MainViewModel Instance { get; set; }
        public Repository<Team> TeamsRepository { get; set; }
        public Repository<Player> PlayersRepository { get; set; }
        public List<Team> TeamsCollection
        {
            get { return teamsCollection; }
            set
            {
                if (teamsCollection == value) return;
                teamsCollection = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(TeamsCollection)));
            }
        }
        public Page CurrentPage
        {
            get { return currentPage; }
            set
            {
                if (currentPage == value) return;
                currentPage = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(CurrentPage)));
            }
        }
        public Team SelectedTeam
        {
            get { return selectedTeam; }
            set
            {
                if (selectedTeam == value) return;
                selectedTeam = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(SelectedTeam)));
                
                if (SelectedTeam != null)
                {
                    CurrentPage = new TeamPage(SelectedTeam);
                }
                else
                {
                    CurrentPage = null;
                }
            }
        }
        public RelayCommand AddTeamCommand { get; set; }
        public RelayCommand RemoveTeamCommand { get; set; }   
        public RelayCommand EditTeamCommand { get; set; }
        public RelayCommand ExecuteTransactionCommand { get; set; }
        public RelayCommand AsyncWorkCommand { get; set; }

        public MainViewModel()
        {
            Instance = this;
            Lab9DbContext cntxt = new Lab9DbContext();
            PlayersRepository = new Repository<Player>(cntxt);
            TeamsRepository = new Repository<Team>(cntxt);

            TeamsCollection = new List<Team>();

            AddTeamCommand = new RelayCommand(() =>
            {
                Console.Write("Before NewTeamWindow Open: ");
                Console.WriteLine(Repository<Team>.context.GetHashCode());
                NewTeamWindow newTeamWindow = new NewTeamWindow();

                // Show window modally
                // NOTE: Returns only when window is closed
                bool? dialogResult = newTeamWindow.ShowDialog();

                if (dialogResult == true)
                {
                    Console.Write("After NewTeamWindow Closed: ");
                    Console.WriteLine(Repository<Team>.context.GetHashCode());
                    Team t = new Team()
                    {
                        Name = newTeamWindow.TeamName.Text,
                        Region = newTeamWindow.TeamRegion.Text
                    };
                    TeamsRepository.Create(t);
                    UpdateTeamsList();
                    //TeamsRepository.Update(t);

                    SelectedTeam = t;
                }
            });

            RemoveTeamCommand = new RelayCommand(() =>
            {
                if(SelectedTeam != null)
                {
                    TeamsRepository.Remove(SelectedTeam);
                    CurrentPage = null;
                    UpdateTeamsList();
                }
            });

            EditTeamCommand = new RelayCommand(() =>
            {
                Team teamToChange = SelectedTeam;
                // Instantiate window
                NewTeamWindow newTeamWindow = new NewTeamWindow();

                newTeamWindow.TeamName.Text = SelectedTeam.Name;
                newTeamWindow.TeamRegion.Text = SelectedTeam.Region;

                // Show window modally
                // NOTE: Returns only when window is closed
                bool? dialogResult = newTeamWindow.ShowDialog();

                if (dialogResult == true)
                {
                    SelectedTeam.Name = newTeamWindow.TeamName.Text;
                    SelectedTeam.Region = newTeamWindow.TeamRegion.Text;

                    TeamsRepository.Update(SelectedTeam);
                    UpdateTeamsList();
                    CurrentPage = new TeamPage(SelectedTeam);
                }
            });

            ExecuteTransactionCommand = new RelayCommand(() =>
            {
                Database db = Repository<Team>.context.Database;
                using (DbContextTransaction transaction = db.BeginTransaction())
                {
                    try
                    {
                        db.ExecuteSqlCommand(@"insert into Teams (Name, Region) values('transaction' + space(1) + cast(CURRENT_TIMESTAMP as nvarchar(20)), 'transaction')");
                        decimal id = db.SqlQuery<decimal>(@"select SCOPE_IDENTITY()").FirstOrDefault();

                        db.ExecuteSqlCommand(@"insert into Players (Name, Nickname, TeamId) values('p' + space(1) + cast(CURRENT_TIMESTAMP as nvarchar(20)), 'p', @id)", 
                            new SqlParameter("@id", id));

                        id = db.SqlQuery<decimal>(@"select @@IDENTITY").FirstOrDefault();
                        db.ExecuteSqlCommand(@"update Players set Nickname = 'p_updated' where Id = @id",
                            new SqlParameter("@id", id));

                        Repository<Team>.context.SaveChanges();
                        //Console.WriteLine(id);

                        // ################################### 
                        // ################################### 
                        // ################################### 
                        // Uncomment it to demonstrate transaction rollback
                        throw new Exception("Custom error");

                        transaction.Commit();

                        UpdateTeamsList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);

                        transaction.Rollback();
                    }
                }
            });

            AsyncWorkCommand = new RelayCommand(async () =>
            {
                InsertOperation(5000);
                InsertOperation(1000);
            });

            UpdateTeamsList();
        }

        public void UpdateTeamsList()
        {
            TeamsCollection = new List<Team>(TeamsRepository.GetAllTeamsWithPlayers());
        }

        public async Task InsertOperation(int milliseconds)
        {
            await Task.Delay(milliseconds);

            Database db = Repository<Team>.context.Database;
            
            int id = db.SqlQuery<int>(@"insert into Teams (Name, Region) output inserted.Id values('delay1', 'd')").FirstOrDefault();
            Repository<Team>.context.SaveChanges();

            UpdateTeamsList();

            Console.WriteLine($"Добавлен элемент с id {id.ToString()}\nЗа время {milliseconds}ms");
        }
    }
}
