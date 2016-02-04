﻿using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(AggregationJsonConverter<AverageAggregation>))]
	public interface IAverageAggregation : IMetricAggregation { }

	[AggregateType(typeof(ValueAggregate))]
	public class AverageAggregation : MetricAggregationBase, IAverageAggregation
	{
		internal AverageAggregation() { }

		public AverageAggregation(string name, Field field) : base(name, field) { }

		internal override void WrapInContainer(AggregationContainer c) => c.Average = this;
	}

	[AggregateType(typeof(ValueAggregate))]
	public class AverageAggregationDescriptor<T> 
		: MetricAggregationDescriptorBase<AverageAggregationDescriptor<T>, IAverageAggregation, T>
			, IAverageAggregation 
		where T : class { }
}
