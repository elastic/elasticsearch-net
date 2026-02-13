using System.Text;

namespace Elastic.Clients.Elasticsearch;

public partial class IndexAlias : RequestConverter.ICodeFormattable
{
	public void FormatCode(StringBuilder sb)
	{
		sb.Append("\"");
		sb.Append(Alias);
		sb.Append("\"");
	}
}
