using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Configuration;
using System.Text;
using Nest.Resolvers;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<CardinalityAggregator>))]
	public interface ICardinalityAggregator : IMetricAggregator
	{
		[JsonProperty("precision_threshold")]
		int? _PrecisionThreshold { get; set; }

		[JsonProperty("rehash")]
		bool? _Rehash { get; set; }
	}

	public class CardinalityAggregator : MetricAggregator, ICardinalityAggregator
	{
		public int? _PrecisionTreshold { get; set; }
		public bool? _Rehash { get; set; }
	}

	public class CardinalityAggregationDescriptor<T> : MetricAggregationBaseDescriptor<CardinalityAggregationDescriptor<T>, T>, ICardinalityAggregator where T : class
	{
		private ICardinalityAggregator Self { get { return this; } }

		int? ICardinalityAggregator._PrecisionThreshold { get; set; }

		public CardinalityAggregationDescriptor<T> PrecisionThreshold(int precisionTreshold)
		{
			this._PrecisionTreshold = precisionThreshold;
			Self._PrecisionTreshold = precisionThreshold;
			return this;
		}

		bool? ICardinalityAggregator._Rehash { get; set; }

		public CardinalityAggregationDescriptor<T> Rehash(bool rehash = true)
		{
			Self._Rehash = rehash;
			return this;
		}

	}
}
