using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<AverageAggregator>))]
	public interface IAverageAggregator : IMetricAggregator
	{
	}

	public class AverageAggregator : MetricAggregator, IAverageAggregator
	{
		
	}

	public class AverageAggregationDescriptor<T> : MetricAggregationBaseDescriptor<AverageAggregationDescriptor<T>, T>, IAverageAggregator where T : class
	{
		
	}
}
