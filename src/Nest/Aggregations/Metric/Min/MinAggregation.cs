using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(ReadAsTypeJsonConverter<MinAggregation>))]
	public interface IMinAggregation : IMetricAggregation { }

	public class MinAggregation : MetricAggregationBase, IMinAggregation
	{
		public override string TypeName => "min";

		internal MinAggregation() { }

		public MinAggregation(string name, Field field) : base(name, field) { }

		internal override void WrapInContainer(AggregationContainer c) => c.Min = this;
	}

	public class MinAggregationDescriptor<T>
		: MetricAggregationDescriptorBase<MinAggregationDescriptor<T>, IMinAggregation, T>
			, IMinAggregation
		where T : class
	{
		public override string TypeName => "min";
	}
}
