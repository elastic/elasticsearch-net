// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;
using System.Text.Json;
using System.Text.Json.Serialization;

using RequestConverter;

// An entry point is required for an Exe; the module is driven through the JSExport methods below.
return;

/// <summary>
/// A request as parsed by the host (<c>@elastic/request-converter</c>). Field names are snake_case to match the
/// JSON the host emits; the heavy <c>request</c> (schema) property is stripped by the host before sending, so it
/// is intentionally absent here. Mirrors the <c>ParsedRequest</c> shape in <c>request-converter/src/parse.ts</c>.
/// </summary>
internal sealed record ParsedRequest
{
	public string? Api { get; init; }
	public JsonElement? Body { get; init; }
	public required string Method { get; init; }
	public IReadOnlyDictionary<string, JsonElement>? Params { get; init; }
	public required string Path { get; init; }
	public IReadOnlyDictionary<string, JsonElement>? Query { get; init; }
	public string? RawPath { get; init; }
	public required string Source { get; init; }
	public required string Url { get; init; }
}

/// <summary>
/// The host's conversion options. Only <see cref="TypeNameStyle"/> drives the .NET converter; the remaining
/// fields exist to round-trip the host contract. <see cref="TypeNameStyle"/> is an extension the harness passes
/// through the host's open-ended options bag to select the emitted type-name spelling.
/// </summary>
internal sealed record ConvertOptions
{
	public bool? CheckOnly { get; init; }
	public bool? Complete { get; init; }
	public bool? PrintResponse { get; init; }
	public string? ElasticsearchUrl { get; init; }
	public bool? Debug { get; init; }
	public string? TypeNameStyle { get; init; }

	/// <summary>Selects the emitted syntax: <c>object_initializer</c> (default) emits object initializers; <c>descriptor</c>
	/// emits the fluent descriptor chain. Extension to the host contract.</summary>
	public string? SyntaxMode { get; init; }

	/// <summary>Emit field references as <c>Infer.Field&lt;DocumentTypeName&gt;(x =&gt; x.Path)</c> lambdas and document bodies
	/// as <c>new DocumentTypeName { ... }</c>. Illustrative: the named type is not generated. Extension to the host contract.</summary>
	public bool? UseStronglyTypedDocument { get; init; }

	/// <summary>The placeholder document type name used when <see cref="UseStronglyTypedDocument"/> is set. Defaults to <c>MyDocument</c>.</summary>
	public string? DocumentTypeName { get; init; }
}

internal sealed record Input
{
	public IReadOnlyList<ParsedRequest>? Requests { get; init; }
	public ConvertOptions? Options { get; init; }
}

internal readonly record struct SuccessResponse<T>
{
	public required T? Return { get; init; }
}

internal readonly record struct ErrorResponse
{
	public required string? Error { get; init; }
}

[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.SnakeCaseLower)]
[JsonSerializable(typeof(Input))]
[JsonSerializable(typeof(SuccessResponse<bool>))]
[JsonSerializable(typeof(SuccessResponse<string>))]
[JsonSerializable(typeof(ErrorResponse))]
internal partial class SerializerContext : JsonSerializerContext;

/// <summary>
/// The WASM entry points the host's <c>ExternalExporter</c> binds to. Named <c>Exporter</c> rather than
/// <c>RequestConverter</c> to avoid colliding with the <see cref="RequestConverter"/> namespace it calls into.
/// Input and output are plain JSON strings of the form <c>{"requests": [...], "options": {...}}</c> and
/// <c>{"return": ...}</c> / <c>{"error": "..."}</c>; the host raises on a present <c>error</c>.
/// </summary>
[SupportedOSPlatform("browser")]
internal static partial class Exporter
{
	[JSExport]
	internal static string Check(string input)
	{
		return Execute(() =>
		{
			var parsed = JsonSerializer.Deserialize(input, SerializerContext.Default.Input);
			if (parsed is null)
			{
				return ErrorResponse("Failed to deserialize input.");
			}

			// A request set is convertible only if every request in it converts; one unsupported request
			// (e.g. an NDJSON bulk) makes the whole set uncheckable, matching the host's all-or-nothing semantics.
			foreach (var request in parsed.Requests ?? [])
			{
				try
				{
					ConvertRequest(request, parsed.Options, "request");
				}
				catch (NotSupportedException)
				{
					return BoolResponse(false);
				}
			}

			return BoolResponse(true);
		});
	}

