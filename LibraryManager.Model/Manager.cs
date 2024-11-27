using System.Collections.ObjectModel;
using LibraryManager.Model.Exceptions;

namespace LibraryManager.Model
{
    public class Manager 
    {
        public ObservableCollection<LibraryTransaction> Transactions { get; private set; }
        public ObservableCollection<Visitor> Visitors { get; private set; }
        public ObservableCollection<Book> Books { get; private set; }
        public  ObservableCollection<Author> Authors { get; private set; }

        private readonly LibraryDbContext _dbContext;
        private readonly string dbInfoFilePath = "D:/proj/Visual Studio/LibManage/Other/DBConnectionInfo.xml";
        public Manager()
        {
            try
            {
                string dbConnectionString = ConnectionStringGetter.GetConnectionString(dbInfoFilePath);
                _dbContext = new LibraryDbContext(dbConnectionString);

                Books = new ObservableCollection<Book>(_dbContext.Books);
                Authors = new ObservableCollection<Author>(_dbContext.Authors);
                Visitors = new ObservableCollection<Visitor>(_dbContext.Visitors);
                Transactions = new ObservableCollection<LibraryTransaction>(_dbContext.Tranactions);

            }
            catch(Exception ex)
            {
                throw new DbConnectionException("Ошибка подключения к базе данных:\n" + ex.Message);
            }
        }
        public void AddVisitor(Visitor visitor)
        {
            Visitors.Add(visitor);
            _dbContext.Visitors.Add(visitor);
            _dbContext.SaveChanges();
        }
        public void AddBook(Book book)
        {
            Books.Add(book);
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
        }
        public void AddAuthor(Author author)
        {
            Authors.Add(author);
            _dbContext.Authors.Add(author);
            _dbContext.SaveChanges();
        }
        public void AddTransaction(LibraryTransaction transaction)
        {
            Transactions.Add(transaction);
            _dbContext.Tranactions.Add(transaction);
            _dbContext.SaveChanges();
        }

        public void RemoveVisitor(Visitor visitor)
        {
            Visitors.Remove(visitor);
            _dbContext.Visitors.Remove(visitor);
            _dbContext.SaveChanges();
        }
        public void RemoveBook(Book book)
        {
            Books.Remove(book);
            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
        }
        public void RemoveAuthor(Author author)
        {
            Authors.Remove(author);
            _dbContext.Authors.Remove(author);
            _dbContext.SaveChanges();
        }
        public void RemoveTransaction(LibraryTransaction transaction)
        {
            Transactions.Remove(transaction);
            _dbContext.Tranactions.Remove(transaction);
            _dbContext.SaveChanges();
        }
        public void EditBook(Book oldVer, Book newVer)
        {
            int index = Books.IndexOf(oldVer);
            if (index != -1)
            {
                Books[index] = newVer;

                var existingBook = _dbContext.Books.Find(oldVer.Id);

                if (existingBook != null)
                    _dbContext.Entry(existingBook).CurrentValues.SetValues(newVer);

                _dbContext.SaveChanges();
            }
        }
        public Book? FindBook(int id)
        {
            return Books.FirstOrDefault(b => b.Id == id);
        }
    }
}
