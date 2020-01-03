using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace ExamplesGenerator
{
	internal static class AsciiDocParser
	{
		private static readonly Regex NameRegex =
			new Regex(@"(?<name>.*?)\.a(?:scii)?doc$");

		public static List<ReferencePage> ParsePages(string path)
		{
			var pages = new Dictionary<string, ReferencePage>();

			using (var stream = GetStreamFromPath(path))
			using (var document = JsonDocument.Parse(stream))
			{
				var i = 0;
				foreach (var element in document.RootElement.EnumerateArray())
				{
					var lang = element.GetProperty("lang").GetString();

					// skip anything other than console examples for now, such
					// as console-result.
					if (lang != "console")
						continue;

					var sourceLocation = element.GetProperty("source_location");
					var file = sourceLocation.GetProperty("file").GetString();
					var match = NameRegex.Match(file);
					if (!match.Success)
					{
						Console.WriteLine($"Could not find title match, index {i}");
						continue;
					}

					var name = match.Groups["name"].Value;
					var lineNumber = sourceLocation.GetProperty("line").GetInt32();
					var hash = element.GetProperty("digest").GetString();

					var languages = element.GetProperty("found")
						.EnumerateArray()
						.Select(s => new Language { Name = s.GetString(), Implemented = true })
						.ToList();

					var content = element.GetProperty("source").GetString();

					if (!pages.TryGetValue(name, out var page))
					{
						page = new ReferencePage(name);
						pages.Add(name, page);
					}

					var example = new ReferenceExample(hash, lineNumber, content);
					example.Languages.AddRange(languages);

					if (!page.Examples.Contains(example))
						page.Examples.Add(example);

					i++;
				}
			}

			return pages.Values.ToList();
		}

		private static Stream GetStreamFromPath(string path)
		{
			var uri = new Uri(path);

			if (uri.IsFile)
				return File.OpenRead(path);

			using (var client = new WebClient())
			{
				try
				{
					return client.OpenRead(path);
				}
				catch (Exception e)
				{
					throw new Exception($"Could not download reference file from {path}", e);
				}
			}
		}
	}
}
