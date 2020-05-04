// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Tests.Core.VsTest
{
	internal static class WordWrapper
	{
		public static void WriteWordWrapped(this string paragraph, Action<string> write = null, bool printAll = true, int tabSize = 4, int indent = 7)
		{
			write ??= Console.WriteLine;
			var lines = paragraph.ToWordWrappedLines(tabSize, indent);
			if (!printAll)
				lines = lines.Take(2).Concat(new[] { $"{new string(' ', indent)} ..abbreviated.." });
			foreach (var line in lines)
				write(line);
		}

		public static IEnumerable<string> ToWordWrappedLines(this string paragraph, int tabSize = 4, int indent = 7)
		{
			var lines = paragraph
				.Replace("\t", new string(' ', tabSize))
				.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

			var indentation = new string(' ', indent);
			var spacedWith = Console.WindowWidth - indent;
			foreach (var l in lines)
			{
				var line = l;
				var wrapped = new List<string>();

				while (line.Length > spacedWith)
				{
					var wrapAt = line.LastIndexOf(' ', Math.Min(spacedWith - 1, line.Length));
					if (wrapAt <= 0) break;

					wrapped.Add(indentation + line.Substring(0, wrapAt));
					line = line.Remove(0, wrapAt + 1);
				}

				foreach (var wrap in wrapped)
					yield return wrap;

				yield return indentation + line;
			}
		}

		public static void WriteWithExceptionHighlighted(string text)
		{
			if (new Regex(@"^.*?Exception :").IsMatch(text))
			{
				var tokens = text.Split(new[] { ':' }, 2);
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.Write(tokens[0]);
				Console.ResetColor();
				Console.WriteLine($":{tokens[1]}");
			}
			else Console.WriteLine(text);
		}
	}
}
