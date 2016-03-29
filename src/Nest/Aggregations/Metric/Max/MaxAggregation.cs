using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(AggregationJsonConverter<MaxAggregation>))]
	public interface IMaxAggregation : IMetricAggregation { }

	public class MaxAggregation : MetricAggregationBase, IMaxAggregation
	{
		internal MaxAggregation() { }

		public MaxAggregation(string name, Field field) : base(name, field) { }

		internal override void WrapInContainer(AggregationContainer c) => c.Max = this;
	}

	public class MaxAggregationDescriptor<T> 
		: MetricAggregationDescriptorBase<MaxAggregationDescriptor<T>, IMaxAggregation, T>
			, IMaxAggregation 
		where T : class { }
}
