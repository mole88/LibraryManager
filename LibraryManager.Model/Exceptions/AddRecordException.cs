namespace LibraryManager.Model.Exceptions
{
    public class AddRecordException : Exception
    {
        public AddRecordException() { }

        public AddRecordException(string message) : base(message) { }

        public AddRecordException(string message, Exception innerException)
            : base(message, innerException) { }
    }

}
