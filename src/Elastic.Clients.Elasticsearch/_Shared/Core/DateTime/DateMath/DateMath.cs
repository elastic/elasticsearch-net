// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Elastic.Clients.Elasticsearch;

[JsonConverter(typeof(DateMathConverter))]
public abstract class DateMath
{
	private static readonly Regex DateMathRegex =
		new(@"^(?<anchor>now|.+(?:\|\||$))(?<ranges>(?:(?:\+|\-)[^\/]*))?(?<rounding>\/(?:y|M|w|d|h|m|s))?$");

	public static DateMathExpression Now => new("now");

	internal DateMath(string anchor) => Anchor = anchor;
	internal DateMath(DateTime anchor) => Anchor = anchor;
	internal DateMath(Union<DateTime, string> anchor, DateMathTime range, DateMathOperation operation)
	{
		anchor.ThrowIfNull(nameof(anchor));
		range.ThrowIfNull(nameof(range));
		operation.ThrowIfNull(nameof(operation));
		Anchor = anchor;
		Ranges.Add(Tuple.Create(operation, range));
	}

	public Union<DateTime, string> Anchor { get; }
	public IList<Tuple<DateMathOperation, DateMathTime>> Ranges { get; } = new List<Tuple<DateMathOperation, DateMathTime>>();
	public DateMathTimeUnit? Round { get; protected set; }

	public static DateMathExpression Anchored(DateTime anchor) => new(anchor);

	public static DateMathExpression Anchored(string dateAnchor) => new(dateAnchor);

	public static implicit operator DateMath(DateTime dateTime) => Anchored(dateTime);

	public static implicit operator DateMath(string dateMath) => FromString(dateMath);

	public static DateMath FromString(string dateMath)
	{
		if (dateMath == null) return null;

		var match = DateMathRegex.Match(dateMath);
		if (!match.Success) throw new ArgumentException($"Cannot create a {nameof(DateMathExpression)} out of '{dateMath}'");

		var math = new DateMathExpression(match.Groups["anchor"].Value);

		if (match.Groups["ranges"].Success)
		{
			var rangeString = match.Groups["ranges"].Value;
			do
			{
				var nextRangeStart = rangeString.Substring(1).IndexOfAny(new[] { '+', '-', '/' });
				if (nextRangeStart == -1) nextRangeStart = rangeString.Length - 1;
				var unit = rangeString.Substring(1, nextRangeStart);
				if (rangeString.StartsWith("+", StringComparison.Ordinal))
				{
					math = math.Add(unit);
					rangeString = rangeString.Substring(nextRangeStart + 1);
				}
				else if (rangeString.StartsWith("-", StringComparison.Ordinal))
				{
					math = math.Subtract(unit);
					rangeString = rangeString.Substring(nextRangeStart + 1);
				}
				else rangeString = null;
			} while (!rangeString.IsNullOrEmpty());
		}

		if (match.Groups["rounding"].Success)
		{
			var rounding = match.Groups["rounding"].Value.Substring(1).ToEnum<DateMathTimeUnit>(StringComparison.Ordinal);
			if (rounding.HasValue)
				return math.RoundTo(rounding.Value);
		}
		return math;
	}

	internal static bool IsValidDateMathString(string dateMath) => dateMath != null && DateMathRegex.IsMatch(dateMath);

	internal bool IsValid => Anchor.Match(_ => true, s => !s.IsNullOrEmpty());

	public override string ToString()
	{
		if (!IsValid) return string.Empty;

		var separator = Round.HasValue || Ranges.HasAny() ? "||" : string.Empty;

		var sb = new StringBuilder();
		var anchor = Anchor.Match(
			d => ToMinThreeDecimalPlaces(d) + separator,
			s => s == "now" || s.EndsWith("||", StringComparison.Ordinal) ? s : s + separator
		);
		sb.Append(anchor);
		foreach (var r in Ranges)
		{
			sb.Append(r.Item1.GetStringValue());
			//date math does not support fractional time units so e.g TimeSpan.FromHours(25) should not yield 1.04d
			sb.Append(r.Item2);
		}
		if (Round.HasValue)
			sb.Append("/" + Round.Value.GetStringValue());

		return sb.ToString();
	}

	/// <summary>
	/// Formats a <see cref="DateTime"/> to have a minimum of 3 decimal places if there are sub second values
	/// </summary>
	private static string ToMinThreeDecimalPlaces(DateTime dateTime)
	{
		var builder = new StringBuilder(33);
		var format = dateTime.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFF", CultureInfo.InvariantCulture);
		builder.Append(format);

		if (format.Length > 20 && format.Length < 23)
		{
			var diff = 23 - format.Length;
			for (var i = 0; i < diff; i++)
				builder.Append('0');
		}

		switch (dateTime.Kind)
		{
			case DateTimeKind.Local:
				var offset = TimeZoneInfo.Local.GetUtcOffset(dateTime);
				if (offset >= TimeSpan.Zero)
					builder.Append('+');
				else
				{
					builder.Append('-');
					offset = offset.Negate();
				}

				AppendTwoDigitNumber(builder, offset.Hours);
				builder.Append(':');
				AppendTwoDigitNumber(builder, offset.Minutes);
				break;
			case DateTimeKind.Utc:
				builder.Append('Z');
				break;
		}

		return builder.ToString();
	}

	private static void AppendTwoDigitNumber(StringBuilder result, int val)
	{
		result.Append((char)('0' + (val / 10)));
		result.Append((char)('0' + (val % 10)));
	}
}

internal sealed class DateMathConverter : JsonConverter<DateMath>
{
	public override DateMath? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType != JsonTokenType.String)
			return null;

		// TODO: Performance - Review potential to avoid allocation on DateTime path and use Span<byte>

		var value = reader.GetString();
		reader.Read();

		if (!value.Contains("|") && DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out var dateTime))
			return DateMath.Anchored(dateTime);

		return DateMath.Anchored(value);
	}

	public override void Write(Utf8JsonWriter writer, DateMath value, JsonSerializerOptions options)
	{
		if (value is null)
		{
			writer.WriteNullValue();
			return;
		}

		writer.WriteStringValue(value.ToString());
	}
}
