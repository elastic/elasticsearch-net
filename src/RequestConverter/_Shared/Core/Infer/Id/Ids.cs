using System.Text;

namespace Elastic.Clients.Elasticsearch;

public partial class Ids : RequestConverter.ICodeFormattable
{
	public void FormatCode(StringBuilder sb)
	{
		sb.Append("[");
		var first = true;
		foreach (var id in _ids)
		{
			if (!first)
				sb.Append(", ");
			first = false;
			((RequestConverter.ICodeFormattable)id).FormatCode(sb);
		}
		sb.Append("]");
	}
}
