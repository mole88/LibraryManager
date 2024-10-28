namespace LibraryManager.Model.Exceptions
{
    public class DbConnectionException : Exception
    {
        public DbConnectionException() { }

        public DbConnectionException(string message) : base(message) { }

        public DbConnectionException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
