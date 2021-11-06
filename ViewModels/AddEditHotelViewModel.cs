using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public AddEditHotelViewModel(ViewModelNavigationStore viewModelNavigationStore, object hotel)
        {
            NavigateToHotelsViewModelCommand = new RelayCommand(param => viewModelNavigationStore.CurrentViewModel = new HotelsViewModel(viewModelNavigationStore));
            Title = $"Добавление нового отеля";
            if (hotel != null)
            {
                CurrentHotel = Context.Hotel.Find((hotel as Hotel).Id);
                Title = $"Редактирование отеля {CurrentHotel.Name}";
            }
        }

        private void SaveChanges()
        {
            if (HasAnyErrors())
            {
                return;
            }
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

        private bool HasAnyErrors()
        {
            StringBuilder errorsBuilder = new StringBuilder();
            if (string.IsNullOrWhiteSpace(CurrentHotel.Name))
            {
                _ = errorsBuilder.AppendLine("Наименование отеля обязательно для заполнения");
            }
            if (CurrentHotel.Country is null)
            {
                _ = errorsBuilder.AppendLine("Необходимо указать страну отеля");
            }
            if (string.IsNullOrWhiteSpace(CurrentHotel.Description))
            {
                _ = errorsBuilder.AppendLine("Описание отеля обязательно для заполнения");
            }
            if (errorsBuilder.Length > 0)
            {
                UserFeedback = errorsBuilder.ToString();
                return true;
            }
            return false;
        }
    }
}
