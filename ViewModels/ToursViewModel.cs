using System.Collections.Generic;
using System.Linq;
using TravelAcrossRussiaMVVM.Models;

namespace TravelAcrossRussiaMVVM.ViewModels
{
    public class ToursViewModel : ViewModelBase
    {
        private ToursBaseEntities _context;
        private List<Tour> _toursList;

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
    }
}
