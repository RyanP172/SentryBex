namespace SentryBex.Utilitty
{
    /// <summary>
    /// This class is for deep copy object purpose
    /// </summary>
    public static class UtilittyHelper
    {
        //Assign object to another object 
        public static void DeepCopyObjects<T>(this T source, T dest)
        {
            foreach (var prop in source.GetType().GetProperties())
            {
                var propValue = prop.GetValue(source);
                try
                {
                    if (dest.GetType().GetProperty(prop.Name) != null)
                    {
                        dest.GetType().GetProperty(prop.Name).SetValue(dest, propValue);
                    }
                }
                catch (Exception ex)
                {
                    if (dest.GetType().GetProperty(prop.Name) != null)
                    {
                        dest.GetType().GetProperty(prop.Name).SetValue(dest, propValue);
                    }
                }
            }
        }
    }
    
    
}
