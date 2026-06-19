using System.Text;

namespace Elastic.Clients.Elasticsearch;

public partial class IndexName : RequestConverter.ICodeFormattable
{
	public void FormatCode(RequestConverter.CodeWriter writer)
	{
		writer.Append("\"");
		writer.Append(Name ?? Type?.Name);
		writer.Append("\"");
	}
}
