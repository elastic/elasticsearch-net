// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Buffers;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Aggregations;

using RequestConverter.Hosting;

namespace RequestConverter.Console;

using System;
using System.Buffers.Text;

// TODO: A shortcut property basically defines a union. We can re-use the existing union (de-)serialization strategies.

internal class Program
{
	// Loaded from the shared data file so this harness and the round-trip test skip the same set. Only
	// entries that fail conversion are relevant here; entries that convert but fail round-trip validation
	// (the test's concern) are not skipped.
	private sealed record BlacklistEntry(string Digest, string Reason, bool FailsConversion);

	private static readonly IReadOnlySet<string> Blacklist = LoadBlacklist(
		LocateRepoFile(Path.Combine("tests", "RequestConverter.Tests", "TestData", "blacklist.json")),
		conversionOnly: true);

	private static IReadOnlySet<string> LoadBlacklist(string path, bool conversionOnly)
	{
		using var stream = File.OpenRead(path);
		var entries = JsonSerializer.Deserialize<BlacklistEntry[]>(stream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
			?? throw new InvalidOperationException("Failed to parse blacklist.json.");
		return entries.Where(e => !conversionOnly || e.FailsConversion).Select(e => e.Digest).ToHashSet(StringComparer.Ordinal);
	}

	private class Test
	{
		public int? A { get; set; }
		public int B { get; set; }
	}

	private static string LocateRepoFile(string relativePath)
	{
		var directory = new DirectoryInfo(AppContext.BaseDirectory);
		while (directory is not null)
		{
			if (File.Exists(Path.Combine(directory.FullName, "RequestConverter.sln")))
				return Path.Combine(directory.FullName, relativePath);

			directory = directory.Parent;
		}

		throw new InvalidOperationException("Repository root (RequestConverter.sln) not found above the application base directory.");
	}

	public static void Main(string[] args)
	{
		using var file = File.OpenRead(LocateRepoFile(Path.Combine("tests", "RequestConverter.Tests", "TestData", "alternatives_report.json")));

		var total = 0;
		var valid = 0;

		var examples = JsonSerializer.Deserialize<ExampleModel[]>(file, JsonSerializerOptions.Default)!;
		foreach (var example in examples)
		{
			if (example.Lang != "console")
			{
				continue;
			}

			++total;

			if (Blacklist.Contains(example.Digest))
			{
				continue;
			}

			if (example.ParsedSource is null or [])
			{
				continue;
			}

			if (example.ParsedSource.Any(x => x.Api is "bulk" or "msearch" or "msearch_template"))
			{
				// We currently don't support IStreamSerializable.
				continue;
			}

			Console.WriteLine(example.Digest);

			foreach (var source in example.ParsedSource)
			{
				Console.WriteLine(source.Api);

				var body = source.Body?.ToString();
				if (source.Api is "bulk" or "msearch" or "msearch_template")
				{
					body = source.Body!.Value.EnumerateArray().Aggregate("", (current, element) => current + (JsonSerializer.Serialize(element, JsonSerializerOptions.Default) + "\n"));
				}

				try
				{
					var result = RequestConverter.Convert(
						RequestConverter.DefaultSerializer,
						source.Api,
						source.PathParameters,
						source.QueryParameters,
						body
					);

					Console.WriteLine(result.Code);
					if (result.UnsupportedParameters.Count > 0)
					{
						Console.WriteLine($"WARN unsupported query parameters ({example.Digest}): {string.Join(", ", result.UnsupportedParameters)}");
					}
				}
				catch (NotSupportedException)
				{
					Console.WriteLine("not supported");
				}
			}

			++valid;
		}

		Console.WriteLine($"Total: {total}");
		Console.WriteLine($"Valid: {valid}");
		Console.WriteLine($"Ratio: {(float)valid / total * 100}");
	}
}
