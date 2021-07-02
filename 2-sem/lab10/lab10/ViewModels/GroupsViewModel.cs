using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace lab10
{
    public class GroupsViewModel : INotifyPropertyChanged
    {
        private List<Group> groupsCollection;
        private List<GroupItemControl> groupItemsCollection;
        private Repository<Group> groupsRepository;

        public List<Group> GroupsCollection
        {
            get { return groupsCollection; }
            set
            {
                if (groupsCollection == value) return;
                groupsCollection = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(GroupsCollection)));
            }
        }
        public List<GroupItemControl> GroupItemsCollection
        {
            get { return groupItemsCollection; }
            set
            {
                if (groupItemsCollection == value) return;
                groupItemsCollection = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(GroupItemsCollection)));
            }
        }
        public GroupsViewModel()
        {
            groupsRepository = MainViewModel.Instance.GroupsRepository;

            GroupsCollection = groupsRepository.GetAll();

            GroupItemsCollection = new List<GroupItemControl>();
            foreach (Group g in GroupsCollection)
            {
                GroupItemsCollection.Add(new GroupItemControl(g));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
    }
}
