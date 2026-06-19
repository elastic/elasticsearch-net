using System.Text;

namespace Elastic.Clients.Elasticsearch;

public sealed partial class Name : RequestConverter.ICodeFormattable
{
	public void FormatCode(RequestConverter.CodeWriter writer)
	{
		writer.Append("\"");
		writer.Append(Value);
		writer.Append("\"");
	}
}
