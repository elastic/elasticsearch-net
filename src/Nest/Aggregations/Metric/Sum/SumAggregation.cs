using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(AggregationJsonConverter<SumAggregation>))]
	public interface ISumAggregation : IMetricAggregation { }

	[AggregateType(typeof(ValueAggregate))]
	public class SumAggregation : MetricAggregationBase, ISumAggregation
	{
		internal SumAggregation() { }

		public SumAggregation(string name, Field field) : base(name, field) { }

		internal override void WrapInContainer(AggregationContainer c) => c.Sum = this;
	}

	[AggregateType(typeof(ValueAggregate))]
	public class SumAggregationDescriptor<T> 
		: MetricAggregationDescriptorBase<SumAggregationDescriptor<T>, ISumAggregation, T>
			, ISumAggregation 
		where T : class { }
}
