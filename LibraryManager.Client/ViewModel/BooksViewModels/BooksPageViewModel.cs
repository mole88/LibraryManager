using LibraryManager.Model;
using LibraryManager.Client.Core;
using System.Collections.ObjectModel;
using System.Windows;
using LibraryManager.Client.SupportClasses;
using NLog;


namespace LibraryManager.Client.ViewModel.BooksViewModels
{
    public class BooksPageViewModel : ObservableObject
    {
        private Manager _manager;
        Logger log = LogManager.GetCurrentClassLogger();

        private ObservableCollection<Book> _books;
        public ObservableCollection<Book> Books
        {
            get { return _books; }
            set
            {
                _books = value;
                OnPropertyChanged(nameof(Books));
            }
        }

        private Book _selectedBook;

        public Book SelectedBook
        {
            get { return _selectedBook; }
            set
            {
                _selectedBook = value;
                OnPropertyChanged(nameof(SelectedBook));
            }
        }

        public BooksPageViewModel()
        {
            _manager = ManagerInstance.Instance;
            RefrashBooks();
            _manager.Books.CollectionChanged += (s, e) => RefrashBooks();

            EditCommand = new RelayCommand((o) =>
            {
                if (SelectedBook != null)
                {
                    EditEvent?.Invoke(this, new ObjEventArgs(SelectedBook));
                    RefrashBooks();
                    TopMessage = "";
                    log.Info("Book edited");
                }
            });

            DeleteCommand = new RelayCommand(async (o) =>
            {
                if (SelectedBook != null)
                {
                    DeleteEvent?.Invoke(this, new ObjEventArgs(SelectedBook));
                    RefrashBooks();
                    TopMessage = "";
                    log.Info("Book deleted");
                }
            });

            FindCommand = new RelayCommand((o) =>
            {
                RefrashBooks();
                FindEvent?.Invoke(this, EventArgs.Empty);
                TopMessage = "";
                log.Info("Book found");
            });

            RefrashTableCommand = new RelayCommand((o) =>
            {
                RefrashBooks();
                TopMessage = "";
                log.Debug("Book table refrashed");
            });

            SortCommand = new RelayCommand((o) =>
            {
                MessageBox.Show($"Sort");
                RefrashBooks();
                TopMessage = "";
            });

            AddCommand = new RelayCommand((o) =>
            {
                AddEvent?.Invoke(this, EventArgs.Empty);
                RefrashBooks();
                TopMessage = "";
                log.Info("Book added");
            });
        }
        private void RefrashBooks()
        {
            Books = new ObservableCollection<Book>(
                _manager.Books
                    .OrderBy(b => !b.IsAvailable)
                    .ThenBy(b => b.Id)            
            );
        }
        private string _topMessage;
        public string TopMessage
        {
            get { return _topMessage; }
            set { 
                _topMessage = value; 
                OnPropertyChanged(nameof(TopMessage));
            }
        }

        public RelayCommand EditCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand AddCommand { get; set; }
        public RelayCommand FindCommand { get; set; }
        public RelayCommand SortCommand { get; set; }
        public RelayCommand RefrashTableCommand { get; set; }

        public event EventHandler AddEvent;
        public event EventHandler<ObjEventArgs> EditEvent;
        public event EventHandler<ObjEventArgs> DeleteEvent;
        public event EventHandler FindEvent;
    }
}
