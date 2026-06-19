using System.Text;

namespace Elastic.Clients.Elasticsearch;

public sealed partial class Indices : RequestConverter.ICodeFormattable
{
	public void FormatCode(RequestConverter.CodeWriter writer)
	{
		writer.Append("[");
		var first = true;
		foreach (var index in this)
		{
			if (!first)
				writer.Append(", ");
			first = false;
			((RequestConverter.ICodeFormattable)index).FormatCode(writer);
		}
		writer.Append("]");
	}
}
