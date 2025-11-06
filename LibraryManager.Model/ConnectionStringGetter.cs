using System.Xml.Linq;

namespace LibraryManager.Model
{
    public class ConnectionStringGetter
    {
        public static string GetConnectionString(string filePath)
        {
            var doc = XDocument.Load(filePath);

            string host = doc.Root.Element("Host").Value;
            string port = doc.Root.Element("Port").Value;
            string database = doc.Root.Element("Database").Value;
            string username = doc.Root.Element("Username").Value;
            string password = doc.Root.Element("Password").Value;

            string connectionString = $"Host={host};Port={port};Database={database};Username={username};Password={password}";

            return connectionString;
        }
    }
}
