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

	public class CardinalityAggregationDescriptor<T> : MetricAggregationBaseDescriptor<CardinalityAggregationDescriptor<T>, T>, ICardinalityAggregator where T : class
	{
		private ICardinalityAggregator Self { get { return this; } }

		int? ICardinalityAggregator.PrecisionThreshold { get; set; }

		bool? ICardinalityAggregator.Rehash { get; set; }

		public CardinalityAggregationDescriptor<T> PrecisionThreshold(int precisionThreshold)
		{
			Self.PrecisionThreshold = precisionThreshold;
			return this;
		}

		public CardinalityAggregationDescriptor<T> Rehash(bool rehash = true)
		{
			Self.Rehash = rehash;
			return this;
		}

	}
}
