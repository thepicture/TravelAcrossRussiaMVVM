namespace TravelAcrossRussiaMVVM.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            CurrentViewModel = new ToursViewModel();
        }

        public ViewModelBase CurrentViewModel { get; set; }
    }
}
