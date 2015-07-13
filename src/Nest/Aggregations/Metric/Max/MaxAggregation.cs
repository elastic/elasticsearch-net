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

	public class MaxAgg : MetricAgg, IMaxAggregator
	{
		public MaxAgg(string name, FieldName field) : base(name, field) { }

		internal override void WrapInContainer(AggregationContainer c) => c.Max = this;
	}

	public class MaxAggregatorDescriptor<T> 
		: MetricAggregationBaseDescriptor<MaxAggregatorDescriptor<T>, IMaxAggregator, T>
			, IMaxAggregator 
		where T : class { }
}
