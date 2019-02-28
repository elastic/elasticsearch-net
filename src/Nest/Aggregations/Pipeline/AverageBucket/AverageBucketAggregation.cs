using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(AverageBucketAggregation))]
	public interface IAverageBucketAggregation : IPipelineAggregation { }

	public class AverageBucketAggregation
		: PipelineAggregationBase, IAverageBucketAggregation
	{
		internal AverageBucketAggregation() { }

		public AverageBucketAggregation(string name, SingleBucketsPath bucketsPath)
			: base(name, bucketsPath) { }

		internal override void WrapInContainer(AggregationContainer c) => c.AverageBucket = this;
	}

	public class AverageBucketAggregationDescriptor
		: PipelineAggregationDescriptorBase<AverageBucketAggregationDescriptor, IAverageBucketAggregation, SingleBucketsPath>
			, IAverageBucketAggregation { }
}
