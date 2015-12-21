using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(AggregationJsonConverter<MaxBucketAggregation>))]
	public interface IMaxBucketAggregation : IPipelineAggregation { }

	public class MaxBucketAggregation
		: PipelineAggregationBase, IMaxBucketAggregation
	{
		internal MaxBucketAggregation () { }

		public MaxBucketAggregation(string name, SingleBucketsPath bucketsPath)
			: base(name, bucketsPath) { }

		internal override void WrapInContainer(AggregationContainer c) => c.MaxBucket = this;
	}

	public class MaxBucketAggregationDescriptor
		: PipelineAggregationDescriptorBase<MaxBucketAggregationDescriptor, IMaxBucketAggregation, SingleBucketsPath>
		, IMaxBucketAggregation
	{
	}
}
