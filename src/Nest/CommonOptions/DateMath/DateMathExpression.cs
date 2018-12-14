using System;
using Utf8Json;
using Utf8Json.Formatters;

namespace Nest
{
	[JsonFormatter(typeof(DateMathExpressionFormatter))]
	public class DateMathExpression : DateMath
	{
		public DateMathExpression(string anchor) => Anchor = anchor;

		public DateMathExpression(DateTime anchor) => Anchor = anchor;

		public DateMathExpression(Union<DateTime, string> anchor, DateMathTime range, DateMathOperation operation)
		{
			anchor.ThrowIfNull(nameof(anchor));
			range.ThrowIfNull(nameof(range));
			operation.ThrowIfNull(nameof(operation));
			Anchor = anchor;
			Self.Ranges.Add(Tuple.Create(operation, range));
		}

		public DateMathExpression Add(DateMathTime expression)
		{
			Self.Ranges.Add(Tuple.Create(DateMathOperation.Add, expression));
			return this;
		}

		public DateMathExpression Subtract(DateMathTime expression)
		{
			Self.Ranges.Add(Tuple.Create(DateMathOperation.Subtract, expression));
			return this;
		}

		public DateMathExpression Operation(DateMathTime expression, DateMathOperation operation)
		{
			Self.Ranges.Add(Tuple.Create(operation, expression));
			return this;
		}

		public DateMath RoundTo(DateMathTimeUnit round)
		{
			Round = round;
			return this;
		}
	}

	internal class DateMathExpressionFormatter : IJsonFormatter<DateMathExpression>
	{
		public DateMathExpression Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();
			if (token != JsonToken.String)
				return null;

			var segment = reader.ReadStringSegmentUnsafe();
			var segmentReader = new JsonReader(segment.Array, segment.Offset - 1); // include opening "

			try
			{
				// TODO: possibly nicer way of doing this than brute try?
				var dateTime = ISO8601DateTimeFormatter.Default.Deserialize(ref segmentReader, formatterResolver);
				return new DateMathExpression(dateTime);
			}
			catch (InvalidOperationException)
			{
				var value = segment.Utf8String();
				return new DateMathExpression(value);
			}
		}

		public void Serialize(ref JsonWriter writer, DateMathExpression value, IJsonFormatterResolver formatterResolver) =>
			writer.WriteString(value.ToString());
	}
}
