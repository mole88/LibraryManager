using LibraryManager.Model;
using System.Collections.ObjectModel;
using LibraryManager.Client.Core;

namespace LibraryManager.Client.ViewModel
{
    public class AuthorsPageViewModel : ObservsbleObject
    {
        private Manager _manager;

        //TODO: Нормально реализовать связь
        public ReadOnlyObservableCollection<Author> Authors => _manager.Authors;
        public AuthorsPageViewModel()
        {
            _manager = ManagerInstance.Instance;
        }
    }
}
