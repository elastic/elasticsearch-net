using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using DiffPlex;
using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;
using Newtonsoft.Json.Linq;

namespace Tests.Core.Extensions
{
	public static class DiffExtensions
	{
		public static void DeepSort(this JObject jObj)
		{
			if (jObj == null) return;

			var props = jObj.Properties().ToList();
			foreach (var prop in props)
			{
				prop.Remove();
			}

			foreach (var prop in props.OrderBy(p => p.Name))
			{
				jObj.Add(prop);
				var o = prop.Value as JObject;
				o?.DeepSort();
			}
		}

		public static string CreateCharacterDifference(this string expected, string actual, string message = null)
		{
			var d = new Differ();
			var result = d.CreateCharacterDiffs(expected.Trim('"'), actual.Trim('"'), ignoreWhitespace: false);
			if (!result.DiffBlocks.Any()) return string.Empty;
			var builder = new StringBuilder()
				.AppendLine(message)
				.AppendLine($"expect: \"{expected}\"")
				.AppendLine($"actual: \"{actual}\"")
				.AppendLine();

			//TODO come up with a clever way to dispay inline character differences.
			//Looked at the diffplex source but they don't have one either
			return builder.ToString();
		}

		public static string DiffNoApproximation(this string expected, string actual, string message = null) => CreateDiff(expected, actual, message, new Differ());

		public static string Diff(this string expected, string actual, string message = null)
		{
			var d = new Differ();
			var diff = CreateDiff(expected, actual, message, d);
			return !string.IsNullOrWhiteSpace(diff) ? AppendCsharpApproximation(expected, actual, diff) : diff;
		}

		private static string CreateDiff(string expected, string actual, string message, Differ d)
		{
			var inlineBuilder = new InlineDiffBuilder(d);
			var result = inlineBuilder.BuildDiffModel(expected, actual);
			var hasChanges = result.Lines.Any(l => l.Type != ChangeType.Unchanged);
			if (!hasChanges) return string.Empty;


			return result.Lines.Aggregate(new StringBuilder().AppendLine(message), (sb, line) =>
			{
				if (line.Type == ChangeType.Inserted)
					sb.Append("+ ");
				else if (line.Type == ChangeType.Deleted)
					sb.Append("- ");
				else
					sb.Append("  ");
				sb.AppendLine(line.Text);
				return sb;
			}, sb => sb.ToString());
		}

		private static string AppendCsharpApproximation(string expected, string actual, string diff)
		{
			diff += "\r\n C# approximation of actual ------ ";
			diff += "\r\n new ";
			var approx = Regex.Replace(actual, @"^(?=.*:.*)[^:]+:", (s) => s
							.Value.Replace("\"", "")
							.Replace(":", " =")
						, RegexOptions.Multiline)
					.Replace(" = {", " = new {")
					.Replace(" = [", " = new [] {")
				;
			approx = Regex.Replace(approx, @"^\s*\],?.*$", s => s.Value.Replace("]", "}"), RegexOptions.Multiline);
			diff += approx + ";";

			diff += "\r\n C# approximation of expected ------ ";
			diff += "\r\n new ";
			approx = Regex.Replace(expected, @"^(?=.*:.*)[^:]+:", (s) => s
							.Value.Replace("\"", "")
							.Replace(":", " =")
						, RegexOptions.Multiline)
					.Replace(" = {", " = new {")
					.Replace(" = [", " = new [] {")
				;
			approx = Regex.Replace(approx, @"^\s*\],?.*$", s => s.Value.Replace("]", "}"), RegexOptions.Multiline);
			diff += approx + ";";
			return diff;
		}
	}
}
