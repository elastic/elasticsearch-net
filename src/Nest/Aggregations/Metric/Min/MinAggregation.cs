using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(AggregationJsonConverter<MinAggregation>))]
	public interface IMinAggregation : IMetricAggregation { }

	[AggregateType(typeof(ValueAggregate))]
	public class MinAggregation : MetricAggregationBase, IMinAggregation
	{
		internal MinAggregation() { }

		public MinAggregation(string name, Field field) : base(name, field) { }

		internal override void WrapInContainer(AggregationContainer c) => c.Min = this;
	}

	[AggregateType(typeof(ValueAggregate))]
	public class MinAggregationDescriptor<T> 
		: MetricAggregationDescriptorBase<MinAggregationDescriptor<T>, IMinAggregation, T>
			, IMinAggregation 
		where T : class { }
}
