namespace LibraryManager.Model
{
    static public class UniqueIDMaker
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
    }
}
