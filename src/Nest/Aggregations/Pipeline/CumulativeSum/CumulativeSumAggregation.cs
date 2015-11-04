using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<CumulativeSumAggregation>))]
	public interface ICumulativeSumAggregation : IPipelineAggregation { }

	public class CumulativeSumAggregation
		: PipelineAggregationBase, ICumulativeSumAggregation
	{
		internal CumulativeSumAggregation () { }

		public CumulativeSumAggregation(string name, SingleBucketsPath bucketsPath)
			: base(name, bucketsPath) { }

		internal override void WrapInContainer(AggregationContainer c) => c.CumulativeSum = this;
	}

	public class CumulativeSumAggregationDescriptor
		: PipelineAggregationDescriptorBase<CumulativeSumAggregationDescriptor, ICumulativeSumAggregation, SingleBucketsPath>
		, ICumulativeSumAggregation
	{
	}
}
