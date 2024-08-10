namespace GitRepoManager;

public static class Extensions
{
    public static void Guard(this string value, params string[] values)
    {
        // Check if the value is null or empty
        if (string.IsNullOrEmpty(value))
        {
            return;
        }
        
        // Check if the value is in the list of values (ignore case)
        if (values.Any(v => v.Equals(value, StringComparison.OrdinalIgnoreCase)))
        {
            return;   
        }

        throw new ArgumentException();
    }
}