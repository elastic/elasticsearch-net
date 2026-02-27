using System.Globalization;
using System.Text;

namespace Elastic.Clients.Elasticsearch.Aggregations;

public sealed partial class PercentilesItem : RequestConverter.ICodeFormattable
{
	public void FormatCode(StringBuilder sb)
	{
		sb.Append("new()");
		var hasProps = false;
		{
			sb.Append(hasProps ? ", " : " { ");
			hasProps = true;
			sb.Append("Key = ");
			sb.Append("\"");
			sb.Append(Key);
			sb.Append("\"");
		}

		{
			sb.Append(hasProps ? ", " : " { ");
			hasProps = true;
			sb.Append("Value = ");
			if (Value.HasValue)
				sb.Append(Value.Value.ToString(CultureInfo.InvariantCulture));
			else
				sb.Append("null");
		}

		if (hasProps)
			sb.Append(" }");
	}
}
