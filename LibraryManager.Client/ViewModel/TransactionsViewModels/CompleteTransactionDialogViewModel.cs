using LibraryManager.Client.Core;
using LibraryManager.Client.SupportClasses;
using LibraryManager.Model;

namespace LibraryManager.Client.ViewModel.TransactionsViewModels
{
    class CompleteTransactionDialogViewModel : ObservableObject
    {
        private Manager _manager;

        public CompleteTransactionDialogViewModel(LibraryTransaction trans)
        {
            _manager = ManagerInstance.Instance;
            CompleteCommand = new RelayCommand(async (o) =>
            {
                if (trans != null && trans.IsAvailable == true)
                {
                    await _manager.CompleteTransactionAsync(trans);
                    CancelCommand.Execute(o);
                    TransactionCompleted?.Invoke(this, EventArgs.Empty);
                }
            });
        }

        public RelayCommand CompleteCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        public event EventHandler TransactionCompleted;
    }
}
