namespace LibraryManager.Model.Exceptions
{
    public class RemoveRecordException : Exception
    {
        public RemoveRecordException() { }

        public RemoveRecordException(string message) : base(message) { }

        public RemoveRecordException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
