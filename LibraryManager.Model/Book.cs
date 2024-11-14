namespace LibraryManager.Model
{
    public class Book : IIdentifiable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AuthorId { get; set; }
        public Author? BookAuthor { get; set; }
        public int Year { get; set; }
        public bool IsAvailable { get; set; }
        public List<LibraryTransaction> Transactions { get; set; } = new();
    }
}
