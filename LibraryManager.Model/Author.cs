namespace LibraryManager.Model
{
    public class Author : IIdentifiable
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public List<Book> Books { get; set; } = new();
    }
}
