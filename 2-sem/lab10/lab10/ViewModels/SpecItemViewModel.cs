using lab10.Views;

namespace lab10
{
    public class SpecItemViewModel
    {
        public Spec Spec { get; set; }
        public RelayCommand OpenSpecPageCommand { get; set; }

        public SpecItemViewModel(Spec spec)
        {
            Spec = spec;

            OpenSpecPageCommand = new RelayCommand(() =>
            {
                MainViewModel.Instance.CurrentPage = new SpecGroupsPage(Spec.Id);
            });
        }
    }
}
