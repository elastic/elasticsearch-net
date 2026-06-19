using System;
using System.Collections.Generic;

using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport;

namespace RequestConverter;

public sealed class RequestConverter
{
	public static readonly Serializer DefaultSerializer = new DefaultRequestResponseSerializer(new ElasticsearchClientSettings());

	public static string Convert(
		Serializer requestResponseSerializer,
		string id,
		IReadOnlyDictionary<string, string>? pathParameters,
		IReadOnlyDictionary<string, string>? queryParameters,
		string? body,
		FormattingOptions? options = null)
		=> ConvertWithType(requestResponseSerializer, id, pathParameters, queryParameters, body, options).Code;

	/// <summary>
	/// Materializes the request and returns its runtime CLR type alongside the generated code.
	/// Test-only: the round-trip test needs the concrete request type to compile the target-typed
	/// (<c>new() { ... }</c>) snippet it produces.
	/// </summary>
	internal static (Type RequestType, string Code) ConvertWithType(
		Serializer requestResponseSerializer,
		string id,
		IReadOnlyDictionary<string, string>? pathParameters,
		IReadOnlyDictionary<string, string>? queryParameters,
		string? body,
		FormattingOptions? options = null)
	{
		var request = RequestFactory.Materialize(requestResponseSerializer, id, queryParameters, pathParameters, body ?? "{}");
		if (request is null)
		{
			throw new NotSupportedException($"Endpoint '{id}' is not supported.");
		}

		if (request is not ICodeFormattable formattable)
		{
			throw new NotSupportedException($"Request for endpoint '{id}' does not implement '{nameof(ICodeFormattable)}'.");
		}

		var writer = new CodeWriter(options);
		formattable.FormatCode(writer);
		return (request.GetType(), writer.ToString());
	}
}
