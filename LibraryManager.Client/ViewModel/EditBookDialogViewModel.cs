using LibraryManager.Client.Core;
using LibraryManager.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace LibraryManager.Client.ViewModel
{
    public class EditBookDialogViewModel : ObservableObject
    {
        private Manager _manager;
        public ObservableCollection<string> AuthorsNames => new(_manager.Authors.Select(a => a.FullName));
        public EditBookDialogViewModel(Book editedBook)
        {
            _manager = ManagerInstance.Instance;

            BookId = editedBook.Id.ToString();
            BookName = editedBook.Name;
            BookYear = editedBook.Year.ToString();
            SearchAuthorText = editedBook.BookAuthor.FullName;


            EditBookCommand = new RelayCommand((o) =>
            {
                Author bookAuthor = GetAuthor(SearchAuthorText);
                if (bookAuthor != null && !string.IsNullOrEmpty(BookName)
                    && int.TryParse(BookYear, out int year) && int.TryParse(BookId, out int id))
                {
                    if (id == editedBook.Id || UniqueIDMaker.IsUnique(id, _manager.Books))
                    {
                        Book newBook = new()
                        {
                            Id = id,
                            Name = BookName,
                            BookAuthor = bookAuthor,
                            AuthorId = bookAuthor.Id,
                            Year = year
                        };

                        _manager.EditBook(editedBook, newBook);
                        ClearDialog();
                        CancelCommand.Execute(o);
                    }
                    else
                    {
                        MessageBox.Show("Id is not uniqie");
                    }
                }
            });
        }
        private string _bookId;
        public string BookId
        {
            get => _bookId;
            set
            {
                _bookId = value;
                OnPropertyChanged(nameof(BookId));
            }
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
        public RelayCommand EditBookCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        private Author? GetAuthor(string fullName)
        {
            return _manager.Authors.FirstOrDefault(a => a.FullName == fullName);
        }
        private void ClearDialog()
        {
            BookName = "";
            BookYear = "";
            SearchAuthorText = "";
        }
    }
}
