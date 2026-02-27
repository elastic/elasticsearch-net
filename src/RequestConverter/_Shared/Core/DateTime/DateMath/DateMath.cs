using System.Text;

namespace Elastic.Clients.Elasticsearch;

public abstract partial class DateMath : RequestConverter.ICodeFormattable
{
	public void FormatCode(StringBuilder sb)
	{
		sb.Append("\"");
		sb.Append(ToString());
		sb.Append("\"");
	}
}
