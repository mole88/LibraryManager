namespace LibraryManager.Client.SupportClasses
{
    public class ObjEventArgs(object item) : EventArgs
    {
        public object Item { get; } = item;
    }
}
