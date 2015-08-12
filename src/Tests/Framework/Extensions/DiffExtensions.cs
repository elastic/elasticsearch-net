using DiffPlex;
using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;
using System;
using System.Linq;
using System.Text;

namespace Tests.Framework
{
	public static class DiffExtensions
	{

		public static void Diff(this byte[] expected, byte[] actual, string message = null)
		{
			Encoding.UTF8.GetString(expected).Diff(Encoding.UTF8.GetString(actual), message);
		}
		public static void Diff(this string expected, string actual, string message = null)
		{
			var d = new Differ();
			var inlineBuilder = new InlineDiffBuilder(d);
			var result = inlineBuilder.BuildDiffModel(expected, actual);
			var hasChanges = result.Lines.Any(l => l.Type != ChangeType.Unchanged);
			if (!hasChanges) return;

			var diff = result.Lines.Aggregate(new StringBuilder().AppendLine(message), (sb, line) =>
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
			throw new Exception(diff);
		}

	}
}