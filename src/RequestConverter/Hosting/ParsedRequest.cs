// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Text.Json;

namespace RequestConverter.Hosting;

/// <summary>
/// A request as parsed by the host (<c>@elastic/request-converter</c>). Field names are snake_case to match the
/// JSON the host emits; the heavy <c>request</c> (schema) property is stripped by the host before sending, so it
/// is intentionally absent here. Mirrors the <c>ParsedRequest</c> shape in <c>request-converter/src/parse.ts</c>.
/// </summary>
public sealed record ParsedRequest
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
/// The host's conversion options. The typed fields - <see cref="TypeNameStyle"/>, <see cref="SyntaxMode"/>,
/// <see cref="UseStronglyTypedDocument"/>, <see cref="DocumentTypeName"/>, <see cref="ClientCallFormat"/>, and
/// <see cref="ClientCallStyle"/> - drive the .NET converter output. The remaining fields exist to round-trip
/// the host contract. <see cref="TypeNameStyle"/> is an extension the harness passes through the host's
/// open-ended options bag to select the emitted type-name spelling.
/// </summary>
public sealed record ConvertOptions
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

	/// <summary>Selects the client invocation flavor when <see cref="ClientCallFormat"/> is set: <c>async</c>
	/// (default) or <c>sync</c>. Extension to the host contract.</summary>
	public string? ClientCallStyle { get; init; }

	/// <summary>How the client invocation that executes the request is emitted: <c>none</c> (default),
	/// <c>statement</c> (request variable plus a call statement), or <c>inline</c> (request or descriptor lambda
	/// inlined as the call argument). Extension to the host contract.</summary>
	public string? ClientCallFormat { get; init; }
}
