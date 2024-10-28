using System.Collections.ObjectModel;
using LibraryManager.Model.Exceptions;

namespace LibraryManager.Model
{
    public class Manager
    {
        private readonly ObservableCollection<LibraryTransaction> _transactions;
        private readonly ObservableCollection<Visitor> _visitors;
        private readonly ObservableCollection<Book> _books;
        private readonly ObservableCollection<Author> _authors;

        public readonly ReadOnlyObservableCollection<LibraryTransaction> Transactions;
        public readonly ReadOnlyObservableCollection<Visitor> Visitors;
        public readonly ReadOnlyObservableCollection<Book> Books;
        public readonly ReadOnlyObservableCollection<Author> Authors;

        public Manager()
        {
            try
            {
                using (LibraryDbContext db = new LibraryDbContext())
                {
                    _books = new ObservableCollection<Book>(db.Books);
                    _authors = new ObservableCollection<Author>(db.Authors);
                    _visitors = new ObservableCollection<Visitor>(db.Visitors);
                    _transactions = new ObservableCollection<LibraryTransaction>(db.Tranactions);
                }

                Transactions = new ReadOnlyObservableCollection<LibraryTransaction>(_transactions);
                Visitors = new ReadOnlyObservableCollection<Visitor>(_visitors);
                Books = new ReadOnlyObservableCollection<Book>(_books);
                Authors = new ReadOnlyObservableCollection<Author>(_authors);
            }
            catch
            {
                throw new DbConnectionException("Ошибка подключения к базе данных.");
            }
        }
        public void AddVisitor(Visitor visitor)
        {
            //TODO
        }
        public void AddBook(Book book)
        {
            //TODO
        }
        public void RemoveVisitor(Visitor visitor)
        {
            //TODO
        }
        public void RemoveBook(Book book)
        {
            //TODO
        }
        public void FindBook(int id)
        {
            //TODO
        }
        public void ChangeBookId(Book book)
        {
            //TODO
        }
        public void GetMonthReport()
        {
            //TODO
        }
        public void GetVisitorsCount()
        {
            //TODO
        }

    }
}
