using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Elasticsearch.Net.Extensions;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	public interface IDateMath
	{
		Union<DateTime, string> Anchor { get; }
		IList<Tuple<DateMathOperation, DateMathTime>> Ranges { get; }
		DateMathTimeUnit? Round { get; }
	}

	[JsonFormatter(typeof(DateMathFormatter))]
	public abstract class DateMath : IDateMath
	{
		private static readonly Regex DateMathRegex =
			new Regex(@"^(?<anchor>now|.+(?:\|\||$))(?<ranges>(?:(?:\+|\-)[^\/]*))?(?<rounding>\/(?:y|M|w|d|h|m|s))?$");

		protected Union<DateTime, string> Anchor;

		protected DateMathTimeUnit? Round;

		public static DateMathExpression Now => new DateMathExpression("now");

		protected IDateMath Self => this;

		Union<DateTime, string> IDateMath.Anchor => Anchor;
		IList<Tuple<DateMathOperation, DateMathTime>> IDateMath.Ranges { get; } = new List<Tuple<DateMathOperation, DateMathTime>>();
		DateMathTimeUnit? IDateMath.Round => Round;

		public static DateMathExpression Anchored(DateTime anchor) => new DateMathExpression(anchor);

		public static DateMathExpression Anchored(string dateAnchor) => new DateMathExpression(dateAnchor);

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

		internal bool IsValid => Self.Anchor.Match(d => d != default, s => !s.IsNullOrEmpty());

		public override string ToString()
		{
			if (!IsValid) return string.Empty;

			var separator = Self.Round.HasValue || Self.Ranges.HasAny() ? "||" : string.Empty;

			var sb = new StringBuilder();
			var anchor = Self.Anchor.Match(
				d => ToMinThreeDecimalPlaces(d) + separator,
				s => s == "now" || s.EndsWith("||", StringComparison.Ordinal) ? s : s + separator
			);
			sb.Append(anchor);
			foreach (var r in Self.Ranges)
			{
				sb.Append(r.Item1.GetStringValue());
				//date math does not support fractional time units so e.g TimeSpan.FromHours(25) should not yield 1.04d
				sb.Append(r.Item2);
			}
			if (Self.Round.HasValue)
				sb.Append("/" + Self.Round.Value.GetStringValue());

			return sb.ToString();
		}

		/// <summary>
		/// Formats a <see cref="DateTime"/> to have a minimum of 3 decimal places if there are sub second values
		/// </summary>
		private static string ToMinThreeDecimalPlaces(DateTime dateTime)
		{
			var builder = StringBuilderCache.Acquire(33);
			var format = dateTime.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFF", CultureInfo.InvariantCulture);
			builder.Append(format);

			// Fixes bug in Elasticsearch: https://github.com/elastic/elasticsearch/pull/41871
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

			return StringBuilderCache.GetStringAndRelease(builder);
		}

		private static void AppendTwoDigitNumber(StringBuilder result, int val)
		{
			result.Append((char)('0' + (val / 10)));
			result.Append((char)('0' + (val % 10)));
		}
	}

	internal class DateMathFormatter : IJsonFormatter<DateMath>
	{
		public DateMath Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();
			if (token != JsonToken.String)
				return null;

			var segment = reader.ReadStringSegmentUnsafe();

			if (!segment.ContainsDateMathSeparator() && segment.IsDateTime(formatterResolver, out var dateTime))
				return DateMath.Anchored(dateTime);

			var value = segment.Utf8String();
			return DateMath.FromString(value);
		}

		public void Serialize(ref JsonWriter writer, DateMath value, IJsonFormatterResolver formatterResolver) =>
			writer.WriteString(value.ToString());
	}
}
