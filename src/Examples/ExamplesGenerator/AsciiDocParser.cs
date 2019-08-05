using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ExamplesGenerator {
	public static class AsciiDocParser
	{
		public static List<Page> ParsePages(string path)
		{
			var file = File.ReadAllLines(path);
			var pages = new Dictionary<string, Page>();

			for (var index = 0; index < file.Length; index++)
			{
				var line = file[index];
				if (line.StartsWith("=== "))
				{
					// title
					var match = Regex.Match(line, @"=== (?<name>.*?)\.asciidoc: line (?<lineNumber>\d+): (?<hash>.*)$");
					if (!match.Success)
					{
						Console.WriteLine($"Could not find title match, line: {index}, input: {line}");
						continue;
					}

					var name = match.Groups["name"].Value;
					var lineNumber = int.Parse(match.Groups["lineNumber"].Value);
					var hash = match.Groups["hash"].Value;

					// skip to start of body
					index += 3;
					line = file[index];
					var builder = new StringBuilder();

					while (line != "----")
					{
						builder.AppendLine(line);
						index++;
						line = file[index];
					}

					var content = builder.ToString();

					index += 2;
					line = file[index];

					var languages = line.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
					index += 2;
					line = file[index];
					var implemented = line.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

					var exampleLanguages = languages
						.Zip(implemented, (l, i) => new Language { Name = l.Trim(), Implemented = i.Trim() == "&check;" })
						.ToList();

					index++;

					if (!pages.TryGetValue(name, out var page))
					{
						page = new Page(name);
						pages.Add(name, page);
					}

					var example = new Example(hash, lineNumber, content);
					example.Languages.AddRange(exampleLanguages);
					page.Examples.Add(example);
				}
			}

			return pages.Values.ToList();
		}
	}
}
