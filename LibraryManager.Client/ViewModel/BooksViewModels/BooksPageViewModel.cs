using LibraryManager.Model;
using LibraryManager.Client.Core;
using System.Collections.ObjectModel;
using System.Windows;
using LibraryManager.Client.SupportClasses;


namespace LibraryManager.Client.ViewModel.BooksViewModels
{
    public class BooksPageViewModel : ObservableObject
    {
        private Manager _manager;
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
            Books = new(_manager.Books);
            _manager.Books.CollectionChanged += (s, e) =>
            {
                Books = new(_manager.Books);
            };

            EditCommand = new RelayCommand((o) =>
            {
                if (SelectedBook != null)
                {
                    EditEvent?.Invoke(this, new EditEventArgs(SelectedBook));
                }
            });

            DeleteCommand = new RelayCommand((o) =>
            {
                if (SelectedBook != null)
                {
                    _manager.RemoveBook(SelectedBook);
                }
            });

            FindCommand = new RelayCommand((o) =>
            {
                FindEvent?.Invoke(this, EventArgs.Empty);
            });

            RefrashTableCommand = new RelayCommand((o) =>
            {
                Books = new(_manager.Books);
            });

            SortCommand = new RelayCommand((o) =>
            {
                MessageBox.Show($"Sort");
            });

            AddCommand = new RelayCommand((o) =>
            {
                AddEvent?.Invoke(this, EventArgs.Empty);
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
