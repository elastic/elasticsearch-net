using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(AggregationJsonConverter<SumBucketAggregation>))]
	public interface ISumBucketAggregation : IPipelineAggregation { }

	public class SumBucketAggregation
		: PipelineAggregationBase, ISumBucketAggregation
	{
		internal SumBucketAggregation () { }

		public SumBucketAggregation(string name, SingleBucketsPath bucketsPath)
			: base(name, bucketsPath) { }

		internal override void WrapInContainer(AggregationContainer c) => c.SumBucket = this;
	}

	public class SumBucketAggregationDescriptor
		: PipelineAggregationDescriptorBase<SumBucketAggregationDescriptor, ISumBucketAggregation, SingleBucketsPath>
		, ISumBucketAggregation
	{
	}
}
