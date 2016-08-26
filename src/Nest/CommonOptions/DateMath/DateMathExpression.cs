using System;

namespace Nest
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
			Self.Ranges.Add(Tuple.Create(DateMathOperation.Add, Floor(expression)));
			return this;
		}

		public DateMathExpression Subtract(Time expression)
		{
			Self.Ranges.Add(Tuple.Create(DateMathOperation.Subtract, Floor(expression)));
			return this;
		}

		public DateMathExpression Operation(Time expression, DateMathOperation operation)
		{
			Self.Ranges.Add(Tuple.Create(operation, Floor(expression)));
			return this;
		}

		public DateMath RoundTo(TimeUnit round)
		{
			this.Round = round;
			return this;
		}

		/// Time may result in a fractional factor, which date math expressions do not support.
		/// Thus we need take the floor of the current factor, for instance: 1.04d => 1d
		private Time Floor(Time expression) => new Time(Math.Floor(expression.Factor.Value), expression.Interval.Value);
	}
}
