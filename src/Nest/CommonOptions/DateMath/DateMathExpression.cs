using System;

namespace Nest_5_2_0
{
	public class DateMathExpression : DateMath
	{
		public DateMathExpression(string anchor) { this.Anchor = anchor; }
		public DateMathExpression(DateTime anchor) { this.Anchor = anchor; }

		public DateMathExpression(Union<DateTime, string> anchor, Time range, DateMathOperation operation)
		{
			anchor.ThrowIfNull(nameof(anchor));
			range.ThrowIfNull(nameof(range));
			operation.ThrowIfNull(nameof(operation));
			this.Anchor = anchor;
			Self.Ranges.Add(Tuple.Create(operation, range));
		}

		public DateMathExpression Add(Time expression)
		{
			Self.Ranges.Add(Tuple.Create(DateMathOperation.Add, expression));
			return this;
		}

		public DateMathExpression Subtract(Time expression)
		{
			Self.Ranges.Add(Tuple.Create(DateMathOperation.Subtract, expression));
			return this;
		}

		public DateMathExpression Operation(Time expression, DateMathOperation operation)
		{
			Self.Ranges.Add(Tuple.Create(operation, expression));
			return this;
		}

		public DateMath RoundTo(TimeUnit round)
		{
			this.Round = round;
			return this;
		}
	}
}