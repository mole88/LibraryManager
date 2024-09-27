using LibraryManager.Model;

namespace LibraryManager.Client
{
    public class MainViewVM
    {
        private Manager _manager;
        public MainViewVM() 
        {
            _manager = new Manager();
        }
        /*public DelegateCommand AddBookCommand { get; }
        public DelegateCommand SortBookCommand { get; }
        public DelegateCommand FindBookCommand { get; }
        public DelegateCommand AddVisitorCommand { get; }
        public DelegateCommand SortVisitorCommand { get; }
        public DelegateCommand FindVisitorCommand { get; }
        public ObservableCollection<BookVM> Books { get; }
        public ObservableCollection<BookVM> Visitors { get; }*/
    }
}
