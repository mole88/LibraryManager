using LibraryManager.Client.Core;
using LibraryManager.Client.SupportClasses;
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
                if (bookAuthor != null && !string.IsNullOrEmpty(BookName) && BookYear != 0)
                {
                    Book newBook = new()
                    {
                        Id = UniqueIDMaker.GetUniqueID(_manager.Books),
                        Name = BookName,
                        BookAuthor = bookAuthor,
                        AuthorId = bookAuthor.Id,
                        Year = BookYear,
                        IsAvailable = true
                    };
                    _manager.AddBook(newBook);
                    ClearDialog();
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
        private void ClearDialog()
        {
            BookName = "";
            BookYear = 0;
            SearchAuthorText = "";
        }
    }
}
