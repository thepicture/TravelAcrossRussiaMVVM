using System.Windows.Controls;
using TravelAcrossRussiaMVVM.Commands;

namespace TravelAcrossRussiaMVVM.Views
{
    /// <summary>
    /// Interaction logic for HotelsView.xaml
    /// </summary>
    public partial class HotelsView : UserControl
    {
        public RelayCommand NavigateToAddEditHotelViewModelCommand { get; }
        public HotelsView()
        {
            InitializeComponent();
        }
    }
}
