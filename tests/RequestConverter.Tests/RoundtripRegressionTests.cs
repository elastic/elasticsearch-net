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
		var reportPath = LocateRepoRootFile("alternatives_report.json");
		using var reportStream = File.OpenRead(reportPath);
		var examples = JsonSerializer.Deserialize<ExampleModel[]>(reportStream)
			?? throw new InvalidOperationException("Failed to parse alternatives_report.json.");

		var serializer = global::RequestConverter.RequestConverter.DefaultSerializer;

		var options = new FormattingOptions { SyntaxMode = syntaxMode, ConstructorStyle = constructorStyle };

		// 1) Convert every valid, non-blacklisted, non-NDJSON example.
		var cases = new List<Case>();
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
					var (request, result) = global::RequestConverter.RequestConverter.ConvertCore(
						serializer, source.Api, source.PathParameters, source.QueryParameters, body, options);
					cases.Add(new Case(example.Digest, source.Api, result.RequestType, result.Code, result.Namespaces, source.Body, request));
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
			ReferenceAssemblies(),
			new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
				.WithNullableContextOptions(NullableContextOptions.Annotations));

		using var peStream = new MemoryStream();
		var emit = compilation.Emit(peStream);

		var compileErrors = emit.Diagnostics
			.Where(d => d.Severity == DiagnosticSeverity.Error)
			.Select(d => d.ToString())
			.ToList();

		_output.WriteLine($"converted={cases.Count}, skipped(unsupported)={skippedUnsupported}, convert-failures={convertFailures.Count}");

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

	private static List<MetadataReference> ReferenceAssemblies()
	{
		var tpa = (string)AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES")!;
		return tpa
			.Split(Path.PathSeparator)
			.Where(p => p.Length > 0 && File.Exists(p))
			.Select(p => (MetadataReference)MetadataReference.CreateFromFile(p))
			.ToList();
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

	// Examples that are known not to convert/round-trip yet (mirrors RequestConverter.Console).
	private static readonly HashSet<string> Blacklist =
	[
		"46a0eaaf5c881f1ba716d1812b36c724",
		"dac8ec8547bc446637fd97d9fa872f4f",
		"12d5ff4b8d3d832b32a7e7e2a520d0bb",
		"421e68e2b9789f0e8c08760d9e685d1c",
		"fbb38243221c8fb311660616e3add9ce",
		"aee4734ee63dbbbd12a21ee886f7a829",
		"f8833488041f3d318435b60917fa877c",
		"634ecacf14b83c5f0bb8b6273cf6418e",
		"41fd33a293a575bd71a1fac7bcc8b47c",
		"52bc577a0d0cd42b46f33e0ef5124df8",
		"bd2a387e8c21bf01a1039e81d7602921",
		"98b403c356a9b14544e9b9f646845e9f",
		"30bd3c0785f3df4795684754adeb5ecb",
		"9e962baf1fb407c21d6c47dcd37cec29",
		"1eb9c6ecb827ca69f7b17f7d2a26eae9",
		"ff05842419968a2141bde0371ac2f6f4",
		"e2b4867a9f72bda87ebaa3608d3fba4c",
		"0c7c40cd17985c3dd32aeaadbafc4fce",
		"13917f7cfb6a382c293275ff71134ec4",
		"856c10ad554c26b70f1121454caff40a",
		"7fde3ff91c4a2e7080444af37d5cd287",
		"c4272ad0309ffbcbe9ce96bf9fb4352a",
		"89a6b24618cafd60de1702a5b9f28a8d",
		"bd68666ca2e0be12f7624016317a62bc",
		"f7ec9062b3a7578fed55f119d7c22b74",
		"c6d39d22188dc7bbfdad811a94cbcc2b",
		"a512e4dd8880ce0395937db1bab1d205",
		"09a44b619a99f6bf3f01bd5e258fd22d",
		"c95d5317525c2ff625e6971c277247af",
		"76448aaaaa2c352bb6e09d2f83a3fbb3",
		"a99bc141066ef673e35f306157750ec9",
		"39963032d423e2f20f53c4621b6ca3c6",
		"dc4dcfeae8a5f248639335c2c9809549",
		"1a6dbe5df488c4a16e2f1101ba8a25d9",
		"88a08d0b15ef41324f5c23db533d47d1",
		"a1e5f3956f9a697e79478fc9a6e30e1f",
		"d12df43ffcdcd937bae9b26fb475e239",
		"7b9dfe5857bde1bd8483ea3241656714",
		"3343a4cf559060c422d86c786a95e535",
		"00d65f7b9daa1c6b18eedd8ace206bae",
		"b8c03bbd917d0cf5474a3e46ebdd7aad",
		"76b279835936ee4b546a171c671c3cd7",
		"c8bbf362f06a0d8dab33ec0d99743343",
		"2fd0b3c132b46aa34cc9d92dd2d4bc85",
		"09944369863fd8666d5301d717317276",
		"a21319c9eff1ac47d7fe7490f1ef2efa",
		"7dc82f7d36686fd57a47e34cbda39a4e",
		"3fecd5c6d0c172566da4a54320e1cff3",
		"6dbfe5565a95508e65d304131847f9fc",
		"446e8fc8ccfb13bb5ec64e32a5676d18",
		"df82a9cb21a7557f3ddba2509f76f608",
		"2c27a8eb6528126f37a843d434cd88b6",
		"ef10e8d07d9fae945e035d5dee1e9754",
		"62f1ec1bb5cc5a9c2efd536a7474f549",
		"f34c02351662481dd61a5c2a3e206c60",
		"83cd4eb89818b4c32f654d370eafa920",
		"d94f666616dea141dcb7aaf08a35bc10",
		"9a036a792be1d39af9fd0d1adb5f3402",
		"26f237f9bf14e8b972cc33ff6aebefa2",
		"5302f4f2bcc0f400ff71c791e6f68d7b",
		"059e04aaf093379401f665c33ac796dc",
		"a037beb3d02296e1d36dd43ef5c935dd",
		"8cbf9b46ce3ccc966c4902d2e0c56317",
		"29783e5de3a5f3c985cbf11094cf49a0",
		"68a891f609ca3a379d2d64e4914f3067",
		"1659420311d907d9fc024b96f4150216",
		"5a3855f1b3e37d89ab7cbcc4f7ae1dd3",
		"aa3284717241ed79d3d1d3bdbbdce598",
		"f65abb38dd0cfedeb06e0cef206fbdab",
		"2ec8d757188349a4630e120ba2c98c3b",
		"0d54ddad2bf6f76aa5c35f53ba77748a",
		"a159143bb578403bb9c7ff37d635d7ad",
		"15d948d593d2624ac5e2b155052048f0",
		"bab4c3b22c1768fcc7153345e4096dfb",
		"e09d30195108bd6a1f6857394a6123ea",
		"c065a200c00e2005d88ec2f0c10c908a",
		"ac366b9dda7040e743dee85335354094",
		"56fa6c9e08258157d445e2f92274962b",
		"12ec704d62ffedcb03787e6aba69d382",
		"a4e510aa9145ccedae151c4a6634f0a4",
		"e9738fe09a99080506a07945795e8eda",
		"c318fde926842722825a51e5c9c326a9",
		"a3a14f7f0e80725f695a901a7e1d579d",
		"ee2d97090d617ed8aa2a87ea33556dd7",
		"50d5c5b7e8ed9a95b8d9a25a32a77425",
		"9f7671119236423e0e40801ef6485af1",
		"c42bc6e74afc3d43cd032ec2bfd77385",
		"ffcf80e1094aa2d774f56f6b0bc54827",
		"affc7ff234dc3acccb2bf7dc51f54813",
		"02853293a5b7cd9cc7a886eb413bbeb6",
		"6a3f06962cceb3dfd3cd4fb5c679fa75",
		"6edfc35a66afd9b884431fccf48fdbf5",
		"ef33b3b373f7040b874146599db5d557",
		"dc8c94c9bef1f879282caea5c406f36e",
		"22dde5fe7ac5d85d52115641a68b3c55",
		"15a34bfe0ef8ef6333c8c7b55c011e5d",
		"89f8eac24f3ec6a7668d580aaf0eeefa",
		"d0c03847106d23ad632ceb624d647c37",
		"16a9ebe102b53495de9d2231f5ae7158",
		"48b21c5aaf16b87f1a9b1a18a5d27cbd",
		"a0bcad37014cb534a720722c3cb3fefd",
		"e9ae959608d128202921b174f4faa7a8",
		"7c862a20772467e0f5beebbd1b80c4cb",
		"2d633b7f346b828d01f923ce9dbf6ad5",
		"f5815d573cee0447910c9668003887b8",
		"f43d551aaaad73d979adf1b86533e6a3",
		"b0fe9a7c8e519995258786be4bef36c4",

		// --- Known converter gaps (documented; tracked for follow-up) ---
		// WaitForActiveShards is IStringable, so the transport stores the query param as a string; the
		// strongly-typed getter the converter reads then throws InvalidCastException (transport-level).
		"1445ca2e813ed1c25504107b4b11760e",
		"1b3762712c14a19e8c2956b4f530d327",
		"691fe20d467324ed43a36fd15852c492",
		"73646c12ad33a813ab2280f1dc83500e",
		"7c5e41a7c0075d87b8f8348a6efa990c",
		"a3464bd6f0a61623562162859566b078",
		"fabe14480624a99e8ee42c7338672058",
		// A collection/dictionary materializes with a null element/value, which NREs while formatting.
		"585b19369cb9b9763a7e8d405f009a47",
		"7f2d511cb64743c006225e5933a14bb4",
		"0d94d76b7f00d0459d1f8c962c144dcd",
		"1f8a6d2cc57ed8997a52354aca371aac",

		// bulk body recorded as a raw NDJSON string (leading newline, escaped), not a parsed JSON array. The
		// converter reads array / genuine-NDJSON bodies; a JSON-string-wrapped NDJSON is a recording artifact the
		// client never receives on the wire.
		"c9c21191ae15a49955bffde0ac749a49",
		"ba70b92f745a1765f1eb62e3457a86c3",
	];
}
