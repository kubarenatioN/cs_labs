using lab10.Views;
using System.Collections.Generic;
using System.Linq;

namespace lab10
{
    public class GroupPageViewModel
    {
        public RelayCommand GoBackCommand { get; set; }
        public List<Student> StudentsCollection { get; set; }
        public GroupPageViewModel(Group group)
        {

            GoBackCommand = new RelayCommand(() =>
            {
                MainViewModel.Instance.History.Pop();
                MainViewModel.Instance.CurrentPage = MainViewModel.Instance.History.Pop();
            });

            StudentsCollection = MainViewModel.Instance.StudentsRepository.Get(s => s.GroupId == group.Id);
        }
    }
}
