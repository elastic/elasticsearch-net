using System.Text;

namespace Elastic.Clients.Elasticsearch;

public sealed partial class Duration : RequestConverter.ICodeFormattable
{
	public void FormatCode(StringBuilder sb)
	{
		sb.Append("\"");
		sb.Append(ToString());
		sb.Append("\"");
	}
}
