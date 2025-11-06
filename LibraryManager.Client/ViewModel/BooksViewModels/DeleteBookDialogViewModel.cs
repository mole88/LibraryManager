using LibraryManager.Client.Core;
using LibraryManager.Model;

namespace LibraryManager.Client.ViewModel.BooksViewModels
{
    internal class DeleteBookDialogViewModel : ObservableObject
    {
        private Manager _manager;

        public DeleteBookDialogViewModel(Book book)
        {
            _manager = ManagerInstance.Instance;
            DeleteCommand = new RelayCommand(async (o) =>
            {
                if (book != null)
                {
                    await _manager.RemoveBookAsync(book);
                    CancelCommand.Execute(o);
                }
            });
        }

        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
    }
}
