using LibraryManager.Model;

namespace LibraryManager.Tests
{
    public class ManagerTest
    {
        [Fact]
        public async Task AddBookTest()
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
            await manager.AddBookAsync(newBook);

            int newBooksCount = manager.Books.Count;

            Assert.Equal(oldBooksCount + 1, newBooksCount);
        }
    }
}
