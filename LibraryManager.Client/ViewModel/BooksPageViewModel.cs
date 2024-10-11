using LibraryManager.Model;
using LibraryManager.Client.Core;
using System.Collections.ObjectModel;


namespace LibraryManager.Client.ViewModel
{
    public class BooksPageViewModel : ObservsbleObject
    {
        private Manager _manager;

        //TODO: Нормально реализовать связь
        public ReadOnlyObservableCollection<Book> Books => _manager.Books;
        public BooksPageViewModel()
        {
            _manager = ManagerInstance.Instance;
        }
    }
}
