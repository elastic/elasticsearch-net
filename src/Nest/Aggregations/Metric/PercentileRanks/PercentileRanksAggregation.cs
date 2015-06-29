using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<CardinalityAggregator>))]
	public interface IPercentileRanksAggregaor : IMetricAggregator
	{
		[JsonProperty("values")]
		IEnumerable<double> Values { get; set; }
	}

	public class PercentileRanksAggregation : MetricAggregator, IPercentileRanksAggregaor
	{
		public IEnumerable<double> Values { get; set; }
	}

	public class PercentileRanksAggregationDescriptor<T> 
		: MetricAggregationBaseDescriptor<PercentileRanksAggregationDescriptor<T>, T>, IPercentileRanksAggregaor
		where T : class
	{
		IPercentileRanksAggregaor Self => this;
		IEnumerable<double> IPercentileRanksAggregaor.Values { get; set; }

		public PercentileRanksAggregationDescriptor<T> Values(IEnumerable<double> values)
		{
			this.Self.Values = values;
			return this;
		}
	}
}
