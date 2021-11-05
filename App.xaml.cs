using System.Windows;
using TravelAcrossRussiaMVVM.Stores;
using TravelAcrossRussiaMVVM.ViewModels;

namespace TravelAcrossRussiaMVVM
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            ViewModelNavigationStore viewModelNavigationStore = new ViewModelNavigationStore();
            viewModelNavigationStore.CurrentViewModel = new ToursViewModel(viewModelNavigationStore);
            MainWindow = new MainView
            {
                DataContext = new MainViewModel(viewModelNavigationStore)
            };
            MainWindow.Show();
            base.OnStartup(e);
        }
    }
}
