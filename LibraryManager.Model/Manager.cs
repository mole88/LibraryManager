using System.Collections.ObjectModel;
using System.Transactions;
using LibraryManager.Model.Exceptions;

namespace LibraryManager.Model
{
    public class Manager 
    {
        public ObservableCollection<LibraryTransaction> Transactions { get; private set; }
        public ObservableCollection<Visitor> Visitors { get; private set; }
        public ObservableCollection<Book> Books { get; private set; }
        public ObservableCollection<Author> Authors { get; private set; }

        private readonly LibraryDbContext _dbContext;
        private readonly string dbInfoFilePath = "./Other/DBConnectionInfo.xml";

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
            catch (Exception ex)
            {
                throw new DbConnectionException("Ошибка подключения к базе данных: " + ex.Message, ex);
            }
        }

        public async Task AddVisitorAsync(Visitor visitor)
        {
            try
            {
                Visitors.Add(visitor);
                _dbContext.Visitors.Add(visitor);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new AddRecordException("Ошибка добавления посетителя: " + ex.Message, ex);
            }
        }

        public async Task AddBookAsync(Book book)
        {
            try
            {
                Books.Add(book);
                _dbContext.Books.Add(book);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new AddRecordException("Ошибка добавления книги: " + ex.Message, ex);
            }
        }

        public async Task AddAuthorAsync(Author author)
        {
            try
            {
                Authors.Add(author);
                _dbContext.Authors.Add(author);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new AddRecordException("Ошибка добавления автора:", ex);
            }
        }

