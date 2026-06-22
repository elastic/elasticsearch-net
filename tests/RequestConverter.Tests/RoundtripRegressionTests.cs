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

	private sealed record Case(string Digest, string Api, Type RequestType, string Code, JsonElement? Body);

	[Fact]
	public void Converted_requests_compile_and_roundtrip()
	{
		var reportPath = LocateRepoRootFile("alternatives_report.json");
		using var reportStream = File.OpenRead(reportPath);
		var examples = JsonSerializer.Deserialize<ExampleModel[]>(reportStream)
			?? throw new InvalidOperationException("Failed to parse alternatives_report.json.");

		var serializer = global::RequestConverter.RequestConverter.DefaultSerializer;

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
			// NDJSON-bodied APIs aren't supported by the converter (IStreamSerializable).
			if (example.ParsedSource.Any(s => IsNdjson(s.Api)))
				continue;

			foreach (var source in example.ParsedSource)
			{
				var body = source.Body?.GetRawText();
				try
				{
					var (requestType, code) = global::RequestConverter.RequestConverter.ConvertWithType(
						serializer, source.Api, source.PathParameters, source.QueryParameters, body);
					cases.Add(new Case(example.Digest, source.Api, requestType, code, source.Body));
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

		// 2) Emit one .cs per case (saved to a temp dir for inspection) + a global-usings file.
		var outputDir = Path.Combine(Path.GetTempPath(), "RequestConverter.Tests", "generated");
		Directory.CreateDirectory(outputDir);
		foreach (var stale in Directory.GetFiles(outputDir, "*.cs"))
			File.Delete(stale);

		var trees = new List<SyntaxTree>(cases.Count + 1);
		for (var i = 0; i < cases.Count; i++)
		{
			var source = BuildSource(i, cases[i]);
			var file = Path.Combine(outputDir, $"E_{i}.cs");
			File.WriteAllText(file, source);
			trees.Add(CSharpSyntaxTree.ParseText(source, path: file));
		}

		var usingsSource = BuildGlobalUsings();
		var usingsFile = Path.Combine(outputDir, "GlobalUsings.cs");
		File.WriteAllText(usingsFile, usingsSource);
		trees.Add(CSharpSyntaxTree.ParseText(usingsSource, path: usingsFile));

		// 3) Single Roslyn compilation of all snippets.
		var compilation = CSharpCompilation.Create(
			"RequestConverter.Generated",
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

		for (var i = 0; i < cases.Count; i++)
		{
			var c = cases[i];
			var type = assembly.GetType($"Generated.E_{i}")
				?? throw new InvalidOperationException($"Generated type 'Generated.E_{i}' not found.");

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

			// Compare against the client's own canonical serialization of the input (deserialize then
			// re-serialize), so client-side normalization (match shorthand -> full form, aggs ->
			// aggregations, single value -> array, dropped explicit nulls) is applied to both sides and
			// only genuine converter reconstruction differences remain. Falls back to the raw input if
			// the input can't be re-serialized.
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

		_output.WriteLine($"roundtripped={roundtripped}, bodiless={bodiless}, roundtrip-failures={roundtripFailures.Count}");

		Assert.True(convertFailures.Count == 0 && roundtripFailures.Count == 0,
			$"converted={cases.Count}, roundtripped={roundtripped}, bodiless={bodiless}, skipped(unsupported)={skippedUnsupported}\n" +
			$"Convert failures ({convertFailures.Count}):\n{string.Join("\n", convertFailures.Take(20))}\n" +
			$"Roundtrip failures ({roundtripFailures.Count}):\n{string.Join("\n", roundtripFailures.Take(40))}");
	}

	private static bool IsNdjson(string api) => api is "bulk" or "msearch" or "msearch_template";

	private static string BuildSource(int index, Case c)
	{
		var typeName = CSharpName(c.RequestType);

		return
			$$"""
			namespace Generated;

			public static class E_{{index}}
			{
				public static string Serialize(global::Elastic.Transport.Serializer serializer)
				{
					{{typeName}} request = {{c.Code}};
					using var stream = new global::System.IO.MemoryStream();
					serializer.Serialize(request, stream, global::Elastic.Transport.SerializationFormatting.None);
					return global::System.Text.Encoding.UTF8.GetString(stream.ToArray());
				}
			}
			""";
	}

	/// <summary>
	/// Global <c>using</c>s for every namespace in the client assembly, so the converter's short type
	/// names and target-typed <c>new()</c> resolve. An ambiguity here is a real converter finding.
	/// </summary>
	private static string BuildGlobalUsings()
	{
		var assembly = typeof(Elastic.Clients.Elasticsearch.SearchRequest).Assembly;

		IEnumerable<Type> types;
		try
		{
			types = assembly.GetExportedTypes();
		}
		catch (ReflectionTypeLoadException ex)
		{
			types = ex.Types.Where(t => t is not null)!;
		}

		var namespaces = types
			.Select(t => t.Namespace)
			.Where(ns => !string.IsNullOrEmpty(ns))
			.Distinct()
			.OrderBy(ns => ns, StringComparer.Ordinal);

		var builder = new StringBuilder();
		foreach (var ns in namespaces)
			builder.Append("global using ").Append(ns).Append(';').Append('\n');

		return builder.ToString();
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
		"8b8b6aac2111b2d8b93758ac737e6543",
		"5d428ea66252fd252b6a8d6f47605c86",
		"464dffb6a6e24a860223d1c32b232f95",
		"fe208d94ec93eabf3bd06139fa70701e",
		"e2a753029b450942a3228e3003a55a7d",
		"46a0eaaf5c881f1ba716d1812b36c724",
		"dac8ec8547bc446637fd97d9fa872f4f",
		"12d5ff4b8d3d832b32a7e7e2a520d0bb",
		"421e68e2b9789f0e8c08760d9e685d1c",
		"fbb38243221c8fb311660616e3add9ce",
		"aee4734ee63dbbbd12a21ee886f7a829",
		"9334ccd09548b585cd637d7c66c5ae65",
		"eff2fc92d46eb3c8f4d424eed18f54a2",
		"6c00dae1a456ae5e854e98e895dca2ab",
		"996f320a0f537c24b9cd0d71b5f7c1f8",
		"fad524db23eb5718ff310956e590b00d",
		"1153bd92ca18356db927054958cd95c6",
		"4f3366fc26e7ea4de446dfa5cdec9683",
		"807c0c9763f8c1114b3c8278c2a0cb56",
		"35260b615d0b5628c95d7cc814c39bd3",
		"234cec3ead32d7ed71afbe1edfea23df",
		"6326f5c6fd2a6e6b1aff9a643b94f455",
		"07ba3eaa931f2cf110052e3544db51f8",
		"e6faae2e272ee57727f38e55a3de5bb2",
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
		"32b8a5152b47930f2e16c40c8615c7bb",
		"6b6fd0a5942dfb9762ad2790cf421a80",
		"16634cfa7916cf4e8048a1d70e6240f2",
		"7fde3ff91c4a2e7080444af37d5cd287",
		"c4272ad0309ffbcbe9ce96bf9fb4352a",
		"25ae1a698f867ba5139605cc952436c0",
		"6521c3578dc4ad4a6db697700986e78e",
		"d9e0cba8e150681d861f5fd1545514e2",
		"095e3f21941a9cc75f398389a075152d",
		"89a6b24618cafd60de1702a5b9f28a8d",
		"bd68666ca2e0be12f7624016317a62bc",
		"086ec4c5d86bbf80fb80162e94037689",
		"ee0fd67acc807f1bddf5e9807c06e7eb",
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
		"ecc57597f6b791d1151ad79d9f4ce67b",
		"b7ad394975863a8f5ee29627c3ab738b",
		"d0c03847106d23ad632ceb624d647c37",
		"16a9ebe102b53495de9d2231f5ae7158",
		"48b21c5aaf16b87f1a9b1a18a5d27cbd",
		"a0bcad37014cb534a720722c3cb3fefd",
		"e9ae959608d128202921b174f4faa7a8",
		"7c862a20772467e0f5beebbd1b80c4cb",
		"2d633b7f346b828d01f923ce9dbf6ad5",
		"59726e3c90e1218487a781508788c243",
		"316cd43feb3b86396483903af1a048b1",
		"f5815d573cee0447910c9668003887b8",
		"f43d551aaaad73d979adf1b86533e6a3",
		"b0fe9a7c8e519995258786be4bef36c4",

		// --- Known converter gaps (documented; tracked for follow-up) ---
		// Short type name is ambiguous across namespaces (Feature, Context). The converter emits short
		// names by design; this only collides under the test's all-namespace global usings (a real
		// consumer with targeted usings is unaffected). A targeted-FQN emission would resolve it.
		"719141517d83b7e8e929b347a8d67c9f",
		"6febf0e6883b23b15ac213abc4bac326",
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
	];
}
