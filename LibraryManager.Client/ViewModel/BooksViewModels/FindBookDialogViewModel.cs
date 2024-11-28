using LibraryManager.Client.Core;
using LibraryManager.Model;
using System.Collections.ObjectModel;

namespace LibraryManager.Client.ViewModel.BooksViewModels
{
    internal class FindBookDialogViewModel : ObservableObject
    {
        private Manager _manager;
        public ObservableCollection<string> AuthorsNames => new(_manager.Authors.Select(a => a.FullName));
        public FindBookDialogViewModel(ObservableCollection<Book> table)
        {
            _manager = ManagerInstance.Instance;
            FindBookCommand = new RelayCommand((o) =>
            {
                if (int.TryParse(BookId, out int id))
                {
                    Book foundBook = _manager.FindBook(id);
                    table.Clear();
                    table.Add(foundBook);
                    CancelCommand.Execute(o);
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
        public RelayCommand FindBookCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
    }
}
