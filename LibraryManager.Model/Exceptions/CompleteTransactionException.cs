namespace LibraryManager.Model.Exceptions
{
    public class CompleteTransactionException : Exception
    {
        public CompleteTransactionException() { }

        public CompleteTransactionException(string message) : base(message) { }

        public CompleteTransactionException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
