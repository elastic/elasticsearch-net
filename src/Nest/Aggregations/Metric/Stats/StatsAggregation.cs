using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(StatsAggregation))]
	public interface IStatsAggregation : IFormattableMetricAggregation { }

	public class StatsAggregation : FormattableMetricAggregationBase, IStatsAggregation
	{
		internal StatsAggregation() { }

		public StatsAggregation(string name, Field field) : base(name, field) { }

		internal override void WrapInContainer(AggregationContainer c) => c.Stats = this;
	}

	public class StatsAggregationDescriptor<T>
		: FormattableMetricAggregationDescriptorBase<StatsAggregationDescriptor<T>, IStatsAggregation, T>
			, IStatsAggregation
		where T : class { }
}
