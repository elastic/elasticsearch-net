// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Text.Json;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

using Xunit;
using Xunit.Abstractions;

using RequestConverter;
using RequestConverter.Hosting;

namespace RequestConverter.Tests;

/// <summary>
/// End-to-end regression test for the request converter. For every valid, non-blacklisted recorded
/// example in <c>alternatives_report.json</c> it: generates the C# snippet via the converter, compiles
/// all snippets in a single Roslyn pass, then executes each and round-trips it through the
/// RequestResponseSerializer, asserting the serialized JSON matches the original request body.
/// </summary>
public sealed class RoundtripRegressionTests
{
	private readonly ITestOutputHelper _output;

	public RoundtripRegressionTests(ITestOutputHelper output) => _output = output;

	private sealed record Case(string Digest, string Api, Type RequestType, string Code, IReadOnlyCollection<string> Namespaces, JsonElement? Body, Elastic.Clients.Elasticsearch.Requests.Request Materialized);

	[Theory]
	[InlineData(SyntaxMode.ObjectInitializer, ConstructorStyle.TargetTyped)]
	[InlineData(SyntaxMode.Descriptor, ConstructorStyle.TargetTyped)]
	public void Converted_requests_compile_and_roundtrip(SyntaxMode syntaxMode, ConstructorStyle constructorStyle)
	{
		var reportPath = LocateRepoRootFile(Path.Combine("tests", "RequestConverter.Tests", "TestData", "alternatives_report.json"));
		using var reportStream = File.OpenRead(reportPath);
		var examples = JsonSerializer.Deserialize<ExampleModel[]>(reportStream)
			?? throw new InvalidOperationException("Failed to parse alternatives_report.json.");

		var serializer = global::RequestConverter.RequestConverter.DefaultSerializer;

		var options = new FormattingOptions { SyntaxMode = syntaxMode, ConstructorStyle = constructorStyle };

		// 1) Convert every valid, non-blacklisted, non-NDJSON example.
		var cases = new List<Case>();
		var convertFailures = new List<string>();
		var parameterFailures = new List<string>();
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
					var (request, result) = global::RequestConverter.RequestConverter.ConvertCore(
						serializer, source.Api, source.PathParameters, source.QueryParameters, body, options);
					cases.Add(new Case(example.Digest, source.Api, result.RequestType, result.Code, result.Namespaces, source.Body, request));
					if (result.UnsupportedParameters.Count > 0)
					{
						parameterFailures.Add($"{source.Api} [{example.Digest}]: unsupported query parameters: {string.Join(", ", result.UnsupportedParameters)}");
					}
				}
				catch (NotSupportedException)
				{
					// The converter doesn't (yet) support this endpoint; not a regression.
					skippedUnsupported++;
				}
				catch (Exception ex)
				{
					convertFailures.Add($"{source.Api} [{example.Digest}]: convert threw {ex.GetType().Name}: {ex.Message}");
				}
			}
		}

		// 2) Emit one .cs per case (saved to a temp dir for inspection). Each snippet carries its own using
		// directives built from the namespaces the converter reported, mirroring how a real caller consumes the
		// Simplified output - and exercising the short-identifier rendering plus its collision fallback.
		var outputDir = Path.Combine(Path.GetTempPath(), "RequestConverter.Tests", $"generated_{syntaxMode}_{constructorStyle}");
		Directory.CreateDirectory(outputDir);
		foreach (var stale in Directory.GetFiles(outputDir, "*.cs"))
			File.Delete(stale);

		var trees = new List<SyntaxTree>(cases.Count);
		for (var i = 0; i < cases.Count; i++)
		{
			var source = BuildSource(i, cases[i]);
			var file = Path.Combine(outputDir, $"E_{i}.cs");
			File.WriteAllText(file, source);
			trees.Add(CSharpSyntaxTree.ParseText(source, path: file));
		}

		// 3) Single Roslyn compilation of all snippets.
		var compilation = CSharpCompilation.Create(
			$"RequestConverter.Generated.{syntaxMode}.{constructorStyle}",
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

		_output.WriteLine($"converted={cases.Count}, skipped(unsupported)={skippedUnsupported}, convert-failures={convertFailures.Count}, parameter-failures={parameterFailures.Count}");

		Assert.True(emit.Success,
			$"Generated code failed to compile ({compileErrors.Count} errors). Snippets saved under '{outputDir}'.\n" +
			$"First compile errors:\n{string.Join("\n", compileErrors.Take(40))}\n" +
			(convertFailures.Count > 0 ? $"Convert failures ({convertFailures.Count}):\n{string.Join("\n", convertFailures.Take(20))}" : ""));

		// 4) Load the assembly and round-trip each case through the serializer.
		peStream.Position = 0;
		var assembly = AssemblyLoadContext.Default.LoadFromStream(peStream);

		var roundtripFailures = new List<string>();
		var roundtripped = 0;
		var bodiless = 0;
		var pathQueryFailures = new List<string>();
		var settings = new Elastic.Clients.Elasticsearch.ElasticsearchClientSettings();

		for (var i = 0; i < cases.Count; i++)
		{
			var c = cases[i];
			var type = assembly.GetType($"Generated.E_{i}")
				?? throw new InvalidOperationException($"Generated type 'Generated.E_{i}' not found.");

			// Validate path + query reconstruction: resolve both the materialized request (A) and the
			// request rebuilt by the generated code (B) through the client's own URL + query-string builder,
			// then compare. Both sides go through the client, so normalization is symmetric and only genuine
			// emission differences (a dropped or mis-emitted route/query value, or HTTP method) surface.
			try
			{
				var rebuilt = (Elastic.Clients.Elasticsearch.Requests.Request)type.GetMethod("Build")!.Invoke(null, null)!;
				if (!EndpointEquals(ResolveEndpoint(c.Materialized, settings), ResolveEndpoint(rebuilt, settings), out var endpointDiff))
					pathQueryFailures.Add($"{c.Api} [{c.Digest}]: {endpointDiff}");
			}
			catch (Exception ex)
			{
				var inner = ex is TargetInvocationException { InnerException: { } tie } ? tie : ex;
				pathQueryFailures.Add($"{c.Api} [{c.Digest}]: endpoint resolution threw {inner.GetType().Name}: {inner.Message}");
			}

			string outputJson;
			try
			{
				outputJson = (string)type.GetMethod("Serialize")!.Invoke(null, [serializer])!;
			}
			catch (Exception ex)
			{
				var inner = ex is TargetInvocationException { InnerException: { } tie } ? tie : ex;
				roundtripFailures.Add($"{c.Api} [{c.Digest}]: serialize threw {inner.GetType().Name}: {inner.Message}");
				continue;
			}

			if (c.Body is null)
			{
				bodiless++;
				continue;
			}

			// NDJSON-bodied requests (bulk/msearch/msearch_template) serialize to newline-delimited JSON, not a
			// single JSON document. Compare the materialized request (A) against the rebuilt request (B), both
			// serialized by the client. A carries the URL-level index applied during conversion (bulk's index
			// resolution depends on it; re-deserializing the body alone would resolve operation indices
			// differently). Symmetric: both go through the client, so client normalization applies to both
			// sides and only genuine emission differences surface; compare line-by-line.
			if (IsNdjson(c.Api))
			{
				string expectedNdjson;
				try
				{
					using var aStream = new MemoryStream();
					serializer.Serialize(c.Materialized, aStream, global::Elastic.Transport.SerializationFormatting.None);
					expectedNdjson = Encoding.UTF8.GetString(aStream.ToArray());
				}
				catch (Exception ex)
				{
					var inner = ex is TargetInvocationException { InnerException: { } tie } ? tie : ex;
					roundtripFailures.Add($"{c.Api} [{c.Digest}]: materialized serialize threw {inner.GetType().Name}: {inner.Message}");
					continue;
				}

				if (NdjsonEquals(expectedNdjson, outputJson, out var ndjsonDiff))
					roundtripped++;
				else
					roundtripFailures.Add($"{c.Api} [{c.Digest}]: ndjson body mismatch -> {ndjsonDiff}");

				continue;
			}

			// Non-NDJSON: compare against the client's own canonical serialization of the input (deserialize then
			// re-serialize), so client-side normalization (match shorthand -> full form, aggs -> aggregations,
			// single value -> array, dropped explicit nulls) is applied to both sides and only genuine converter
			// reconstruction differences remain. Falls back to the raw input if the input can't be re-serialized.
			string expectedJson;
			try
			{
				using var inStream = new MemoryStream(Encoding.UTF8.GetBytes(c.Body.Value.GetRawText()));
				var reparsed = serializer.Deserialize(c.RequestType, inStream);
				using var outStream = new MemoryStream();
				serializer.Serialize(reparsed, outStream, global::Elastic.Transport.SerializationFormatting.None);
				expectedJson = Encoding.UTF8.GetString(outStream.ToArray());
			}
			catch
			{
				expectedJson = c.Body.Value.GetRawText();
			}

			using var expectedDoc = JsonDocument.Parse(expectedJson);
			if (JsonEquals(expectedDoc.RootElement, outputJson, out var diff))
				roundtripped++;
			else
				roundtripFailures.Add($"{c.Api} [{c.Digest}]: body mismatch -> expected={expectedJson} | actual={outputJson}");
		}

		_output.WriteLine($"roundtripped={roundtripped}, bodiless={bodiless}, roundtrip-failures={roundtripFailures.Count}, path/query-failures={pathQueryFailures.Count}");

		Assert.True(convertFailures.Count == 0 && roundtripFailures.Count == 0 && pathQueryFailures.Count == 0,
			$"converted={cases.Count}, roundtripped={roundtripped}, bodiless={bodiless}, skipped(unsupported)={skippedUnsupported}\n" +
			$"Convert failures ({convertFailures.Count}):\n{string.Join("\n", convertFailures.Take(20))}\n" +
			$"Roundtrip failures ({roundtripFailures.Count}):\n{string.Join("\n", roundtripFailures.Take(40))}\n" +
			$"Path/query failures ({pathQueryFailures.Count}):\n{string.Join("\n", pathQueryFailures.Take(40))}");
	}

	/// <summary>
	/// Gates the published-example flavor (Descriptor syntax + strongly typed document): every corpus example
	/// the round-trip Theory converts must also convert here without throwing. This flavor's <c>MyDocument</c>
	/// type is illustrative and never compiles or round-trips, so unlike the Theory above this only exercises
	/// conversion, not compilation or round-tripping.
	/// </summary>
	[Fact]
	public void Converted_requests_convert_in_typed_document_mode()
	{
		var reportPath = LocateRepoRootFile(Path.Combine("tests", "RequestConverter.Tests", "TestData", "alternatives_report.json"));
		using var reportStream = File.OpenRead(reportPath);
		var examples = JsonSerializer.Deserialize<ExampleModel[]>(reportStream)
			?? throw new InvalidOperationException("Failed to parse alternatives_report.json.");

		var serializer = global::RequestConverter.RequestConverter.DefaultSerializer;

		var options = new FormattingOptions
		{
			SyntaxMode = SyntaxMode.Descriptor,
			UseStronglyTypedDocument = true,
			EmitVariableDeclaration = true,
		};

		var failures = new List<string>();
		var converted = 0;
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
					_ = global::RequestConverter.RequestConverter.ConvertCore(
						serializer, source.Api, source.PathParameters, source.QueryParameters, body, options);
					converted++;
				}
				catch (NotSupportedException)
				{
					// The converter doesn't (yet) support this endpoint; not a regression (mirrors the Theory above).
					skippedUnsupported++;
				}
				catch (Exception ex)
				{
					failures.Add($"{source.Api} [{example.Digest}]: {ex.GetType().Name}: {ex.Message}");
				}
			}
		}

		_output.WriteLine($"converted={converted}, skipped(unsupported)={skippedUnsupported}, typed-document-failures={failures.Count}");

		Assert.True(failures.Count == 0,
			$"{failures.Count} typed-document conversion failures:\n{string.Join("\n", failures)}");
	}

	/// <summary>
	/// Resolves a request to its on-the-wire endpoint shape: HTTP method, resolved route values, and
	/// query-string parameters, using the client's own URL and query-string builder (the same calls the
	/// client makes in <c>PrepareRequest</c>). <c>RequestParameters</c> is internal and lives on the
	/// generic <c>Request&lt;TParameters&gt;</c> base, so it is reached reflectively.
	/// </summary>
	private static (string Method, IReadOnlyDictionary<string, string> Route, IReadOnlyDictionary<string, string> Query) ResolveEndpoint(
		Elastic.Clients.Elasticsearch.Requests.Request request, Elastic.Clients.Elasticsearch.IElasticsearchClientSettings settings)
	{
		var (resolvedUrl, _, route) = request.GetUrl(settings);

		var requestParameters = (Elastic.Transport.RequestParameters?)request.GetType()
			.GetProperty("RequestParameters", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
			?.GetValue(request);
		var pathAndQuery = requestParameters?.CreatePathWithQueryStrings(resolvedUrl ?? string.Empty, settings) ?? resolvedUrl ?? string.Empty;

		return (request.HttpMethod.ToString(), route ?? new Dictionary<string, string>(), ParseQuery(pathAndQuery));
	}

	private static IReadOnlyDictionary<string, string> ParseQuery(string pathAndQuery)
	{
		var result = new Dictionary<string, string>(StringComparer.Ordinal);
		var q = pathAndQuery.IndexOf('?');
		if (q < 0)
			return result;

		foreach (var pair in pathAndQuery[(q + 1)..].Split('&', StringSplitOptions.RemoveEmptyEntries))
		{
			var eq = pair.IndexOf('=');
			var key = eq < 0 ? pair : pair[..eq];
			var value = eq < 0 ? string.Empty : pair[(eq + 1)..];
			result[Uri.UnescapeDataString(key)] = Uri.UnescapeDataString(value);
		}

		return result;
	}

	private static bool EndpointEquals(
		(string Method, IReadOnlyDictionary<string, string> Route, IReadOnlyDictionary<string, string> Query) expected,
		(string Method, IReadOnlyDictionary<string, string> Route, IReadOnlyDictionary<string, string> Query) actual,
		out string diff)
	{
		if (!string.Equals(expected.Method, actual.Method, StringComparison.Ordinal))
		{
			diff = $"method mismatch -> expected={expected.Method} | actual={actual.Method}";
			return false;
		}

		if (!DictEquals(expected.Route, actual.Route))
		{
			diff = $"route mismatch -> expected={Render(expected.Route)} | actual={Render(actual.Route)}";
			return false;
		}

		if (!DictEquals(expected.Query, actual.Query))
		{
			diff = $"query mismatch -> expected={Render(expected.Query)} | actual={Render(actual.Query)}";
			return false;
		}

		diff = string.Empty;
		return true;
	}

	private static bool DictEquals(IReadOnlyDictionary<string, string> a, IReadOnlyDictionary<string, string> b)
	{
		if (a.Count != b.Count)
			return false;

		foreach (var (k, v) in a)
		{
			if (!b.TryGetValue(k, out var other) || !string.Equals(v, other, StringComparison.Ordinal))
				return false;
		}

		return true;
	}

	private static string Render(IReadOnlyDictionary<string, string> d) =>
		"{" + string.Join(", ", d.OrderBy(kv => kv.Key, StringComparer.Ordinal).Select(kv => $"{kv.Key}={kv.Value}")) + "}";

	private static bool IsNdjson(string api) => api is "bulk" or "msearch" or "msearch_template";

	/// <summary>Compares two NDJSON payloads line-by-line: equal non-empty line counts and each line semantically equal.</summary>
	private static bool NdjsonEquals(string expectedNdjson, string actualNdjson, out string diff)
	{
		var expectedLines = SplitNdjson(expectedNdjson);
		var actualLines = SplitNdjson(actualNdjson);

		if (expectedLines.Count != actualLines.Count)
		{
			diff = $"line count expected={expectedLines.Count} actual={actualLines.Count} | expected={expectedNdjson} | actual={actualNdjson}";
			return false;
		}

		for (var i = 0; i < expectedLines.Count; i++)
		{
			using var expectedDoc = JsonDocument.Parse(expectedLines[i]);
			if (!JsonEquals(expectedDoc.RootElement, actualLines[i], out _))
			{
				diff = $"line {i} -> expected={expectedLines[i]} | actual={actualLines[i]}";
				return false;
			}
		}

		diff = string.Empty;
		return true;
	}

	private static List<string> SplitNdjson(string ndjson) =>
		ndjson.Split('\n').Where(line => !string.IsNullOrWhiteSpace(line)).ToList();

	private static string BuildSource(int index, Case c)
	{
		var typeName = CSharpName(c.RequestType);

		// The using directives the converter reported, so the snippet's Simplified short identifiers resolve (the
		// converter falls back to a global::-qualified name for any type whose simple name would be ambiguous across
		// these namespaces, so importing all of them never produces a CS0104).
		var usings = string.Concat(c.Namespaces.Select(ns => $"using {ns};\n"));

		// The converter emits at base indent 0, but the snippet is embedded one method-body level deep here, so its
		// continuation lines would land a level short. Prefix every line after the first with the template's indent
		// (one tab); applying it uniformly keeps any multi-line raw-string literal in the snippet valid.
		var code = c.Code.Replace("\n", "\n\t");

		return
			$$"""
			{{usings}}namespace Generated;

			public static class E_{{index}}
			{
				public static {{typeName}} Build() => {{code}};

				public static string Serialize(global::Elastic.Transport.Serializer serializer)
				{
					var request = Build();
					using var stream = new global::System.IO.MemoryStream();
					serializer.Serialize(request, stream, global::Elastic.Transport.SerializationFormatting.None);
					return global::System.Text.Encoding.UTF8.GetString(stream.ToArray());
				}
			}
			""";
	}

	/// <summary>Formats a CLR type as a fully-qualified C# type reference (handles closed generics).</summary>
	private static string CSharpName(Type type)
	{
		if (type.IsGenericType)
		{
			var definition = type.GetGenericTypeDefinition();
			var raw = (definition.FullName ?? definition.Name).Replace('+', '.');
			var tick = raw.IndexOf('`');
			if (tick >= 0)
				raw = raw[..tick];

			var args = string.Join(", ", type.GetGenericArguments().Select(CSharpName));
			return $"global::{raw}<{args}>";
		}

		return "global::" + (type.FullName ?? type.Name).Replace('+', '.');
	}

	private static bool JsonEquals(JsonElement expected, string actualJson, out string diff)
	{
		using var actual = JsonDocument.Parse(actualJson);
		if (JsonDeepEquals(expected, actual.RootElement))
		{
			diff = string.Empty;
			return true;
		}

		diff = $"expected={expected.GetRawText()} | actual={actualJson}";
		return false;
	}

	private static bool JsonDeepEquals(JsonElement a, JsonElement b)
	{
		if (a.ValueKind != b.ValueKind)
		{
			// Treat numeric kinds uniformly (e.g. 1 vs 1.0).
			if (IsNumber(a) && IsNumber(b))
				return NumbersEqual(a, b);
			return false;
		}

		switch (a.ValueKind)
		{
			case JsonValueKind.Object:
			{
				var aProps = a.EnumerateObject().ToDictionary(p => p.Name, p => p.Value);
				var bProps = b.EnumerateObject().ToDictionary(p => p.Name, p => p.Value);
				if (aProps.Count != bProps.Count)
					return false;
				foreach (var (name, value) in aProps)
				{
					if (!bProps.TryGetValue(name, out var other) || !JsonDeepEquals(value, other))
						return false;
				}
				return true;
			}
			case JsonValueKind.Array:
			{
				var aItems = a.EnumerateArray().ToList();
				var bItems = b.EnumerateArray().ToList();
				if (aItems.Count != bItems.Count)
					return false;
				for (var i = 0; i < aItems.Count; i++)
				{
					if (!JsonDeepEquals(aItems[i], bItems[i]))
						return false;
				}
				return true;
			}
			case JsonValueKind.Number:
				return NumbersEqual(a, b);
			case JsonValueKind.String:
				return string.Equals(a.GetString(), b.GetString(), StringComparison.Ordinal);
			default:
				return true; // True/False/Null compare equal once the kinds match.
		}
	}

	private static bool IsNumber(JsonElement e) => e.ValueKind == JsonValueKind.Number;

	private static bool NumbersEqual(JsonElement a, JsonElement b)
	{
		if (a.TryGetDecimal(out var da) && b.TryGetDecimal(out var db))
			return da == db;
		return a.GetDouble() == b.GetDouble();
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

	// Examples that are known not to convert or round-trip yet, loaded from the shared data file so the
	// Console harness and this test skip the same set. The test skips every entry; the Console skips only
	// conversion failures.
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
