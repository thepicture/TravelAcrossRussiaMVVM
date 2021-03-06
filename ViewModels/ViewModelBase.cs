using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TravelAcrossRussiaMVVM.Commands;

namespace TravelAcrossRussiaMVVM.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _title = "";
        private string _userFeedback;
        private RelayCommand _closeFeedbackCommand;
        private bool? _userChoice;
        private bool _isInChoiceMode = false;

        public string UserFeedback
        {
            get => _userFeedback;

            set
            {
                _userFeedback = value;
                OnPropertyChanged();
            }
        }

        public bool? UserChoice
        {
            get => _userChoice; set
            {
                _userChoice = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand CloseFeedbackCommand
        {
            get
            {
                if (_closeFeedbackCommand == null)
                {
                    _closeFeedbackCommand = new RelayCommand(param =>
                    {
                        UserFeedback = string.Empty;
                        if (param != null)
                        {
                            UserChoice = Convert.ToBoolean(param);
                        }
                    });
                }
                return _closeFeedbackCommand;
            }
        }

        public string Title
        {
            get => _title; set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        public bool IsInChoiceMode
        {
            get => _isInChoiceMode; set
            {
                _isInChoiceMode = value;
                OnPropertyChanged();
            }
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
