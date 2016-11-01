using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(AggregationJsonConverter<StatsAggregation>))]
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
