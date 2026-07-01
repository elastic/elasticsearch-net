// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;

using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport;

namespace RequestConverter;

/// <summary>
/// The result of converting an Elasticsearch request to C# source code.
/// </summary>
/// <param name="Code">The generated C# source that reconstructs the request.</param>
/// <param name="RequestType">The CLR type of the target request the code builds.</param>
/// <param name="Namespaces">
/// The namespaces the generated code references. When <see cref="FormattingOptions.TypeNameStyle"/> is
/// <see cref="TypeNameStyle.Simplified"/>, add these as <c>using</c> directives so the short type names resolve.
/// </param>
public sealed record ConversionResult(
	string Code,
	Type RequestType,
	IReadOnlyCollection<string> Namespaces);

public sealed class RequestConverter
{
	public static readonly Serializer DefaultSerializer = new DefaultRequestResponseSerializer(new ElasticsearchClientSettings());

	/// <summary>
	/// Materializes the request for endpoint <paramref name="id"/> and converts it to C# source, returning the code,
	/// the target request type, and the namespaces the code references.
	/// </summary>
	public static ConversionResult Convert(
		Serializer requestResponseSerializer,
		string id,
		IReadOnlyDictionary<string, string>? pathParameters,
		IReadOnlyDictionary<string, string>? queryParameters,
		string? body,
		FormattingOptions? options = null)
	{
		var (request, result) = ConvertCore(requestResponseSerializer, id, pathParameters, queryParameters, body, options);
		_ = request;
		return result;
	}

	/// <summary>
	/// Materializes the request and returns the request instance alongside the conversion result.
	/// Test-only: the round-trip test needs the materialized request to validate path/query parameter
	/// reconstruction (by resolving its URL + query string through the client).
	/// </summary>
	internal static (Elastic.Clients.Elasticsearch.Requests.Request Request, ConversionResult Result) ConvertCore(
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

		// `TypeName variableName = ` goes before the initializer. Writing the type name here (not after FormatCode)
		// records its namespace up front so the body's collision-aware shortening accounts for it. The materialized
		// request type is already closed over JsonElement for generic requests (e.g. IndexRequest<JsonElement>), so the
		// rendered declaration names that concrete type.
		if (writer.Options.EmitVariableDeclaration)
		{
			writer.WriteTypeName(request.GetType()).Write(" ").Write(writer.Options.VariableName).Write(" = ");
		}

		formattable.FormatCode(writer);

		if (writer.Options.EmitVariableDeclaration)
		{
			writer.Write(";");
		}

		return (request, new ConversionResult(writer.ToString(), request.GetType(), writer.Namespaces));
	}
}
