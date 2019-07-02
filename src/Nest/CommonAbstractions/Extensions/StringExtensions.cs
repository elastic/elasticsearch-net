using System.Collections.Specialized;

namespace Nest
{
	internal static class StringExtensions
	{
		internal static string ToCamelCase(this string s)
		{
			if (string.IsNullOrEmpty(s)) return s;

			if (!char.IsUpper(s[0])) return s;

			var camelCase = char.ToLowerInvariant(s[0]).ToString();
			if (s.Length > 1)
				camelCase += s.Substring(1);

			return camelCase;
		}
	}
}
