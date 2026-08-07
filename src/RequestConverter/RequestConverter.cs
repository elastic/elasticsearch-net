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
		var format = writer.Options.ClientCallFormat;

		// Defensive: the table and the factory generate from the same endpoint set, so a missing entry is
		// unreachable in practice; degrade to the variable-declaration form so the output stays complete.
		if (format is ClientCallFormat.Inline && clientCall is null)
		{
			format = ClientCallFormat.Statement;
		}

		if (format is ClientCallFormat.Inline)
		{
			WriteInlineClientCall(writer, clientCall!.Value, formattable);
		}
		else
		{
			// The client call references the request by name, so it forces the variable-declaration form.
			var emitVariableDeclaration = writer.Options.EmitVariableDeclaration || format is ClientCallFormat.Statement;

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

			if (format is ClientCallFormat.Statement && clientCall is { } clientMethod)
			{
				WriteClientCall(writer, clientMethod);
			}
		}

		return (request, new ConversionResult(writer.ToString(), request.GetType(), writer.Namespaces, unsupportedParameters, clientCall));
	}

	/// <summary>
	/// Writes <c>var response = [await ]client[.Sub]</c>: the response assignment and the receiver the executing
	/// method is invoked on, up to but not including the method name, so the caller decides whether the method
	/// continues on the same line or drops onto its own.
	/// </summary>
	private static void WriteClientCallReceiver(CodeWriter writer, ClientCallInfo clientMethod)
	{
		var options = writer.Options;

		writer.Write("var ").Write(options.ResponseVariableName).Write(" = ");

		if (options.ClientCallStyle == ClientCallStyle.Async)
		{
			writer.Write("await ");
		}

		writer.Write(options.ClientVariableName);

		if (clientMethod.SubClient.Length > 0)
		{
			writer.Write(".").Write(clientMethod.SubClient);
		}
	}

	/// <summary>
	/// Writes <c>.Method[Async][&lt;T, ...&gt;](</c> with <paramref name="genericArity"/> type arguments. They are
	/// spelled explicitly because the compiler cannot infer them from the argument, as the placeholder document type
	/// in strongly-typed-document mode and <see cref="System.Text.Json.JsonElement"/> otherwise. The count depends on
	/// which overload the call targets: the request overload leaves only the response-only parameters open, while a
	/// descriptor-action overload takes a lambda and so infers nothing at all.
	/// </summary>
	private static void WriteClientCallMethod(CodeWriter writer, ClientCallInfo clientMethod, int genericArity)
	{
		var options = writer.Options;
		var async = options.ClientCallStyle == ClientCallStyle.Async;

		writer.Write(".").Write(async ? clientMethod.Method + "Async" : clientMethod.Method);

		if (genericArity > 0)
		{
			writer.Write("<");
			for (var i = 0; i < genericArity; i++)
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

		writer.Write("(");
	}

	/// <summary>Writes <c>var response = [await ]client.[Sub.]Method[Async][&lt;T, ...&gt;](</c> on one line;
	/// see <see cref="WriteClientCallMethod"/> for the generic-arity rules.</summary>
	private static void WriteClientCallPrefix(CodeWriter writer, ClientCallInfo clientMethod, int genericArity)
	{
		WriteClientCallReceiver(writer, clientMethod);
		WriteClientCallMethod(writer, clientMethod, genericArity);
	}

	/// <summary>Appends the executing client invocation as a second statement referencing the request variable.</summary>
	private static void WriteClientCall(CodeWriter writer, ClientCallInfo clientMethod)
	{
		writer.WriteLine().WriteLine();
		WriteClientCallPrefix(writer, clientMethod, clientMethod.ResponseGenericArity);
		writer.Write(writer.Options.VariableName).Write(");");
	}

	/// <summary>
	/// Writes the whole invocation with the request inlined as the argument: the configuration lambda
	/// (plus hoisted chain-head arguments) for a descriptor-capable request in descriptor mode, the request
	/// expression otherwise. A call whose argument spans multiple lines wraps: the method drops onto its own
	/// line one indent in, so the lambda or initializer reads as a block under it (the lambda additionally
	/// closes <c>);</c> on its own line at the method's indent; the initializer keeps C#'s customary
	/// <c>});</c> on the closing-brace line). A call whose argument stays single-line stays on one line.
	/// </summary>
	private static void WriteInlineClientCall(CodeWriter writer, ClientCallInfo clientMethod, ICodeFormattable formattable)
	{
		// A negative descriptor arity means no client overload accepts the hoisted arguments plus a configuration
		// lambda, so even a split-capable request has to take the request form here.
		if (writer.Options.SyntaxMode == SyntaxMode.Descriptor
			&& clientMethod.DescriptorGenericArity >= 0
			&& formattable is IClientCallFormattable descriptorFormattable)
		{
			// The wrap decision must precede the prefix, but whether the chain is empty only shows once it is
			// written, so probe it against a scratch writer first.
			if (writer.WritesNothing(descriptorFormattable.FormatDescriptorChain))
			{
				WriteClientCallPrefix(writer, clientMethod, clientMethod.DescriptorGenericArity);
				writer.WriteInlineDescriptorArguments(
					descriptorFormattable.FormatDescriptorHeadArguments,
					descriptorFormattable.FormatDescriptorChain);
				writer.Write(");");
				return;
			}

			WriteClientCallReceiver(writer, clientMethod);
			writer.WriteLine();
			using (writer.Indent())
			{
				WriteClientCallMethod(writer, clientMethod, clientMethod.DescriptorGenericArity);
			}

			// The arguments are written at the statement's own level: the chain body already renders two levels
			// deeper (the configuration lambda's indent plus the indent the generated chain applies to itself),
			// which is exactly one level below the wrapped method line.
			writer.WriteInlineDescriptorArguments(
				descriptorFormattable.FormatDescriptorHeadArguments,
				descriptorFormattable.FormatDescriptorChain);

			writer.WriteLine();
			using (writer.Indent())
			{
				writer.Write(");");
			}

			return;
		}

		// The argument is a request, so it must render as one even in descriptor mode - a split-capable
		// request's FormatCode would otherwise emit a descriptor the request overload does not accept. The root
		// argument must also name its type: a target-typed new() is ambiguous against the client method's
		// overload set (request vs. descriptor-action overloads).
		void WriteRequestArgument(CodeWriter w)
		{
			using var _objectInitializer = w.ForceObjectInitializer();
			w.ForceNextExplicitConstructor();
			formattable.FormatCode(w);
		}

		// The wrap decision must precede the prefix, but whether the initializer spans multiple lines only shows
		// once it is written, so probe it against a scratch writer first.
		if (writer.WritesMultipleLines(WriteRequestArgument))
		{
			WriteClientCallReceiver(writer, clientMethod);
			writer.WriteLine();
			using (writer.Indent())
			{
				WriteClientCallMethod(writer, clientMethod, clientMethod.ResponseGenericArity);
				WriteRequestArgument(writer);
				writer.Write(");");
			}

			return;
		}

		WriteClientCallPrefix(writer, clientMethod, clientMethod.ResponseGenericArity);
		WriteRequestArgument(writer);
		writer.Write(");");
	}
}
