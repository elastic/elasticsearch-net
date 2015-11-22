using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(TimeUnitExpressionJsonConverter))]
	public class TimeUnitExpression : IComparable<TimeUnitExpression>, IEquatable<TimeUnitExpression>
	{
		public double? Factor { get; }
		public TimeUnit? Interval { get; }
		public long Milliseconds { get; }

		private static long _year = (long)TimeSpan.FromDays(365).TotalMilliseconds;
		private static long _week = (long)TimeSpan.FromDays(7).TotalMilliseconds;
		private static long _day = (long)TimeSpan.FromDays(1).TotalMilliseconds;
		private static long _hour = (long)TimeSpan.FromHours(1).TotalMilliseconds;
		private static long _minute = (long)TimeSpan.FromMinutes(1).TotalMilliseconds;
		private static long _second = (long)TimeSpan.FromSeconds(1).TotalMilliseconds;

		public static implicit operator TimeUnitExpression(TimeSpan span) => new TimeUnitExpression(span);
		public static implicit operator TimeUnitExpression(long milliseconds) => new TimeUnitExpression(milliseconds);
		public static implicit operator TimeUnitExpression(string expression) => new TimeUnitExpression(expression);

		public TimeUnitExpression(double factor, TimeUnit interval)
		{
			this.Factor = factor;
			this.Interval = interval;

			if (interval == TimeUnit.Year)
				Milliseconds = (long)factor*_year;
			else if (interval == TimeUnit.Week)
				Milliseconds = (long)factor*_week;
			else if (interval == TimeUnit.Day)
				Milliseconds = (long)factor*_day;
			else if (interval == TimeUnit.Hour)
				Milliseconds = (long)factor*_hour;
			else if (interval == TimeUnit.Minute)
				Milliseconds = (long)factor*_minute;
			else if (interval == TimeUnit.Second)
				Milliseconds = (long)factor*_second;
			else //ms
				Milliseconds = (long)factor;
		}

		public TimeUnitExpression(TimeSpan timeSpan)
		{
			var ms = timeSpan.TotalMilliseconds;
			this.Milliseconds = (long)ms;

			if (ms >= _year)
			{
				Factor = ms/_year;
				Interval = TimeUnit.Year;
			}
			else if (ms >= _week)
			{
				Factor = ms/_week;
				Interval = TimeUnit.Week;
			}
			else if (ms >= _day)
			{
				Factor = ms/_day;
				Interval = TimeUnit.Day;
			}
			else if (ms >= _hour)
			{
				Factor = ms/_hour;
				Interval = TimeUnit.Hour;
			}
			else if (ms >= _minute)
			{
				Factor = ms/_minute;
				Interval = TimeUnit.Minute;
			}
			else if (ms >= _second)
			{
				Factor = ms/_second;
				Interval = TimeUnit.Second;
			}
			else
			{
				Factor = ms;
				Interval = TimeUnit.Millisecond;
			}
		}

		public TimeUnitExpression(long milliseconds)
		{
			this.Milliseconds = milliseconds;
		}
		
		private readonly Regex _expressionRegex = new Regex(@"^(?<factor>\d+(?:\.\d+)?)(?<interval>(?:y|m|w|d|h|m|s|ms))?$", RegexOptions.IgnoreCase); 

		public TimeUnitExpression(string unitExpression)
		{
			if (unitExpression.IsNullOrEmpty()) throw new ArgumentException("Time expression string is empty", nameof(unitExpression));
			var match = _expressionRegex.Match(unitExpression);
			if (!match.Success) throw new ArgumentException($"Time expression '{unitExpression}' string is invalid", nameof(unitExpression));

			this.Factor = double.Parse(match.Groups["factor"].Value, CultureInfo.InvariantCulture);
			this.Interval = match.Groups["interval"].Success 
				? match.Groups["interval"].Value.ToEnum<TimeUnit>() 
				: TimeUnit.Millisecond; 
			
			if (this.Interval == TimeUnit.Year)
				Milliseconds = (long)(this.Factor*_year);
			else if (this.Interval == TimeUnit.Week)
				Milliseconds = (long)(this.Factor*_week);
			else if (this.Interval == TimeUnit.Day)
				Milliseconds = (long)(this.Factor*_day);
			else if (this.Interval == TimeUnit.Hour)
				Milliseconds = (long)(this.Factor*_hour);
			else if (this.Interval == TimeUnit.Minute)
				Milliseconds = (long)(this.Factor*_minute);
			else if (this.Interval == TimeUnit.Second)
				Milliseconds = (long)(this.Factor*_second);
			else //ms
				Milliseconds = (long)this.Factor;
		}

		public TimeSpan ToTimeSpan() => TimeSpan.FromMilliseconds(this.Milliseconds);

		public int CompareTo(TimeUnitExpression other)
		{
			if (other == null) return 1;
			if (this.Milliseconds == other.Milliseconds) return 0;
			if (this.Milliseconds < other.Milliseconds) return -1;
			return 1;
		}

		public static bool operator <(TimeUnitExpression left, TimeUnitExpression right) => left.CompareTo(right) < 0;
		public static bool operator <=(TimeUnitExpression left, TimeUnitExpression right) => left.CompareTo(right) < 0 || left.Equals(right);

		public static bool operator >(TimeUnitExpression left, TimeUnitExpression right) => left.CompareTo(right) > 0;
		public static bool operator >=(TimeUnitExpression left, TimeUnitExpression right) => left.CompareTo(right) > 0 || left.Equals(right);

		public static bool operator ==(TimeUnitExpression left, TimeUnitExpression right) => left.Equals(right);

		public static bool operator !=(TimeUnitExpression left, TimeUnitExpression right) => !left.Equals(right);

		public override string ToString()
		{
			if (this.Factor == null) return this.Milliseconds.ToString(CultureInfo.InvariantCulture);
			return this.Factor.Value.ToString("0.##", CultureInfo.InvariantCulture) +
				   this.Interval.GetValueOrDefault().GetStringValue();
		}

		public bool Equals(TimeUnitExpression other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Milliseconds == other.Milliseconds;
		}
		
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((TimeUnitExpression) obj);
		}

		public override int GetHashCode() => this.Milliseconds.GetHashCode();

	}
}