using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(ReadAsTypeJsonConverter<CumulativeSumAggregation>))]
	public interface ICumulativeSumAggregation : IPipelineAggregation { }

	public class CumulativeSumAggregation
		: PipelineAggregationBase, ICumulativeSumAggregation
	{
		public override string TypeName => "cumulative_sum";

		internal CumulativeSumAggregation () { }

		public CumulativeSumAggregation(string name, SingleBucketsPath bucketsPath)
			: base(name, bucketsPath) { }

		internal override void WrapInContainer(AggregationContainer c) => c.CumulativeSum = this;
	}

	public class CumulativeSumAggregationDescriptor
		: PipelineAggregationDescriptorBase<CumulativeSumAggregationDescriptor, ICumulativeSumAggregation, SingleBucketsPath>
		, ICumulativeSumAggregation
	{
		public override string TypeName => "cumulative_sum";
	}
}
