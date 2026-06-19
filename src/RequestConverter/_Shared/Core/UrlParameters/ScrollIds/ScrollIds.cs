using System.Text;

namespace Elastic.Clients.Elasticsearch;

public sealed partial class ScrollIds : RequestConverter.ICodeFormattable
{
	public void FormatCode(RequestConverter.CodeWriter writer)
	{
		writer.Append("[");
		var first = true;
		foreach (var id in this)
		{
			if (!first)
				writer.Append(", ");
			first = false;
			((RequestConverter.ICodeFormattable)id).FormatCode(writer);
		}
		writer.Append("]");
	}
}
