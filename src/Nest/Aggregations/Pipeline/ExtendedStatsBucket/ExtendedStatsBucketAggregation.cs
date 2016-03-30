using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(AggregationJsonConverter<ExtendedStatsBucketAggregation>))]
	public interface IExtendedStatsBucketAggregation : IPipelineAggregation
	{
		[JsonProperty("sigma")]
		int? Sigma { get; set; }
	}

	public class ExtendedStatsBucketAggregation
		: PipelineAggregationBase, IExtendedStatsBucketAggregation
	{
		internal ExtendedStatsBucketAggregation() { }

		public ExtendedStatsBucketAggregation(string name, SingleBucketsPath bucketsPath)
			: base(name, bucketsPath)
		{ }

		public int? Sigma { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.ExtendedStatsBucket = this;
	}

	public class ExtendedStatsBucketAggregationDescriptor
		: PipelineAggregationDescriptorBase<ExtendedStatsBucketAggregationDescriptor, IExtendedStatsBucketAggregation, SingleBucketsPath>
		, IExtendedStatsBucketAggregation
	{

		int? IExtendedStatsBucketAggregation.Sigma { get; set; }

		public ExtendedStatsBucketAggregationDescriptor Sigma(int? sigma) => Assign(a => a.Sigma = sigma);

	}
}
