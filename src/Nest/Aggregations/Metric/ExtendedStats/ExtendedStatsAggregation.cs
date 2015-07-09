using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<ExtendedStatsAggregator>))]
	public interface IExtendedStatsAggregator : IMetricAggregator { }

	public class ExtendedStatsAggregator : MetricAggregator, IExtendedStatsAggregator { }

	public class ExtendedStatsAggregatorDescriptor<T> 
		: MetricAggregationBaseDescriptor<ExtendedStatsAggregatorDescriptor<T>, IExtendedStatsAggregator, T>
			, IExtendedStatsAggregator 
		where T : class { }

}