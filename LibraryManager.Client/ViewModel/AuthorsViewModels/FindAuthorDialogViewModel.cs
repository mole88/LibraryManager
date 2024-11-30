using LibraryManager.Client.Core;
using LibraryManager.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.Client.ViewModel.AuthorsViewModels
{
    class FindAuthorDialogViewModel : ObservableObject
    {
        private Manager _manager;
        public FindAuthorDialogViewModel(ObservableCollection<Author> table)
        {
            _manager = ManagerInstance.Instance;
            FindAuthorCommand = new RelayCommand((o) =>
            {
                if (int.TryParse(AuthorId, out int id))
                {
                    Author foundAuthor = _manager.FindAuthor(id);
                    table.Clear();
                    table.Add(foundAuthor);
                    CancelCommand.Execute(o);
                }
            });
        }
        private string _authorId;
        public string AuthorId
        {
            get => _authorId;
            set
            {
                _authorId = value;
                OnPropertyChanged(nameof(AuthorId));
            }
        }
        public RelayCommand FindAuthorCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
    }
}
