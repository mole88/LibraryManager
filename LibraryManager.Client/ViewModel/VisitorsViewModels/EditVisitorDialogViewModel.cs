using LibraryManager.Client.Core;
using LibraryManager.Model;

namespace LibraryManager.Client.ViewModel.VisitorsViewModels
{
    public class EditVisitorDialogViewModel : ObservableObject
    {
        private Manager _manager;

        public EditVisitorDialogViewModel(Visitor editedVisitor)
        {
            _manager = ManagerInstance.Instance;

            VisitorName = editedVisitor.FullName;
            VisitorAge = editedVisitor.Age.ToString();
            VisitorPhone = editedVisitor.PhoneNumber;

            EditVisitorCommand = new RelayCommand(async (o) =>
            {
                if (!string.IsNullOrEmpty(VisitorPhone) && !string.IsNullOrEmpty(VisitorName) && int.TryParse(VisitorAge, out int age))
                {
                    Visitor newVisitor = new()
                    {
                        Id = editedVisitor.Id,
                        FullName = VisitorName,
                        Age = age,
                        PhoneNumber = VisitorPhone,
                    };
                    await _manager.EditVisitorAsync(editedVisitor, newVisitor);
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
        public RelayCommand EditVisitorCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
    }
}
