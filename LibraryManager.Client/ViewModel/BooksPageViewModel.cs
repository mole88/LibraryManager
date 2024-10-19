using LibraryManager.Model;
using LibraryManager.Client.Core;
using System.Collections.ObjectModel;
using System.Windows;
using System;
using System.Windows.Controls;


namespace LibraryManager.Client.ViewModel
{
    public class BooksPageViewModel : ObservsbleObject
    {
        private Manager _manager;

        //TODO: Нормально реализовать связь
        public ReadOnlyObservableCollection<Book> Books => _manager.Books;

        private Book _selectedBood;

        public Book SelectedBook
        {
            get { return _selectedBood; }
            set 
            {
                _selectedBood = value; 
                OnPropertyChanged(nameof(SelectedBook));
            }
        }

        public BooksPageViewModel()
        {
            _manager = ManagerInstance.Instance;
            RowSelectedCommand = new RelayCommand((o) =>
            {
                if (o is Book book)
                {
                    SelectedBook = book;
                }
                MessageBox.Show($"{SelectedBook.Name}");
            });
        }
        public RelayCommand RowSelectedCommand  { get; set;}
    }
}
