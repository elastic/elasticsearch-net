// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace RequestConverter;

/// <summary>
/// Implemented by request types whose descriptor rendering can be split for the inline client call:
/// the chain-head constructor arguments (hoisted into the client method's argument list) and the receiver-less
/// fluent chain (the configuration lambda's body). <see cref="ICodeFormattable.FormatCode"/> composes the same two
/// parts. Like <see cref="ICodeFormattable"/>, this interface exists only in the request-converter compilation.
/// </summary>
public interface IClientCallFormattable : ICodeFormattable
{
	/// <summary>Writes the chain-head constructor arguments, comma-separated, without surrounding parentheses.
	/// Writes nothing when the request has no chain-head constructor.</summary>
	void FormatDescriptorHeadArguments(CodeWriter writer);

	/// <summary>Writes the receiver-less fluent chain (each call starts on a new line, one indent level in).</summary>
	void FormatDescriptorChain(CodeWriter writer);
}
