namespace LibraryManager.Model.Exceptions
{
    public class FindRecordException : Exception
    {
        public FindRecordException() { }

        public FindRecordException(string message) : base(message) { }

        public FindRecordException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
