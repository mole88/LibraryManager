using LibraryManager.Client.Core;
using LibraryManager.Model;

namespace LibraryManager.Client.ViewModel.VisitorsViewModels
{
    public class AddVisitorDialogViewModel : ObservableObject
    {
        private Manager _manager;

        public AddVisitorDialogViewModel()
        {
            _manager = ManagerInstance.Instance;
            AddVisitorCommand = new RelayCommand((o) =>
            {
                if (!string.IsNullOrEmpty(VisitorPhone) && !string.IsNullOrEmpty(VisitorName) && int.TryParse(VisitorAge, out int age))
                {
                    Visitor newVisitor = new()
                    {
                        Id = UniqueIDMaker.GetUniqueID(_manager.Visitors),
                        FullName = VisitorName,
                        Age = age,
                        PhoneNumber = VisitorPhone,
                    };
                    _manager.AddVisitor(newVisitor);
                    CancelCommand.Execute(o);
                }
            });
        }
        private string _visitorName;
        public string VisitorName
        {
            get => _visitorName;
            set
            {
                _visitorName = value;
                OnPropertyChanged(nameof(VisitorName));
            }
        }
        private string _visitorPhone;
        public string VisitorPhone
        {
            get => _visitorPhone;
            set
            {
                _visitorPhone = value;
                OnPropertyChanged(nameof(VisitorPhone));
            }
        }

        private string _visitorAge;
        public string VisitorAge
        {
            get => _visitorAge;
            set
            {
                _visitorAge = value;
                OnPropertyChanged(nameof(VisitorAge));
            }
        }

        public RelayCommand AddVisitorCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
    }
}
