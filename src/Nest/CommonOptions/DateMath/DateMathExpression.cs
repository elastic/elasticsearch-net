using System;

namespace Nest
{
	public class DateMathExpression : DateMath
	{
		public DateMathExpression(string anchor) { this.Anchor = anchor; }
		public DateMathExpression(DateTime anchor) { this.Anchor = anchor; }

		public DateMathExpression(Union<DateTime, string> anchor, DateMathTime range, DateMathOperation operation)
		{
			anchor.ThrowIfNull(nameof(anchor));
			range.ThrowIfNull(nameof(range));
			operation.ThrowIfNull(nameof(operation));
			this.Anchor = anchor;
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
			this.Round = round;
			return this;
		}
	}
}
