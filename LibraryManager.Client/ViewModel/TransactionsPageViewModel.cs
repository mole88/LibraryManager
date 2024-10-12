using LibraryManager.Model;
using LibraryManager.Client.Core;
using System.Collections.ObjectModel;

namespace LibraryManager.Client.ViewModel
{
    public class TransactionsPageViewModel : ObservsbleObject
    {
        private Manager _manager;

        //TODO: Нормально реализовать связь
        public ReadOnlyObservableCollection<LibraryTransaction> Transactions => _manager.Transactions;
        public TransactionsPageViewModel()
        {
            _manager = ManagerInstance.Instance;
        }
    }
}
