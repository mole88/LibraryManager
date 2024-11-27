namespace LibraryManager.Model
{ 
    public class Visitor : IIdentifiable
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public int Age { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public List<LibraryTransaction> Transactions { get; set; } = new();
        internal void TakeBook(Book book)
        {
            //TODO
        }
        internal void ReturnBook(Book book)
        {
            //TODO
        }
    }
}
