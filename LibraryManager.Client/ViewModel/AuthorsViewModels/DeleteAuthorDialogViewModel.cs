using LibraryManager.Client.Core;
using LibraryManager.Model;

namespace LibraryManager.Client.ViewModel.AuthorsViewModels
{
    internal class DeleteAuthorDialogViewModel
    {
        private Manager _manager;

        public DeleteAuthorDialogViewModel(Author author)
        {
            _manager = ManagerInstance.Instance;
            DeleteCommand = new RelayCommand(async (o) =>
            {
                if (author != null)
                {
                    await _manager.RemoveAuthorAsync(author);
                    CancelCommand.Execute(o);
                }
            });
        }

        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
    }
}
