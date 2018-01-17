using System;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Range that defines a bucket for either the <see cref="RangeAggregation"/> or
	/// <see cref="GeoDistanceAggregation"/>. If you are looking to store ranges as
	/// part of your document please use explicit range class e.g DateRange, FloatRange etc
	/// </summary>
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<Range>))]
	[Obsolete("Renamed to IAggregationRange scheduled for removal in 6.0")]
	public interface IRange
	{
		[JsonProperty("from")]
		double? From { get; set; }

		[JsonProperty("to")]
		double? To { get; set; }

		[JsonProperty("key")]
		string Key { get; set; }
	}

	/// <summary>
	/// Range that defines a bucket for either the <see cref="RangeAggregation"/> or
	/// <see cref="GeoDistanceAggregation"/>. If you are looking to store ranges as
	/// part of your document please use explicit range class e.g DateRange, FloatRange etc
	/// </summary>
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<AggregationRange>))]
#pragma warning disable 618
	public interface IAggregationRange : IRange {}

	/// <inheritdoc/>
	[Obsolete("Renamed to AggregationRange, scheduled for removal in 6.0")]
	public class Range : IRange
	{
		public double? From { get; set; }
		public double? To { get; set; }
		public string Key { get; set; }
	}

	/// <inheritdoc/>
	public class AggregationRange : Range, IAggregationRange { }

	/// <inheritdoc/>
	[Obsolete("Renamed to AggregationRangeDescriptor, scheduled for removal in 6.0")]
	public class RangeDescriptor : DescriptorBase<RangeDescriptor, IRange>, IRange
	{
		double? IRange.From { get; set; }
		string IRange.Key { get; set; }
		double? IRange.To { get; set; }

		public RangeDescriptor Key(string key) => Assign(a => a.Key = key);
		public RangeDescriptor From(double? from) => Assign(a => a.From = from);
		public RangeDescriptor To(double? to) => Assign(a => a.To = to);
	}

	/// <inheritdoc/>
	public class AggregationRangeDescriptor : RangeDescriptor, IAggregationRange { }
#pragma warning restore 618
}
