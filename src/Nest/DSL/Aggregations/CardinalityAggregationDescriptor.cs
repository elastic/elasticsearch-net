using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Configuration;
using System.Text;
using Nest.Resolvers;
using Newtonsoft.Json;

namespace Nest
{
	public class CardinalityAggregationDescriptor<T> : MetricAggregationBaseDescriptor<CardinalityAggregationDescriptor<T>, T>
		where T : class
	{

		[JsonProperty("precision_treshold")]
		internal int? _PrecisionTreshold { get; set; }

		public CardinalityAggregationDescriptor<T> PrecisionTreshold(int precisionTreshold)
		{
			this._PrecisionTreshold = precisionTreshold;
			return this;
		}

		[JsonProperty("rehash")]
		internal bool? _Rehash { get; set; }

		public CardinalityAggregationDescriptor<T> Rehash(bool rehash = true)
		{
			this._Rehash = rehash;
			return this;
		}

	}
}
