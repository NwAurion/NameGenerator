
namespace NameGenerator.extension
{
    public static class StringExtensions
    {
        public static bool NullOrEmpty(this string s)
        {
            if (s == null || s.Equals(""))
                return true;
            else
                return false;
        }
    }
}
