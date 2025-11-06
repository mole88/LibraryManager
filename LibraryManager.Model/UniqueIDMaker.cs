namespace LibraryManager.Model
{
    public static class UniqueIDMaker
    {
        public static int GetUniqueID<T>(IEnumerable<T> array) where T : IIdentifiable
        {
            var existingIds = new HashSet<int>(array.Select(item => item.Id));
            int minUniqueValue = 1;
            while (existingIds.Contains(minUniqueValue))
            {
                minUniqueValue++;
            }

            return minUniqueValue;
        }
        public static bool IsUnique<T>(int id, IEnumerable<T> array) where T: IIdentifiable
        {
            return array.All(item => item.Id != id);
        }
    }
}
