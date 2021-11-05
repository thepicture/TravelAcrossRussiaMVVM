using System;
using TravelAcrossRussiaMVVM.ViewModels;

namespace TravelAcrossRussiaMVVM.Stores
{
    public class ViewModelNavigationStore
    {
        private ViewModelBase _currentViewModel;
        public event Action CurrentViewModelChanged;

        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel; set
            {
                _currentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }
}
