using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(ReadAsTypeJsonConverter<SumBucketAggregation>))]
	public interface ISumBucketAggregation : IPipelineAggregation { }

	public class SumBucketAggregation
		: PipelineAggregationBase, ISumBucketAggregation
	{
		public override string TypeName => "sum_bucket";

		internal SumBucketAggregation () { }

		public SumBucketAggregation(string name, SingleBucketsPath bucketsPath)
			: base(name, bucketsPath) { }

		internal override void WrapInContainer(AggregationContainer c) => c.SumBucket = this;
	}

	public class SumBucketAggregationDescriptor
		: PipelineAggregationDescriptorBase<SumBucketAggregationDescriptor, ISumBucketAggregation, SingleBucketsPath>
		, ISumBucketAggregation
	{
		public override string TypeName => "sum_bucket";
	}
}
