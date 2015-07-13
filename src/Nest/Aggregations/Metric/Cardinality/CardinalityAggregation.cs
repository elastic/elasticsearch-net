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

	public class CardinalityAgg : MetricAgg, ICardinalityAggregator
	{
		public int? PrecisionThreshold { get; set; }
		public bool? Rehash { get; set; }

		public CardinalityAgg(string name, FieldName field) : base(name, field) { }

		internal override void WrapInContainer(AggregationContainer c) => c.Cardinality = this;
	}

	public class CardinalityAggregatorDescriptor<T> 
		: MetricAggregationBaseDescriptor<CardinalityAggregatorDescriptor<T>, ICardinalityAggregator, T>
			, ICardinalityAggregator 
		where T : class
	{
		int? ICardinalityAggregator.PrecisionThreshold { get; set; }

		bool? ICardinalityAggregator.Rehash { get; set; }

		public CardinalityAggregatorDescriptor<T> PrecisionThreshold(int precisionThreshold)
			=> Assign(a => a.PrecisionThreshold = precisionThreshold);

		public CardinalityAggregatorDescriptor<T> Rehash(bool rehash = true) => Assign(a => a.Rehash = rehash);

	}
}
