using System.Text;

namespace Elastic.Clients.Elasticsearch;

public sealed partial class ScrollId : RequestConverter.ICodeFormattable
{
	public void FormatCode(StringBuilder sb)
	{
		sb.Append("\"");
		sb.Append(Id);
		sb.Append("\"");
	}
}
