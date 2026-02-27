using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;

namespace RequestConverter;

internal static class CodeFormatter
{
	private static readonly Type TypeOfJsonElement = typeof(JsonElement);

	public static string FormatCode(ICodeFormattable formattable)
	{
		ArgumentNullException.ThrowIfNull(formattable);

		var sb = new StringBuilder();

		formattable.FormatCode(sb);

		return sb.ToString();
	}

	internal static void FormatCode<T>(T? value, StringBuilder builder)
	{
		if (value is ICodeFormattable formattable)
		{
			formattable.FormatCode(builder);
		}

		if (typeof(T) == TypeOfJsonElement)
		{
			var code = JsonElementCodeFormatter.Format(Unsafe.As<T, JsonElement>(ref value), 0);
			builder.Append(code);
			return;
		}

		builder.Append(value);
	}

	internal static void FormatCode(object? value, StringBuilder builder)
	{
		switch (value)
		{
			case ICodeFormattable formattable:
			{
				formattable.FormatCode(builder);
				return;
			}
			case JsonElement jsonElement:
			{
				var code = JsonElementCodeFormatter.Format(jsonElement, 0);
				builder.Append(code);
				return;
			}
			default:
			{
				builder.Append(value);
				break;
			}
		}
	}

	internal static void FormatCode<T>(
		IEnumerable<T> enumerable,
		Action<T, StringBuilder> formatElement,
		StringBuilder builder)
	{
		var first = true;

		foreach (var item in enumerable)
		{
			if (first)
			{
				first = false;
			}
			else
			{
				builder.Append(", ");
			}

			formatElement(item, builder);
		}
	}

	internal static void FormatCode<TKey, TValue>(
		IEnumerable<KeyValuePair<TKey, TValue>> dictionary,
		Action<TKey, StringBuilder> formatKey,
		Action<TValue, StringBuilder> formatValue,
		StringBuilder builder)
	{
		var first = true;

		foreach (var (key, value) in dictionary)
		{
			if (first)
			{
				builder.Append("{ ");
				first = false;
			}
			else
			{
				builder.Append(", { ");
			}

			formatKey(key, builder);
			builder.Append(", ");
			formatValue(value, builder);

			builder.Append(" }");
		}
	}
}
