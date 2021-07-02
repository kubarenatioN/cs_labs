using System.Collections.Generic;
using System.ComponentModel;

namespace lab10
{
    public class SpecsViewModel : INotifyPropertyChanged
    {
        private List<SpecItemControl> specItemsCollection;
        private Repository<Spec> specsRepository;
        public List<SpecItemControl> SpecItemsCollection
        {
            get { return specItemsCollection; }
            set
            {
                if (specItemsCollection == value) return;
                specItemsCollection = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(SpecItemsCollection)));
            }
        }
        public SpecsViewModel()
        {
            specsRepository = MainViewModel.Instance.SpecsRepository;

            SpecItemsCollection = new List<SpecItemControl>();

            List<Spec> specs = specsRepository.GetAll();
            foreach (Spec s in specs)
            {
                SpecItemsCollection.Add(new SpecItemControl(s));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
    }
}
