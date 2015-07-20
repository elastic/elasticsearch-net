using System;
using System.Text.RegularExpressions;

namespace Nest
{
	public class TimeUnitExpression
	{
		private readonly double? _factor;
		private readonly TimeUnit? _interval;
		private readonly long _milliseconds;

		private static long _year = TimeSpan.FromDays(365).Milliseconds;
		private static long _week = TimeSpan.FromDays(7).Milliseconds;
		private static long _day = TimeSpan.FromDays(1).Milliseconds;
		private static long _hour = TimeSpan.FromHours(1).Milliseconds;
		private static long _minute = TimeSpan.FromMinutes(1).Milliseconds;
		private static long _second = TimeSpan.FromSeconds(1).Milliseconds;

		public TimeUnitExpression(double factor, TimeUnit interval)
		{
			this._factor = factor;
			this._interval = interval;

			if (interval == TimeUnit.Year)
				_milliseconds = (long)factor*_year;
			else if (interval == TimeUnit.Week)
				_milliseconds = (long)factor*_week;
			else if (interval == TimeUnit.Day)
				_milliseconds = (long)factor*_day;
			else if (interval == TimeUnit.Hour)
				_milliseconds = (long)factor*_hour;
			else if (interval == TimeUnit.Minute)
				_milliseconds = (long)factor*_minute;
			else if (interval == TimeUnit.Second)
				_milliseconds = (long)factor*_second;
			else //ms
				_milliseconds = (long)factor;
		}

		public TimeUnitExpression(TimeSpan timeSpan)
		{
			var ms = timeSpan.Milliseconds;
			this._milliseconds = ms;

			if (ms >= _year)
			{
				_factor = ms/_year;
				_interval = TimeUnit.Year;
			}
			else if (ms >= _week)
			{
				_factor = ms/_week;
				_interval = TimeUnit.Week;
			}
			else if (ms >= _day)
			{
				_factor = ms/_day;
				_interval = TimeUnit.Day;
			}
			else if (ms >= _hour)
			{
				_factor = ms/_hour;
				_interval = TimeUnit.Hour;
			}
			else if (ms >= _minute)
			{
				_factor = ms/_minute;
				_interval = TimeUnit.Minute;
			}
			else if (ms >= _second)
			{
				_factor = ms/_second;
				_interval = TimeUnit.Second;
			}
			else
			{
				_factor = ms;
				_interval = TimeUnit.Millisecond;
			}

		}

		public TimeUnitExpression(long milliseconds)
		{
			this._milliseconds = milliseconds;
		}
		
		private readonly Regex _expressionRegex = new Regex(@"^(?<factor>\d+(?:\.\d+))?(?<interval>y|m|w|d|h|m|s|ms)$", RegexOptions.IgnoreCase); 

		public TimeUnitExpression(string unitExpression)
		{
			if (unitExpression.IsNullOrEmpty()) throw new ArgumentException("Time expression string is empty", nameof(unitExpression));
			var match = _expressionRegex.Match(unitExpression);

			this._factor = double.Parse(match.Groups["factor"].Value);
			if (match.Groups["interval"].Success)
			{
				this._interval = (TimeUnit) Enum.Parse(typeof (TimeUnit), match.Groups["interval"].Value);
			}
			else this._interval = TimeUnit.Millisecond; 
			
			if (this._interval == TimeUnit.Year)
				_milliseconds = (long)this._factor*_year;
			else if (this._interval == TimeUnit.Week)
				_milliseconds = (long)this._factor*_week;
			else if (this._interval == TimeUnit.Day)
				_milliseconds = (long)this._factor*_day;
			else if (this._interval == TimeUnit.Hour)
				_milliseconds = (long)this._factor*_hour;
			else if (this._interval == TimeUnit.Minute)
				_milliseconds = (long)this._factor*_minute;
			else if (this._interval == TimeUnit.Second)
				_milliseconds = (long)this._factor*_second;
			else //ms
				_milliseconds = (long)this._factor;
		}

		public TimeSpan ToTimeSpan() => TimeSpan.FromMilliseconds(this._milliseconds);


	}
}