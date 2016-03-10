using System;
using System.Text.RegularExpressions;

namespace Nest.Litterateur
{
	public static class StringExtensions
	{
		public static string PascalToHyphen(this string input)
		{
			if (string.IsNullOrEmpty(input)) return string.Empty;

			return Regex.Replace(
				Regex.Replace(
					Regex.Replace(input, @"([A-Z]+)([A-Z][a-z])", "$1-$2"), @"([a-z\d])([A-Z])", "$1-$2")
				, @"[-\s]", "-").ToLower();
		}

		public static string TrimDocExtension(this string input)
		{
			if (string.IsNullOrEmpty(input)) return string.Empty;

			return input.EndsWith(".doc", StringComparison.OrdinalIgnoreCase)
				? input.Substring(0, input.Length - 4)
				: input;
		}

		public static string RemoveLeadingAndTrailingMultiLineComments(this string input)
		{
			var match = Regex.Match(input, @"^(?<value>[ \t]*\/\*)");

			if (match.Success)
			{
				input = input.Substring(match.Groups["value"].Value.Length);
			}

			match = Regex.Match(input, @"(?<value>\*\/[ \t]*)$");

			if (match.Success)
			{
				input = input.Substring(0, input.Length - match.Groups["value"].Value.Length);
			}

			return input;
		}

		public static string RemoveLeadingSpacesAndAsterisk(this string input)
		{
			var match = Regex.Match(input, @"^(?<value>[ \t]*\*\s?).*");

			if (match.Success)
			{
				input = input.Substring(match.Groups["value"].Value.Length);
			}

			return input;
		}

		public static string RemoveNumberOfLeadingTabsAfterNewline(this string input , int numberOfTabs)
		{
			return Regex.Replace(input, $"(?<tabs>[\n|\r\n]+\t{{{numberOfTabs}}})", m => m.Value.Replace("\t", string.Empty));
		}
	}
}