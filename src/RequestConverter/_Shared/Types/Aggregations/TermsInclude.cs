namespace Elastic.Clients.Elasticsearch.Aggregations;

public partial class TermsInclude : RequestConverter.ICodeFormattable
{
	public void FormatCode(RequestConverter.CodeWriter writer)
	{
		if (RegexPattern is not null)
		{
			writer.Write("new TermsInclude(").WriteString(RegexPattern).Write(")");
		}
		else if (Values is not null)
		{
			writer.Write("new TermsInclude(new[] ");
			writer.WriteInlineList(Values, static (w, value) => w.WriteString(value), open: "{ ", close: " }");
			writer.Write(")");
		}
		else if (Partition.HasValue && NumberOfPartitions.HasValue)
		{
			writer.Write("new TermsInclude(").WriteValue(Partition.Value).Write("L, ").WriteValue(NumberOfPartitions.Value).Write("L)");
		}
	}
}
