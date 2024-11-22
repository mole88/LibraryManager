using LibraryManager.Model;

namespace LibraryManager.Tests
{
    public class ManagerTest
    {
        [Fact]
        public void AddBookTest()
        {
            var manager = new Manager();

            int oldBooksCount = manager.Books.Count;

            var newBook = new Book()
            {
                AuthorId = 1,
                BookAuthor = manager.Authors[0],
                Name = "TestBook",
                Year = 1010
            };
            manager.AddBook(newBook);

            int newBooksCount = manager.Books.Count;

            Assert.Equal(oldBooksCount + 1, newBooksCount);
        }
    }
}
