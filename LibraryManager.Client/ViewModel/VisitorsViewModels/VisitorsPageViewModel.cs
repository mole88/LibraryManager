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
            RefrashVisitors();
            _manager.Visitors.CollectionChanged += (s, e) => RefrashVisitors();

            EditCommand = new RelayCommand(async (o) =>
            {
                if (SelectedVisitor != null)
                {
                    EditEvent?.Invoke(this, new ObjEventArgs(SelectedVisitor));
                }
            });

            DeleteCommand = new RelayCommand(async (o) =>
            {
                if (SelectedVisitor != null)
                {
                    DeleteEvent?.Invoke(this, new ObjEventArgs(SelectedVisitor));
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
            
            VisitorBooksCommand = new RelayCommand((o) =>
            {
                FilterBooksEvent?.Invoke(this, new ObjEventArgs(SelectedVisitor));
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
        private void RefrashVisitors()
        {
            Visitors = new ObservableCollection<Visitor>(
                _manager.Visitors
                    .OrderBy(v => v.Id)
            );
        }
        public RelayCommand EditCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand AddCommand { get; set; }
        public RelayCommand FindCommand { get; set; }
        public RelayCommand VisitorBooksCommand { get; set; }
        public RelayCommand SortCommand { get; set; }
        public RelayCommand RefrashTableCommand { get; set; }

        public event EventHandler AddEvent;
        public event EventHandler<ObjEventArgs> EditEvent;
        public event EventHandler<ObjEventArgs> DeleteEvent;
        public event EventHandler FindEvent;
        public event EventHandler<ObjEventArgs> FilterBooksEvent;

    }
}
