using LibraryManager.Model;
using LibraryManager.Client.Core;
using System.Windows;
using LibraryManager.Client.ViewModel.BooksViewModels;
using LibraryManager.Client.ViewModel.AuthorsViewModels;
using LibraryManager.Client.ViewModel.TransactionsViewModels;
using LibraryManager.Client.ViewModel.VisitorsViewModels;
using NLog;

namespace LibraryManager.Client.ViewModel
{
    public class MainViewVM : ObservableObject
    {
        private Manager _manager;
        Logger log = LogManager.GetCurrentClassLogger();

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
                log.Debug("Changed table to Books");
            });

            VisitorsViewCommand = new RelayCommand(o =>
            {
                CurrentView = VisitorsVM;
                log.Debug("Changed table to Visitors");
            });

            AuthorsViewCommand = new RelayCommand(o =>
            {
                CurrentView = AuthorsVM;
                log.Debug("Changed table to Authors");
            });

            TransactionsViewCommand = new RelayCommand(o =>
            {
                CurrentView = TransactionsVM;
                log.Debug("Changed table to Transactions");
            });

            StatisticsViewCommand = new RelayCommand(o =>
            {
                StatisticsVM.UpdateStatistics();
                CurrentView = StatisticsVM;
                log.Debug("Changed table to Statistics");
            });

            CancelDialogCommand = new RelayCommand(o =>
            {
                CurrentDialog = null;
                log.Debug("Changed table to Books");
            });

            //ADD
            BooksVM.AddEvent += (s, e) =>
            {
                var addBookDialogVM = new AddBookDialogViewModel();
                addBookDialogVM.CancelCommand = CancelDialogCommand;
                CurrentDialog = addBookDialogVM;
                log.Debug("Book add dialog was opened");
            };

            VisitorsVM.AddEvent += (s, e) =>
            {
                var addVisitorDialogVM = new AddVisitorDialogViewModel();
                addVisitorDialogVM.CancelCommand = CancelDialogCommand;
                CurrentDialog = addVisitorDialogVM;
                log.Debug("Visitors add dialog was opened");
            };

            AuthorsVM.AddEvent += (s, e) =>
            {
                var addAuthorDialogVM = new AddAuthorDialogViewModel();
                addAuthorDialogVM.CancelCommand = CancelDialogCommand;
                CurrentDialog = addAuthorDialogVM;
                log.Debug("Authors add dialog was opened");
            };

            TransactionsVM.AddEvent += (s, e) =>
            {
                var addTransactionDialogVM = new AddTransactionDialogViewModel();
                addTransactionDialogVM.CancelCommand = CancelDialogCommand;
                CurrentDialog = addTransactionDialogVM;
                log.Debug("Transactions add dialog was opened");
            };

            //EDIT
            BooksVM.EditEvent += (s, e) =>
            {
                var editBookDialogVM = new EditBookDialogViewModel(e.Item as Book, (BooksPageViewModel)s);

                editBookDialogVM.CancelCommand = CancelDialogCommand;
                CurrentDialog = editBookDialogVM;
                log.Debug("Book edit dialog was opened");
            };

            VisitorsVM.EditEvent += (s, e) =>
            {
                var editVisitorDialogVM = new EditVisitorDialogViewModel(e.Item as Visitor);

                editVisitorDialogVM.CancelCommand = CancelDialogCommand;
                CurrentDialog = editVisitorDialogVM;
                log.Debug("Visitor edit dialog was opened");
            };

            AuthorsVM.EditEvent += (s, e) =>
            {
                var editAuthorDialogVM = new EditAuthorDialogViewModel(e.Item as Author);

                editAuthorDialogVM.CancelCommand = CancelDialogCommand;
                CurrentDialog = editAuthorDialogVM;
                log.Debug("Author edit dialog was opened");
            };

            TransactionsVM.EditEvent += (s, e) =>
            {
                var editTransDialogVM = new EditTransactionDialogViewModel(e.Item as LibraryTransaction);

                editTransDialogVM.CancelCommand = CancelDialogCommand;
                CurrentDialog = editTransDialogVM;
                log.Debug("Transaction edit dialog was opened");
            };


            //DELETE
            BooksVM.DeleteEvent += (s, e) =>
            {
                var deleteBookDialogVM = new DeleteBookDialogViewModel(e.Item as Book);

                deleteBookDialogVM.CancelCommand = CancelDialogCommand;
                CurrentDialog = deleteBookDialogVM;
                log.Debug("Book delete dialog was opened");
            };

            VisitorsVM.DeleteEvent += (s, e) =>
            {
                var deleteVisDialogVM = new DeleteVisitorDialogViewModel(e.Item as Visitor);

                deleteVisDialogVM.CancelCommand = CancelDialogCommand;
                CurrentDialog = deleteVisDialogVM;
                log.Debug("Visitor delete dialog was opened");
            };

            AuthorsVM.DeleteEvent += (s, e) =>
            {
                var deleteAuthorDialogVM = new DeleteAuthorDialogViewModel(e.Item as Author);

                deleteAuthorDialogVM.CancelCommand = CancelDialogCommand;
                CurrentDialog = deleteAuthorDialogVM;
                log.Debug("Author delete dialog was opened");
            };

            TransactionsVM.DeleteEvent += (s, e) =>
            {
                var deleteTransDialogVM = new DeleteTransactionDialogViewModel(e.Item as LibraryTransaction);

                deleteTransDialogVM.CancelCommand = CancelDialogCommand;
                CurrentDialog = deleteTransDialogVM;
                log.Debug("Transaction delete dialog was opened");
            };
            //FIND
            BooksVM.FindEvent += (s, e) =>
            {
                var findBookDialogVM = new FindBookDialogViewModel(((BooksPageViewModel)s).Books);
                findBookDialogVM.CancelCommand = CancelDialogCommand;

                CurrentDialog = findBookDialogVM;
                log.Debug("Find book dialog was opened");
            };

            VisitorsVM.FindEvent += (s, e) =>
            {
                var findVisitorDialogVM = new FindVisitorDialogViewModel(((VisitorsPageViewModel)s).Visitors);
                findVisitorDialogVM.CancelCommand = CancelDialogCommand;

                CurrentDialog = findVisitorDialogVM;
                log.Debug("Find visitor dialog was opened");
            };

            TransactionsVM.FindEvent += (s, e) =>
            {
                var trans = ((TransactionsPageViewModel)s).Transactions;
                var findTransDialogVM = new FindTransactionDialogViewModel(trans);
                findTransDialogVM.CancelCommand = CancelDialogCommand;

                CurrentDialog = findTransDialogVM;
                log.Debug("Find transaction dialog was opened");
            };

            AuthorsVM.FindEvent += (s, e) =>
            {
                var authors = ((AuthorsPageViewModel)s).Authors;
                var findAuthorDialogVM = new FindAuthorDialogViewModel(authors);
                findAuthorDialogVM.CancelCommand = CancelDialogCommand;

                CurrentDialog = findAuthorDialogVM;
                log.Debug("Find author dialog was opened");
            };
            //FILTER
            VisitorsVM.FilterBooksEvent += (s, e) =>
            {
                Visitor curVis = e.Item as Visitor;
                if (curVis != null)
                {
                    BooksVM.Books = new(
                    curVis.Transactions
                        .Where(t => t.IsAvailable)
                        .Select(t => t.Book)
                        .GroupBy(b => b.Id)
                        .Select(g => g.First()));

                    BooksVM.TopMessage = $"{curVis.FullName}'s books";
                    CurrentView = BooksVM;
                    log.Debug($"{curVis.FullName}'s books filtered");
                }
            };

            //COMPLETE TRANSACTION
            TransactionsVM.CompleteEvent += (s, e) =>
            {
                var completeTransDialogVM = new CompleteTransactionDialogViewModel(e.Item as LibraryTransaction);
                completeTransDialogVM.CancelCommand = CancelDialogCommand;

                completeTransDialogVM.TransactionCompleted += (s, e) => TransactionsVM.RefrashTransactions();

                CurrentDialog = completeTransDialogVM;
                log.Debug("Complete transaction dialog opened");
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
