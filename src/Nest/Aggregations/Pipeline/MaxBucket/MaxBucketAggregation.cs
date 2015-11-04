using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<MaxBucketAggregation>))]
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
