using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum IntervalUnit
	{
		[EnumMember(Value = "s")]
		Second,
		[EnumMember(Value = "m")]
		Minute,
		[EnumMember(Value = "h")]
		Hour,
		[EnumMember(Value = "d")]
		Day,
		[EnumMember(Value = "w")]
		Week,
	}

	[JsonConverter(typeof(IntervalJsonConverter))]
	public class Interval : ScheduleBase, IComparable<Interval>, IEquatable<Interval>
	{
		private static readonly Regex IntervalExpressionRegex = new Regex(@"^(?<factor>\d+)(?<unit>(?:w|d|h|m|s))?$", RegexOptions.Compiled | RegexOptions.ExplicitCapture);

		private const long WeekSeconds = 604800;
		private const long DaySeconds = 86400;
		private const long HourSeconds = 3600;
		private const long MinuteSeconds = 60;
		private const long Second = 1;

		private long _seconds;

		public long Factor { get; }

		public IntervalUnit? Unit { get; }

		public static implicit operator Interval(TimeSpan timeSpan) => new Interval(timeSpan);
		public static implicit operator Interval(long seconds) => new Interval(seconds);
		public static implicit operator Interval(string expression) => new Interval(expression);

		public Interval(TimeSpan timeSpan)
		{
			if (timeSpan.TotalSeconds < 1)
				throw new ArgumentException("must be greater than or equal to 1 second", nameof(timeSpan));

			var totalSeconds = (long)timeSpan.TotalSeconds;

			if (totalSeconds >= WeekSeconds && totalSeconds % WeekSeconds == 0)
			{
				this.Factor = totalSeconds / WeekSeconds;
				this.Unit = IntervalUnit.Week;
			}
			else if (totalSeconds >= DaySeconds && totalSeconds % DaySeconds == 0)
			{
				this.Factor = totalSeconds / DaySeconds;
				this.Unit = IntervalUnit.Day;
			}
			else if (totalSeconds >= HourSeconds && totalSeconds % HourSeconds == 0)
			{
				this.Factor = totalSeconds / HourSeconds;
				this.Unit = IntervalUnit.Hour;
			}
			else if (totalSeconds >= MinuteSeconds && totalSeconds % MinuteSeconds == 0)
			{
				this.Factor = totalSeconds / MinuteSeconds;
				this.Unit = IntervalUnit.Minute;
			}
			else
			{
				this.Factor = totalSeconds;
				this.Unit = IntervalUnit.Second;
			}

			this._seconds = totalSeconds;
		}

		public Interval(long seconds)
		{
			this.Factor = seconds;
			this._seconds = seconds;
		}

		public Interval(long factor, IntervalUnit unit)
		{
			this.Factor = factor;
			this.Unit = unit;
			SetSeconds(this.Factor, unit);
		}

		public Interval(string intervalUnit)
		{
			if (intervalUnit.IsNullOrEmpty())
				throw new ArgumentException("Interval expression string cannot be null or empty", nameof(intervalUnit));

			var match = IntervalExpressionRegex.Match(intervalUnit);
			if (!match.Success)
				throw new ArgumentException($"Interval expression '{intervalUnit}' string is invalid", nameof(intervalUnit));

			this.Factor = long.Parse(match.Groups["factor"].Value, CultureInfo.InvariantCulture);

			var unit = match.Groups["unit"].Success
				? match.Groups["unit"].Value.ToEnum<IntervalUnit>().GetValueOrDefault(IntervalUnit.Second)
				: IntervalUnit.Second;

			this.Unit = unit;
			SetSeconds(this.Factor, unit);
		}

		public int CompareTo(Interval other)
		{
			if (other == null) return 1;
			if (this._seconds == other._seconds) return 0;
			if (this._seconds < other._seconds) return -1;
			return 1;
		}

		public static bool operator <(Interval left, Interval right) => left.CompareTo(right) < 0;
		public static bool operator <=(Interval left, Interval right) => left.CompareTo(right) < 0 || left.Equals(right);

		public static bool operator >(Interval left, Interval right) => left.CompareTo(right) > 0;
		public static bool operator >=(Interval left, Interval right) => left.CompareTo(right) > 0 || left.Equals(right);

		public static bool operator ==(Interval left, Interval right) =>
			ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.Equals(right);

		public static bool operator !=(Interval left, Interval right) => !(left == right);

		public override string ToString()
		{
			var factor = this.Factor.ToString(CultureInfo.InvariantCulture);
			return (this.Unit.HasValue) ? factor + this.Unit.Value.GetStringValue() : factor;
		}

		public bool Equals(Interval other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return this._seconds == other._seconds;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((Interval)obj);
		}

		public override int GetHashCode() => this._seconds.GetHashCode();

		internal override void WrapInContainer(IScheduleContainer container) => container.Interval = this;

		private void SetSeconds(long factor, IntervalUnit interval)
		{
			switch (interval)
			{
				case IntervalUnit.Week:
					this._seconds = factor * WeekSeconds;
					break;
				case IntervalUnit.Day:
					this._seconds = factor * DaySeconds;
					break;
				case IntervalUnit.Hour:
					this._seconds = factor * HourSeconds;
					break;
				case IntervalUnit.Minute:
					this._seconds = factor * MinuteSeconds;
					break;
				case IntervalUnit.Second:
					this._seconds = factor * Second;
					break;
			}
		}
	}

	internal class IntervalJsonConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var v = (Interval)value;
			if (v.Unit.HasValue) writer.WriteValue(v.ToString());
			else writer.WriteValue(v.Factor);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			switch (reader.TokenType)
			{
				case JsonToken.String:
					return new Interval((string)reader.Value);
				case JsonToken.Integer:
				case JsonToken.Float:
					var seconds = Convert.ToInt64(reader.Value);
					return new Interval(seconds);
			}

			return null;
		}

		public override bool CanConvert(Type objectType) => true;
	}
}
