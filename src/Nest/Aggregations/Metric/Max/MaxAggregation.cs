using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(ReadAsTypeJsonConverter<MaxAggregation>))]
	public interface IMaxAggregation : IMetricAggregation { }

	public class MaxAggregation : MetricAggregationBase, IMaxAggregation
	{
		public override string TypeName => "max";

		internal MaxAggregation() { }

		public MaxAggregation(string name, Field field) : base(name, field) { }

		internal override void WrapInContainer(AggregationContainer c) => c.Max = this;
	}

	public class MaxAggregationDescriptor<T>
		: MetricAggregationDescriptorBase<MaxAggregationDescriptor<T>, IMaxAggregation, T>
			, IMaxAggregation
		where T : class
	{
		public override string TypeName => "max";
	}
}
