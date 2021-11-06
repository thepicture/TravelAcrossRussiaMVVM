using System;
using System.Collections.Generic;
using System.Linq;
using TravelAcrossRussiaMVVM.Commands;
using TravelAcrossRussiaMVVM.Models;
using TravelAcrossRussiaMVVM.Stores;

namespace TravelAcrossRussiaMVVM.ViewModels
{
    public class AddEditHotelViewModel : ViewModelBase
    {
        private RelayCommand _navigateToHotelsViewModelCommand;
        private RelayCommand _saveCommand;
        private ToursBaseEntities _context;
        private Hotel _currentHotel;
        private List<Country> _countriesList;
        private string _userFeedback;
        private RelayCommand _closeFeedbackCommand;

        public AddEditHotelViewModel(ViewModelNavigationStore viewModelNavigationStore, object hotel)
        {
            NavigateToHotelsViewModelCommand = new RelayCommand(param => viewModelNavigationStore.CurrentViewModel = new HotelsViewModel(viewModelNavigationStore));
            if (hotel != null)
            {
                CurrentHotel = Context.Hotel.Find((hotel as Hotel).Id);
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

        public Hotel CurrentHotel
        {
            get
            {
                if (_currentHotel == null)
                {
                    _currentHotel = new Hotel();
                }
                return _currentHotel;
            }

            set
            {
                _currentHotel = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand NavigateToHotelsViewModelCommand
        {
            get => _navigateToHotelsViewModelCommand; set
            {
                _navigateToHotelsViewModelCommand = value;
                OnPropertyChanged();
            }
        }

        public List<Country> CountriesList
        {
            get
            {
                if (_countriesList == null)
                {
                    _countriesList = Context.Country.ToList();
                }
                return _countriesList;
            }

            set
            {
                _countriesList = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand(param => SaveChanges());
                }
                return _saveCommand;
            }
        }

        public string UserFeedback
        {
            get => _userFeedback;

            set
            {
                _userFeedback = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand CloseFeedbackCommand
        {
            get
            {
                if (_closeFeedbackCommand == null)
                {
                    _closeFeedbackCommand = new RelayCommand(param => UserFeedback = string.Empty);
                }
                return _closeFeedbackCommand;
            }
        }

        private void SaveChanges()
        {
            if (CurrentHotel.Id == 0)
            {
                _ = Context.Hotel.Add(CurrentHotel);
            }
            try
            {
                _ = Context.SaveChanges();
                UserFeedback = "Изменения успешно сохранены!";
            }
            catch (Exception ex)
            {
                UserFeedback = $"Не удалось {(CurrentHotel.Id == 0 ? "добавить" : "отредактировать")} отель. " +
                    $"Пожалуйста, попробуйте ещё раз. Ошибка: " + ex.Message;
            }
        }
    }
}
