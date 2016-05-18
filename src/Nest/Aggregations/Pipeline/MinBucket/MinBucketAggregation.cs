using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(ReadAsTypeJsonConverter<MinBucketAggregation>))]
	public interface IMinBucketAggregation : IPipelineAggregation { }

	public class MinBucketAggregation
		: PipelineAggregationBase, IMinBucketAggregation
	{
		public override string TypeName => "min_bucket";

		internal MinBucketAggregation () { }

		public MinBucketAggregation(string name, SingleBucketsPath bucketsPath)
			: base(name, bucketsPath) { }

		internal override void WrapInContainer(AggregationContainer c) => c.MinBucket = this;
	}

	public class MinBucketAggregationDescriptor
		: PipelineAggregationDescriptorBase<MinBucketAggregationDescriptor, IMinBucketAggregation, SingleBucketsPath>
		, IMinBucketAggregation
	{
		public override string TypeName => "min_bucket";
	}
}
