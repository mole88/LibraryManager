using LibraryManager.Client.Core;
using LibraryManager.Model;
using System.Collections.ObjectModel;
using System.Windows;

namespace LibraryManager.Client.ViewModel
{
    public class AddBookDialogViewModel : ObservableObject
    {
        private Manager _manager;
        public ObservableCollection<string> AuthorsNames => new(_manager.Authors.Select(a => a.FullName));
        public AddBookDialogViewModel()
        {
            _manager = ManagerInstance.Instance;
            AddBookCommand = new RelayCommand((o) =>
            {
                Author bookAuthor = GetAuthor(SearchAuthorText);
                if (bookAuthor != null && !string.IsNullOrEmpty(BookName) && int.TryParse(BookYear, out int year))
                {
                    Book newBook = new()
                    {
                        Id = UniqueIDMaker.GetUniqueID(_manager.Books),
                        Name = BookName,
                        BookAuthor = bookAuthor,
                        AuthorId = bookAuthor.Id,
                        Year = year,
                        IsAvailable = true
                    };
                    _manager.AddBook(newBook);
                    CancelCommand.Execute(o);
                }
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

        private string _bookYear;
        public string BookYear
        {
            get => _bookYear;
            set
            {
                _bookYear = value;
                OnPropertyChanged(nameof(BookYear));
            }
        }

        private string _searchAuthorText;
        public string SearchAuthorText
        {
            get => _searchAuthorText;
            set
            {
                if (_searchAuthorText != value)
                {
                    _searchAuthorText = value;
                    OnPropertyChanged(nameof(SearchAuthorText));
                }
            }
        }
        public RelayCommand AddBookCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        private Author? GetAuthor(string fullName)
        {
            return _manager.Authors.FirstOrDefault(a => a.FullName == fullName);
        }
    }
}
