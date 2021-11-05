using System.Collections.Generic;
using System.Linq;
using TravelAcrossRussiaMVVM.Commands;
using TravelAcrossRussiaMVVM.Models;
using TravelAcrossRussiaMVVM.Stores;

namespace TravelAcrossRussiaMVVM.ViewModels
{
    public class ToursViewModel : ViewModelBase
    {
        private ToursBaseEntities _context;
        private List<Tour> _toursList;
        private string _searchText;
        private List<Type> _typesOfTour;
        private Type _selectedTypeOfTour;
        private bool areOnlyActualTours = true;
        private decimal _totalToursPrice;
        private List<string> _toursOrderTypes;
        private string _selectedToursOrderType;

        public ToursViewModel(ViewModelNavigationStore viewModelNavigationStore)
        {
            NavigateToHotelsControlCommand = new RelayCommand(param => viewModelNavigationStore.CurrentViewModel = new HotelsViewModel(viewModelNavigationStore));
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
            set => _context = value;
        }

        public List<Tour> ToursList
        {
            get
            {
                if (_toursList == null)
                {
                    _toursList = Context.Tour.ToList();
                }
                return _toursList;
            }

            set
            {
                _toursList = value;
                OnPropertyChanged();
            }
        }

        public string SearchText
        {
            get => _searchText; set
            {
                _searchText = value;
                FilterTours();
                OnPropertyChanged();
            }
        }
        public List<Type> TypesOfTour
        {
            get
            {
                if (_typesOfTour == null)
                {
                    _typesOfTour = new List<Type>
                    {
                        new Type
                        {
                            Name = "Все типы"
                        },
                    };
                    _typesOfTour.AddRange(Context.Type.ToList());
                    SelectedTypeOfTour = _typesOfTour.First();
                }
                return _typesOfTour;
            }

            set
            {
                _typesOfTour = value;
                OnPropertyChanged();
            }
        }
        public bool AreOnlyActualTours
        {
            get => areOnlyActualTours; set
            {
                areOnlyActualTours = value;
                FilterTours();
                OnPropertyChanged();
            }
        }

        public Type SelectedTypeOfTour
        {
            get => _selectedTypeOfTour; set
            {
                _selectedTypeOfTour = value;
                FilterTours();
                OnPropertyChanged();
            }
        }

        public decimal TotalToursPrice
        {
            get => _totalToursPrice; set
            {
                _totalToursPrice = value;
                OnPropertyChanged();
            }
        }

        public List<string> ToursOrderTypes
        {
            get
            {
                if (_toursOrderTypes == null)
                {
                    _toursOrderTypes = new List<string>
                    {
                        "Без сортировки",
                        "По убыванию",
                        "По возрастанию"
                    };
                }
                SelectedToursOrderType = _toursOrderTypes.First();
                return _toursOrderTypes;
            }

            set
            {
                _toursOrderTypes = value;
                OnPropertyChanged();
            }
        }

        public string SelectedToursOrderType
        {
            get => _selectedToursOrderType;

            set
            {
                _selectedToursOrderType = value;
                FilterTours();
                OnPropertyChanged();
            }
        }

        public RelayCommand NavigateToHotelsControlCommand { get; }

        private void FilterTours()
        {
            List<Tour> currentTours = Context.Tour.ToList();
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                currentTours = currentTours
                    .Where(tour => tour.Name.ToLower().Contains(SearchText.ToLower()))
                    .ToList();
            }
            if (SelectedTypeOfTour.Name != "Все типы")
            {
                currentTours = currentTours
                    .Where(tour => tour.Type.Select(type => type.Id).Contains(SelectedTypeOfTour.Id))
                    .ToList();
            }
            if (areOnlyActualTours)
            {
                currentTours = currentTours.Where(tour => tour.IsActual).ToList();
            }
            if (SelectedToursOrderType != "Без сортировки")
            {
                switch (SelectedToursOrderType)
                {
                    case "По убыванию":
                        currentTours = currentTours.OrderByDescending(t => t.Price).ToList();
                        break;
                    case "По возрастанию":
                        currentTours = currentTours.OrderBy(t => t.Price).ToList();
                        break;
                }
            }
            TotalToursPrice = currentTours.Sum(tour => tour.Price * tour.TicketCount);
            ToursList = currentTours;
        }
    }
}
