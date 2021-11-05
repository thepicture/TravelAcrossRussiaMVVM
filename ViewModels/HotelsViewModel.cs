using System.Collections.Generic;
using System.Linq;
using TravelAcrossRussiaMVVM.Commands;
using TravelAcrossRussiaMVVM.Models;
using TravelAcrossRussiaMVVM.Stores;

namespace TravelAcrossRussiaMVVM.ViewModels
{
    public class HotelsViewModel : ViewModelBase
    {
        private List<Hotel> _hotelsList;
        private ToursBaseEntities _context;
        public HotelsViewModel(ViewModelNavigationStore viewModelNavigationStore)
        {
            NavigateToToursControlCommand = new RelayCommand(() => viewModelNavigationStore.CurrentViewModel = new ToursViewModel(viewModelNavigationStore));
        }

        public RelayCommand NavigateToToursControlCommand { get; }
        public List<Hotel> HotelsList
        {
            get
            {
                if (_hotelsList == null)
                {
                    _hotelsList = Context.Hotel.ToList();
                }
                return _hotelsList;
            }

            set
            {
                _hotelsList = value;
                OnPropertyChanged();
            }
        }

        public ToursBaseEntities Context
        {
            get
            {
                if (_context == null)
                {
                    _context = new ToursBaseEntities();
                }
                return _context;
            }
            set
            {
                _context = value;
                OnPropertyChanged();
            }
        }
    }
}
