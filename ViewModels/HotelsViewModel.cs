using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private RelayCommand _deleteHotelCommand;
        private int _hotelsCountPerPage = 10;
        private int _currentPage = 1;
        private int _totalPagesShown = 1;

        public RelayCommand NavigateToToursViewModelCommand { get; }
        public RelayCommand NavigateToAddEditHotelViewModelCommand { get; }

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
            get => _totalPagesShown;

            set
            {
                _totalPagesShown = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand DeleteHotelCommand
        {
            get
            {
                if (_deleteHotelCommand == null)
                {
                    _deleteHotelCommand = new RelayCommand(param => DeleteHotel(param as Hotel));
                }
                return _deleteHotelCommand;
            }
        }

        public HotelsViewModel(ViewModelNavigationStore viewModelNavigationStore)
        {
            NavigateToToursViewModelCommand = new RelayCommand(param => viewModelNavigationStore.CurrentViewModel = new ToursViewModel(viewModelNavigationStore));
            NavigateToAddEditHotelViewModelCommand = new RelayCommand(param => viewModelNavigationStore.CurrentViewModel = new AddEditHotelViewModel(viewModelNavigationStore, param));
            UpdatePagination();
            UpdateTotalPages();
            Title = "Список отелей";
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

        private async void DeleteHotel(Hotel hotel)
        {
            if (hotel.Tour.Any(tour => tour.IsActual))
            {
                UserFeedback = $"Удаление запрещено системой, " +
                    $"отель {hotel.Name} находится " +
                    $"в числе подходящих отелей для актуальных туров.";
                return;
            }
            IsInChoiceMode = true;
            UserFeedback = $"Вы действительно хотите удалить {hotel.Name}?";
            while (UserChoice is null)
            {
                await Task.Delay(100);
            }
            if (UserChoice == true)
            {
                hotel.HotelImage.Clear();
                Context.Hotel.Remove(hotel);
                try
                {
                    Context.SaveChanges();
                    UserFeedback = $"Отель {hotel.Name} успешно удалён!";
                }
                catch (Exception ex)
                {
                    UserFeedback = $"Отель {hotel.Name} не был удалён! " +
                        $"Пожалуйста, попробуйте ещё раз. Ошибка: {ex.Message}";
                }
            }
            else
            {
                UserFeedback = $"Удаление отеля {hotel.Name} отменено!";
            }
            UserChoice = null;
            IsInChoiceMode = false;
            UpdatePagination();
        }
    }
}
