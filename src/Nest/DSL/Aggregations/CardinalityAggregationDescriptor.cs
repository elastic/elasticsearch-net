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
		[JsonProperty("precision_treshold")]
		int? _PrecisionTreshold { get; set; }

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

		int? ICardinalityAggregator._PrecisionTreshold { get; set; }

		public CardinalityAggregationDescriptor<T> PrecisionTreshold(int precisionTreshold)
		{
			Self._PrecisionTreshold = precisionTreshold;
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
