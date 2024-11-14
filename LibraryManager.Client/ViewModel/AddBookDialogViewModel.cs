using LibraryManager.Client.Core;
using LibraryManager.Client.SupportClasses;
using LibraryManager.Model;
using System.Collections.ObjectModel;
using System.Xml;

namespace LibraryManager.Client.ViewModel
{
    public class AddBookDialogViewModel : ObservableObject
    {
        private Manager _manager;
        public ReadOnlyObservableCollection<Author> Authors => _manager.Authors;
        public AddBookDialogViewModel()
        {
            _manager = ManagerInstance.Instance;
            AddBookCommand = new RelayCommand((o) =>
            {
                Book newBook = new()
                {
                    Id = UniqueIDMaker.GetUniqueID(_manager.Books),
                    Name = BookName,
                    AuthorId = SelectedAuthor?.Id ?? 0,
                    BookAuthor = SelectedAuthor,
                    Year = BookYear,
                    IsAvailable = true
                };
                _manager.AddBook(newBook);
            });
        }
        private string _bookName;
        public string BookName
        {
            get => _bookName;
            set
            {
                _bookName = value;
                OnPropertyChanged(nameof(BookName));
            }
        }

        private int _bookYear;
        public int BookYear
        {
            get => _bookYear;
            set
            {
                _bookYear = value;
                OnPropertyChanged(nameof(BookName));
            }
        }
        public Author SelectedAuthor { get; set; }

        public RelayCommand AddBookCommand { get; set; }
    }
}
