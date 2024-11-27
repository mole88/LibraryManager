using LibraryManager.Model;
using System.Collections.ObjectModel;
using LibraryManager.Client.Core;
using System.Windows;

namespace LibraryManager.Client.ViewModel
{
    public class AuthorsPageViewModel : ObservableObject
    {
        private Manager _manager;
        public ObservableCollection<Author> Authors => _manager.Authors;

        private Author _selectedAuthor;

        public Author SelectedAuthor
        {
            get { return _selectedAuthor; }
            set
            {
                _selectedAuthor = value;
                OnPropertyChanged(nameof(SelectedAuthor));
            }
        }
        public AuthorsPageViewModel()
        {
            _manager = ManagerInstance.Instance;
            EditCommand = new RelayCommand((o) =>
            {
                if (SelectedAuthor != null)
                {
                    MessageBox.Show($"Edit: {SelectedAuthor.FullName}");
                }
            });

            DeleteCommand = new RelayCommand((o) =>
            {
                if (SelectedAuthor != null)
                {
                    _manager.RemoveAuthor(SelectedAuthor);
                }
            });

            AddCommand = new RelayCommand((o) =>
            {
                AddEvent?.Invoke(this, EventArgs.Empty);
            });

            FindCommand = new RelayCommand((o) =>
            {
                MessageBox.Show($"Find");
            });

            SortCommand = new RelayCommand((o) =>
            {
                MessageBox.Show($"Sort");
            });

        }
        public RelayCommand EditCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand AddCommand { get; set; }
        public RelayCommand FindCommand { get; set; }
        public RelayCommand SortCommand { get; set; }
        public event EventHandler AddEvent;

    }
}
