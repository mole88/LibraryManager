namespace LibraryManager.Client.SupportClasses
{
    public class EditEventArgs : EventArgs
    {
        public object EditedItem { get; }

        public EditEventArgs(object editedItem)
        {
            EditedItem = editedItem;
        }
    }
}
