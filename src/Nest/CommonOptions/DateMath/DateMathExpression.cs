// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Nest.Utf8Json;

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

			if (!segment.ContainsDateMathSeparator() && segment.IsDateTime(formatterResolver, out var dateTime))
				return new DateMathExpression(dateTime);

			var value = segment.Utf8String();
			return new DateMathExpression(value);
		}

		public void Serialize(ref JsonWriter writer, DateMathExpression value, IJsonFormatterResolver formatterResolver) =>
			writer.WriteString(value.ToString());
	}
}
