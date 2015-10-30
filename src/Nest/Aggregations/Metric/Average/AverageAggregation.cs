using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<AverageAggregation>))]
	public interface IAverageAggregation : IMetricAggregation { }

	public class AverageAggregation : MetricAggregation, IAverageAggregation
	{
		public AverageAggregation(string name, FieldName field) : base(name, field) { }

		internal override void WrapInContainer(AggregationContainer c) => c.Average = this;
	}

	public class AverageAggregationDescriptor<T> 
		: MetricAggregationDescriptorBase<AverageAggregationDescriptor<T>, IAverageAggregation, T>
			, IAverageAggregation 
		where T : class { }
}
