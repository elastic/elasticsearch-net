using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	public interface IDateMath
	{
		Union<DateTime, string> Anchor { get; }
		IList<Tuple<DateMathOperation, TimeUnitExpression>> Ranges { get; }
		TimeUnit? Round { get; }
	}

	[JsonConverter(typeof(DateMath.Json))]
	public abstract class DateMath : IDateMath
	{
		protected IDateMath Self => this;

		protected Union<DateTime, string> _anchor;
		Union<DateTime, string> IDateMath.Anchor => _anchor;

		protected TimeUnit? _round;
		TimeUnit? IDateMath.Round => _round;

		IList<Tuple<DateMathOperation, TimeUnitExpression>> IDateMath.Ranges { get; } = new List<Tuple<DateMathOperation, TimeUnitExpression>>();

		protected DateMath() { }

		public static DateMathExpression Now => new DateMathExpression("now");
		public static DateMathExpression Anchored(DateTime anchor) => new DateMathExpression(anchor);
		public static DateMathExpression Anchored(string dateAnchor) => new DateMathExpression(dateAnchor);

		private static Regex _dateMathRe =
			new Regex(@"^(?<anchor>now|.+(?:\|\||$))(?<ranges>(?:(?:\+|\-)[^\/]*))?(?<rounding>\/(?:y|M|w|d|h|m|s))?$");

		public static implicit operator DateMath(DateTime dateTime) => DateMath.Anchored(dateTime);
		public static implicit operator DateMath(string dateMath) => DateMath.FromString(dateMath);

		public static DateMath FromString(string dateMath)
		{
			var match = _dateMathRe.Match(dateMath);
			if (!match.Success) throw new ArgumentException($"Can not create a DateMathExpression out of '{dateMath}'");

			var math = new DateMathExpression(match.Groups["anchor"].Value);

			if (match.Groups["ranges"].Success)
			{
				var rangeString = match.Groups["ranges"].Value;
				do
				{
					var nextRangeStart = rangeString.Substring(1).IndexOfAny(new char[] { '+', '-', '/' });
					if (nextRangeStart == -1) nextRangeStart = rangeString.Length - 1;
					var unit = rangeString.Substring(1, nextRangeStart);
					if (rangeString.StartsWith("+"))
					{
						math = math.Add(unit);
						rangeString = rangeString.Substring(nextRangeStart + 1);
					}
					else if (rangeString.StartsWith("-"))
					{
						math = math.Subtract(unit);
						rangeString = rangeString.Substring(nextRangeStart + 1);
					}
					else rangeString = null;
				} while (!rangeString.IsNullOrEmpty());
			}

			if (match.Groups["rounding"].Success)
			{
				var rounding = match.Groups["rounding"].Value.Substring(1).ToEnum<TimeUnit>();
				if (rounding.HasValue)
					return math.RoundTo(rounding.Value);
			}
			return math;
		}

		public override string ToString()
		{
			var isValid = Self.Anchor.Match(d => d != default(DateTime), s => !s.IsNullOrEmpty());
			if (!isValid) return null;

			var separator = Self.Round.HasValue || Self.Ranges.HasAny() ? "||" : string.Empty;

			var sb = new StringBuilder();
			var anchor = Self.Anchor.Match(
				d => d.ToJsonNetString() + separator,
				s => s == "now" || s.EndsWith("||") ? s : s + separator
			);
			sb.Append(anchor);
			foreach (var r in Self.Ranges)
			{
				sb.Append(r.Item1.GetStringValue());
				sb.Append(r.Item2);
			}
			if (Self.Round.HasValue)
				sb.Append("/" + Self.Round.Value.GetStringValue());

			return sb.ToString();
		}

		private class Json : JsonConverter
		{
			public override bool CanConvert(Type objectType) => true;
			public override bool CanWrite => true;
			public override bool CanRead => true;

			public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
			{
				var v = value as DateMath;
				if (v == null) return;
				writer.WriteValue(v.ToString());
			}

			public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
			{
				if (reader.TokenType == JsonToken.String)
					return DateMath.FromString(reader.Value as string);
				if (reader.TokenType == JsonToken.Date)
				{
					var d = reader.Value as DateTime?;
					return d.HasValue ? DateMath.Anchored(d.Value) : null;
				}
				return null;
			}
		}
	}
}