using System;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using Elasticsearch.Net;
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
		private const double NanosecondsInAMillisecond = 100;
		private const double MicrosecondsInAMillisecond = 10;

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

		private static double FLOAT_TOLERANCE = 0.0000001;

		public double? Factor { get; private set; }
		public TimeUnit? Interval { get; private set; }

		// TODO make nullable in 3.0
		public double Milliseconds { get; private set; }
		private double ApproximateMilliseconds { get; set; }

		public static implicit operator Time(TimeSpan span) => new Time(span);
		public static implicit operator Time(double milliseconds) => new Time(milliseconds);
		public static implicit operator Time(string expression) => new Time(expression);

		public Time(TimeSpan timeSpan)
		{
			Reduce(timeSpan.TotalMilliseconds);
		}

		public Time(double milliseconds)
		{
			Reduce(milliseconds);
		}

		public Time(double factor, TimeUnit interval)
		{
			this.Factor = factor;
			this.Interval = interval;
			SetMilliseconds(this.Interval, this.Factor.Value);
		}

		public Time(string timeUnit)
		{
			if (timeUnit.IsNullOrEmpty()) throw new ArgumentException("Time expression string is empty", nameof(timeUnit));
			var match = ExpressionRegex.Match(timeUnit);
			if (!match.Success)
				throw new ArgumentException($"Time expression '{timeUnit}' string is invalid", nameof(timeUnit));
			var factor = match.Groups["factor"].Value;
			if (!double.TryParse(factor, NumberStyles.Any ,CultureInfo.InvariantCulture, out double f))
				throw new ArgumentException($"Time expression '{timeUnit}' contains invalid factor: {factor}", nameof(timeUnit));

			this.Factor = f;



			if (this.Factor > 0)
			{
                var interval = match.Groups["interval"].Success ? match.Groups["interval"].Value : null;
                switch (interval)
                {
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
			}

			SetMilliseconds(this.Interval, this.Factor.Value);
		}

		public int CompareTo(Time other)
		{
			if (other == null) return 1;
			if (Math.Abs(this.ApproximateMilliseconds - other.ApproximateMilliseconds) < FLOAT_TOLERANCE) return 0;
			if (this.ApproximateMilliseconds < other.ApproximateMilliseconds) return -1;
			return 1;
		}

		public static Time ToFirstUnitYieldingInteger(Time fractionalTime)
		{

			var fraction = fractionalTime.Factor.GetValueOrDefault(double.Epsilon);
			if (IsIntegerGreaterThanZero(fraction)) return fractionalTime;

			var ms = fractionalTime.ApproximateMilliseconds;
			if (ms > MillisecondsInAWeek)
			{
				fraction = ms / MillisecondsInAWeek;
				if (IsIntegerGreaterThanZero(fraction)) return new Time(fraction, TimeUnit.Week);
			}
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
			return new Time(ms, TimeUnit.Millisecond);
		}

		private static bool IsIntegerGreaterThanZero(double d) => Math.Abs(d % 1) < double.Epsilon;


		public static bool operator <(Time left, Time right) => left.CompareTo(right) < 0;
		public static bool operator <=(Time left, Time right) => left.CompareTo(right) < 0 || left.Equals(right);

		public static bool operator >(Time left, Time right) => left.CompareTo(right) > 0;
		public static bool operator >=(Time left, Time right) => left.CompareTo(right) > 0 || left.Equals(right);

		public static bool operator ==(Time left, Time right) =>
			ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.Equals(right);

		public static bool operator !=(Time left, Time right) => !(left == right);

		public TimeSpan ToTimeSpan() => TimeSpan.FromMilliseconds(this.ApproximateMilliseconds);

		public override string ToString()
		{

			var mantissa = ExponentFormat(this.Factor.Value);
			var factor = this.Factor.Value.ToString("0." + mantissa, CultureInfo.InvariantCulture);
			return (this.Interval.HasValue) ? factor + this.Interval.Value.GetStringValue() : factor;
		}

		public bool Equals(Time other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return this.ApproximateMilliseconds == other.ApproximateMilliseconds;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((Time)obj);
		}

		public override int GetHashCode() => this.ApproximateMilliseconds.GetHashCode();

		private void SetMilliseconds(TimeUnit? interval, double factor)
		{
			this.Milliseconds = interval.HasValue ? GetExactMilliseconds(interval.Value, factor) : factor;
			this.ApproximateMilliseconds = interval.HasValue ? GetApproximateMilliseconds(interval.Value, factor) : factor;
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
				case TimeUnit.Year:
				case TimeUnit.Month:
					// Cannot calculate exact milliseconds for non-fixed intervals
					return -1;
				default: // ms
					return factor;
			}
		}

		private double GetApproximateMilliseconds(TimeUnit interval, double factor)
		{
			switch (interval)
			{
				case TimeUnit.Year:
					return factor * MillisecondsInAYearApproximate;
				case TimeUnit.Month:
					return factor * MillisecondsInAMonthApproximate;
				default:
					return GetExactMilliseconds(interval, factor);
			}
		}

		private void Reduce(double ms)
		{
			this.Milliseconds = ms;
			this.ApproximateMilliseconds = ms;
			if (ms >= MillisecondsInAWeek)
			{
				Factor = ms / MillisecondsInAWeek;
				Interval = TimeUnit.Week;
			}
			else if (ms >= MillisecondsInADay)
			{
				Factor = ms / MillisecondsInADay;
				Interval = TimeUnit.Day;
			}
			else if (ms >= MillisecondsInAnHour)
			{
				Factor = ms / MillisecondsInAnHour;
				Interval = TimeUnit.Hour;
			}
			else if (ms >= MillisecondsInAMinute)
			{
				Factor = ms / MillisecondsInAMinute;
				Interval = TimeUnit.Minute;
			}
			else if (ms >= MillisecondsInASecond)
			{
				Factor = ms / MillisecondsInASecond;
				Interval = TimeUnit.Second;
			}
			else
			{
				Factor = ms;
				// If milliseconds is <= 0 then don't set an interval.
				// This is used when setting things like index.refresh_interval = -1 (the only case where a unit isn't required)
				Interval = (ms > 0) ? (TimeUnit?)TimeUnit.Millisecond : null;
			}
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
