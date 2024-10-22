using LibraryManager.Model;
using LibraryManager.Client.Core;
using System.Collections.ObjectModel;
using System.Windows;


namespace LibraryManager.Client.ViewModel
{
    public class BooksPageViewModel : ObservsbleObject
    {
        private Manager _manager;

        //TODO: Нормально реализовать связь
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
                    MessageBox.Show($"Delete: {SelectedBook.Name}");
                }
            });

            AddCommand = new RelayCommand((o) =>
            {
                MessageBox.Show($"Add");
            });

            FindCommand = new RelayCommand((o) =>
            {
                MessageBox.Show($"Find");
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
    }
}
