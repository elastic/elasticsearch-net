using System.Collections.Generic;
using System.Text;

namespace Elastic.Clients.Elasticsearch.Aggregations;

public sealed partial class BucketsPath : RequestConverter.ICodeFormattable
{
	public void FormatCode(StringBuilder sb)
	{
		switch (_kind)
		{
			case Kind.Single:
				sb.Append("\"");
				sb.Append((string)_value);
				sb.Append("\"");
				break;
			case Kind.Array:
				sb.Append("[");
				var first = true;
				foreach (var item in (string[])_value)
				{
					if (!first)
						sb.Append(", ");
					first = false;
					sb.Append("\"");
					sb.Append(item);
					sb.Append("\"");
				}
				sb.Append("]");
				break;
			case Kind.Dictionary:
				sb.Append("new() { ");
				var firstKv = true;
				foreach (var kvp in (Dictionary<string, string>)_value)
				{
					if (!firstKv)
						sb.Append(", ");
					firstKv = false;
					sb.Append("[\"");
					sb.Append(kvp.Key);
					sb.Append("\"] = \"");
					sb.Append(kvp.Value);
					sb.Append("\"");
				}
				sb.Append(" }");
				break;
		}
	}
}
