using System.Text;

namespace Elastic.Clients.Elasticsearch;

public sealed partial class ScrollId : RequestConverter.ICodeFormattable
{
	public void FormatCode(RequestConverter.CodeWriter writer)
	{
		writer.Append("\"");
		writer.Append(Id);
		writer.Append("\"");
	}
}
