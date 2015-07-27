using System;

namespace Nest
{
	public class DateMathExpression : DateMath
	{
		public DateMathExpression(string anchor) { this._anchor = anchor; }
		public DateMathExpression(DateTime anchor) { this._anchor = anchor; }

		public DateMathExpression(Union<DateTime, string> anchor, TimeUnitExpression range, DateMathOperation operation)
		{
			anchor.ThrowIfNull(nameof(anchor));
			range.ThrowIfNull(nameof(range));
			operation.ThrowIfNull(nameof(operation));
			this._anchor = anchor;
			Self.Ranges.Add(Tuple.Create(operation, range));
		}

		public DateMathExpression Add(TimeUnitExpression expression)
		{
			Self.Ranges.Add(Tuple.Create(DateMathOperation.Add, expression));
			return this;
		}

		public DateMathExpression Subtract(TimeUnitExpression expression)
		{
			Self.Ranges.Add(Tuple.Create(DateMathOperation.Subtract, expression));
			return this;
		}

		public DateMathExpression Operation(TimeUnitExpression expression, DateMathOperation operation)
		{
			Self.Ranges.Add(Tuple.Create(operation, expression));
			return this;
		}

		public DateMath RoundTo(TimeUnit round)
		{
			this._round = round;
			return this;
		}
	}
}