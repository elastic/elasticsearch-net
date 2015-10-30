using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IAverageBucketAggregation : IPipelineAggregation { }

	public class AverageBucketAggregation
		: PipelineAggregationBase, IAverageBucketAggregation
	{
		public AverageBucketAggregation(string name, string bucketsPath)
			: base(name, bucketsPath) { }

		internal override void WrapInContainer(AggregationContainer c) => c.AverageBucket = this;
	}

	public class AverageBucketAggregationDescriptor
		: PipelineAggregationDescriptorBase<AverageBucketAggregationDescriptor, IAverageBucketAggregation>, IAverageBucketAggregation
	{
	}
}
