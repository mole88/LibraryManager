using LibraryManager.Model;
using System.Collections.ObjectModel;
using LibraryManager.Client.Core;
using System.Windows;

namespace LibraryManager.Client.ViewModel
{
    public class VisitorsPageViewModel : ObservableObject
    {
        private Manager _manager;

        //TODO: Нормально реализовать связь
        public ReadOnlyObservableCollection<Visitor> Visitors => _manager.Visitors;

        private Visitor _selectedVisitor;

        public Visitor SelectedVisitor
        {
            get { return _selectedVisitor; }
            set
            {
                _selectedVisitor = value;
                OnPropertyChanged(nameof(SelectedVisitor));
            }
        }
        public VisitorsPageViewModel()
        {
            _manager = ManagerInstance.Instance;

            EditCommand = new RelayCommand((o) =>
            {
                if (SelectedVisitor != null)
                {
                    MessageBox.Show($"Edit: {SelectedVisitor.FullName}");
                }
            });

            DeleteCommand = new RelayCommand((o) =>
            {
                if (SelectedVisitor != null)
                {
                    MessageBox.Show($"Delete: {SelectedVisitor.FullName}");
                }
            });

            AddCommand = new RelayCommand((o) =>
            {
                MessageBox.Show($"Add");
            });

            FindCommand = new RelayCommand((o) =>
            {
                MessageBox.Show($"Find");
            });

            SortCommand = new RelayCommand((o) =>
            {
                MessageBox.Show($"Sort");
            });
        }
        public RelayCommand EditCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand AddCommand { get; set; }
        public RelayCommand FindCommand { get; set; }
        public RelayCommand SortCommand { get; set; }
    }
}
