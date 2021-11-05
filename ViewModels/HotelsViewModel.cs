using TravelAcrossRussiaMVVM.Commands;
using TravelAcrossRussiaMVVM.Stores;

namespace TravelAcrossRussiaMVVM.ViewModels
{
    public class HotelsViewModel : ViewModelBase
    {
        public HotelsViewModel(ViewModelNavigationStore viewModelNavigationStore)
        {
            NavigateToToursControlCommand = new RelayCommand(() => viewModelNavigationStore.CurrentViewModel = new ToursViewModel(viewModelNavigationStore));
        }

        public RelayCommand NavigateToToursControlCommand { get; }
    }
}