        public async Task AddTransactionAsync(LibraryTransaction transaction)
        {
            try
            {
                if (transaction.Book.IsAvailable)
                {
                    transaction.Book.IsAvailable = false;
                    Transactions.Add(transaction);
                    _dbContext.Transactions.Add(transaction);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new TransactionException("Ошибка добавления транзакции: " + ex.Message, ex);
            }
        }

        public async Task RemoveVisitorAsync(Visitor visitor)
        {
            try
            {
                if (visitor != null)
                {
                    var transactionsToRemove = Transactions.Where(t => t.Visitor == visitor).ToList();
                    foreach (var transaction in transactionsToRemove)
                    {
                        transaction.Book.IsAvailable = true;
                        Transactions.Remove(transaction);
                        _dbContext.Transactions.Remove(transaction);
                    }

                    Visitors.Remove(visitor);
                    _dbContext.Visitors.Remove(visitor);

                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new RemoveRecordException("Ошибка удаления посетителя: " + ex.Message, ex);
            }
        }

        public async Task RemoveBookAsync(Book book)
        {
            try
            {
                if (book != null && book.IsAvailable)
                {
                    var transactionsToRemove = Transactions.Where(t => t.Book == book).ToList();
                    foreach (var transaction in transactionsToRemove)
                    {
                        Transactions.Remove(transaction);
                        _dbContext.Transactions.Remove(transaction);
                    }

                    Books.Remove(book);
                    _dbContext.Books.Remove(book);

                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new RemoveRecordException("Ошибка удаления книги: " + ex.Message, ex);
            }
        }

        public async Task RemoveAuthorAsync(Author author)
        {
            try
            {
                if (author != null)
                {
                    var booksToRemove = Books.Where(b => b.AuthorId == author.Id).ToList();
                    foreach (var book in booksToRemove)
                    {
                        await RemoveBookAsync(book);
                    }
                    Authors.Remove(author);
                    _dbContext.Authors.Remove(author);

                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new RemoveRecordException("Ошибка удаления автора: " + ex.Message, ex);
            }
        }
        public async Task RemoveTransactionAsync(LibraryTransaction transaction)
        {
            try
            {
                if (transaction != null)
                {
                    transaction.Book.IsAvailable = true;
                    transaction.Visitor.Transactions.Remove(transaction);
                    Transactions.Remove(transaction);
                    _dbContext.Transactions.Remove(transaction);

                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new EditRecordException("Ошибка удаления книги: " + ex.Message, ex);
            }
        }

        public async Task EditBookAsync(Book oldVer, Book newVer)
        {
            int index = Books.IndexOf(oldVer);
            var existingBook = await _dbContext.Books.FindAsync(oldVer.Id);

            if (oldVer.Id != newVer.Id)
            {
                var newBookWithSameId = await _dbContext.Books.FindAsync(newVer.Id);
                if (newBookWithSameId != null)
                    throw new EditRecordException("Книга с таким ID уже существует.");

                using var transaction = await _dbContext.Database.BeginTransactionAsync();
                try
                {
                    var newBook = new Book
                    {
                        Id = newVer.Id,
                        Name = newVer.Name,
                        Year = newVer.Year,
                        AuthorId = newVer.AuthorId,
                        IsAvailable = newVer.IsAvailable,
                        CreationDate = newVer.CreationDate
                    };
                    _dbContext.Books.Add(newBook);

                    await _dbContext.SaveChangesAsync();
                    foreach (var transactionToUpdate in _dbContext.Transactions.Where(t => t.BookId == oldVer.Id))
                    {
                        transactionToUpdate.BookId = newVer.Id;
                    }
                    await _dbContext.SaveChangesAsync();
                    _dbContext.Books.Remove(existingBook);
                    await _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                    Books[index] = newBook;
                }
                catch(Exception ex)
                {
                    await transaction.RollbackAsync();

                    throw new EditRecordException("Ошибка редактирования книги: " + ex.Message, ex);
                }
            }
            else
            {
                _dbContext.Entry(existingBook).CurrentValues.SetValues(newVer);
                await _dbContext.SaveChangesAsync();
                Books[index] = newVer;
            }
        }
        public async Task EditVisitorAsync(Visitor oldVer, Visitor newVer)
        {
            try
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
            catch (Exception ex)
            {
                throw new EditRecordException("Ошбика изменения посетителя: " + ex.Message, ex);
            }
        }
        public async Task EditAuthorAsync(Author oldVer, Author newVer)
        {
            try
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
            catch (Exception ex)
            {
                throw new EditRecordException("Ошибка изменения автора: " + ex.Message, ex);
            }
        }

        public async Task EditTransactionAsync(LibraryTransaction oldVer, LibraryTransaction newVer)
        {
            int index = Transactions.IndexOf(oldVer);
            if (index != -1)
            {

                using var action = await _dbContext.Database.BeginTransactionAsync();
                try
                {
                    _dbContext.Transactions.Remove(oldVer);
                    await _dbContext.SaveChangesAsync();

                    var newBook = new LibraryTransaction
                    {
                        Id = newVer.Id,
                        Book = newVer.Book,
                        BookId = newVer.Id,
                        Visitor = newVer.Visitor,
                        VisitorId = newVer.VisitorId,
                        DateTaken = newVer.DateTaken,
                        DueDate = newVer.DueDate,
                        ReturnDate = newVer.ReturnDate,
                        IsAvailable = newVer.IsAvailable
                    };
                    _dbContext.Transactions.Add(newBook);
                    await _dbContext.SaveChangesAsync();

                    await action.CommitAsync();

                    Transactions[index] = newBook;
                    if (newVer.Book != oldVer.Book)
                    {
                        newVer.Book.IsAvailable = false;
                        oldVer.Book.IsAvailable = true;
                    }

                    var existingTrans = await _dbContext.Transactions.FindAsync(oldVer.Id);
                    _dbContext.Entry(existingTrans).CurrentValues.SetValues(newVer);
                }
                catch (Exception ex)
                {
                    await action.RollbackAsync();
                    throw new EditRecordException("Ошибка изменения транзакции: " + ex.Message, ex);
                }
            }
        }
        public async Task CompleteTransactionAsync(LibraryTransaction transaction)
        {
            try
            {
                if (transaction != null && transaction.IsAvailable == true)
                {
                    transaction.Book.IsAvailable = true;
                    transaction.ReturnDate = DateTime.Now;
                    transaction.IsAvailable = false;

                    _dbContext.Transactions.Update(transaction);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new CompleteTransactionException("Ошибка завершения транзакции: " + ex.Message, ex);
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
