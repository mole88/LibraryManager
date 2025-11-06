using LibraryManager.Client.Core;
using LibraryManager.Model;

namespace LibraryManager.Client.ViewModel.TransactionsViewModels
{
    internal class DeleteTransactionDialogViewModel
    {
        private Manager _manager;

        public DeleteTransactionDialogViewModel(LibraryTransaction trans)
        {
            _manager = ManagerInstance.Instance;
            DeleteCommand = new RelayCommand(async (o) =>
            {
                if (trans != null)
                {
                    await _manager.RemoveTransactionAsync(trans);
                    CancelCommand.Execute(o);
                }
            });
        }

        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
    }
}
