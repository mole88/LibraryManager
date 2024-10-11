using LibraryManager.Model;
using LibraryManager.Client.ViewModel;
using System.Windows.Controls;
using LibraryManager.Client.Core;
using LibraryManager.Client.View;

namespace LibraryManager.Client
{
    public class MainViewVM : ObservsbleObject
    {
        private Manager _manager;
        public MainViewVM() 
        {
            _manager = ManagerInstance.Instance;

            BooksVM = new BooksPageViewModel();
            VisitorsVM = new VisitorsPageViewModel();
            AuthorsVM = new AuthorsPageViewModel();
            TransactionsVM = new TransactionsPageViewModel();

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
        }

        public BooksPageViewModel BooksVM {get; set; }
        public VisitorsPageViewModel VisitorsVM {get; set; }
        public AuthorsPageViewModel AuthorsVM {get; set; }
        public TransactionsPageViewModel TransactionsVM {get; set; }


        public RelayCommand BooksViewCommand { get; set; }
        public RelayCommand VisitorsViewCommand { get; set; }
        public RelayCommand AuthorsViewCommand { get; set; }
        public RelayCommand TransactionsViewCommand { get; set; }

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

        //public BooksPageViewModel BooksVM => new BooksPageViewModel();
    }
}
