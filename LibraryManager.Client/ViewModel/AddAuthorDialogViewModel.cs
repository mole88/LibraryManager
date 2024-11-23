using LibraryManager.Client.Core;
using LibraryManager.Client.SupportClasses;
using LibraryManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.Client.ViewModel
{
    public class AddAuthorDialogViewModel : ObservableObject
    {
        private Manager _manager;

        public AddAuthorDialogViewModel()
        {
            _manager = ManagerInstance.Instance;
            AddAuthorCommand = new RelayCommand((o) =>
            {
                if (!string.IsNullOrEmpty(AuthorName))
                {
                    Author newAuthor = new Author()
                    {
                        FullName = AuthorName,
                    };
                    _manager.AddAuthor(newAuthor);
                    ClearDialog();
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

        public RelayCommand AddAuthorCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        private void ClearDialog()
        {
            AuthorName = "";
        }
    }
}
