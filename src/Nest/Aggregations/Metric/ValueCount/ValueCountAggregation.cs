using System.Collections.Generic;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<ValueCountAggregator>))]
	public interface IValueCountAggregator : IMetricAggregator { }
	
	public class ValueCountAggregator : MetricAggregator, IValueCountAggregator { }

	public class ValueCountAggregatorDescriptor<T> 
		: MetricAggregationBaseDescriptor<ValueCountAggregatorDescriptor<T>, IValueCountAggregator, T>
			, IValueCountAggregator 
		where T : class { }
}