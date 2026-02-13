using System.Text;

namespace Elastic.Clients.Elasticsearch;

public sealed partial class ScrollIds : RequestConverter.ICodeFormattable
{
	public void FormatCode(StringBuilder sb)
	{
		sb.Append("[");
		var first = true;
		foreach (var id in this)
		{
			if (!first)
				sb.Append(", ");
			first = false;
			((RequestConverter.ICodeFormattable)id).FormatCode(sb);
		}
		sb.Append("]");
	}
}
