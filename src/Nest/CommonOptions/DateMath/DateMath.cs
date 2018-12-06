using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Utf8Json;

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
					var nextRangeStart = rangeString.Substring(1).IndexOfAny(new char[] { '+', '-', '/' });
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

		internal bool IsValid => Self.Anchor.Match(d => d != default, s => !s.IsNullOrEmpty());

		public override string ToString()
		{
			if (!IsValid) return string.Empty;

			var separator = Self.Round.HasValue || Self.Ranges.HasAny() ? "||" : string.Empty;

			var sb = new StringBuilder();
			var anchor = Self.Anchor.Match(
				d => JsonSerializer.ToJsonString(d) + separator,
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
	}

	internal class DateMathFormatter : IJsonFormatter<DateMath>
	{
		public DateMath Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();
			if (token != JsonToken.String)
				return null;

			var value = reader.ReadString();

			if (string.IsNullOrEmpty(value))
				return null;

			try
			{
				// TODO: possibly nicer way of doing this than brute try?
				var dateTime = JsonSerializer.Deserialize<DateTime>(value, formatterResolver);
				return DateMath.Anchored(dateTime);
			}
			catch
			{
				return DateMath.FromString(reader.ReadString());
			}

			return null;
		}

		public void Serialize(ref JsonWriter writer, DateMath value, IJsonFormatterResolver formatterResolver) =>
			writer.WriteString(value.ToString());
	}
}
