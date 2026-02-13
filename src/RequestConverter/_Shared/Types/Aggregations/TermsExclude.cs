using System.Text;

namespace Elastic.Clients.Elasticsearch.Aggregations;

public sealed partial class TermsExclude : RequestConverter.ICodeFormattable
{
	public void FormatCode(StringBuilder sb)
	{
		if (RegexPattern is not null)
		{
			sb.Append("new TermsExclude(\"");
			sb.Append(RegexPattern);
			sb.Append("\")");
		}
		else if (Values is not null)
		{
			sb.Append("new TermsExclude(new[] { ");
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
	}
}
