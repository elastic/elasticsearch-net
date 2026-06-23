using System.Text;

namespace Elastic.Clients.Elasticsearch;

public sealed partial class NodeIds : RequestConverter.ICodeFormattable
{
	public void FormatCode(RequestConverter.CodeWriter writer)
	{
		writer.WriteString(ToString());
	}
}
