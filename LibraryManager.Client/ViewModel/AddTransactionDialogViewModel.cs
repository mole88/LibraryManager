using LibraryManager.Client.Core;
using LibraryManager.Client.SupportClasses;
using LibraryManager.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.Client.ViewModel
{
    public class AddTransactionDialogViewModel : ObservableObject
    {
        private Manager _manager;
        public ObservableCollection<string> AuthorsNames => new(_manager.Authors.Select(a => a.FullName));
        public AddTransactionDialogViewModel()
        {
            _manager = ManagerInstance.Instance;
            AddTransactionCommand = new RelayCommand((o) =>
            {
                Book book = GetBook(SearchBookText);
                Visitor visitor = GetVisitor(SearchVisitorText);

                /*if (bookAuthor != null && !string.IsNullOrEmpty(BookName) && BookYear != 0)
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
                }*/
            });
        }

        private string _searchBookText;
        public string SearchBookText
        {
            get => _searchBookText;
            set
            {
                if (_searchBookText != value)
                {
                    _searchBookText = value;
                    OnPropertyChanged(nameof(SearchBookText));
                }
            }
        }

        private string _searchVisitorText;
        public string SearchVisitorText
        {
            get => _searchVisitorText;
            set
            {
                if (_searchVisitorText != value)
                {
                    _searchVisitorText = value;
                    OnPropertyChanged(nameof(SearchVisitorText));
                }
            }
        }
        public RelayCommand AddTransactionCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        private Book? GetBook(string name)
        {
            return _manager.Books.FirstOrDefault(b => b.Name == name);
        }
        private Visitor? GetVisitor(string fullName)
        {
            return _manager.Visitors.FirstOrDefault(v => v.FullName == fullName);
        }
        private void ClearDialog()
        {
            SearchBookText = "";
            SearchVisitorText = "";
        }
    }
}
