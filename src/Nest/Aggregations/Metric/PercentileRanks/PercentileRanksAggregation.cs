using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<PercentileRanksAggregator>))]
	public interface IPercentileRanksAggregator : IMetricAggregator
	{
		[JsonProperty("values")]
		IEnumerable<double> Values { get; set; }
	}

	public class PercentileRanksAggregator : MetricAggregator, IPercentileRanksAggregator
	{
		public IEnumerable<double> Values { get; set; }
	}

	public class PercentileRanksAgg : MetricAgg, IPercentileRanksAggregator
	{
		public IEnumerable<double> Values { get; set; }

		public PercentileRanksAgg(string name, FieldName field) : base(name, field) { }

		internal override void WrapInContainer(AggregationContainer c) => c.PercentileRanks = this;
	}

	public class PercentileRanksAggregatorDescriptor<T> 
		: MetricAggregationBaseDescriptor<PercentileRanksAggregatorDescriptor<T>, IPercentileRanksAggregator, T>, IPercentileRanksAggregator
		where T : class
	{
		IEnumerable<double> IPercentileRanksAggregator.Values { get; set; }

		public PercentileRanksAggregatorDescriptor<T> Values(IEnumerable<double> values) =>
			Assign(a => a.Values = values.ToListOrNullIfEmpty());

	}
}
