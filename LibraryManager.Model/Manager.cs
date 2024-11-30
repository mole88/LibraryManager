using System.Collections.ObjectModel;
using LibraryManager.Model.Exceptions;
using Microsoft.EntityFrameworkCore;

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
                Transactions = new ObservableCollection<LibraryTransaction>(_dbContext.Transactions);

            }
            catch(Exception ex)
            {
                throw new DbConnectionException("Ошибка подключения к базе данных:\n" + ex.Message);
            }
        }
        public async Task AddVisitorAsync(Visitor visitor)
        {
            Visitors.Add(visitor);
            _dbContext.Visitors.Add(visitor);
            await _dbContext.SaveChangesAsync();
        }
        public async Task AddBookAsync(Book book)
        {
            Books.Add(book);
            _dbContext.Books.Add(book);
            await _dbContext.SaveChangesAsync();
        }
        public async Task AddAuthorAsync(Author author)
        {
            Authors.Add(author);
            _dbContext.Authors.Add(author);
            await _dbContext.SaveChangesAsync();
        }
        public async Task AddTransactionAsync(LibraryTransaction transaction)
        {
            Transactions.Add(transaction);
            _dbContext.Transactions.Add(transaction);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveVisitorAsync(Visitor visitor)
        {
            Visitors.Remove(visitor);
            _dbContext.Visitors.Remove(visitor);
            await _dbContext.SaveChangesAsync();
        }
        public async Task RemoveBookAsync(Book book)
        {
            Books.Remove(book);
            _dbContext.Books.Remove(book);
            await _dbContext.SaveChangesAsync();
        }
        public async Task RemoveAuthorAsync(Author author)
        {
            Authors.Remove(author);
            _dbContext.Authors.Remove(author);
            await _dbContext.SaveChangesAsync();
        }
        public async Task RemoveTransactionAsync(LibraryTransaction transaction)
        {
            Transactions.Remove(transaction);
            _dbContext.Transactions.Remove(transaction);
            await _dbContext.SaveChangesAsync();
        }

        //ИСПРАВИТЬ ИЗМЕНЕНИЕ ID
        public async Task EditBookAsync(Book oldVer, Book newVer)
        {
            int index = Books.IndexOf(oldVer);
            if (index == -1)
                throw new InvalidOperationException("Книга не найдена в коллекции.");

            var existingBook = await _dbContext.Books.FindAsync(oldVer.Id);
            if (existingBook == null)
                throw new InvalidOperationException("Книга не найдена в базе данных.");

            if (oldVer.Id != newVer.Id)
            {
                var newBookWithSameId = await _dbContext.Books.FindAsync(newVer.Id);
                if (newBookWithSameId != null)
                    throw new InvalidOperationException("Книга с таким ID уже существует.");

                using var transaction = await _dbContext.Database.BeginTransactionAsync();
                try
                {
                    // 1. Добавляем или обновляем новую книгу с новым ID
                    var newBook = new Book
                    {
                        Id = newVer.Id,
                        Name = newVer.Name,
                        Year = newVer.Year,
                        AuthorId = newVer.AuthorId,
                        IsAvailable = newVer.IsAvailable,
                        CreationDate = newVer.CreationDate
                    };
                    _dbContext.Books.Add(newBook); // Вставляем запись с новым ID
                    await _dbContext.SaveChangesAsync();

                    // 2. Обновляем внешние ключи в таблице transactions
                    await _dbContext.Transactions
                        .Where(t => t.BookId == oldVer.Id)
                        .ExecuteUpdateAsync(t => t.SetProperty(t => t.BookId, newVer.Id));

                    // 3. Удаляем старую книгу
                    _dbContext.Books.Remove(existingBook);
                    await _dbContext.SaveChangesAsync();

                    // Завершаем транзакцию
                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
            else
            {
                _dbContext.Entry(existingBook).CurrentValues.SetValues(newVer);
                await _dbContext.SaveChangesAsync();
            }
        }


        public async Task EditVisitorAsync(Visitor oldVer, Visitor newVer)
        {
            int index = Visitors.IndexOf(oldVer);
            if (index != -1)
            {
                var existingVisitor = await _dbContext.Visitors.FindAsync(oldVer.Id);

                if (existingVisitor != null)
                {    
                    _dbContext.Entry(existingVisitor).CurrentValues.SetValues(newVer);
                    await _dbContext.SaveChangesAsync();
                    Visitors[index] = newVer;
                }
            }
        }

        public async Task EditAuthorAsync(Author oldVer, Author newVer)
        {
            int index = Authors.IndexOf(oldVer);
            if (index != -1)
            {
                var existingAuthor = await _dbContext.Authors.FindAsync(oldVer.Id);

                if (existingAuthor != null)
                {
                    _dbContext.Entry(existingAuthor).CurrentValues.SetValues(newVer);
                    await _dbContext.SaveChangesAsync();

                    Authors[index] = newVer;
                }
            }
        }

        public async Task EditTransactionAsync(LibraryTransaction oldVer, LibraryTransaction newVer)
        {
            int index = Transactions.IndexOf(oldVer);
            if (index != -1)
            {
                var existingTrans = await _dbContext.Transactions.FindAsync(oldVer.Id);

                if (existingTrans != null)
                {
                    _dbContext.Entry(existingTrans).CurrentValues.SetValues(newVer);
                    await _dbContext.SaveChangesAsync();

                    Transactions[index] = newVer;
                }
            }
        }

        public Book? FindBook(int id)
        {
            return Books.FirstOrDefault(b => b.Id == id);
        }
        public Visitor? FindVisitor(int id)
        {
            return Visitors.FirstOrDefault(v => v.Id == id);
        }
        public Author? FindAuthor(int id)
        {
            return Authors.FirstOrDefault(a => a.Id == id);
        }
        public LibraryTransaction? FindTransaction(int id)
        {
            return Transactions.FirstOrDefault(t => t.Id == id);
        }
    }
}
