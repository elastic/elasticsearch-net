using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(AggregationJsonConverter<SumBucketAggregation>))]
	public interface ISumBucketAggregation : IPipelineAggregation { }

	[AggregateType(typeof(ValueAggregate))]
	public class SumBucketAggregation
		: PipelineAggregationBase, ISumBucketAggregation
	{
		internal SumBucketAggregation () { }

		public SumBucketAggregation(string name, SingleBucketsPath bucketsPath)
			: base(name, bucketsPath) { }

		internal override void WrapInContainer(AggregationContainer c) => c.SumBucket = this;
	}

	[AggregateType(typeof(ValueAggregate))]
	public class SumBucketAggregationDescriptor
		: PipelineAggregationDescriptorBase<SumBucketAggregationDescriptor, ISumBucketAggregation, SingleBucketsPath>
		, ISumBucketAggregation
	{
	}
}
