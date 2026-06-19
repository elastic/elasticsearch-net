using System.Text;

namespace Elastic.Clients.Elasticsearch;

public sealed partial class TaskId : RequestConverter.ICodeFormattable
{
	public void FormatCode(RequestConverter.CodeWriter writer)
	{
		writer.Append("\"");
		writer.Append(ToString());
		writer.Append("\"");
	}
}
