// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Text;

namespace Elastic.Clients.Elasticsearch.Aggregations;

public sealed partial class TermsExclude : RequestConverter.ICodeFormattable
{
	public void FormatCode(RequestConverter.CodeWriter writer)
	{
		if (RegexPattern is not null)
		{
			writer.Append("new TermsExclude(\"");
			writer.Append(RegexPattern);
			writer.Append("\")");
		}
		else if (Values is not null)
		{
			writer.Append("new TermsExclude(new[] { ");
			var first = true;
			foreach (var value in Values)
			{
				if (!first)
					writer.Append(", ");
				first = false;
				writer.Append("\"");
				writer.Append(value);
				writer.Append("\"");
			}
			writer.Append(" })");
		}
	}
}
