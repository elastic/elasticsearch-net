using System;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// The groups section of the configuration is where you decide which fields should be grouped on, and with what aggregations.
	/// These fields will then be available later for aggregating into buckets
	/// </summary>
	[JsonConverter(typeof(ReadAsTypeJsonConverter<RollupGroupings>))]
	public interface IRollupGroupings
	{
		/// <inheritdoc cref="IDateHistogramRollupGrouping"/>
		[JsonProperty("date_histogram")]
		IDateHistogramRollupGrouping DateHistogram { get; set; }

		/// <inheritdoc cref="IHistogramRollupGrouping"/>
		[JsonProperty("histogram")]
		IHistogramRollupGrouping Histogram { get; set; }

		/// <inheritdoc cref="ITermsRollupGrouping"/>
		[JsonProperty("terms")]
		ITermsRollupGrouping Terms { get; set; }
	}

	/// <inheritdoc cref="IRollupGroupings"/>
	public class RollupGroupings : IRollupGroupings
	{
		/// <inheritdoc cref="IDateHistogramRollupGrouping"/>
		public IDateHistogramRollupGrouping DateHistogram { get; set; }
		/// <inheritdoc cref="IHistogramRollupGrouping"/>
		public IHistogramRollupGrouping Histogram { get; set; }
		/// <inheritdoc cref="ITermsRollupGrouping"/>
		public ITermsRollupGrouping Terms { get; set; }
	}

	/// <inheritdoc cref="IRollupGroupings"/>
	public class RollupGroupingsDescriptor<T> : DescriptorBase<RollupGroupingsDescriptor<T>, IRollupGroupings>, IRollupGroupings
		where T : class
	{
		IDateHistogramRollupGrouping IRollupGroupings.DateHistogram { get; set; }
		IHistogramRollupGrouping IRollupGroupings.Histogram { get; set; }
		ITermsRollupGrouping IRollupGroupings.Terms { get; set; }

		/// <inheritdoc cref="IDateHistogramRollupGrouping"/>
		public RollupGroupingsDescriptor<T> DateHistogram(
			Func<DateHistogramRollupGroupingDescriptor<T>, IDateHistogramRollupGrouping> selector) =>
			Assign(a => a.DateHistogram = selector?.Invoke(new DateHistogramRollupGroupingDescriptor<T>()));

		/// <inheritdoc cref="IHistogramRollupGrouping"/>
		public RollupGroupingsDescriptor<T> Histogram(Func<HistogramRollupGroupingDescriptor<T>, IHistogramRollupGrouping> selector) =>
			Assign(a => a.Histogram = selector?.Invoke(new HistogramRollupGroupingDescriptor<T>()));

		/// <inheritdoc cref="ITermsRollupGrouping"/>
		public RollupGroupingsDescriptor<T> Terms(Func<TermsRollupGroupingDescriptor<T>, ITermsRollupGrouping> selector) =>
			Assign(a => a.Terms = selector?.Invoke(new TermsRollupGroupingDescriptor<T>()));
	}
}
