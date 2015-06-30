using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<CardinalityAggregator>))]
	public interface ICardinalityAggregator : IMetricAggregator
	{
		[JsonProperty("precision_threshold")]
		int? PrecisionThreshold { get; set; }

		[JsonProperty("rehash")]
		bool? Rehash { get; set; }
	}

	public class CardinalityAggregator : MetricAggregator, ICardinalityAggregator
	{
		public int? PrecisionThreshold { get; set; }
		public bool? Rehash { get; set; }
	}

	public class CardinalityAggregationDescriptor<T> 
		: MetricAggregationBaseDescriptor<CardinalityAggregationDescriptor<T>, ICardinalityAggregator, T>
			, ICardinalityAggregator 
		where T : class
	{
		int? ICardinalityAggregator.PrecisionThreshold { get; set; }

		bool? ICardinalityAggregator.Rehash { get; set; }

		public CardinalityAggregationDescriptor<T> PrecisionThreshold(int precisionThreshold)
			=> Assign(a => a.PrecisionThreshold = precisionThreshold);

		public CardinalityAggregationDescriptor<T> Rehash(bool rehash = true) => Assign(a => a.Rehash = rehash);

	}
}
