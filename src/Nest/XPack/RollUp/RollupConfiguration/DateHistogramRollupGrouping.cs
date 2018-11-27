using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// A date_histogram group aggregates a date field into time-based buckets. The date_histogram group is mandatory ,
	/// you currently cannot rollup documents without a timestamp and a date_histogram group
	/// </summary>
	[ReadAs(typeof(DateHistogramRollupGrouping))]
	public interface IDateHistogramRollupGrouping
	{
		/// <summary>
		/// How long to wait before rolling up new documents. By default, the indexer attempts to roll up all data that is available.
		/// </summary>
		[DataMember(Name ="delay")]
		Time Delay { get; set; }

		/// <summary>
		/// The date field that is to be rolled up
		/// </summary>
		[DataMember(Name ="field")]
		Field Field { get; set; }

		/// <summary>
		/// The interval of time buckets to be generated when rolling up. E.g. "60m" will produce 60 minute (hourly) rollups.
		/// The interval defines the minimum interval that can be aggregated only.
		/// </summary>
		[DataMember(Name ="interval")]
		Time Interval { get; set; }

		/// <summary>
		/// Defines what time_zone the rollup documents are stored as. Unlike raw data, which can shift timezones on the fly, rolled
		/// documents have to be stored with a specific timezone. By default, rollup documents are stored in UT
		/// </summary>
		[DataMember(Name ="time_zone")]
		string TimeZone { get; set; }
	}

	/// <inheritdoc />
	public class DateHistogramRollupGrouping : IDateHistogramRollupGrouping
	{
		/// <inheritdoc />
		public Time Delay { get; set; }

		/// <inheritdoc />
		public Field Field { get; set; }

		/// <inheritdoc />
		public Time Interval { get; set; }

		/// <inheritdoc />
		public string TimeZone { get; set; }
	}

	/// <inheritdoc cref="IDateHistogramRollupGrouping" />
	public class DateHistogramRollupGroupingDescriptor<T>
		: DescriptorBase<DateHistogramRollupGroupingDescriptor<T>, IDateHistogramRollupGrouping>, IDateHistogramRollupGrouping
		where T : class
	{
		Time IDateHistogramRollupGrouping.Delay { get; set; }
		Field IDateHistogramRollupGrouping.Field { get; set; }
		Time IDateHistogramRollupGrouping.Interval { get; set; }
		string IDateHistogramRollupGrouping.TimeZone { get; set; }

		/// <inheritdoc cref="IDateHistogramRollupGrouping.Field" />
		public DateHistogramRollupGroupingDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		/// <inheritdoc cref="IDateHistogramRollupGrouping.Field" />
		public DateHistogramRollupGroupingDescriptor<T> Field(Expression<Func<T, object>> field) => Assign(a => a.Field = field);

		/// <inheritdoc cref="IDateHistogramRollupGrouping.Interval" />
		public DateHistogramRollupGroupingDescriptor<T> Interval(Time interval) => Assign(a => a.Interval = interval);

		/// <inheritdoc cref="IDateHistogramRollupGrouping.Delay" />
		public DateHistogramRollupGroupingDescriptor<T> Delay(Time delay) => Assign(a => a.Delay = delay);

		/// <inheritdoc cref="IDateHistogramRollupGrouping.TimeZone" />
		public DateHistogramRollupGroupingDescriptor<T> TimeZone(string timeZone) => Assign(a => a.TimeZone = timeZone);
	}
}
