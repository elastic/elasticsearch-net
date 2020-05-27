using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	public interface IDateHistogramGroupSource : ISingleGroupSource
	{
		/// <summary>
		/// Return a formatted date string as the key instead an epoch long
		/// </summary>
		[DataMember(Name ="format")]
		string Format { get; set; }

		/// <summary>
		/// Date-times are stored in Elasticsearch in UTC. By default, all bucketing and rounding is also done in UTC.
		/// The time_zone parameter can be used to indicate that bucketing should use a different time zone.
		/// Time zones may either be specified as an ISO 8601 UTC offset (e.g. +01:00 or -08:00) or as a timezone id,
		/// an identifier used in the TZ database like America/Los_Angeles.
		/// </summary>
		[DataMember(Name ="time_zone")]
		string TimeZone { get; set; }

		/// <summary>
		/// Calendar-aware intervals are configured with the calendar_interval parameter. Calendar intervals can only be specified
		/// in "singular" quantities of the unit (1d, 1M, etc). Multiples, such as 2d, are not supported
		/// </summary>
		[DataMember(Name = "calendar_interval")]
		Union<DateInterval, DateMathTime> CalendarInterval { get; set; }

		/// <summary>
		///  fixed intervals are a fixed number of SI units and never deviate, regardless of where they fall on the calendar. One second is
		/// always composed of 1000ms. This allows fixed intervals to be specified in any multiple of the supported units.
		/// </summary>
		[DataMember(Name = "fixed_interval")]
		Union<DateInterval, Time> FixedInterval { get; set; }
	}

	public class DateHistogramGroupSource : SingleGroupSourceBase, IDateHistogramGroupSource
	{
		/// <inheritdoc cref="IDateHistogramGroupSource.Format"/>
		public string Format { get; set; }

		/// <inheritdoc cref="IDateHistogramGroupSource.TimeZone"/>
		public string TimeZone { get; set; }

		/// <inheritdoc cref="IDateHistogramGroupSource.CalendarInterval"/>
		public Union<DateInterval, DateMathTime> CalendarInterval { get; set; }

		/// <inheritdoc cref="IDateHistogramGroupSource.FixedInterval"/>
		public Union<DateInterval, Time> FixedInterval { get; set; }
	}

	public class DateHistogramGroupSourceDescriptor<T>
		: SingleGroupSourceDescriptorBase<DateHistogramGroupSourceDescriptor<T>, IDateHistogramGroupSource, T>,
			IDateHistogramGroupSource
	{
		string IDateHistogramGroupSource.Format { get; set; }
		string IDateHistogramGroupSource.TimeZone { get; set; }
		Union<DateInterval, DateMathTime> IDateHistogramGroupSource.CalendarInterval { get; set; }
		Union<DateInterval, Time> IDateHistogramGroupSource.FixedInterval { get; set; }

		/// <inheritdoc cref="IDateHistogramGroupSource.Format"/>
		public DateHistogramGroupSourceDescriptor<T> Format(string format) => Assign(format, (a, v) => a.Format = v);

		/// <inheritdoc cref="IDateHistogramGroupSource.TimeZone"/>
		public DateHistogramGroupSourceDescriptor<T> TimeZone(string timeZone) => Assign(timeZone, (a, v) => a.TimeZone = v);

		/// <inheritdoc cref="IDateHistogramGroupSource.CalendarInterval"/>
		public DateHistogramGroupSourceDescriptor<T> CalendarInterval(DateInterval dateInterval) => Assign(dateInterval, (a, v) => a.CalendarInterval = v);

		/// <inheritdoc cref="IDateHistogramGroupSource.CalendarInterval"/>
		public DateHistogramGroupSourceDescriptor<T> CalendarInterval(DateMathTime time) => Assign(time, (a, v) => a.CalendarInterval = v);

		/// <inheritdoc cref="IDateHistogramGroupSource.FixedInterval"/>
		public DateHistogramGroupSourceDescriptor<T> FixedInterval(DateInterval dateInterval) => Assign(dateInterval, (a, v) => a.FixedInterval = v);

		/// <inheritdoc cref="IDateHistogramGroupSource.FixedInterval"/>
		public DateHistogramGroupSourceDescriptor<T> FixedInterval(Time time) => Assign(time, (a, v) => a.FixedInterval = v);
	}
}
