using lab10.Views;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;

namespace lab10
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private Page currentPage;
        public Stack<Page> History { get; set; } = new Stack<Page>();
        public Page CurrentPage
        {
            get { return currentPage; }
            set
            {
                if (currentPage == value) return;
                currentPage = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(CurrentPage)));
                History.Push(CurrentPage);
            }
        }

        public static MainViewModel Instance { get; set; }
        public Repository<Group> GroupsRepository { get; set; }
        public Repository<Spec> SpecsRepository { get; set; }
        public Repository<Student> StudentsRepository { get; set; }
        public MainWindow MainWindow { get; set; }

        public RelayCommand OpenGroupsPageCommand { get; set; }
        public RelayCommand OpenSpecsPageCommand { get; set; }

        public MainViewModel(MainWindow w)
        {
            MainWindow = w;
            Instance = this;

            FacultyDbContext context = new FacultyDbContext();
            GroupsRepository = new Repository<Group>(context);
            SpecsRepository = new Repository<Spec>(context);
            StudentsRepository = new Repository<Student>(context);

            OpenGroupsPageCommand = new RelayCommand(() => CurrentPage = new GroupsPage());
            OpenSpecsPageCommand = new RelayCommand(() => CurrentPage = new SpecsPage());
        }

        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
    }
}
