using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(AggregationJsonConverter<StatsAggregation>))]
	public interface IStatsAggregator : IMetricAggregation { }

	public class StatsAggregation : MetricAggregationBase, IStatsAggregator
	{
		internal StatsAggregation() { }

		public StatsAggregation(string name, Field field) : base(name, field) { }

		internal override void WrapInContainer(AggregationContainer c) => c.Stats = this;
	}

	public class StatsAggregationDescriptor<T>
		: MetricAggregationDescriptorBase<StatsAggregationDescriptor<T>, IStatsAggregator, T>
			, IStatsAggregator 
		where T : class { }
}
