using System.Globalization;
using System.Text;

namespace Elastic.Clients.Elasticsearch.Aggregations;

public sealed partial class PercentilesItem : RequestConverter.ICodeFormattable
{
	public void FormatCode(RequestConverter.CodeWriter writer)
	{
		writer.Append("new()");
		var hasProps = false;
		{
			writer.Append(hasProps ? ", " : " { ");
			hasProps = true;
			writer.Append("Key = ");
			writer.Append("\"");
			writer.Append(Key);
			writer.Append("\"");
		}

		{
			writer.Append(hasProps ? ", " : " { ");
			hasProps = true;
			writer.Append("Value = ");
			if (Value.HasValue)
				writer.Append(Value.Value.ToString(CultureInfo.InvariantCulture));
			else
				writer.Append("null");
		}

		if (hasProps)
			writer.Append(" }");
	}
}
