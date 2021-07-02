using lab10.Views;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace lab10
{
    public class SpecGroupsPageViewModel : INotifyPropertyChanged
    {
        private List<GroupItemControl> groupItemsCollection;
        private Repository<Group> groupsRepository;

        public string SpecName { get; set; }

        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        public RelayCommand GoBackCommand { get; set; }

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

        public SpecGroupsPageViewModel(int id)
        {
            groupsRepository = MainViewModel.Instance.GroupsRepository;

            SpecName = MainViewModel.Instance.SpecsRepository.Get(s => s.Id == id).First().Name;

            List<Group> groups = groupsRepository.Get(g => g.SpecId == id);

            GroupItemsCollection = new List<GroupItemControl>();
            foreach (Group g in groups)
            {
                GroupItemsCollection.Add(new GroupItemControl(g));
            }


            GoBackCommand = new RelayCommand(() =>
            {
                MainViewModel.Instance.History.Pop();
                MainViewModel.Instance.CurrentPage = MainViewModel.Instance.History.Pop();
            });
        }
    }
}
