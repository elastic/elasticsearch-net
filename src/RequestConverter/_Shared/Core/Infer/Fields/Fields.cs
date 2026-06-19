namespace Elastic.Clients.Elasticsearch;

public sealed partial class Fields : RequestConverter.ICodeFormattable
{
	public void FormatCode(RequestConverter.CodeWriter writer)
	{
		writer.WriteInlineList(ListOfFields, static (w, field) => w.WriteValue(field));
	}
}
