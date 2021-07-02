using lab10.Views;

namespace lab10
{
    public class GroupItemViewModel
    {
        public Group Group { get; set; }
        public RelayCommand OpenGroupPageCommand { get; set; }
        public GroupItemViewModel(Group g)
        {
            Group = g;

            OpenGroupPageCommand = new RelayCommand(() => MainViewModel.Instance.CurrentPage = new GroupPage(Group));
        }
    }
}
