using LibraryManager.Model;
using LibraryManager.Client.Core;
using System.Collections.ObjectModel;
using System.Windows;
using LibraryManager.Client.SupportClasses;


namespace LibraryManager.Client.ViewModel
{
    public class BooksPageViewModel : ObservableObject
    {
        private Manager _manager;

        public ReadOnlyObservableCollection<Book> Books => _manager.Books;

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


            EditCommand = new RelayCommand((o) =>
            {
                if (SelectedBook != null)
                {
                    MessageBox.Show($"Edit: {SelectedBook.Name}");
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
                MessageBox.Show($"Find");
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

        public event EventHandler AddEvent;
    }
}
