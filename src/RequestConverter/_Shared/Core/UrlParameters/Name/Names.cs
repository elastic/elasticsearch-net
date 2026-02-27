using System.Text;

namespace Elastic.Clients.Elasticsearch;

public sealed partial class Names : RequestConverter.ICodeFormattable
{
	public void FormatCode(StringBuilder sb)
	{
		sb.Append("[");
		var first = true;
		foreach (var name in Values)
		{
			if (!first)
				sb.Append(", ");
			first = false;
			((RequestConverter.ICodeFormattable)name).FormatCode(sb);
		}
		sb.Append("]");
	}
}
