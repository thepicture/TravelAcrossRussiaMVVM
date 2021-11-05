using System;
using TravelAcrossRussiaMVVM.Stores;
using TravelAcrossRussiaMVVM.ViewModels;

namespace TravelAcrossRussiaMVVM.Commands
{
    public class NavigateCommand<TViewModel> where TViewModel : ViewModelBase
    {
        private readonly ViewModelNavigationStore _viewModelNavigationStore;
        private readonly Func<TViewModel> _createViewModelFunc;

        public NavigateCommand(ViewModelNavigationStore viewModelNavigationStore, Func<TViewModel> createViewModelFunc)
        {
            _viewModelNavigationStore = viewModelNavigationStore;
            _createViewModelFunc = createViewModelFunc;
            new RelayCommand(() => _viewModelNavigationStore.CurrentViewModel = _createViewModelFunc()).Execute(null);
        }
    }
}
