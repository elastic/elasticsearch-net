// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

using Xunit;
using Xunit.Abstractions;

using RequestConverter;
using RequestConverter.Hosting;

namespace RequestConverter.Tests;

/// <summary>
/// Compile-only corpus pass for the client-call output formats. For every valid, non-blacklisted example
/// in <c>alternatives_report.json</c> it generates the C# snippet via the converter, wraps it in a
/// statement-bodied async method that accepts an <c>ElasticsearchClient</c>, and compiles all snippets
/// in a single Roslyn pass. No execution or round-trip is attempted.
/// </summary>
public sealed class ClientCallCompilationTests
{
	private readonly ITestOutputHelper _output;

	public ClientCallCompilationTests(ITestOutputHelper output) => _output = output;

	[Theory]
	[InlineData(ClientCallFormat.Statement, SyntaxMode.ObjectInitializer)]
	[InlineData(ClientCallFormat.Statement, SyntaxMode.Descriptor)]
	[InlineData(ClientCallFormat.Inline, SyntaxMode.ObjectInitializer)]
	[InlineData(ClientCallFormat.Inline, SyntaxMode.Descriptor)]
	public void Converted_client_calls_compile(ClientCallFormat format, SyntaxMode syntaxMode)
	{
		var reportPath = LocateRepoRootFile(Path.Combine("tests", "RequestConverter.Tests", "TestData", "alternatives_report.json"));
		using var reportStream = File.OpenRead(reportPath);
		var examples = JsonSerializer.Deserialize<ExampleModel[]>(reportStream)
			?? throw new InvalidOperationException("Failed to parse alternatives_report.json.");

		var serializer = global::RequestConverter.RequestConverter.DefaultSerializer;
		var options = new FormattingOptions { ClientCallFormat = format, SyntaxMode = syntaxMode };

		var codes = new List<(string Code, IReadOnlyCollection<string> Namespaces)>();
		var convertFailures = new List<string>();
		var skippedUnsupported = 0;

		foreach (var example in examples)
		{
			if (example.Lang != "console" || example.ParsedSource is null || example.ParsedSource.Count == 0)
				continue;
			if (Blacklist.Contains(example.Digest))
				continue;

			foreach (var source in example.ParsedSource)
			{
				var body = source.Body?.GetRawText();
				try
				{
					var result = global::RequestConverter.RequestConverter.Convert(
						serializer, source.Api, source.PathParameters, source.QueryParameters, body, options);
					codes.Add((result.Code, result.Namespaces));
				}
				catch (NotSupportedException)
				{
					skippedUnsupported++;
				}
				catch (Exception ex)
				{
					convertFailures.Add($"{source.Api} [{example.Digest}]: convert threw {ex.GetType().Name}: {ex.Message}");
				}
			}
		}

		var outputDir = Path.Combine(Path.GetTempPath(), "RequestConverter.Tests", $"generated_clientcall_{format}_{syntaxMode}");
		Directory.CreateDirectory(outputDir);
		foreach (var stale in Directory.GetFiles(outputDir, "*.cs"))
			File.Delete(stale);

		var trees = new List<SyntaxTree>(codes.Count);
		for (var i = 0; i < codes.Count; i++)
		{
			var source = BuildSource(i, codes[i].Code, codes[i].Namespaces);
			var file = Path.Combine(outputDir, $"C_{i}.cs");
			File.WriteAllText(file, source);
			trees.Add(CSharpSyntaxTree.ParseText(source, path: file));
		}

		var compilation = CSharpCompilation.Create(
			$"RequestConverter.ClientCall.{format}.{syntaxMode}",
			trees,
			CompilationHarness.ReferenceAssemblies(),
			new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
				.WithNullableContextOptions(NullableContextOptions.Annotations));

		using var peStream = new MemoryStream();
		var emit = compilation.Emit(peStream);

		var compileErrors = emit.Diagnostics
			.Where(d => d.Severity == DiagnosticSeverity.Error)
			.Select(d => d.ToString())
			.ToList();

		_output.WriteLine($"converted={codes.Count}, skipped(unsupported)={skippedUnsupported}, convert-failures={convertFailures.Count}");

		Assert.True(emit.Success,
			$"Generated code failed to compile ({compileErrors.Count} errors). Snippets saved under '{outputDir}'.\n" +
			$"First compile errors:\n{string.Join("\n", compileErrors.Take(40))}\n" +
			(convertFailures.Count > 0 ? $"Convert failures ({convertFailures.Count}):\n{string.Join("\n", convertFailures.Take(20))}" : ""));
	}

	private static string BuildSource(int index, string code, IReadOnlyCollection<string> namespaces)
	{
		var usings = string.Concat(namespaces.Select(ns => $"using {ns};\n"));
		var body = code.Replace("\n", "\n\t\t");

		return
			$$"""
			{{usings}}namespace Generated;

			public static class C_{{index}}
			{
				public static async global::System.Threading.Tasks.Task Run(global::Elastic.Clients.Elasticsearch.ElasticsearchClient client)
				{
					{{body}}
				}
			}
			""";
	}

	private static string LocateRepoRootFile(string fileName)
	{
		var dir = new DirectoryInfo(AppContext.BaseDirectory);
		while (dir is not null)
		{
			var candidate = Path.Combine(dir.FullName, fileName);
			if (File.Exists(candidate))
				return candidate;
			dir = dir.Parent;
		}

		throw new FileNotFoundException($"Could not locate '{fileName}' walking up from '{AppContext.BaseDirectory}'.");
	}

	private sealed record BlacklistEntry(string Digest, string Reason, bool FailsConversion);

	private static readonly IReadOnlySet<string> Blacklist = LoadBlacklist(
		LocateRepoRootFile(Path.Combine("tests", "RequestConverter.Tests", "TestData", "blacklist.json")),
		conversionOnly: false);

	private static IReadOnlySet<string> LoadBlacklist(string path, bool conversionOnly)
	{
		using var stream = File.OpenRead(path);
		var entries = JsonSerializer.Deserialize<BlacklistEntry[]>(stream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
			?? throw new InvalidOperationException("Failed to parse blacklist.json.");
		return entries.Where(e => !conversionOnly || e.FailsConversion).Select(e => e.Digest).ToHashSet(StringComparer.Ordinal);
	}
}
