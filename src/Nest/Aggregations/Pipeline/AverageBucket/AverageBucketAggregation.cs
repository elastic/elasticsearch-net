using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(ReadAsTypeJsonConverter<AverageBucketAggregation>))]
	public interface IAverageBucketAggregation : IPipelineAggregation { }

	public class AverageBucketAggregation
		: PipelineAggregationBase, IAverageBucketAggregation
	{
		public override string TypeName => "avg_bucket";

		internal AverageBucketAggregation () { }

		public AverageBucketAggregation(string name, SingleBucketsPath bucketsPath)
			: base(name, bucketsPath) { }

		internal override void WrapInContainer(AggregationContainer c) => c.AverageBucket = this;
	}

	public class AverageBucketAggregationDescriptor
		: PipelineAggregationDescriptorBase<AverageBucketAggregationDescriptor, IAverageBucketAggregation, SingleBucketsPath>
		, IAverageBucketAggregation
	{
		public override string TypeName => "avg_bucket";
	}
}
