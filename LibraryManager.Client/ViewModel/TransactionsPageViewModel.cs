using LibraryManager.Model;
using LibraryManager.Client.Core;
using System.Collections.ObjectModel;
using System.Windows;

namespace LibraryManager.Client.ViewModel
{
    public class TransactionsPageViewModel : ObservableObject
    {
        private Manager _manager;

        //TODO: Нормально реализовать связь
        public ReadOnlyObservableCollection<LibraryTransaction> Transactions => _manager.Transactions;

        private LibraryTransaction _selectedTransaction;

        public LibraryTransaction SelectedTransaction
        {
            get { return _selectedTransaction; }
            set
            {
                _selectedTransaction = value;
                OnPropertyChanged(nameof(SelectedTransaction));
            }
        }
        public TransactionsPageViewModel()
        {
            _manager = ManagerInstance.Instance;

            EditCommand = new RelayCommand((o) =>
            {
                if (SelectedTransaction != null)
                {
                    MessageBox.Show($"Edit: {SelectedTransaction.Book.Name}");
                }
            });

            DeleteCommand = new RelayCommand((o) =>
            {
                if (SelectedTransaction != null)
                {
                    MessageBox.Show($"Delete: {SelectedTransaction.Book.Name}");
                }
            });

            AddCommand = new RelayCommand((o) =>
            {
                MessageBox.Show($"Add");
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
    }
}
