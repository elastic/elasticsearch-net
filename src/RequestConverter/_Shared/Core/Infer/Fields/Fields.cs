using System.Text;

namespace Elastic.Clients.Elasticsearch;

public sealed partial class Fields : RequestConverter.ICodeFormattable
{
	public void FormatCode(StringBuilder sb)
	{
		sb.Append("[");
		var first = true;
		foreach (var field in ListOfFields)
		{
			if (!first)
				sb.Append(", ");
			first = false;
			((RequestConverter.ICodeFormattable)field).FormatCode(sb);
		}
		sb.Append("]");
	}
}
