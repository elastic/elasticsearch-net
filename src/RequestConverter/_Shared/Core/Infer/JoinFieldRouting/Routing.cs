using System.Text;

namespace Elastic.Clients.Elasticsearch;

public partial class Routing : RequestConverter.ICodeFormattable
{
	public void FormatCode(StringBuilder sb)
	{
		sb.Append("\"");
		sb.Append(StringOrLongValue);
		sb.Append("\"");
	}
}
