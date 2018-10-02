using System;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// The terms group can be used on keyword or numeric fields, to allow bucketing via the terms aggregation at a
	/// later point. The terms group is optional. If defined, the indexer will enumerate and store all values of a field for
	/// each time-period.
	/// </summary>
	[JsonConverter(typeof(ReadAsTypeJsonConverter<HistogramRollupGrouping>))]
	public interface IHistogramRollupGrouping
	{
		/// <summary>
		/// The set of fields that you wish to build histograms for. All fields specified must be some kind of numeric. Order does not matter
		/// </summary>
		[JsonProperty("fields")]
		Fields Fields { get; set; }

		/// <summary>
		/// The interval of histogram buckets to be generated when rolling up. Note that only one interval can be specified in the
		/// histogram group, meaning that all fields being grouped via the histogram must share the same interval.
		/// </summary>
		[JsonProperty("interval")]
		long? Interval { get; set; }
	}

	/// <inheritdoc cref="IHistogramRollupGrouping"/>
	public class HistogramRollupGrouping : IHistogramRollupGrouping
	{
		/// <inheritdoc cref="IHistogramRollupGrouping.Fields"/>
		public Fields Fields { get; set; }

		/// <inheritdoc cref="IHistogramRollupGrouping.Interval"/>
		public long? Interval { get; set; }
	}

	/// <inheritdoc cref="IHistogramRollupGrouping"/>
	public class HistogramRollupGroupingDescriptor<T>
		: DescriptorBase<HistogramRollupGroupingDescriptor<T>, IHistogramRollupGrouping>, IHistogramRollupGrouping
		where T : class
	{
		Fields IHistogramRollupGrouping.Fields { get; set; }
		long? IHistogramRollupGrouping.Interval { get; set; }

		/// <inheritdoc cref="IHistogramRollupGrouping.Fields"/>
		public HistogramRollupGroupingDescriptor<T> Fields(Func<FieldsDescriptor<T>, IPromise<Fields>> fields) =>
			Assign(a => a.Fields = fields?.Invoke(new FieldsDescriptor<T>())?.Value);

		/// <inheritdoc cref="IHistogramRollupGrouping.Fields"/>
		public HistogramRollupGroupingDescriptor<T> Fields(Fields fields) => Assign(a => a.Fields = fields);

		/// <inheritdoc cref="IHistogramRollupGrouping.Interval"/>
		public HistogramRollupGroupingDescriptor<T> Interval(long? interval) => Assign(a => a.Interval = interval);
	}
}
