using LibraryManager.Model;
using LibraryManager.Client.Core;
using System.Windows;
using System.Collections.ObjectModel;
using LibraryManager.Client.ViewModel.BooksViewModels;
using LibraryManager.Client.ViewModel.AuthorsViewModels;
using LibraryManager.Client.ViewModel.TransactionsViewModels;
using LibraryManager.Client.ViewModel.VisitorsViewModels;

namespace LibraryManager.Client.ViewModel
{
    public class MainViewVM : ObservableObject
    {
        private Manager _manager;
        public MainViewVM()
        {
            try{
                _manager = ManagerInstance.Instance;
            }
            catch (Exception ex){
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

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
                StatisticsVM.UpdateStatistics();
                CurrentView = StatisticsVM;
            });

            CancelDialogCommand = new RelayCommand(o =>
            {
                CurrentDialog = null;
            });

            //ADD
            BooksVM.AddEvent += (s, e) =>
            {
                var addBookDialogVM = new AddBookDialogViewModel();
                addBookDialogVM.CancelCommand = CancelDialogCommand;
                CurrentDialog = addBookDialogVM;
            };

            VisitorsVM.AddEvent += (s, e) =>
            {
                var addVisitorDialogVM = new AddVisitorDialogViewModel();
                addVisitorDialogVM.CancelCommand = CancelDialogCommand;
                CurrentDialog = addVisitorDialogVM;
            };

            AuthorsVM.AddEvent += (s, e) =>
            {
                var addAuthorDialogVM = new AddAuthorDialogViewModel();
                addAuthorDialogVM.CancelCommand = CancelDialogCommand;
                CurrentDialog = addAuthorDialogVM;
            };

            TransactionsVM.AddEvent += (s, e) =>
            {
                var addTransactionDialogVM = new AddTransactionDialogViewModel();
                addTransactionDialogVM.CancelCommand = CancelDialogCommand;
                CurrentDialog = addTransactionDialogVM;
            };

            //EDIT
            BooksVM.EditEvent += (s, e) =>
            {
                var editBookDialogVM = new EditBookDialogViewModel((Book)e.EditedItem);

                editBookDialogVM.CancelCommand = CancelDialogCommand;
                CurrentDialog = editBookDialogVM;
            };

            //FIND
            BooksVM.FindEvent += (s, e) =>
            {
                var findBookDialogVM = new FindBookDialogViewModel(((BooksPageViewModel)s).Books);
                findBookDialogVM.CancelCommand = CancelDialogCommand;

                CurrentDialog = findBookDialogVM;

            };
        }

        public BooksPageViewModel BooksVM { get; set; } = new();
        public VisitorsPageViewModel VisitorsVM { get; set; } = new();
        public AuthorsPageViewModel AuthorsVM { get; set; } = new();
        public TransactionsPageViewModel TransactionsVM { get; set; } = new();
        public StatisticsPageViewModel StatisticsVM { get; set; } = new();

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
