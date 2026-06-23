using System.Text;

namespace Elastic.Clients.Elasticsearch;

public partial class Username : RequestConverter.ICodeFormattable
{
	public void FormatCode(RequestConverter.CodeWriter writer)
	{
		writer.WriteString(ToString());
	}
}
