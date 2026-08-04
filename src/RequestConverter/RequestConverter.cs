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
/// <param name="UnsupportedParameters">
/// Query-parameter keys the endpoint does not define. The converter drops them from the generated code, so a
/// non-empty collection means the output is not a faithful reconstruction; hosts should warn the user.
/// </param>
/// <param name="ClientCall">
/// The client method that executes the request. Always populated for a successfully materialized endpoint (the
/// table is generated from the same endpoint set as the factory); <c>null</c> only as a defensive fallback.
/// </param>
public sealed record ConversionResult(
	string Code,
	Type RequestType,
	IReadOnlyCollection<string> Namespaces,
	IReadOnlyCollection<string> UnsupportedParameters,
	ClientCallInfo? ClientCall);

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
		var unsupportedParameters = new List<string>();
		var request = RequestFactory.Materialize(requestResponseSerializer, id, queryParameters, pathParameters, body ?? "{}", unsupportedParameters);
		if (request is null)
		{
			throw new NotSupportedException($"Endpoint '{id}' is not supported.");
		}

		if (request is not ICodeFormattable formattable)
		{
			throw new NotSupportedException($"Request for endpoint '{id}' does not implement '{nameof(ICodeFormattable)}'.");
		}

		ClientCallInfo? clientCall = ClientMethods.Lookup.TryGetValue(id, out var call) ? call : null;

		var writer = new CodeWriter(options);

		// The client call references the request by name, so it forces the variable-declaration form.
		var emitVariableDeclaration = writer.Options.EmitVariableDeclaration
			|| writer.Options.ClientCallFormat == ClientCallFormat.Statement;

		// `TypeName variableName = ` goes before the initializer. Writing the type name here (not after FormatCode)
		// records its namespace up front so the body's collision-aware shortening accounts for it. The materialized
		// request type is already closed over JsonElement for generic requests (e.g. IndexRequest<JsonElement>), so the
		// rendered declaration names that concrete type.
		if (emitVariableDeclaration)
		{
			writer.WriteTypeName(request.GetType()).Write(" ").Write(writer.Options.VariableName).Write(" = ");
		}

		formattable.FormatCode(writer);

		if (emitVariableDeclaration)
		{
			writer.Write(";");
		}

		if (writer.Options.ClientCallFormat == ClientCallFormat.Statement && clientCall is { } clientMethod)
		{
			WriteClientCall(writer, clientMethod);
		}

		return (request, new ConversionResult(writer.ToString(), request.GetType(), writer.Namespaces, unsupportedParameters, clientCall));
	}

	/// <summary>
	/// Appends the executing client invocation as a second statement, e.g.
	/// <c>var response = await client.Esql.QueryAsync(request);</c>. Response-only generic type parameters are
	/// spelled explicitly (the compiler cannot infer them from the request argument): the placeholder document
	/// type in strongly-typed-document mode, <see cref="System.Text.Json.JsonElement"/> otherwise, matching how
	/// the declared request variable renders.
	/// </summary>
	private static void WriteClientCall(CodeWriter writer, ClientCallInfo clientMethod)
	{
		var options = writer.Options;
		var async = options.ClientCallStyle == ClientCallStyle.Async;

		writer.WriteLine().WriteLine();
		writer.Write("var ").Write(options.ResponseVariableName).Write(" = ");

		if (async)
		{
			writer.Write("await ");
		}

		writer.Write(options.ClientVariableName).Write(".");

		if (clientMethod.SubClient.Length > 0)
		{
			writer.Write(clientMethod.SubClient).Write(".");
		}

		writer.Write(async ? clientMethod.Method + "Async" : clientMethod.Method);

		if (clientMethod.ResponseGenericArity > 0)
		{
			writer.Write("<");
			for (var i = 0; i < clientMethod.ResponseGenericArity; i++)
			{
				if (i > 0)
				{
					writer.Write(", ");
				}

				if (options.UseStronglyTypedDocument)
				{
					writer.Write(options.DocumentTypeName);
				}
				else
				{
					writer.WriteTypeRef("System.Text.Json.JsonElement");
				}
			}

			writer.Write(">");
		}

		writer.Write("(").Write(options.VariableName).Write(");");
	}
}
