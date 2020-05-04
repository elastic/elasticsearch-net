// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// Represents a time value
	/// </summary>
	[JsonFormatter(typeof(TimeFormatter))]
	public class Time : IComparable<Time>, IEquatable<Time>, IUrlParameter
	{
		private const double MicrosecondsInATick = 0.1; // 10 ticks = 1 microsecond
		private const double MillisecondsInADay = MillisecondsInAnHour * 24;
		private const double MillisecondsInAMicrosecond = MillisecondsInAMillisecond / 1000;
		private const double MillisecondsInAMillisecond = 1;
		private const double MillisecondsInAMinute = MillisecondsInASecond * 60;
		private const double MillisecondsInANanosecond = MillisecondsInAMicrosecond / 1000;
		private const double MillisecondsInAnHour = MillisecondsInAMinute * 60;
		private const double MillisecondsInASecond = 1000;
		private const double NanosecondsInATick = 100; // 1 tick = 100 nanoseconds

		private static readonly Regex ExpressionRegex =
			new Regex(@"^
				(?<factor>[+\-]? # open factor capture, allowing optional +- signs
					(?:(?#numeric)(?:\d+(?:\.\d*)?)|(?:\.\d+)) #a numeric in the forms: (N, N., .N, N.N)
					(?:(?#exponent)e[+\-]?\d+)? #an optional exponential scientific component, E also matches here (IgnoreCase)
				) # numeric and exponent fall under the factor capture
				\s{0,10} #optional spaces (sanity checked for max 10 repetitions)
				(?<interval>(?:d|h|m|s|ms|nanos|micros))? #optional interval indicator
				$",
				RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);

		private static readonly double FLOAT_TOLERANCE = 1e-7; // less than 1 nanosecond

		private Time(int specialFactor, bool specialValue)
		{
			if (!specialValue) throw new ArgumentException("this constructor is only for static TimeValues");

			StaticTimeValue = specialFactor;
		}

		public Time(TimeSpan timeSpan)
			: this(timeSpan.TotalMilliseconds) { }

		public Time(double milliseconds)
		{
			if (Math.Abs(milliseconds - -1) < FLOAT_TOLERANCE) StaticTimeValue = -1;
			else if (Math.Abs(milliseconds) < FLOAT_TOLERANCE) StaticTimeValue = 0;
			else Reduce(milliseconds);
		}

		public Time(double factor, TimeUnit interval)
		{
			Factor = factor;
			Interval = interval;
			Milliseconds = GetExactMilliseconds(Factor.Value, Interval.Value);
		}

		public Time(string timeUnit)
		{
			if (timeUnit.IsNullOrEmpty()) throw new ArgumentException("Expression string is empty", nameof(timeUnit));

			if (timeUnit == "-1" || timeUnit == "0") StaticTimeValue = int.Parse(timeUnit);
			else ParseExpression(timeUnit);
		}

		public double? Factor { get; private set; }

		public TimeUnit? Interval { get; private set; }

		public double? Milliseconds { get; private set; }

		public static Time MinusOne { get; } = new Time(-1, true);

		public static Time Zero { get; } = new Time(0, true);

		private int? StaticTimeValue { get; }

		public static implicit operator Time(TimeSpan span) => new Time(span);

		public static implicit operator Time(double milliseconds) => new Time(milliseconds);

		public static implicit operator Time(string expression) => new Time(expression);

		private void ParseExpression(string timeUnit)
		{
			var match = ExpressionRegex.Match(timeUnit);
			if (!match.Success) throw new ArgumentException($"Expression '{timeUnit}' is invalid", nameof(timeUnit));

			var factor = match.Groups["factor"].Value;
			if (!double.TryParse(factor, NumberStyles.Any, CultureInfo.InvariantCulture, out var f))
				throw new ArgumentException($"Expression '{timeUnit}' contains invalid factor: {factor}", nameof(timeUnit));

			Factor = f;
			var interval = match.Groups["interval"].Success ? match.Groups["interval"].Value : null;
			Interval = interval.ToEnum<TimeUnit>();
			if (!Interval.HasValue)
				throw new ArgumentException($"Expression '{interval}' cannot be parsed to an interval", nameof(timeUnit));

			Milliseconds = GetExactMilliseconds(Factor.Value, Interval.Value);
		}

		public int CompareTo(Time other)
		{
			if (other == null) return 1;
			if (StaticTimeValue.HasValue && !other.StaticTimeValue.HasValue) return -1;
			if (!StaticTimeValue.HasValue && other.StaticTimeValue.HasValue) return 1;

			if (StaticTimeValue.HasValue && other.StaticTimeValue.HasValue)
			{
				if (StaticTimeValue.Value == other.StaticTimeValue.Value) return 0;
				if (StaticTimeValue.Value < other.StaticTimeValue.Value) return -1;
				return 1;
			}

			if (Milliseconds == null && other.Milliseconds == null) return 0;
			if (Milliseconds != null && other.Milliseconds == null) return 1;
			if (Milliseconds == null || other.Milliseconds == null) return 1;

			if (Math.Abs(Milliseconds.Value - other.Milliseconds.Value) < FLOAT_TOLERANCE) return 0;
			if (other.Milliseconds != null && Milliseconds < other.Milliseconds.Value) return -1;

			return 1;
		}

		private static bool IsIntegerGreaterThanZero(double d) => Math.Abs(d % 1) < double.Epsilon;

		public static bool operator <(Time left, Time right) => left.CompareTo(right) < 0;

		public static bool operator <=(Time left, Time right) => left.CompareTo(right) < 0 || left.Equals(right);

		public static bool operator >(Time left, Time right) => left.CompareTo(right) > 0;

		public static bool operator >=(Time left, Time right) => left.CompareTo(right) > 0 || left.Equals(right);

		public static bool operator ==(Time left, Time right) =>
			ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.Equals(right);

		public static bool operator !=(Time left, Time right) => !(left == right);

		/// <summary>
		/// Converts this instance of <see cref="Time" /> to an instance of <see cref="TimeSpan" />.
		/// For values in <see cref="TimeUnit.Microseconds" /> and <see cref="TimeUnit.Nanoseconds" />, value will be rounded to
		/// the nearest Tick.
		/// All other values will be rounded to the nearest Millisecond.
		/// </summary>
		/// <exception cref="InvalidOperationException">
		/// <para>
		/// special time values <see cref="MinusOne" /> and <see cref="Zero" /> do not have a <see cref="TimeSpan" />
		/// representation.
		/// </para>
		/// <para>instance of <see cref="Time" /> has no value for <see cref="Interval" /></para>
		/// </exception>
		public TimeSpan ToTimeSpan()
		{
			if (StaticTimeValue.HasValue)
				throw new InvalidOperationException("Static time values like -1 or 0 have no logical TimeSpan representation");
			if (!Interval.HasValue)
				throw new InvalidOperationException($"Time has no value for Interval so you can not call {nameof(ToTimeSpan)} on it");

			switch (Interval.Value)
			{
				case TimeUnit.Microseconds:
					if (!Factor.HasValue) 
						throw new InvalidOperationException("Time is in microseconds but factor has no value, this is a bug please report!");
					return TimeSpan.FromTicks((long)(Factor.Value / MicrosecondsInATick));
				case TimeUnit.Nanoseconds:
					if (!Factor.HasValue) 
						throw new InvalidOperationException("Time is in nanoseconds but factor has no value, this is a bug please report!");
					return TimeSpan.FromTicks((long)(Factor.Value / NanosecondsInATick));
				default:
					if (!Milliseconds.HasValue) 
						throw new InvalidOperationException("Milliseconds is null so we have nothing to create a TimeSpan from, this is a bug please report!");
					return TimeSpan.FromMilliseconds(Milliseconds.Value);
			}
		}

		public override string ToString()
		{
			if (StaticTimeValue.HasValue)
				return StaticTimeValue.Value.ToString();
			if (!Factor.HasValue)
				return "<bad Time object should not happen>";

			var mantissa = ExponentFormat(Factor.Value);
			var factor = Factor.Value.ToString("0." + mantissa, CultureInfo.InvariantCulture);
			return Interval.HasValue ? factor + Interval.Value.GetStringValue() : factor;
		}

		string IUrlParameter.GetString(IConnectionConfigurationValues settings)
		{
			if (this == MinusOne) return "-1";
			if (this == Zero) return "0";
			if (Factor.HasValue && Interval.HasValue) return ToString();

			return Milliseconds.ToString();
		}

		public bool Equals(Time other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			if (StaticTimeValue.HasValue && !other.StaticTimeValue.HasValue) return false;
			if (!StaticTimeValue.HasValue && other.StaticTimeValue.HasValue) return false;
			if (StaticTimeValue.HasValue && other.StaticTimeValue.HasValue)
				return StaticTimeValue == other.StaticTimeValue;
			
			
			if (Milliseconds == null && other.Milliseconds == null) return true;
			if (Milliseconds == null || other.Milliseconds == null) return false;
			return Math.Abs(Milliseconds.Value - other.Milliseconds.Value) < FLOAT_TOLERANCE;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != GetType()) return false;

			return Equals((Time)obj);
		}

		public override int GetHashCode() => StaticTimeValue.HasValue
			? StaticTimeValue.Value.GetHashCode()
			// ReSharper disable once NonReadonlyMemberInGetHashCode
			: Milliseconds.GetHashCode();

		private void Reduce(double ms)
		{
			Milliseconds = ms;
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

			fraction = ms / MillisecondsInAMicrosecond;
			if (IsIntegerGreaterThanZero(fraction))
			{
				Factor = fraction;
				Interval = TimeUnit.Microseconds;
				return;
			}

			// expressed as fraction of nanoseconds
			Factor = ms / MillisecondsInANanosecond;
			Interval = TimeUnit.Nanoseconds;
		}

		private static double GetExactMilliseconds(double factor, TimeUnit interval)
		{
			switch (interval)
			{
				case TimeUnit.Day:
					return factor * MillisecondsInADay;
				case TimeUnit.Hour:
					return factor * MillisecondsInAnHour;
				case TimeUnit.Minute:
					return factor * MillisecondsInAMinute;
				case TimeUnit.Second:
					return factor * MillisecondsInASecond;
				case TimeUnit.Millisecond:
					return factor;
				case TimeUnit.Microseconds:
					return factor * MillisecondsInAMicrosecond;
				case TimeUnit.Nanoseconds:
					return factor * MillisecondsInANanosecond;
				default:
					throw new ArgumentOutOfRangeException(nameof(interval), interval, null);
			}
		}

		private static string ExponentFormat(double d)
		{
			// Translate the double into sign, exponent and mantissa.
			var bits = BitConverter.DoubleToInt64Bits(d);
			// Note that the shift is sign-extended, hence the test against -1 not 1
			var exponent = (int)((bits >> 52) & 0x7ffL);
			return new string('#', Math.Max(2, exponent));
		}
	}
}
