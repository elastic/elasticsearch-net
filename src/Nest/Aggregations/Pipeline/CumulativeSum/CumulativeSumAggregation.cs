using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(AggregationJsonConverter<CumulativeSumAggregation>))]
	public interface ICumulativeSumAggregation : IPipelineAggregation { }

	[AggregateType(typeof(ValueAggregate))]
	public class CumulativeSumAggregation
		: PipelineAggregationBase, ICumulativeSumAggregation
	{
		internal CumulativeSumAggregation () { }

		public CumulativeSumAggregation(string name, SingleBucketsPath bucketsPath)
			: base(name, bucketsPath) { }

		internal override void WrapInContainer(AggregationContainer c) => c.CumulativeSum = this;
	}

	[AggregateType(typeof(ValueAggregate))]
	public class CumulativeSumAggregationDescriptor
		: PipelineAggregationDescriptorBase<CumulativeSumAggregationDescriptor, ICumulativeSumAggregation, SingleBucketsPath>
		, ICumulativeSumAggregation
	{
	}
}
