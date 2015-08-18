using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<ExtendedStatsAggregator>))]
	public interface IExtendedStatsAggregator : IMetricAggregator { }

	public class ExtendedStatsAggregator : MetricAggregator, IExtendedStatsAggregator { }

	public class ExtendedStatsAgg: MetricAgg, IExtendedStatsAggregator
	{
		public ExtendedStatsAgg(string name, FieldName field) : base(name, field) { }

		internal override void WrapInContainer(AggregationContainer c) => c.ExtendedStats = this;
	}

	public class ExtendedStatsAggregatorDescriptor<T> 
		: MetricAggregationBaseDescriptor<ExtendedStatsAggregatorDescriptor<T>, IExtendedStatsAggregator, T>
			, IExtendedStatsAggregator 
		where T : class { }

}