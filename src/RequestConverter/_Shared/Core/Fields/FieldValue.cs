using System.Globalization;
using System.Text;

namespace Elastic.Clients.Elasticsearch;

public readonly partial struct FieldValue : RequestConverter.ICodeFormattable
{
	public void FormatCode(StringBuilder sb)
	{
		switch (Kind)
		{
			case ValueKind.Null:
				sb.Append("null");
				break;
			case ValueKind.Boolean:
				sb.Append((bool)Value! ? "true" : "false");
				break;
			case ValueKind.Long:
				sb.Append(((long)Value!).ToString(CultureInfo.InvariantCulture));
				sb.Append("L");
				break;
			case ValueKind.Double:
				sb.Append(((double)Value!).ToString(CultureInfo.InvariantCulture));
				break;
			case ValueKind.String:
				sb.Append("\"");
				sb.Append((string)Value!);
				sb.Append("\"");
				break;
		}
	}
}
