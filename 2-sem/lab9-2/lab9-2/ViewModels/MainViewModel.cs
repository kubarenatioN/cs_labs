﻿using lab9.EntityFramework;
using lab9.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
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
        //public Repository<Team> TeamsRepository { get; set; }
        //public Repository<Player> PlayersRepository { get; set; }
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

        public MainViewModel()
        {
            Instance = this;
            LabDbContext cntxt = new LabDbContext();
            //PlayersRepository = new Repository<Player>(cntxt);
            //TeamsRepository = new Repository<Team>(cntxt);

            TeamsCollection = new List<Team>();

            AddTeamCommand = new RelayCommand(() =>
            {
                NewTeamWindow newTeamWindow = new NewTeamWindow();

                // Show window modally
                // NOTE: Returns only when window is closed
                bool? dialogResult = newTeamWindow.ShowDialog();

                if (dialogResult == true)
                {
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

            UpdateTeamsList();
        }

        public void UpdateTeamsList()
        {
            TeamsCollection = new List<Team>(TeamsRepository.GetAllTeamsWithPlayers());
        }
    }
}
