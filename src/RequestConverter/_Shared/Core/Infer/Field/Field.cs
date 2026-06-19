namespace Elastic.Clients.Elasticsearch;

public sealed partial class Field : RequestConverter.ICodeFormattable
{
	public void FormatCode(RequestConverter.CodeWriter writer)
	{
		writer.WriteString(Name ?? Expression?.ToString() ?? Property?.Name);
	}
}
