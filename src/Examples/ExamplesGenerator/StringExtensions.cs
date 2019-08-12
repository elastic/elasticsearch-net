using System.Text.RegularExpressions;

namespace ExamplesGenerator {
	internal static class StringExtensions
	{
		private static readonly Regex DoubleQuotes = new Regex("\"");

		private static readonly Regex NewLine = new Regex("\r?\n");

		private static readonly Regex LowercasePascal = new Regex(@"\b([a-z])");

		public static string EscapeDoubleQuotes(this string input) => DoubleQuotes.Replace(input, "\"\"");

		public static string Indent(this string input, string indent) => NewLine.Replace(input, "\n" + indent);

		public static string LowercaseHyphenUnderscoreToPascal(this string input) =>
			LowercasePascal.Replace(input
					.Replace("-", " ")
					.Replace("_", " "), m => m.Captures[0].Value.ToUpper())
				.Replace(" ", string.Empty);
	}
}
