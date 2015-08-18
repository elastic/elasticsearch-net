using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<SumAggregator>))]
	public interface ISumAggregator : IMetricAggregator { }

	public class SumAggregator : MetricAggregator, ISumAggregator { }

	public class SumAgg : MetricAgg, ISumAggregator
	{
		public SumAgg(string name, FieldName field) : base(name, field) { }

		internal override void WrapInContainer(AggregationContainer c) => c.Sum = this;
	}

	public class SumAggregatorDescriptor<T> 
		: MetricAggregationBaseDescriptor<SumAggregatorDescriptor<T>, ISumAggregator, T>
			, ISumAggregator 
		where T : class { }
}
