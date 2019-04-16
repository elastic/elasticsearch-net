using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(MinBucketAggregation))]
	public interface IMinBucketAggregation : IPipelineAggregation { }

	public class MinBucketAggregation
		: PipelineAggregationBase, IMinBucketAggregation
	{
		internal MinBucketAggregation() { }

		public MinBucketAggregation(string name, SingleBucketsPath bucketsPath)
			: base(name, bucketsPath) { }

		internal override void WrapInContainer(AggregationContainer c) => c.MinBucket = this;
	}

	public class MinBucketAggregationDescriptor
		: PipelineAggregationDescriptorBase<MinBucketAggregationDescriptor, IMinBucketAggregation, SingleBucketsPath>
			, IMinBucketAggregation { }
}
