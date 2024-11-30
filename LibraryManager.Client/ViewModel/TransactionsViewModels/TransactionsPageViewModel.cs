using LibraryManager.Model;
using LibraryManager.Client.Core;
using System.Collections.ObjectModel;
using System.Windows;
using LibraryManager.Client.SupportClasses;

namespace LibraryManager.Client.ViewModel.TransactionsViewModels
{
    public class TransactionsPageViewModel : ObservableObject
    {
        private Manager _manager;

        private ObservableCollection<LibraryTransaction> _transactions;
        public ObservableCollection<LibraryTransaction> Transactions
        {
            get { return _transactions; }
            set
            {
                _transactions = value;
                OnPropertyChanged(nameof(Transactions));
            }
        }

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
            Transactions = new(_manager.Transactions);
            _manager.Transactions.CollectionChanged += (s, e) =>
            {
                Transactions = new(_manager.Transactions);
            };

            EditCommand = new RelayCommand((o) =>
            {
                if (SelectedTransaction != null)
                {
                    EditEvent?.Invoke(this, new EditEventArgs(SelectedTransaction));
                }
            });

            DeleteCommand = new RelayCommand(async (o) =>
            {
                if (SelectedTransaction != null)
                {
                    await _manager.RemoveTransactionAsync(SelectedTransaction);
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

            RefrashTableCommand = new RelayCommand((o) =>
            {
                Transactions = new(_manager.Transactions);
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
        public RelayCommand RefrashTableCommand { get; set; }

        public event EventHandler AddEvent;
        public event EventHandler<EditEventArgs> EditEvent;
        public event EventHandler FindEvent;
    }
}
