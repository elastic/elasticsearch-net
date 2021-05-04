// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using Elasticsearch.Net;
using Elastic.Transport.Extensions;
using Nest.Utf8Json;

namespace Nest
{
	[StringEnum]
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

	[JsonFormatter(typeof(IntervalFormatter))]
	public class Interval : ScheduleBase, IComparable<Interval>, IEquatable<Interval>
	{
		private const long DaySeconds = 86400;
		private const long HourSeconds = 3600;
		private const long MinuteSeconds = 60;
		private const long Second = 1;

		private const long WeekSeconds = 604800;

		private static readonly Regex IntervalExpressionRegex =
			new Regex(@"^(?<factor>\d+)(?<unit>(?:w|d|h|m|s))?$", RegexOptions.Compiled | RegexOptions.ExplicitCapture);

		private long _seconds;

		public Interval(TimeSpan timeSpan)
		{
			if (timeSpan.TotalSeconds < 1)
				throw new ArgumentException("must be greater than or equal to 1 second", nameof(timeSpan));

			var totalSeconds = (long)timeSpan.TotalSeconds;

			if (totalSeconds >= WeekSeconds && totalSeconds % WeekSeconds == 0)
			{
				Factor = totalSeconds / WeekSeconds;
				Unit = IntervalUnit.Week;
			}
			else if (totalSeconds >= DaySeconds && totalSeconds % DaySeconds == 0)
			{
				Factor = totalSeconds / DaySeconds;
				Unit = IntervalUnit.Day;
			}
			else if (totalSeconds >= HourSeconds && totalSeconds % HourSeconds == 0)
			{
				Factor = totalSeconds / HourSeconds;
				Unit = IntervalUnit.Hour;
			}
			else if (totalSeconds >= MinuteSeconds && totalSeconds % MinuteSeconds == 0)
			{
				Factor = totalSeconds / MinuteSeconds;
				Unit = IntervalUnit.Minute;
			}
			else
			{
				Factor = totalSeconds;
				Unit = IntervalUnit.Second;
			}

			_seconds = totalSeconds;
		}

		public Interval(long seconds)
		{
			Factor = seconds;
			_seconds = seconds;
		}

		public Interval(long factor, IntervalUnit unit)
		{
			Factor = factor;
			Unit = unit;
			SetSeconds(Factor, unit);
		}

		public Interval(string intervalUnit)
		{
			if (intervalUnit.IsNullOrEmpty())
				throw new ArgumentException("Interval expression string cannot be null or empty", nameof(intervalUnit));

			var match = IntervalExpressionRegex.Match(intervalUnit);
			if (!match.Success)
				throw new ArgumentException($"Interval expression '{intervalUnit}' string is invalid", nameof(intervalUnit));

			Factor = long.Parse(match.Groups["factor"].Value, CultureInfo.InvariantCulture);

			var unit = match.Groups["unit"].Success
				? match.Groups["unit"].Value.ToEnum<IntervalUnit>().GetValueOrDefault(IntervalUnit.Second)
				: IntervalUnit.Second;

			Unit = unit;
			SetSeconds(Factor, unit);
		}

		public long Factor { get; }

		public IntervalUnit? Unit { get; }

		public int CompareTo(Interval other)
		{
			if (other == null) return 1;
			if (_seconds == other._seconds) return 0;
			if (_seconds < other._seconds) return -1;

			return 1;
		}

		public bool Equals(Interval other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;

			return _seconds == other._seconds;
		}

		public static implicit operator Interval(TimeSpan timeSpan) => new Interval(timeSpan);

		public static implicit operator Interval(long seconds) => new Interval(seconds);

		public static implicit operator Interval(string expression) => new Interval(expression);

		public static bool operator <(Interval left, Interval right) => left.CompareTo(right) < 0;

		public static bool operator <=(Interval left, Interval right) => left.CompareTo(right) < 0 || left.Equals(right);

		public static bool operator >(Interval left, Interval right) => left.CompareTo(right) > 0;

		public static bool operator >=(Interval left, Interval right) => left.CompareTo(right) > 0 || left.Equals(right);

		public static bool operator ==(Interval left, Interval right) =>
			ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.Equals(right);

		public static bool operator !=(Interval left, Interval right) => !(left == right);

		public override string ToString()
		{
			var factor = Factor.ToString(CultureInfo.InvariantCulture);
			return Unit.HasValue ? factor + Unit.Value.GetStringValue() : factor;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != GetType()) return false;

			return Equals((Interval)obj);
		}

		// ReSharper disable once NonReadonlyMemberInGetHashCode
		public override int GetHashCode() => _seconds.GetHashCode();

		internal override void WrapInContainer(IScheduleContainer container) => container.Interval = this;

		private void SetSeconds(long factor, IntervalUnit interval)
		{
			switch (interval)
			{
				case IntervalUnit.Week:
					_seconds = factor * WeekSeconds;
					break;
				case IntervalUnit.Day:
					_seconds = factor * DaySeconds;
					break;
				case IntervalUnit.Hour:
					_seconds = factor * HourSeconds;
					break;
				case IntervalUnit.Minute:
					_seconds = factor * MinuteSeconds;
					break;
				case IntervalUnit.Second:
					_seconds = factor * Second;
					break;
			}
		}
	}

	internal class IntervalFormatter : IJsonFormatter<Interval>
	{
		public Interval Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();
			switch (token)
			{
				case JsonToken.String:
					return new Interval(reader.ReadString());
				case JsonToken.Number:
					var seconds = Convert.ToInt64(reader.ReadDouble());
					return new Interval(seconds);
				default:
					reader.ReadNextBlock();
					return null;
			}
		}

		public void Serialize(ref JsonWriter writer, Interval value, IJsonFormatterResolver formatterResolver)
		{
			if (value.Unit.HasValue) writer.WriteString(value.ToString());
			else writer.WriteInt64(value.Factor);
		}
	}
}
