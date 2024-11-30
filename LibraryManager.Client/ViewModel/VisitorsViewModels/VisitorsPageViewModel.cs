using LibraryManager.Model;
using System.Collections.ObjectModel;
using LibraryManager.Client.Core;
using System.Windows;
using LibraryManager.Client.SupportClasses;
using static System.Reflection.Metadata.BlobBuilder;

namespace LibraryManager.Client.ViewModel.VisitorsViewModels
{
    public class VisitorsPageViewModel : ObservableObject
    {
        private Manager _manager;

        private ObservableCollection<Visitor> _visitors;
        public ObservableCollection<Visitor> Visitors
        {
            get { return _visitors; }
            set
            {
                _visitors = value;
                OnPropertyChanged(nameof(Visitors));
            }
        }

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
            Visitors = new(_manager.Visitors);
            _manager.Visitors.CollectionChanged += (s, e) =>
            {
                Visitors = new(_manager.Visitors);
            };

            EditCommand = new RelayCommand(async (o) =>
            {
                if (SelectedVisitor != null)
                {
                    EditEvent?.Invoke(this, new EditEventArgs(SelectedVisitor));
                }
            });

            DeleteCommand = new RelayCommand(async (o) =>
            {
                if (SelectedVisitor != null)
                {
                    await _manager.RemoveVisitorAsync(SelectedVisitor);
                }
            });

            AddCommand = new RelayCommand((o) =>
            {
                AddEvent?.Invoke(this, EventArgs.Empty);
            });

            FindCommand = new RelayCommand((o) =>
            {
                FindEvent?.Invoke(this, EventArgs.Empty);
            });

            RefrashTableCommand = new RelayCommand((o) =>
            {
                Visitors = new(_manager.Visitors);
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
        public RelayCommand RefrashTableCommand { get; set; }

        public event EventHandler AddEvent;
        public event EventHandler<EditEventArgs> EditEvent;
        public event EventHandler FindEvent;

    }
}
