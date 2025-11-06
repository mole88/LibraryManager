using LibraryManager.Client.Core;
using LibraryManager.Model;
using System.Collections.ObjectModel;

namespace LibraryManager.Client.ViewModel.TransactionsViewModels
{
    class FindTransactionDialogViewModel : ObservableObject
    {
        private Manager _manager;
        public FindTransactionDialogViewModel(ObservableCollection<LibraryTransaction> table)
        {
            _manager = ManagerInstance.Instance;
            FindTransactionCommand = new RelayCommand((o) =>
            {
                if (int.TryParse(TransactionId, out int id))
                {
                    LibraryTransaction foundTrans = _manager.FindTransaction(id);
                    table.Clear();
                    table.Add(foundTrans);
                    CancelCommand.Execute(o);
                }
            });
        }
        private string _transactionId;
        public string TransactionId
        {
            get => _transactionId;
            set
            {
                _transactionId = value;
                OnPropertyChanged(nameof(TransactionId));
            }
        }
        public RelayCommand FindTransactionCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
    }
}
