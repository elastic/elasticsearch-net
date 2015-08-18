using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<MinAggregator>))]
	public interface IMinAggregator : IMetricAggregator { }

	public class MinAggregator : MetricAggregator, IMinAggregator { }

	public class MinAgg : MetricAgg, IMinAggregator
	{
		public MinAgg(string name, FieldName field) : base(name, field) { }

		internal override void WrapInContainer(AggregationContainer c) => c.Min = this;
	}

	public class MinAggregatorDescriptor<T> 
		: MetricAggregationBaseDescriptor<MinAggregatorDescriptor<T>, IMinAggregator, T>
			, IMinAggregator 
		where T : class { }

}
