using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A values source that can be applied on date values to build fixed size interval over the values.
	/// The interval parameter defines a date/time expression that determines how values should be transformed.
	/// For instance an interval set to month will translate any values to its closest month interval..
	/// </summary>
	public interface IDateHistogramCompositeAggregationSource : ICompositeAggregationSource
	{
		/// <summary>
		///	The interval to use when bucketing documents
		/// </summary>
		[JsonProperty("interval")]
		Union<DateInterval?, Time> Interval { get; set; }

		/// <summary>
		/// Used to indicate that bucketing should use a different time zone.
		/// Time zones may either be specified as an ISO 8601 UTC offset (e.g. +01:00 or -08:00)
		/// or as a timezone id, an identifier used in the TZ database like America/Los_Angeles.
		/// </summary>
		[JsonProperty("time_zone")]
		string Timezone { get; set; }
	}

	/// <inheritdoc cref="IDateHistogramCompositeAggregationSource"/>
	public class DateHistogramCompositeAggregationSource : CompositeAggregationSourceBase, IDateHistogramCompositeAggregationSource
	{
		public DateHistogramCompositeAggregationSource(string name) : base(name) {}

		/// <inheritdoc />
		public Union<DateInterval?,Time> Interval { get; set; }

		/// <inheritdoc />
		public string Timezone { get; set; }

		/// <inheritdoc />
		protected override string SourceType => "date_histogram";
	}

	/// <inheritdoc cref="IDateHistogramCompositeAggregationSource"/>
	public class DateHistogramCompositeAggregationSourceDescriptor<T>
		: CompositeAggregationSourceDescriptorBase<DateHistogramCompositeAggregationSourceDescriptor<T>, IDateHistogramCompositeAggregationSource, T>,
			IDateHistogramCompositeAggregationSource
	{
		Union<DateInterval?,Time> IDateHistogramCompositeAggregationSource.Interval { get; set; }
		string IDateHistogramCompositeAggregationSource.Timezone { get; set; }

		public DateHistogramCompositeAggregationSourceDescriptor(string name) : base(name, "date_histogram") {}

		/// <inheritdoc cref="IDateHistogramCompositeAggregationSource.Interval"/>
		public DateHistogramCompositeAggregationSourceDescriptor<T> Interval(DateInterval? interval) =>
			Assign(a => a.Interval = interval);

		/// <inheritdoc cref="IDateHistogramCompositeAggregationSource.Interval"/>
		public DateHistogramCompositeAggregationSourceDescriptor<T> Interval(Time interval) =>
			Assign(a => a.Interval = interval);

		/// <inheritdoc cref="IDateHistogramCompositeAggregationSource.Timezone"/>
		public DateHistogramCompositeAggregationSourceDescriptor<T> Timezone(string timezone) =>
			Assign(a => a.Timezone = timezone);
	}
}
