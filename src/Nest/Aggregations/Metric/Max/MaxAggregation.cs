using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<MaxAggregator>))]
	public interface IMaxAggregator : IMetricAggregator { }

	public class MaxAggregator : MetricAggregator, IMaxAggregator { }

	public class MaxAggregationDescriptor<T> 
		: MetricAggregationBaseDescriptor<MaxAggregationDescriptor<T>, IMaxAggregator, T>
			, IMaxAggregator 
		where T : class { }
}
