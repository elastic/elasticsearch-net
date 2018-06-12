using System;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A time representation for use within <see cref="DateMath"/> expressions.
	/// </summary>
	public class DateMathTime : IComparable<DateMathTime>, IEquatable<DateMathTime>
	{
		private const int MonthsInAYear = 12;
		private const double MillisecondsInAYearApproximate = MillisecondsInADay * 365;
		private const double MillisecondsInAMonthApproximate = MillisecondsInAYearApproximate / MonthsInAYear;
		private const double MillisecondsInAWeek = MillisecondsInADay * 7;
		private const double MillisecondsInADay = MillisecondsInAnHour * 24;
		private const double MillisecondsInAnHour = MillisecondsInAMinute * 60;
		private const double MillisecondsInAMinute = MillisecondsInASecond * 60;
		private const double MillisecondsInASecond = 1000;

		private static readonly Regex ExpressionRegex =
			new Regex(@"^
				(?<factor>[+\-]? # open factor capture, allowing optional +- signs
					(?:(?#numeric)(?:\d+(?:\.\d*)?)|(?:\.\d+)) #a numeric in the forms: (N, N., .N, N.N)
					(?:(?#exponent)e[+\-]?\d+)? #an optional exponential scientific component, E also matches here (IgnoreCase)
				) # numeric and exponent fall under the factor capture
				\s{0,10} #optional spaces (sanity checked for max 10 repetitions)
				(?<interval>(?:y|w|d|h|m|s)) #interval indicator
				$",
				RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);

		private double _approximateSeconds;

		/// <summary>
		/// The numeric time factor
		/// </summary>
		public int Factor { get; private set; }

		/// <summary>
		/// The time units
		/// </summary>
		public DateMathTimeUnit Interval { get; private set; }

		public static implicit operator DateMathTime(TimeSpan span) => new DateMathTime(span);
		public static implicit operator DateMathTime(double milliseconds) => new DateMathTime(milliseconds);
		public static implicit operator DateMathTime(string expression) => new DateMathTime(expression);

		/// <summary>
		/// Instantiates a new instance of <see cref="DateMathTime"/> from a TimeSpan.
		/// Rounding can be specified to determine how fractional second values should be rounded.
		/// </summary>
		public DateMathTime(TimeSpan timeSpan, MidpointRounding rounding = MidpointRounding.AwayFromZero)
			: this(timeSpan.TotalMilliseconds, rounding) { }

		/// <summary>
		/// Instantiates a new instance of <see cref="DateMathTime"/> from a milliseconds value.
		/// Rounding can be specified to determine how fractional second values should be rounded.
		/// </summary>
		public DateMathTime(double milliseconds, MidpointRounding rounding = MidpointRounding.AwayFromZero) =>
			SetWholeFactorIntervalAndSeconds(milliseconds, rounding);

		/// <summary>
		/// Instantiates a new instance of <see cref="DateMathTime"/> from a factor and interval.
		/// </summary>
		public DateMathTime(int factor, DateMathTimeUnit interval) =>
			SetWholeFactorIntervalAndSeconds(factor, interval, MidpointRounding.AwayFromZero);

		/// <summary>
		/// Instantiates a new instance of <see cref="DateMathTime"/> from the timeUnit string expression.
		/// Rounding can be specified to determine how fractional second values should be rounded.
		/// </summary>
		public DateMathTime(string timeUnit, MidpointRounding rounding = MidpointRounding.AwayFromZero)
		{
			if (timeUnit == null) throw new ArgumentNullException(nameof(timeUnit));
			if (timeUnit.Length == 0) throw new ArgumentException("Expression string is empty", nameof(timeUnit));

			var match = ExpressionRegex.Match(timeUnit);
			if (!match.Success) throw new ArgumentException($"Expression '{timeUnit}' string is invalid", nameof(timeUnit));

			var factor = match.Groups["factor"].Value;
			if (!double.TryParse(factor, NumberStyles.Any ,CultureInfo.InvariantCulture, out var fraction))
				throw new ArgumentException($"Expression '{timeUnit}' contains invalid factor: {factor}", nameof(timeUnit));

			var intervalValue = match.Groups["interval"].Value;
			DateMathTimeUnit interval;

			switch (intervalValue)
			{
				case "M":
					interval= DateMathTimeUnit.Month;
					break;
				case "m":
					interval = DateMathTimeUnit.Minute;
					break;
				default:
					interval = intervalValue.ToEnum<DateMathTimeUnit>().Value;
					break;
			}

			SetWholeFactorIntervalAndSeconds(fraction, interval, rounding);
		}

		private void SetWholeFactorIntervalAndSeconds(double factor, DateMathTimeUnit interval, MidpointRounding rounding)
		{
			var fraction = factor;
			double milliseconds;

			// if the factor is already a whole number then use it
			if (TryGetIntegerGreaterThanZero(fraction, out var whole))
			{
				this.Factor = whole;
				this.Interval = interval;
				switch (interval)
				{
					case DateMathTimeUnit.Second:
						this._approximateSeconds = whole;
						break;
					case DateMathTimeUnit.Minute:
						this._approximateSeconds = whole * (MillisecondsInAMinute / MillisecondsInASecond);
						break;
					case DateMathTimeUnit.Hour:
						this._approximateSeconds = whole * (MillisecondsInAnHour / MillisecondsInASecond);
						break;
					case DateMathTimeUnit.Day:
						this._approximateSeconds = whole * (MillisecondsInADay / MillisecondsInASecond);
						break;
					case DateMathTimeUnit.Week:
						this._approximateSeconds = whole * (MillisecondsInAWeek / MillisecondsInASecond);
						break;
					case DateMathTimeUnit.Month:
						this._approximateSeconds = whole * (MillisecondsInAMonthApproximate / MillisecondsInASecond);
						break;
					case DateMathTimeUnit.Year:
						this._approximateSeconds = whole * (MillisecondsInAYearApproximate / MillisecondsInASecond);
						break;
					default:
						throw new ArgumentOutOfRangeException(nameof(interval), interval, null);
				}
				return;
			}

			switch (interval)
			{
				case DateMathTimeUnit.Second:
					milliseconds = factor * MillisecondsInASecond;
					break;
				case DateMathTimeUnit.Minute:
					milliseconds = factor * MillisecondsInAMinute;
					break;
				case DateMathTimeUnit.Hour:
					milliseconds = factor * MillisecondsInAnHour;
					break;
				case DateMathTimeUnit.Day:
					milliseconds = factor * MillisecondsInADay;
					break;
				case DateMathTimeUnit.Week:
					milliseconds = factor * MillisecondsInAWeek;
					break;
				case DateMathTimeUnit.Month:
					if (TryGetIntegerGreaterThanZero(fraction, out whole))
					{
						this.Factor = whole;
						this.Interval = interval;
						this._approximateSeconds = whole * (MillisecondsInAMonthApproximate / MillisecondsInASecond);
						return;
					}

					milliseconds = factor * MillisecondsInAMonthApproximate;
					break;
				case DateMathTimeUnit.Year:
					if (TryGetIntegerGreaterThanZero(fraction, out whole))
					{
						this.Factor = whole;
						this.Interval = interval;
						this._approximateSeconds = whole * (MillisecondsInAYearApproximate / MillisecondsInASecond);
						return;
					}

					fraction = fraction * MonthsInAYear;
					if (TryGetIntegerGreaterThanZero(fraction, out whole))
					{
						this.Factor = whole;
						this.Interval = DateMathTimeUnit.Month;
						this._approximateSeconds = whole * (MillisecondsInAMonthApproximate / MillisecondsInASecond);
						return;
					}
					milliseconds = factor * MillisecondsInAYearApproximate;
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(interval), interval, null);
			}

			SetWholeFactorIntervalAndSeconds(milliseconds, rounding);
		}

		private void SetWholeFactorIntervalAndSeconds(double milliseconds, MidpointRounding rounding)
		{
			double fraction;
			int whole;

			if (milliseconds >= MillisecondsInAWeek)
			{
				fraction = milliseconds / MillisecondsInAWeek;
				if (TryGetIntegerGreaterThanZero(fraction, out whole))
				{
					this.Factor = whole;
					this.Interval = DateMathTimeUnit.Week;
					this._approximateSeconds = Factor * (MillisecondsInAWeek / MillisecondsInASecond);
					return;
				}
			}
			if (milliseconds >= MillisecondsInADay)
			{
				fraction = milliseconds / MillisecondsInADay;
				if (TryGetIntegerGreaterThanZero(fraction, out whole))
				{
					this.Factor = whole;
					this.Interval = DateMathTimeUnit.Day;
					this._approximateSeconds = Factor * (MillisecondsInADay / MillisecondsInASecond);
					return;
				}
			}
			if (milliseconds >= MillisecondsInAnHour)
			{
				fraction = milliseconds / MillisecondsInAnHour;
				if (TryGetIntegerGreaterThanZero(fraction, out whole))
				{
					this.Factor = whole;
					this.Interval = DateMathTimeUnit.Hour;
					this._approximateSeconds = Factor * (MillisecondsInAnHour / MillisecondsInASecond);
					return;
				}
			}
			if (milliseconds >= MillisecondsInAMinute)
			{
				fraction = milliseconds / MillisecondsInAMinute;
				if (TryGetIntegerGreaterThanZero(fraction, out whole))
				{
					this.Factor = whole;
					this.Interval = DateMathTimeUnit.Minute;
					this._approximateSeconds = Factor * (MillisecondsInAMinute / MillisecondsInASecond);
					return;
				}
			}
			if (milliseconds >= MillisecondsInASecond)
			{
				fraction = milliseconds / MillisecondsInASecond;
				if (TryGetIntegerGreaterThanZero(fraction, out whole))
				{
					this.Factor = whole;
					this.Interval = DateMathTimeUnit.Second;
					this._approximateSeconds = Factor;
					return;
				}
			}

			// round to nearest second, using specified rounding
			this.Factor = Convert.ToInt32(Math.Round(milliseconds / MillisecondsInASecond, rounding));
			this.Interval = DateMathTimeUnit.Second;
			this._approximateSeconds = Factor;
		}

		public int CompareTo(DateMathTime other)
		{
			if (other == null) return 1;
			if (this._approximateSeconds == other._approximateSeconds) return 0;
			if (this._approximateSeconds < other._approximateSeconds) return -1;
			return 1;
		}

		private static bool TryGetIntegerGreaterThanZero(double d, out int value)
		{
			if (Math.Abs(d % 1) < double.Epsilon)
			{
				value = Convert.ToInt32(d);
				return true;
			}

			value = 0;
			return false;
		}

		public static bool operator <(DateMathTime left, DateMathTime right) => left.CompareTo(right) < 0;
		public static bool operator <=(DateMathTime left, DateMathTime right) => left.CompareTo(right) < 0 || left.Equals(right);

		public static bool operator >(DateMathTime left, DateMathTime right) => left.CompareTo(right) > 0;
		public static bool operator >=(DateMathTime left, DateMathTime right) => left.CompareTo(right) > 0 || left.Equals(right);

		public static bool operator ==(DateMathTime left, DateMathTime right) =>
			ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.Equals(right);

		public static bool operator !=(DateMathTime left, DateMathTime right) => !(left == right);

		public override string ToString() => this.Factor + this.Interval.GetStringValue();

		public bool Equals(DateMathTime other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return this._approximateSeconds == other._approximateSeconds;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((DateMathTime) obj);
		}

		public override int GetHashCode() => this._approximateSeconds.GetHashCode();
	}
}
