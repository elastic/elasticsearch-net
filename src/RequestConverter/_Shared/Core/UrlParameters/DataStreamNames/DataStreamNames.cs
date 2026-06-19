using System.Text;

namespace Elastic.Clients.Elasticsearch;

public sealed partial class DataStreamNames : RequestConverter.ICodeFormattable
{
	public void FormatCode(RequestConverter.CodeWriter writer)
	{
		writer.Append("[");
		var first = true;
		foreach (var name in this)
		{
			if (!first)
				writer.Append(", ");
			first = false;
			((RequestConverter.ICodeFormattable)name).FormatCode(writer);
		}
		writer.Append("]");
	}
}
