using LibraryManager.Client.Core;
using LibraryManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.Client.ViewModel.AuthorsViewModels
{
    public class EditAuthorDialogViewModel : ObservableObject
    {
        private Manager _manager;

        public EditAuthorDialogViewModel(Author editedAuthor)
        {
            _manager = ManagerInstance.Instance;

            AuthorName = editedAuthor.FullName;

            EditAuthorCommand = new RelayCommand(async (o) =>
            {
                if (!string.IsNullOrEmpty(AuthorName))
                {
                    Author newAuthor = new Author()
                    {
                        Id = editedAuthor.Id,
                        FullName = AuthorName,
                    };
                    await _manager.EditAuthorAsync(editedAuthor, newAuthor);
                    CancelCommand.Execute(o);
                }
            });
        }
        private string _authorName;
        public string AuthorName
        {
            get => _authorName;
            set
            {
                _authorName = value;
                OnPropertyChanged(nameof(AuthorName));
            }
        }

        public RelayCommand EditAuthorCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
    }
}
