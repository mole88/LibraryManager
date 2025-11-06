using LibraryManager.Model;

namespace LibraryManager.Tests
{
    public class LibraryDbContextTest
    {
        [Fact]
        public void GetConnectionString()
        {
            var infoPath = "D:/proj/Visual Studio/LibManage/Other/DBConnectionInfo.xml";

            var dbConnectionString = ConnectionStringGetter.GetConnectionString(infoPath);
            
            Assert.NotNull(dbConnectionString);
            Assert.NotEmpty(dbConnectionString);

        }

        [Fact]
        public void DbConnectionTest()
        {
            var conStr = $"Host=locat=localhost;Database=postgre;Username=postgre;Password=0000";

            var dbContext = new LibraryDbContext(conStr);
            Assert.NotNull(dbContext);
        }
    }
}
