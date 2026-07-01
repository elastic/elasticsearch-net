// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace Elastic.Clients.Elasticsearch.Aggregations;

public partial class TermsInclude : RequestConverter.ICodeFormattable
{
	public void FormatCode(RequestConverter.CodeWriter writer)
	{
		// WriteTypeRef (not a raw type name) so the Aggregations namespace is registered for the snippet's using set; in
		// descriptor mode the enclosing `.AddAggregation(k, d => d…)` chain never spells an Aggregations type, so nothing
		// else imports it.
		const string typeName = "Elastic.Clients.Elasticsearch.Aggregations.TermsInclude";
		if (RegexPattern is not null)
		{
			writer.Write("new ").WriteTypeRef(typeName).Write("(").WriteString(RegexPattern).Write(")");
		}
		else if (Values is not null)
		{
			writer.Write("new ").WriteTypeRef(typeName).Write("(new[] ");
			writer.WriteInlineList(Values, static (w, value) => w.WriteString(value), open: "{ ", close: " }");
			writer.Write(")");
		}
		else if (Partition.HasValue && NumberOfPartitions.HasValue)
		{
			writer.Write("new ").WriteTypeRef(typeName).Write("(").WriteValue(Partition.Value).Write("L, ").WriteValue(NumberOfPartitions.Value).Write("L)");
		}
	}
}
