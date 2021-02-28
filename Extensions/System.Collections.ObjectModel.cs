namespace System.Collections.ObjectModel
{
    public static class ObservableCollectionExtension
    {
        /// <summary>
        /// Adds an object to the end of the Collection<T> if its not null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="value"></param>
        public static void AddIfNotNull<T>(this ObservableCollection<T> collection, T value)
            where T : class
        {
            if (value != null) { collection.Add(value); }
        }
    }
}