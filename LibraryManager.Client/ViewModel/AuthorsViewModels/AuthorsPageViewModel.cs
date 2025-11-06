using LibraryManager.Model;
using System.Collections.ObjectModel;
using LibraryManager.Client.Core;
using System.Windows;
using LibraryManager.Client.SupportClasses;

namespace LibraryManager.Client.ViewModel.AuthorsViewModels
{
    public class AuthorsPageViewModel : ObservableObject
    {
        private Manager _manager;

        private ObservableCollection<Author> _authors;
        public ObservableCollection<Author> Authors
        {
            get { return _authors; }
            set
            {
                _authors = value;
                OnPropertyChanged(nameof(Authors));
            }
        }

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
            RefrashAuthors();
            _manager.Authors.CollectionChanged += (s, e) => RefrashAuthors();

            RefrashTableCommand = new RelayCommand((o) =>
            {
                Authors = new(_manager.Authors);
            });

            EditCommand = new RelayCommand((o) =>
            {
                if (SelectedAuthor != null)
                {
                    EditEvent?.Invoke(this, new ObjEventArgs(SelectedAuthor));
                }
            });

            DeleteCommand = new RelayCommand(async (o) =>
            {
                if (SelectedAuthor != null)
                {
                    DeleteEvent?.Invoke(this, new ObjEventArgs(SelectedAuthor));
                }
            });

            AddCommand = new RelayCommand((o) =>
            {
                AddEvent?.Invoke(this, EventArgs.Empty);
            });

            FindCommand = new RelayCommand((o) =>
            {
                FindEvent?.Invoke(this, EventArgs.Empty);
            });

            SortCommand = new RelayCommand((o) =>
            {
                MessageBox.Show($"Sort");
            });

        }
        private void RefrashAuthors()
        {
            Authors = new ObservableCollection<Author>(
                _manager.Authors
                    .OrderBy(a => a.Id)
            );
        }
        public RelayCommand EditCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand AddCommand { get; set; }
        public RelayCommand FindCommand { get; set; }
        public RelayCommand SortCommand { get; set; }
        public RelayCommand RefrashTableCommand { get; set; }

        public event EventHandler AddEvent;
        public event EventHandler<ObjEventArgs> EditEvent;
        public event EventHandler<ObjEventArgs> DeleteEvent;
        public event EventHandler FindEvent;

    }
}
