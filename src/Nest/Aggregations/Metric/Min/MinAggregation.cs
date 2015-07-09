using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<MinAggregator>))]
	public interface IMinAggregator : IMetricAggregator { }

	public class MinAggregator : MetricAggregator, IMinAggregator { }

	public class MinAggregatorDescriptor<T> 
		: MetricAggregationBaseDescriptor<MinAggregatorDescriptor<T>, IMinAggregator, T>
			, IMinAggregator 
		where T : class { }

}
