using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(AggregationJsonConverter<ExtendedStatsBucketAggregation>))]
	public interface IExtendedStatsBucketAggregation : IPipelineAggregation { }

	public class ExtendedStatsBucketAggregation
        : PipelineAggregationBase, IExtendedStatsBucketAggregation
    {
		internal ExtendedStatsBucketAggregation() { }

		public ExtendedStatsBucketAggregation(string name, SingleBucketsPath bucketsPath)
			: base(name, bucketsPath) { }

		internal override void WrapInContainer(AggregationContainer c) => c.ExtendedStatsBucket = this;
	}

	public class ExtendedStatsBucketAggregationDescriptor
        : PipelineAggregationDescriptorBase<ExtendedStatsBucketAggregationDescriptor, IExtendedStatsBucketAggregation, SingleBucketsPath>
		, IExtendedStatsBucketAggregation
    {
	}
}
