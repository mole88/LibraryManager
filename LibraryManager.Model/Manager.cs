namespace LibraryManager.Model
{
    public class Manager
    {
        private readonly List<LibraryTransaction> _transactions;
        private readonly List<Visitor> _visitors;
        private readonly List<Book> _books;
        private readonly List<Author> _authors;

        public Manager()
        {
            using (LibraryDbContext db = new LibraryDbContext())
            {
                _books = db.Books.ToList();
                _authors = db.Authors.ToList();
                _visitors = db.Visitors.ToList();
                _transactions = db.Tranactions.ToList();
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
