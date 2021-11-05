using System;
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
        private RelayCommand _pageFirstCommand;
        private RelayCommand _pagePreviousCommand;
        private RelayCommand _pageNextCommand;
        private RelayCommand _pageLastCommand;
        private int _hotelsCountPerPage = 10;
        private int _currentPage = 1;
        private int _totalPagesShown = 1;
        public HotelsViewModel(ViewModelNavigationStore viewModelNavigationStore)
        {
            NavigateToToursControlCommand = new RelayCommand(param => viewModelNavigationStore.CurrentViewModel = new ToursViewModel(viewModelNavigationStore));
            UpdatePagination();
            UpdateTotalPages();
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

        public RelayCommand PageFirstCommand
        {
            get
            {
                if (_pageFirstCommand == null)
                {
                    _pageFirstCommand = new RelayCommand(param => CurrentPage = 1);
                }
                return _pageFirstCommand;
            }
        }
        public RelayCommand PagePreviousCommand
        {
            get
            {
                if (_pagePreviousCommand == null)
                {
                    _pagePreviousCommand = new RelayCommand(param => CurrentPage--);
                }
                return _pagePreviousCommand;
            }
        }
        public RelayCommand PageNextCommand
        {
            get
            {
                if (_pageNextCommand == null)
                {
                    _pageNextCommand = new RelayCommand(param => CurrentPage++);
                }
                return _pageNextCommand;
            }
        }
        public RelayCommand PageLastCommand
        {
            get
            {
                if (_pageLastCommand == null)
                {
                    _pageLastCommand = new RelayCommand(param => CurrentPage = TotalPagesShown);
                }
                return _pageLastCommand;
            }
        }
        public int HotelsCountPerPage
        {
            get => _hotelsCountPerPage;
            set
            {
                if (!int.TryParse(value.ToString(), out _))
                {
                    return;
                }
                _hotelsCountPerPage = value;
                if (_hotelsCountPerPage < 1)
                {
                    _hotelsCountPerPage = 1;
                }
                UpdateTotalPages();
                CurrentPage = 1;
                OnPropertyChanged();
            }
        }

        private void UpdatePagination()
        {
            HotelsList = Context.Hotel
                .ToList()
                .Skip((CurrentPage - 1) * HotelsCountPerPage)
                .Take(HotelsCountPerPage)
                .ToList();
        }

        private void UpdateTotalPages()
        {
            TotalPagesShown = Convert.ToInt32(Math.Ceiling(Context.Hotel.ToList().Count * 1.0 / HotelsCountPerPage));
        }

        public int CurrentPage
        {
            get => _currentPage; set
            {
                _currentPage = value;
                if (_currentPage < 1)
                {
                    _currentPage = 1;
                }
                if (_currentPage > TotalPagesShown)
                {
                    _currentPage = TotalPagesShown;
                }
                UpdatePagination();
                OnPropertyChanged();
            }
        }

        public int TotalPagesShown
        {
            get => _totalPagesShown; set
            {
                _totalPagesShown = value;
                OnPropertyChanged();
            }
        }
    }
}
