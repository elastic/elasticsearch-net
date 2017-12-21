using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(TimeJsonConverter))]
	public class Time : IComparable<Time>, IEquatable<Time>
	{
		private const double MillisecondsInAYearApproximate = MillisecondsInADay * 365;
		private const double MillisecondsInAMonthApproximate = MillisecondsInADay * 30;
		private const double MillisecondsInAWeek = MillisecondsInADay * 7;
		private const double MillisecondsInADay = MillisecondsInAnHour * 24;
		private const double MillisecondsInAnHour = MillisecondsInAMinute * 60;
		private const double MillisecondsInAMinute = MillisecondsInASecond * 60;
		private const double MillisecondsInASecond = 1000;
		private const double MillisecondsInAMillisecond = 1;
		private const double MillisecondsInAMicrosecond = MillisecondsInAMillisecond / 1000;
		private const double MillisecondsInANanosecond = MillisecondsInAMicrosecond / 1000;
		private const double MicrosecondsInATick = 0.1; // 10 ticks = 1 microsecond
		private const double NanosecondsInATick = 100; // 1 tick = 100 nanoseconds

		private static double FLOAT_TOLERANCE = 1e-7; // less than 1 nanosecond

		private static readonly Regex ExpressionRegex =
			new Regex(@"^
				(?<factor>[+\-]? # open factor capture, allowing optional +- signs
					(?:(?#numeric)(?:\d+(?:\.\d*)?)|(?:\.\d+)) #a numeric in the forms: (N, N., .N, N.N)
					(?:(?#exponent)e[+\-]?\d+)? #an optional exponential scientific component, E also matches here (IgnoreCase)
				) # numeric and exponent fall under the factor capture
				\s{0,10} #optional spaces (sanity checked for max 10 repetitions)
				(?<interval>(?:y|w|d|h|m|s|ms|nanos|micros))? #optional interval indicator
				$",
				RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);

		private int? StaticTimeValue { get; }

		public double? Factor { get; private set; }

		public TimeUnit? Interval { get; private set; }

		public double? Milliseconds { get; private set; }

		private double ApproximateMilliseconds { get; set; }

		public static implicit operator Time(TimeSpan span) => new Time(span);

		public static implicit operator Time(double milliseconds)
		{
			if (Math.Abs(milliseconds - (-1)) < FLOAT_TOLERANCE) return MinusOne;
			if (Math.Abs(milliseconds) < FLOAT_TOLERANCE) return Zero;
			return new Time(milliseconds);
		}
		public static implicit operator Time(string expression) => new Time(expression);

		public static Time MinusOne { get; } = new Time(-1, true);
		public static Time Zero { get; } = new Time(0, true);

		private Time(int specialFactor, bool specialValue)
		{
			if (!specialValue) throw new ArgumentException("this constructor is only for static TimeValues");
			this.StaticTimeValue = specialFactor;
		}

		public Time(TimeSpan timeSpan)
			: this(timeSpan.TotalMilliseconds) { }

		public Time(double milliseconds)
		{
			if (Math.Abs(milliseconds - (-1)) < FLOAT_TOLERANCE) StaticTimeValue = -1;
			else if (Math.Abs(milliseconds) < FLOAT_TOLERANCE) StaticTimeValue = 0;
			else Reduce(milliseconds);
		}

		public Time(double factor, TimeUnit interval)
		{
			this.Factor = factor;
			this.Interval = interval;
			SetMilliseconds(this.Interval.Value, this.Factor.Value);
		}

		public Time(string timeUnit)
		{
			if (timeUnit.IsNullOrEmpty()) throw new ArgumentException("Time expression string is empty", nameof(timeUnit));
			if (timeUnit == "-1" || timeUnit == "0")
			{
				this.StaticTimeValue = int.Parse(timeUnit);
				return;
			}
			ParseExpression(timeUnit);
		}

		private void ParseExpression(string timeUnit)
		{
			var match = ExpressionRegex.Match(timeUnit);
			if (!match.Success) throw new ArgumentException($"Time expression '{timeUnit}' string is invalid", nameof(timeUnit));
			var factor = match.Groups["factor"].Value;
			if (!double.TryParse(factor, NumberStyles.Any ,CultureInfo.InvariantCulture, out double f))
				throw new ArgumentException($"Time expression '{timeUnit}' contains invalid factor: {factor}", nameof(timeUnit));

			this.Factor = f;
			var interval = match.Groups["interval"].Success ? match.Groups["interval"].Value : null;
			switch (interval)
			{
				case null:
					throw new ArgumentException($"Time expression '{timeUnit}' is missing an interval", nameof(timeUnit));
				case "M":
					this.Interval = TimeUnit.Month;
					break;
				case "m":
					this.Interval = TimeUnit.Minute;
					break;
				default:
					this.Interval = interval.ToEnum<TimeUnit>(StringComparison.OrdinalIgnoreCase);
					break;
			}

			if (!this.Interval.HasValue)
				throw new ArgumentException($"Time expression '{timeUnit}' can not be parsed to an interval", nameof(timeUnit));

			SetMilliseconds(this.Interval.Value, this.Factor.Value);
		}

		public int CompareTo(Time other)
		{
			if (other == null) return 1;
			if (this.StaticTimeValue.HasValue && !other.StaticTimeValue.HasValue) return -1;
			if (!this.StaticTimeValue.HasValue && other.StaticTimeValue.HasValue) return 1;
			if (this.StaticTimeValue.HasValue && other.StaticTimeValue.HasValue)
			{
				// ReSharper disable PossibleInvalidOperationException
				if (this.StaticTimeValue.Value == other.StaticTimeValue.Value) return 0;
				if (this.StaticTimeValue.Value < other.StaticTimeValue.Value) return -1;
				return 1;
				// ReSharper enable PossibleInvalidOperationException
			};

			if (Math.Abs(this.ApproximateMilliseconds - other.ApproximateMilliseconds) < FLOAT_TOLERANCE) return 0;
			if (this.ApproximateMilliseconds < other.ApproximateMilliseconds) return -1;
			return 1;
		}

		public static Time ToFirstUnitYieldingInteger(Time fractionalTime)
		{
			var fraction = fractionalTime.Factor.GetValueOrDefault(double.Epsilon);
			if (IsIntegerGreaterThanZero(fraction)) return fractionalTime;

			var ms = fractionalTime.ApproximateMilliseconds;
			if (ms > MillisecondsInADay)
			{
				fraction = ms / MillisecondsInADay;
				if (IsIntegerGreaterThanZero(fraction)) return new Time(fraction, TimeUnit.Day);
			}
			if (ms > MillisecondsInAnHour)
			{
				fraction = ms / MillisecondsInAnHour;
				if (IsIntegerGreaterThanZero(fraction)) return new Time(fraction, TimeUnit.Hour);
			}
			if (ms > MillisecondsInAMinute)
			{
				fraction = ms / MillisecondsInAMinute;
				if (IsIntegerGreaterThanZero(fraction)) return new Time(fraction, TimeUnit.Minute);
			}
			if (ms > MillisecondsInASecond)
			{
				fraction = ms / MillisecondsInASecond;
				if (IsIntegerGreaterThanZero(fraction)) return new Time(fraction, TimeUnit.Second);
			}
			if (IsIntegerGreaterThanZero(ms))
			{
				return new Time(ms, TimeUnit.Millisecond);
			}
			if (ms > MillisecondsInAMicrosecond)
			{
				fraction = ms * (1 / MillisecondsInAMicrosecond);
				if (IsIntegerGreaterThanZero(fraction)) return new Time(fraction, TimeUnit.Microseconds);
			}

			// when dealing with fractional nanoseconds, round to integer value
			return new Time(Math.Round(ms * (1 / MillisecondsInANanosecond), MidpointRounding.AwayFromZero), TimeUnit.Nanoseconds);
		}

		private static bool IsIntegerGreaterThanZero(double d) => Math.Abs(d % 1) < double.Epsilon;

		public static bool operator <(Time left, Time right) => left.CompareTo(right) < 0;
		public static bool operator <=(Time left, Time right) => left.CompareTo(right) < 0 || left.Equals(right);

		public static bool operator >(Time left, Time right) => left.CompareTo(right) > 0;
		public static bool operator >=(Time left, Time right) => left.CompareTo(right) > 0 || left.Equals(right);

		public static bool operator ==(Time left, Time right) =>
			ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.Equals(right);

		public static bool operator !=(Time left, Time right) => !(left == right);

		public TimeSpan ToTimeSpan()
		{
			if (this.StaticTimeValue.HasValue) throw new Exception("Static time values like -1 or 0 have no logical TimeSpan representation");
			//should not happen will throw in constructor
			if (!this.Interval.HasValue) throw new Exception("TimeUnit has no interval so you can not call ToTimeStamp on it");

			switch (this.Interval.Value)
			{
				case TimeUnit.Microseconds:
					var microTicks = (long)(this.Factor.Value / MicrosecondsInATick);
					return TimeSpan.FromTicks(microTicks);
				case TimeUnit.Nanoseconds:
					var nanoTicks = (long)(this.Factor.Value / NanosecondsInATick);
					return TimeSpan.FromTicks(nanoTicks);
				default:
					return TimeSpan.FromMilliseconds(this.ApproximateMilliseconds);
			}
		}

		public override string ToString()
		{
			if (this.StaticTimeValue.HasValue)
				return this.StaticTimeValue.Value.ToString();
			if (!this.Factor.HasValue)
				return "<bad Time object should not happen>";

			var mantissa = ExponentFormat(this.Factor.Value);
			var factor = this.Factor.Value.ToString("0." + mantissa, CultureInfo.InvariantCulture);
			return (this.Interval.HasValue) ? factor + this.Interval.Value.GetStringValue() : factor;
		}

		public bool Equals(Time other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			if (this.StaticTimeValue.HasValue && !other.StaticTimeValue.HasValue) return false;
			if (!this.StaticTimeValue.HasValue && other.StaticTimeValue.HasValue) return false;
			if (this.StaticTimeValue.HasValue && other.StaticTimeValue.HasValue)
				return this.StaticTimeValue == other.StaticTimeValue;
			return Math.Abs(this.ApproximateMilliseconds - other.ApproximateMilliseconds) < FLOAT_TOLERANCE;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((Time) obj);
		}

		public override int GetHashCode() => this.StaticTimeValue.HasValue
			? this.StaticTimeValue.Value.GetHashCode()
			: this.ApproximateMilliseconds.GetHashCode();

		private void SetMilliseconds(TimeUnit interval, double factor)
		{
			var exactMilliseconds = GetExactMilliseconds(interval, factor);
			this.Milliseconds = exactMilliseconds;
			this.ApproximateMilliseconds = GetApproximateMilliseconds(interval, factor, exactMilliseconds);
		}

		private double GetExactMilliseconds(TimeUnit interval, double factor)
		{
			switch (interval)
			{
				case TimeUnit.Week:
					return factor * MillisecondsInAWeek;
				case TimeUnit.Day:
					return factor * MillisecondsInADay;
				case TimeUnit.Hour:
					return factor * MillisecondsInAnHour;
				case TimeUnit.Minute:
					return factor * MillisecondsInAMinute;
				case TimeUnit.Second:
					return factor * MillisecondsInASecond;
				case TimeUnit.Microseconds:
					return factor / (1 / MillisecondsInAMicrosecond);
				case TimeUnit.Nanoseconds:
					return factor / (1 / MillisecondsInANanosecond);
				case TimeUnit.Year:
				case TimeUnit.Month:
					// Cannot calculate exact milliseconds for non-fixed intervals
					return -1;
				default: // ms
					return factor;
			}
		}

		private double GetApproximateMilliseconds(TimeUnit interval, double factor, double fallback)
		{
			switch (interval)
			{
				case TimeUnit.Year:
					return factor * MillisecondsInAYearApproximate;
				case TimeUnit.Month:
					return factor * MillisecondsInAMonthApproximate;
				default:
					return fallback;
			}
		}

		private void Reduce(double ms)
		{
			this.Milliseconds = ms;
			this.ApproximateMilliseconds = ms;
			double fraction;

			if (ms >= MillisecondsInADay)
			{
				fraction = ms / MillisecondsInADay;
				if (IsIntegerGreaterThanZero(fraction))
				{
					Factor = fraction;
					Interval = TimeUnit.Day;
					return;
				}
			}
			if (ms >= MillisecondsInAnHour)
			{
				fraction = ms / MillisecondsInAnHour;
				if (IsIntegerGreaterThanZero(fraction))
				{
					Factor = fraction;
					Interval = TimeUnit.Hour;
					return;
				}
			}
			if (ms >= MillisecondsInAMinute)
			{
				fraction = ms / MillisecondsInAMinute;
				if (IsIntegerGreaterThanZero(fraction))
				{
					Factor = fraction;
					Interval = TimeUnit.Minute;
					return;
				}
			}
			if (ms >= MillisecondsInASecond)
			{
				fraction = ms / MillisecondsInASecond;
				if (IsIntegerGreaterThanZero(fraction))
				{
					Factor = fraction;
					Interval = TimeUnit.Second;
					return;
				}
			}
			if (IsIntegerGreaterThanZero(ms))
			{
				Factor = ms;
				Interval = TimeUnit.Millisecond;
				return;
			}

			fraction = ms * (1 / MillisecondsInAMicrosecond);
			if (IsIntegerGreaterThanZero(fraction))
			{
				Factor = fraction;
				Interval = TimeUnit.Microseconds;
				return;
			}

			// expressed as fraction of nanoseconds
			Factor = ms * (1 / MillisecondsInANanosecond);
			Interval = TimeUnit.Nanoseconds;
		}

		private static string ExponentFormat(double d)
		{
			// Translate the double into sign, exponent and mantissa.
			var bits = BitConverter.DoubleToInt64Bits(d);
			// Note that the shift is sign-extended, hence the test against -1 not 1
			var exponent = (int) ((bits >> 52) & 0x7ffL);
			return new string('#', Math.Max(2, exponent));
		}

	}
}
