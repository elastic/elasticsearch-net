using System.Text;

namespace Elastic.Clients.Elasticsearch;

public partial class IndexName : RequestConverter.ICodeFormattable
{
	public void FormatCode(StringBuilder sb)
	{
		sb.Append("\"");
		sb.Append(Name ?? Type?.Name);
		sb.Append("\"");
	}
}
