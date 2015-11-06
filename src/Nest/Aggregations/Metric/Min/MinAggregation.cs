using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<MinAggregation>))]
	public interface IMinAggregation : IMetricAggregation { }

	public class MinAggregation : MetricAggregationBase, IMinAggregation
	{
		internal MinAggregation() { }

		public MinAggregation(string name, Field field) : base(name, field) { }

		internal override void WrapInContainer(AggregationContainer c) => c.Min = this;
	}

	public class MinAggregationDescriptor<T> 
		: MetricAggregationDescriptorBase<MinAggregationDescriptor<T>, IMinAggregation, T>
			, IMinAggregation 
		where T : class { }
}
