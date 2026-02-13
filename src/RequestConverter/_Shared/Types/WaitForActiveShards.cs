using System.Text;

namespace Elastic.Clients.Elasticsearch;

public readonly partial struct WaitForActiveShards : RequestConverter.ICodeFormattable
{
	public void FormatCode(StringBuilder sb)
	{
		sb.Append("\"");
		sb.Append(Value);
		sb.Append("\"");
	}
}
