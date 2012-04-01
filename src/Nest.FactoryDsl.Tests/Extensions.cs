namespace Nest.Tests.FactoryDsl
{
    public static class Extensions
    {
        public static string Strip(this string s)
        {
            return s.Replace('\r', ' ').Replace('\n', ' ').Replace(" ", string.Empty);
        }

        public static string Strip(this object s)
        {
            return s.ToString().Replace('\r', ' ').Replace('\n', ' ').Replace(" ", string.Empty);
        }
    }
}