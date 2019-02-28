using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(StatsAggregation))]
	public interface IStatsAggregation : IMetricAggregation { }

	public class StatsAggregation : MetricAggregationBase, IStatsAggregation
	{
		internal StatsAggregation() { }

		public StatsAggregation(string name, Field field) : base(name, field) { }

		internal override void WrapInContainer(AggregationContainer c) => c.Stats = this;
	}

	public class StatsAggregationDescriptor<T>
		: MetricAggregationDescriptorBase<StatsAggregationDescriptor<T>, IStatsAggregation, T>
			, IStatsAggregation
		where T : class { }
}
