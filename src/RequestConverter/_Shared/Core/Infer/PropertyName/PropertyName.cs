using System.Text;

namespace Elastic.Clients.Elasticsearch;

public sealed partial class PropertyName : RequestConverter.ICodeFormattable
{
	public void FormatCode(RequestConverter.CodeWriter writer)
	{
		writer.Append("\"");
		writer.Append(Name ?? Expression?.ToString() ?? Property?.Name);
		writer.Append("\"");
	}
}
