namespace Task.TestHelpers
{
    using System.Reflection;

    public static class PropertyCloner
    {
        public static void CopyProperties<TResult>(TResult source, TResult destinationObject)
        {
            if (source == null || destinationObject == null)
            {
                return;
            }

            foreach (var propertyInfo in typeof(TResult).GetProperties())
            {
                typeof(TResult)
                    .GetProperty(propertyInfo.Name,
                        BindingFlags.IgnoreCase |
                        BindingFlags.Instance |
                        BindingFlags.Public)
                    ?.SetValue(destinationObject,
                        propertyInfo.GetValue(source));
            }
        }

    }
}