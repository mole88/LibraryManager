using LibraryManager.Client.Core;
using LibraryManager.Model;
using System.Collections.ObjectModel;

namespace LibraryManager.Client.ViewModel.VisitorsViewModels
{
    class FindVisitorDialogViewModel : ObservableObject
    {
        private Manager _manager;
        public FindVisitorDialogViewModel(ObservableCollection<Visitor> table)
        {
            _manager = ManagerInstance.Instance;
            FindVisitorCommand = new RelayCommand((o) =>
            {
                if (int.TryParse(VisitorId, out int id))
                {
                    Visitor foundVisitor = _manager.FindVisitor(id);
                    table.Clear();
                    table.Add(foundVisitor);
                    CancelCommand.Execute(o);
                }
            });
        }
        private string _visitorId;
        public string VisitorId
        {
            get => _visitorId;
            set
            {
                _visitorId = value;
                OnPropertyChanged(nameof(VisitorId));
            }
        }
        public RelayCommand FindVisitorCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
    }
}