	[JSExport]
	internal static string Convert(string input)
	{
		return Execute(() =>
		{
			var parsed = JsonSerializer.Deserialize(input, SerializerContext.Default.Input);
			if (parsed is null)
			{
				return ErrorResponse("Failed to deserialize input.");
			}

			var requests = parsed.Requests ?? [];

			// Each request becomes a typed variable declaration. In a batch the first variable is `request`, then
			// `request1`, `request2`, ... so the snippets don't collide when pasted together.
			var declarations = new List<string>(requests.Count);
			var namespaces = new SortedSet<string>(StringComparer.Ordinal);

			for (var i = 0; i < requests.Count; i++)
			{
				var variableName = i == 0 ? "request" : $"request{i}";
				var result = ConvertRequest(requests[i], parsed.Options, variableName);
				declarations.Add(result.Code);
				foreach (var ns in result.Namespaces)
				{
					namespaces.Add(ns);
				}
			}

			// The using directives appear once at the top, deduplicated and ordered, covering every request below.
			var usings = string.Concat(namespaces.Select(ns => $"using {ns};\n"));
			var body = string.Join("\n\n", declarations);

			return StringResponse(usings.Length > 0 ? $"{usings}\n{body}" : body);
		});
	}

	private static ConversionResult ConvertRequest(ParsedRequest request, ConvertOptions? options, string variableName)
	{
		if (string.IsNullOrEmpty(request.Api))
		{
			throw new NotSupportedException("Request is missing an API name.");
		}

		return global::RequestConverter.RequestConverter.Convert(
			global::RequestConverter.RequestConverter.DefaultSerializer,
			request.Api,
			ToStringMap(request.Params),
			ToStringMap(request.Query),
			request.Body?.GetRawText(),
			BuildFormattingOptions(options, variableName));
	}

	private static FormattingOptions BuildFormattingOptions(ConvertOptions? options, string variableName)
	{
		// Emit a typed variable declaration (e.g. `SearchRequest request = new() { ... };`) so the request type and its
		// namespace surface in the generated snippet.
		var formatting = new FormattingOptions
		{
			EmitVariableDeclaration = true,
			VariableName = variableName
		};

		// Accept the style name case-insensitively; an unknown or absent value keeps the converter's default style.
		if (!string.IsNullOrEmpty(options?.TypeNameStyle)
			&& Enum.TryParse<TypeNameStyle>(options.TypeNameStyle, ignoreCase: true, out var style))
		{
			formatting = formatting with { TypeNameStyle = style };
		}

		// Accept the syntax mode case-insensitively (the host's snake_case `object_initializer`/`descriptor` parse after
		// the underscore is stripped); an unknown or absent value keeps the converter's object-initializer default.
		if (!string.IsNullOrEmpty(options?.SyntaxMode)
			&& Enum.TryParse<SyntaxMode>(options.SyntaxMode.Replace("_", ""), ignoreCase: true, out var syntaxMode))
		{
			formatting = formatting with { SyntaxMode = syntaxMode };
		}

		if (options?.UseStronglyTypedDocument is { } useStronglyTypedDocument)
		{
			formatting = formatting with { UseStronglyTypedDocument = useStronglyTypedDocument };
		}

		if (!string.IsNullOrEmpty(options?.DocumentTypeName))
		{
			formatting = formatting with { DocumentTypeName = options.DocumentTypeName };
		}

		return formatting;
	}

	/// <summary>
	/// Flattens the host's string-to-JSON parameter map to the string-to-string map the converter expects. A JSON
	/// string keeps its text value; any other JSON scalar (number, bool) keeps its raw token, which is the form the
	/// converter reuses when reconstructing path and query parameters.
	/// </summary>
	private static IReadOnlyDictionary<string, string>? ToStringMap(IReadOnlyDictionary<string, JsonElement>? source)
	{
		if (source is null || source.Count == 0)
		{
			return null;
		}

		var map = new Dictionary<string, string>(source.Count);
		foreach (var (key, value) in source)
		{
			map[key] = value.ValueKind == JsonValueKind.String ? value.GetString()! : value.GetRawText();
		}

		return map;
	}

	private static string Execute(Func<string> action)
	{
		try
		{
			return action();
		}
		catch (Exception e)
		{
			return ErrorResponse($"Internal Error:\n {e}");
		}
	}

	private static string BoolResponse(bool result) =>
		JsonSerializer.Serialize(new SuccessResponse<bool> { Return = result }, SerializerContext.Default.SuccessResponseBoolean);

	private static string StringResponse(string result) =>
		JsonSerializer.Serialize(new SuccessResponse<string> { Return = result }, SerializerContext.Default.SuccessResponseString);

	private static string ErrorResponse(string error) =>
		JsonSerializer.Serialize(new ErrorResponse { Error = error }, SerializerContext.Default.ErrorResponse);
}
