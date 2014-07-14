using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<SumAggregator>))]
	public interface ISumAggregator : IMetricAggregator
	{
	}

	public class SumAggregator : MetricAggregator, ISumAggregator
	{
	}

	public class SumAggregationDescriptor<T> : MetricAggregationBaseDescriptor<SumAggregationDescriptor<T>, T>, ISumAggregator where T : class
	{
		
	}
}
