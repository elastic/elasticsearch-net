using System;
using System.Text;
using System.Text.Json;

namespace RequestConverter;

internal static class JsonElementCodeFormatter
{
	public static string Format(JsonElement element, int indentLevel = 0)
	{
		var sb = new StringBuilder();
		var indent = new string(' ', indentLevel * 4);

		switch (element.ValueKind)
		{
			case JsonValueKind.Object:
			{
				sb.AppendLine("new");
				sb.AppendLine(indent + "{");

				foreach (var property in element.EnumerateObject())
				{
					var key = property.Name;
					var value = Format(property.Value, indentLevel + 1);

					sb.AppendLine($"{indent}    {key} = {value},");
				}

				sb.Append(indent + "}");
				break;
			}
			case JsonValueKind.Array:
			{
				sb.Append("[ ");

				foreach (var item in element.EnumerateArray())
				{
					sb.Append(Format(item, indentLevel + 1) + ", ");
				}

				sb.Length -= 2; // Remove trailing comma and space
				sb.Append(" ]");
				break;
			}
			default:
				sb.Append(FormatJsonValue(element));
				break;
		}

		return sb.ToString();
	}

	private static string FormatJsonValue(JsonElement element)
	{
		return element.ValueKind switch
		{
			JsonValueKind.String => $"\"{element.GetString()}\"",
			JsonValueKind.Number => element.ToString(),
			JsonValueKind.True => "true",
			JsonValueKind.False => "false",
			JsonValueKind.Null => "null",
			_ => throw new NotSupportedException($"JSON value kind '{element.ValueKind}' is not supported.")
		};
	}
}
