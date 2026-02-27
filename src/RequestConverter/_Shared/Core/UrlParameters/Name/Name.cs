using System.Text;

namespace Elastic.Clients.Elasticsearch;

public sealed partial class Name : RequestConverter.ICodeFormattable
{
	public void FormatCode(StringBuilder sb)
	{
		sb.Append("\"");
		sb.Append(Value);
		sb.Append("\"");
	}
}
