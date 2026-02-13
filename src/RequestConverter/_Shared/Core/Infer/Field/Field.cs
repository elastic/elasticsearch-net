using System.Text;

namespace Elastic.Clients.Elasticsearch;

public sealed partial class Field : RequestConverter.ICodeFormattable
{
	public void FormatCode(StringBuilder sb)
	{
		sb.Append("\"");
		sb.Append(Name ?? Expression?.ToString() ?? Property?.Name);
		sb.Append("\"");
	}
}
