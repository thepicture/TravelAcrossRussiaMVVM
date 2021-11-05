using TravelAcrossRussiaMVVM.Stores;

namespace TravelAcrossRussiaMVVM.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ViewModelNavigationStore _viewModelNavigationStore;

        public MainViewModel(ViewModelNavigationStore viewModelNavigationStore)
        {
            _viewModelNavigationStore = viewModelNavigationStore;
            _viewModelNavigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        public ViewModelBase CurrentViewModel => _viewModelNavigationStore.CurrentViewModel;

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        public ViewModelNavigationStore ViewModelNavigationStore
        {
            get => _viewModelNavigationStore; set
            {
                _viewModelNavigationStore = value;
                OnPropertyChanged();
            }
        }
    }
}
