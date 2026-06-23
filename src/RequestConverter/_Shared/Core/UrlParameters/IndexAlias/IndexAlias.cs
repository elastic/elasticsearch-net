using System.Text;

namespace Elastic.Clients.Elasticsearch;

public partial class IndexAlias : RequestConverter.ICodeFormattable
{
	public void FormatCode(RequestConverter.CodeWriter writer)
	{
		writer.WriteString(ToString());
	}
}
