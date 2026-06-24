// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace Elastic.Clients.Elasticsearch;

public sealed partial class MinimumShouldMatch : RequestConverter.ICodeFormattable
{
	// MinimumShouldMatch is a Union<int, string> (e.g. 1 or "50%"); render the active arm — an int
	// literal or a quoted string — not ToString(), which has no override and yields the type name.
	public void FormatCode(RequestConverter.CodeWriter writer) =>
		UnionExtensions.FormatCode<int, string>(this, writer);
}
