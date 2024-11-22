using LibraryManager.Model;
using LibraryManager.Client.Core;
using System.Windows;
using LibraryManager.Client.SupportClasses;

namespace LibraryManager.Client.ViewModel
{
    public class MainViewVM : ObservableObject
    {
        private Manager _manager;
        public MainViewVM()
        {
            try
            {
                _manager = ManagerInstance.Instance;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            BooksVM = new BooksPageViewModel();
            VisitorsVM = new VisitorsPageViewModel();
            AuthorsVM = new AuthorsPageViewModel();
            TransactionsVM = new TransactionsPageViewModel();
            StatisticsVM = new StatisticsPageViewModel();

            CurrentView = BooksVM;

            BooksViewCommand = new RelayCommand(o =>
            {
                CurrentView = BooksVM;
            });

            VisitorsViewCommand = new RelayCommand(o =>
            {
                CurrentView = VisitorsVM;
            });

            AuthorsViewCommand = new RelayCommand(o =>
            {
                CurrentView = AuthorsVM;
            });

            TransactionsViewCommand = new RelayCommand(o =>
            {
                CurrentView = TransactionsVM;
            });

            StatisticsViewCommand = new RelayCommand(o =>
            {
                CurrentView = StatisticsVM;
            });

            CancelDialogCommand = new RelayCommand(o =>
            {
                CurrentDialog = null;
            });

            AddBookDialogVM = new AddBookDialogViewModel();
            BooksVM.AddEvent += (s, e) =>
            {
                AddBookDialogVM.CancelCommand = CancelDialogCommand;
                CurrentDialog = AddBookDialogVM;
            };
        }

        public BooksPageViewModel BooksVM { get; set; }
        public VisitorsPageViewModel VisitorsVM { get; set; }
        public AuthorsPageViewModel AuthorsVM { get; set; }
        public TransactionsPageViewModel TransactionsVM { get; set; }
        public StatisticsPageViewModel StatisticsVM { get; set; }

        public RelayCommand BooksViewCommand { get; set; }
        public RelayCommand VisitorsViewCommand { get; set; }
        public RelayCommand AuthorsViewCommand { get; set; }
        public RelayCommand TransactionsViewCommand { get; set; }
        public RelayCommand StatisticsViewCommand { get; set; }

        public RelayCommand CancelDialogCommand { get; set; }

        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public AddBookDialogViewModel AddBookDialogVM { get; set; }

        private object _currentDialog = null;
        public object CurrentDialog
        {
            get { return _currentDialog; }
            set
            {
                _currentDialog = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(DialogVisibility));
            }
        }

        public Visibility DialogVisibility
        {
            get { return _currentDialog == null ? Visibility.Collapsed : Visibility.Visible; }
        }
    }
}
