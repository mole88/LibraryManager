using LibraryManager.Client.Core;
using LibraryManager.Model;
using System.Collections.ObjectModel;

namespace LibraryManager.Client.ViewModel.TransactionsViewModels
{
    public class EditTransactionDialogViewModel : ObservableObject
    {
        private Manager _manager;
        public ObservableCollection<string> BooksNames => new(_manager.Books.Where(b => b.IsAvailable)
                                                                                         .Select(b => b.Name));
        public ObservableCollection<string> VisitorsNames => new(_manager.Visitors.Select(v => v.FullName));
        public EditTransactionDialogViewModel(LibraryTransaction editedTrans)
        {
            _manager = ManagerInstance.Instance;

            SearchBookText = editedTrans.Book.Name;
            SearchVisitorText = editedTrans.Visitor.FullName;
            SelectedDueDate = editedTrans.DueDate;

            EditTransactionCommand = new RelayCommand(async (o) =>
            {
                Book book = GetBook(SearchBookText);
                Visitor visitor = GetVisitor(SearchVisitorText);

                if (book != null && visitor != null && SelectedDueDate != new DateTime())
                {
                    LibraryTransaction trans = new()
                    {
                        Id = editedTrans.Id,
                        Book = book,
                        BookId = book.Id,
                        Visitor = visitor,
                        VisitorId = visitor.Id,
                        DateTaken = editedTrans.DateTaken,
                        DueDate = SelectedDueDate,
                        ReturnDate = editedTrans.ReturnDate,
                        IsAvailable = editedTrans.IsAvailable
                    };
                    await _manager.EditTransactionAsync(editedTrans, trans);
                    CancelCommand.Execute(o);
                }
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
        public DateTime SelectedDueDate { get; set; } = DateTime.Now;

        public RelayCommand EditTransactionCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        private Book? GetBook(string name)
        {
            return _manager.Books.FirstOrDefault(b => b.Name == name);
        }
        private Visitor? GetVisitor(string fullName)
        {
            return _manager.Visitors.FirstOrDefault(v => v.FullName == fullName);
        }
    }
}
