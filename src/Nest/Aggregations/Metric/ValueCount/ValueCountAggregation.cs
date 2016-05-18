using Newtonsoft.Json;

namespace Nest
{

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(ReadAsTypeJsonConverter<ValueCountAggregation>))]
	public interface IValueCountAggregation : IMetricAggregation { }

	public class ValueCountAggregation : MetricAggregationBase, IValueCountAggregation
	{
		public override string TypeName => "value_count";

		internal ValueCountAggregation() { }

		public ValueCountAggregation(string name, Field field) : base(name, field) { }

		internal override void WrapInContainer(AggregationContainer c) => c.ValueCount = this;
	}

	public class ValueCountAggregationDescriptor<T>
		: MetricAggregationDescriptorBase<ValueCountAggregationDescriptor<T>, IValueCountAggregation, T>
			, IValueCountAggregation
		where T : class
	{
		public override string TypeName => "value_count";
	}
}
