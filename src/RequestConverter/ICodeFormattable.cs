// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace RequestConverter;

/// <summary>
/// Implemented (in the request-converter compilation only) by client types that can emit the C#
/// source code which reconstructs them. The member is <c>internal</c> so it never surfaces on the
/// shipped client package.
/// </summary>
public interface ICodeFormattable
{
	internal void FormatCode(CodeWriter writer);
}
