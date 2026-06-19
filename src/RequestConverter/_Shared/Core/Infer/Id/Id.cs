using System.Globalization;
using System.Text;

namespace Elastic.Clients.Elasticsearch;

public partial class Id : RequestConverter.ICodeFormattable
{
	public void FormatCode(RequestConverter.CodeWriter writer)
	{
		if (LongValue.HasValue)
		{
			writer.Append(LongValue.Value);
			writer.Append("L");
		}
		else
		{
			writer.Append("\"");
			writer.Append(StringOrLongValue);
			writer.Append("\"");
		}
	}
}
