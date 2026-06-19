using System.Text;

namespace Elastic.Clients.Elasticsearch;

public partial class Ids : RequestConverter.ICodeFormattable
{
	public void FormatCode(RequestConverter.CodeWriter writer)
	{
		writer.Append("[");
		var first = true;
		foreach (var id in _ids)
		{
			if (!first)
				writer.Append(", ");
			first = false;
			((RequestConverter.ICodeFormattable)id).FormatCode(writer);
		}
		writer.Append("]");
	}
}
