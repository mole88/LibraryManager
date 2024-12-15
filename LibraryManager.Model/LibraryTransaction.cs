namespace LibraryManager.Model
{
    public class LibraryTransaction : IIdentifiable
    {
        public int Id { get; set; }
        public int VisitorId { get; set; }
        public Visitor Visitor { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public DateTime DateTaken { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsAvailable { get; set; }
    }
}
