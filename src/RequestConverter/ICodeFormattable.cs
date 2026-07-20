// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace RequestConverter;

/// <summary>
/// Implemented by client types that can emit the C# source code which reconstructs them. The interface
/// exists only in the request-converter compilation (this project compile-links the client sources), so
/// nothing converter-related surfaces on the shipped client package.
/// </summary>
public interface ICodeFormattable
{
	void FormatCode(CodeWriter writer);
}
