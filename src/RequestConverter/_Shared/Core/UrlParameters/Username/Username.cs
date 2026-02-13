using System.Text;

namespace Elastic.Clients.Elasticsearch;

public partial class Username : RequestConverter.ICodeFormattable
{
	public void FormatCode(StringBuilder sb)
	{
		sb.Append("\"");
		sb.Append(Value);
		sb.Append("\"");
	}
}
