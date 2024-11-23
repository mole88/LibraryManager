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

        private readonly LibraryDbContext _dbContext;
        private readonly string dbInfoFilePath = "D:/proj/Visual Studio/LibManage/Other/DBConnectionInfo.xml";
        public Manager()
        {
            try
            {
                string dbConnectionString = ConnectionStringGetter.GetConnectionString(dbInfoFilePath);
                _dbContext = new LibraryDbContext(dbConnectionString);

                _books = new ObservableCollection<Book>(_dbContext.Books);
                _authors = new ObservableCollection<Author>(_dbContext.Authors);
                _visitors = new ObservableCollection<Visitor>(_dbContext.Visitors);
                _transactions = new ObservableCollection<LibraryTransaction>(_dbContext.Tranactions);

                Transactions = new ReadOnlyObservableCollection<LibraryTransaction>(_transactions);
                Visitors = new ReadOnlyObservableCollection<Visitor>(_visitors);
                Books = new ReadOnlyObservableCollection<Book>(_books);
                Authors = new ReadOnlyObservableCollection<Author>(_authors);
            }
            catch(Exception ex)
            {
                throw new DbConnectionException("Ошибка подключения к базе данных:\n" + ex.Message);
            }
        }
        public void AddVisitor(Visitor visitor)
        {
            _visitors.Add(visitor);
            _dbContext.Visitors.Add(visitor);
            _dbContext.SaveChanges();
        }
        public void AddBook(Book book)
        {
            _books.Add(book);
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
        }
        public void AddAuthor(Author author)
        {
            _authors.Add(author);
            _dbContext.Authors.Add(author);
            _dbContext.SaveChanges();
        }
        public void AddTransaction(LibraryTransaction transaction)
        {
            _transactions.Add(transaction);
            _dbContext.Tranactions.Add(transaction);
            _dbContext.SaveChanges();
        }

        public void RemoveVisitor(Visitor visitor)
        {
            _visitors.Remove(visitor);
            _dbContext.Visitors.Remove(visitor);
            _dbContext.SaveChanges();
        }
        public void RemoveBook(Book book)
        {
            _books.Remove(book);
            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
        }
        public void RemoveAuthor(Author author)
        {
            _authors.Remove(author);
            _dbContext.Authors.Remove(author);
            _dbContext.SaveChanges();
        }
        public void RemoveTransaction(LibraryTransaction transaction)
        {
            _transactions.Remove(transaction);
            _dbContext.Tranactions.Remove(transaction);
            _dbContext.SaveChanges();
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
