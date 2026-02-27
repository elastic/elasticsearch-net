using System.Text;

namespace Elastic.Clients.Elasticsearch.Aggregations;

public partial class TermsInclude : RequestConverter.ICodeFormattable
{
	public void FormatCode(StringBuilder sb)
	{
		if (RegexPattern is not null)
		{
			sb.Append("new TermsInclude(\"");
			sb.Append(RegexPattern);
			sb.Append("\")");
		}
		else if (Values is not null)
		{
			sb.Append("new TermsInclude(new[] { ");
			var first = true;
			foreach (var value in Values)
			{
				if (!first)
					sb.Append(", ");
				first = false;
				sb.Append("\"");
				sb.Append(value);
				sb.Append("\"");
			}
			sb.Append(" })");
		}
		else if (Partition.HasValue && NumberOfPartitions.HasValue)
		{
			sb.Append("new TermsInclude(");
			sb.Append(Partition.Value);
			sb.Append("L, ");
			sb.Append(NumberOfPartitions.Value);
			sb.Append("L)");
		}
	}
}
