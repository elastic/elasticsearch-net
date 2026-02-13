using System.Globalization;
using System.Text;

namespace Elastic.Clients.Elasticsearch;

public partial class Id : RequestConverter.ICodeFormattable
{
	public void FormatCode(StringBuilder sb)
	{
		if (LongValue.HasValue)
		{
			sb.Append(LongValue.Value);
			sb.Append("L");
		}
		else
		{
			sb.Append("\"");
			sb.Append(StringOrLongValue);
			sb.Append("\"");
		}
	}
}
