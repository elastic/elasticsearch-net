using System.Globalization;
using System.Text;

namespace Elastic.Clients.Elasticsearch;

public readonly partial struct FieldValue : RequestConverter.ICodeFormattable
{
	public void FormatCode(RequestConverter.CodeWriter writer)
	{
		switch (Kind)
		{
			case ValueKind.Null:
				writer.Append("null");
				break;
			case ValueKind.Boolean:
				writer.Append((bool)Value! ? "true" : "false");
				break;
			case ValueKind.Long:
				writer.Append(((long)Value!).ToString(CultureInfo.InvariantCulture));
				writer.Append("L");
				break;
			case ValueKind.Double:
				writer.Append(((double)Value!).ToString(CultureInfo.InvariantCulture));
				break;
			case ValueKind.String:
				writer.Append("\"");
				writer.Append((string)Value!);
				writer.Append("\"");
				break;
		}
	}
}
