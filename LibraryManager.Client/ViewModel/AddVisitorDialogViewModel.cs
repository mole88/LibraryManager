using LibraryManager.Client.Core;
using LibraryManager.Client.SupportClasses;
using LibraryManager.Model;

namespace LibraryManager.Client.ViewModel
{
    public class AddVisitorDialogViewModel : ObservableObject
    {
        private Manager _manager;
        
        public AddVisitorDialogViewModel()
        {
            _manager = ManagerInstance.Instance;
            AddVisitorCommand = new RelayCommand((o) =>
            {
                if (!string.IsNullOrEmpty(VisitorPhone) && !string.IsNullOrEmpty(VisitorName) && VisitorAge != 0)
                {
                    Visitor newVisitor = new Visitor()
                    {
                        FullName = VisitorName,
                        Age = VisitorAge,
                        PhoneNumber = VisitorPhone
                    };
                    _manager.AddVisitor(newVisitor);
                    ClearDialog();
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

        private int _visitorAge;
        public int VisitorAge
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
        private void ClearDialog()
        {
            VisitorName = "";
            VisitorAge = 0;
            VisitorPhone = "";
        }
    }
}
