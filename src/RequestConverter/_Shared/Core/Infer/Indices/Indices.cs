using System.Text;

namespace Elastic.Clients.Elasticsearch;

public sealed partial class Indices : RequestConverter.ICodeFormattable
{
	public void FormatCode(StringBuilder sb)
	{
		sb.Append("[");
		var first = true;
		foreach (var index in this)
		{
			if (!first)
				sb.Append(", ");
			first = false;
			((RequestConverter.ICodeFormattable)index).FormatCode(sb);
		}
		sb.Append("]");
	}
}
