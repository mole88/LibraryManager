namespace LibraryManager.Model.Exceptions
{
    public class EditRecordException : Exception
    {
        public EditRecordException() { }

        public EditRecordException(string message) : base(message) { }

        public EditRecordException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
