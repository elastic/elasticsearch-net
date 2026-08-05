// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;
using System.Text.Json;
using System.Text.Json.Serialization;

using RequestConverter;
using RequestConverter.Hosting;

// An entry point is required for an Exe; the module is driven through the JSExport methods below. This runs once at
// module startup, before any JSExport conversion call, so the cap is in place for every request.
//
// Kibana feeds arbitrary user payloads; bound a single NDJSON value so a pathological document cannot exhaust browser
// memory through buffer doubling.
System.AppContext.SetData("Elastic.Clients.Elasticsearch.Serialization.NdjsonMaxValueBytes", 64L * 1024 * 1024);
return;

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
				return ErrorResponse("Failed to deserialize the request-converter input.");
			}

			// check is a yes/no probe: the set is convertible only if every request converts. Any failure
			// (unsupported endpoint, malformed body, ...) means "no", so it is reported by Convert, not here.
			foreach (var request in parsed.Requests ?? [])
			{
				try
				{
					ConvertRequest(request, parsed.Options, "request");
				}
				catch (Exception)
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
				return ErrorResponse("Failed to deserialize the request-converter input.");
			}

			var requests = parsed.Requests ?? [];
			var debug = parsed.Options?.Debug ?? false;

			// Each request becomes a typed variable declaration. In a batch the first variables are `request`/`response`,
			// then `request1`/`response1`, `request2`/`response2`, ... so the snippets don't collide when pasted together.
			var declarations = new List<string>(requests.Count);
			var namespaces = new SortedSet<string>(StringComparer.Ordinal);

			for (var i = 0; i < requests.Count; i++)
			{
				var variableName = i == 0 ? "request" : $"request{i}";
				var responseVariableName = i == 0 ? "response" : $"response{i}";
				ConversionResult result;
				try
				{
					result = ConvertRequest(requests[i], parsed.Options, variableName, responseVariableName);
				}
				catch (Exception e)
				{
					// A single failing request fails the whole conversion; report it with its context.
					return ErrorResponse(DescribeError(e, requests[i], i, requests.Count, debug));
				}

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

	private static ConversionResult ConvertRequest(ParsedRequest request, ConvertOptions? options, string variableName, string responseVariableName = "response")
	{
		if (string.IsNullOrEmpty(request.Api))
		{
			throw new NotSupportedException("Request is missing an API name.");
		}

		return global::RequestConverter.RequestConverter.Convert(
			global::RequestConverter.RequestConverter.DefaultSerializer,
			request.Api,
			ConvertOptionsMapper.ToStringMap(request.Params),
			ConvertOptionsMapper.ToStringMap(request.Query),
			request.Body?.GetRawText(),
			ConvertOptionsMapper.BuildFormattingOptions(options, variableName, responseVariableName));
	}

	/// <summary>
	/// Turns an exception raised while converting one request into a concise, user-facing message. The result is the
	/// whole diagnostic the host shows (it does <c>throw new Error(response.error)</c>), so it avoids stack traces and,
	/// for a batch, names which request failed. When <paramref name="debug"/> is set the full exception is appended.
	/// </summary>
	private static string DescribeError(Exception e, ParsedRequest request, int index, int total, bool debug)
	{
		var message = e switch
		{
			JsonException json => FormatJsonError(json),
			NotSupportedException notSupported => FormatNotSupported(notSupported, request),
			InvalidOperationException => e.Message,
			FormatException or OverflowException or ArgumentException =>
				$"Could not parse a path or query parameter for the '{request.Api}' request: {e.Message}",
			_ => $"Could not convert the '{request.Api}' request. This usually means a required property is missing or a value is not valid for this endpoint."
		};

		// Only disambiguate in a batch; a single request needs no "request N" prefix.
		if (total > 1)
		{
			message = $"Request {index + 1} ('{request.Api}'): {message}";
		}

		if (debug)
		{
			message += $"\n--- debug ---\n{e}";
		}

		return message;
	}

	/// <summary>
	/// Builds the clearest message for a JSON problem. System.Text.Json already appends
	/// <c>Path: ... | LineNumber: ... | BytePositionInLine: ...</c> to the message for malformed JSON and BCL type
	/// mismatches, but not for converter-thrown messages (e.g. an unknown property). When the message lacks a path we
	/// compose a human-readable location from the exception's properties (line/column are 0-based, shown 1-based).
	/// </summary>
	private static string FormatJsonError(JsonException json)
	{
		var message = json.Message;
		if (json.Path is { Length: > 0 } path && !message.Contains(" Path: ", StringComparison.Ordinal))
		{
			var location = $" (at {path}";
			if (json.LineNumber is { } line)
			{
				location += $", line {line + 1}";
			}

			if (json.BytePositionInLine is { } column)
			{
				location += $", column {column + 1}";
			}

			message += location + ")";
		}

		return message;
	}

	/// <summary>
	/// Enriches the library's terse "Endpoint '{id}' is not supported." (an unregistered endpoint) into a message that
	/// names the request line and frames it as a converter coverage gap rather than a bad request. Other
	/// <see cref="NotSupportedException"/> messages (missing API name, not code-formattable) are already clear and pass
	/// through. Keyed off the message prefix, falling back to the raw message if the wording ever changes.
	/// </summary>
	private static string FormatNotSupported(NotSupportedException notSupported, ParsedRequest request)
	{
		if (!notSupported.Message.StartsWith("Endpoint '", StringComparison.Ordinal))
		{
			return notSupported.Message;
		}

		var route = !string.IsNullOrEmpty(request.Method) && !string.IsNullOrEmpty(request.Path)
			? $" ({request.Method} {request.Path})"
			: string.Empty;

		return $"The .NET request converter does not yet support the '{request.Api}' endpoint{route}.";
	}

	private static string Execute(Func<string> action)
	{
		try
		{
			return action();
		}
		catch (Exception e)
		{
			// Backstop for failures outside the per-request classification (e.g. an unreadable input envelope);
			// per-request errors are described precisely in Convert.
			return ErrorResponse($"The request converter failed unexpectedly: {e.Message}");
		}
	}

	private static string BoolResponse(bool result) =>
		JsonSerializer.Serialize(new SuccessResponse<bool> { Return = result }, SerializerContext.Default.SuccessResponseBoolean);

	private static string StringResponse(string result) =>
		JsonSerializer.Serialize(new SuccessResponse<string> { Return = result }, SerializerContext.Default.SuccessResponseString);

	private static string ErrorResponse(string error) =>
		JsonSerializer.Serialize(new ErrorResponse { Error = error }, SerializerContext.Default.ErrorResponse);
}
