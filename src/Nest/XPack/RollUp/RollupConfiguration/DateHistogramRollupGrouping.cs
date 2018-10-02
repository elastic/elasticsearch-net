using System;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A date_histogram group aggregates a date field into time-based buckets. The date_histogram group is mandatory ,
	/// you currently cannot rollup documents without a timestamp and a date_histogram group
	/// </summary>
	[JsonConverter(typeof(ReadAsTypeJsonConverter<DateHistogramRollupGrouping>))]
	public interface IDateHistogramRollupGrouping
	{
		/// <summary>
		/// The date field that is to be rolled up
		/// </summary>
		[JsonProperty("field")]
		Field Field { get; set; }
		/// <summary>
		/// The interval of time buckets to be generated when rolling up. E.g. "60m" will produce 60 minute (hourly) rollups.
		/// The interval defines the minimum interval that can be aggregated only.
		/// </summary>
		[JsonProperty("interval")]
		TimeSpan Interval { get; set; }
		/// <summary>
		/// How long to wait before rolling up new documents. By default, the indexer attempts to roll up all data that is available.
		/// </summary>
		[JsonProperty("delay")]
		TimeSpan Delay { get; set; }
		/// <summary>
		/// Defines what time_zone the rollup documents are stored as. Unlike raw data, which can shift timezones on the fly, rolled
		/// documents have to be stored with a specific timezone. By default, rollup documents are stored in UT
		/// </summary>
		[JsonProperty("time_zone")]
		string TimeZone { get; set; }
	}

	/// <inheritdoc cref="IDateHistogramRollupGrouping"/>
	public class DateHistogramRollupGrouping : IDateHistogramRollupGrouping
	{
		/// <inheritdoc cref="IDateHistogramRollupGrouping.Field"/>
		public Field Field { get; set; }

		/// <inheritdoc cref="IDateHistogramRollupGrouping.Interval"/>
		public TimeSpan Interval { get; set; }

		/// <inheritdoc cref="IDateHistogramRollupGrouping.Delay"/>
		public TimeSpan Delay { get; set; }

		/// <inheritdoc cref="IDateHistogramRollupGrouping.TimeZone"/>
		public string TimeZone { get; set; }
	}

	/// <inheritdoc cref="IDateHistogramRollupGrouping"/>
	public class DateHistogramRollupGroupingDescriptor<T>
		: DescriptorBase<DateHistogramRollupGroupingDescriptor<T>, IDateHistogramRollupGrouping>, IDateHistogramRollupGrouping
		where T : class
	{
		Field IDateHistogramRollupGrouping.Field { get; set; }
		TimeSpan IDateHistogramRollupGrouping.Interval { get; set; }
		TimeSpan IDateHistogramRollupGrouping.Delay { get; set; }
		string IDateHistogramRollupGrouping.TimeZone { get; set; }

		/// <inheritdoc cref="IDateHistogramRollupGrouping.Field"/>
		public DateHistogramRollupGroupingDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		/// <inheritdoc cref="IDateHistogramRollupGrouping.Field"/>
		public DateHistogramRollupGroupingDescriptor<T> Field(Expression<Func<T, object>> field) => Assign(a => a.Field = field);

		/// <inheritdoc cref="IDateHistogramRollupGrouping.Interval"/>
		public DateHistogramRollupGroupingDescriptor<T> Interval(TimeSpan interval) => Assign(a => a.Interval = interval);

		/// <inheritdoc cref="IDateHistogramRollupGrouping.Delay"/>
		public DateHistogramRollupGroupingDescriptor<T> Delay(TimeSpan delay) => Assign(a => a.Delay = delay);

		/// <inheritdoc cref="IDateHistogramRollupGrouping.TimeZone"/>
		public DateHistogramRollupGroupingDescriptor<T> TimeZone(string timeZone) => Assign(a => a.TimeZone = timeZone);
	}
}
