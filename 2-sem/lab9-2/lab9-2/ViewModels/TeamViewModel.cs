using lab9.EntityFramework;
using lab9.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;

namespace lab9
{
    public class TeamViewModel : INotifyPropertyChanged
    {
        private Team team;
        private List<Player> teamPlayers;
        private Player selectedPlayer;
        //private Repository<Player> playersRepository;
        //private Repository<Team> teamsRepository;

        public RelayCommand AddPlayerCommand { get; set; }
        public RelayCommand EditPlayerCommand { get; set; }
        public RelayCommand RemovePlayerCommand { get; set; }

        public List<Player> TeamPlayers
        {
            get { return teamPlayers; }
            set
            {
                if (teamPlayers == value) return;
                teamPlayers = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(TeamPlayers)));
            }
        }
        public Player SelectedPlayer
        {
            get { return selectedPlayer; }
            set
            {
                if (selectedPlayer == value) return;
                selectedPlayer = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(SelectedPlayer)));
            }
        }
        public Team Team
        {
            get { return team; }
            set
            {
                if (team == value) return;
                team = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Team)));
            }
        }

        public TeamViewModel(Team t)
        {
            //playersRepository = MainViewModel.Instance.PlayersRepository;
            //teamsRepository = MainViewModel.Instance.TeamsRepository;

            Team = t;
            TeamPlayers = Team.Players.ToList();

            AddPlayerCommand = new RelayCommand(() =>
            {
                NewPlayerWindow newPlayerWindow = new NewPlayerWindow();

                // Show window modally
                // NOTE: Returns only when window is closed
                bool? dialogResult = newPlayerWindow.ShowDialog();

                if (dialogResult == true)
                {
                    Player p = new Player()
                    {
                        Name = newPlayerWindow.PlayerName.Text,
                        Nickname = newPlayerWindow.PlayerNickname.Text,
                        TeamId = Team.Id
                    };
                    playersRepository.Create(p);

                    UpdateTeamPlayers();
                }
            });

            EditPlayerCommand = new RelayCommand(() =>
            {
                if (SelectedPlayer == null) {
                    MessageBox.Show("Ничего не выбрано");
                    return;
                }

                EditPlayerWindow editPlayerWindow = new EditPlayerWindow();
                editPlayerWindow.PlayerName.Text = SelectedPlayer.Name;
                editPlayerWindow.PlayerNickname.Text = SelectedPlayer.Nickname;
                editPlayerWindow.PlayerTeamId.Text = SelectedPlayer.TeamId.ToString();

                bool? dialogResult = editPlayerWindow.ShowDialog();

                if (dialogResult == true)
                {
                    int newId = int.Parse(editPlayerWindow.PlayerTeamId.Text);
                    Team newTeam = teamsRepository.Get(team => team.Id == newId).SingleOrDefault();
                    if (newTeam == null)
                    {
                        MessageBox.Show("Такой команды не существует\nПроверьте Id");
                        return;
                    }

                    if(newId != SelectedPlayer.TeamId)
                    {
                        SelectedPlayer.TeamId = newId;
                        SelectedPlayer.Team = null;
                    }
                    SelectedPlayer.Name = editPlayerWindow.PlayerName.Text;
                    SelectedPlayer.Nickname = editPlayerWindow.PlayerNickname.Text;
                    
                    playersRepository.Update(SelectedPlayer);

                    UpdateTeamPlayers();
                }
            });

            RemovePlayerCommand = new RelayCommand(() =>
            {
                if (SelectedPlayer == null) return;

                playersRepository.Remove(SelectedPlayer);

                // Update list of teams to get fresh info about all teams
                MainViewModel.Instance.UpdateTeamsList();
                // load fresh info about players in selected team
                TeamPlayers = playersRepository.GetPlayersWithTeams(player => player.TeamId == Team.Id).ToList();
            });
        }

        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        public void UpdateTeamPlayers()
        {
            // Update list of teams to get fresh info about all teams
            MainViewModel.Instance.UpdateTeamsList();

            // load fresh info about players in selected team
            TeamPlayers = playersRepository.GetPlayersWithTeams(player => player.TeamId == Team.Id).ToList();

            // set selected team as currently opened TeamPage Team object
            MainViewModel.Instance.SelectedTeam = teamsRepository.GetTeamsWithPlayers(team => team.Id == Team.Id).SingleOrDefault();
        }
    }
}
