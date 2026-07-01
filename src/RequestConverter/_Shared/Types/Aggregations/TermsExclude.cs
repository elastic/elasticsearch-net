// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace Elastic.Clients.Elasticsearch.Aggregations;

public sealed partial class TermsExclude : RequestConverter.ICodeFormattable
{
	public void FormatCode(RequestConverter.CodeWriter writer)
	{
		// WriteTypeRef (not a raw string) so the Aggregations namespace is registered for the snippet's using set; in
		// descriptor mode the enclosing `.AddAggregation(k, d => d…)` chain never spells an Aggregations type, so nothing
		// else imports it.
		if (RegexPattern is not null)
		{
			writer.Write("new ").WriteTypeRef("Elastic.Clients.Elasticsearch.Aggregations.TermsExclude").Write("(").WriteString(RegexPattern).Write(")");
		}
		else if (Values is not null)
		{
			writer.Write("new ").WriteTypeRef("Elastic.Clients.Elasticsearch.Aggregations.TermsExclude").Write("(new[] ");
			writer.WriteInlineList(Values, static (w, value) => w.WriteString(value), open: "{ ", close: " }");
			writer.Write(")");
		}
	}
}
