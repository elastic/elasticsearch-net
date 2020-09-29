// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(CumulativeSumAggregation))]
	public interface ICumulativeSumAggregation : IPipelineAggregation { }

	public class CumulativeSumAggregation
		: PipelineAggregationBase, ICumulativeSumAggregation
	{
		internal CumulativeSumAggregation() { }

		public CumulativeSumAggregation(string name, SingleBucketsPath bucketsPath)
			: base(name, bucketsPath) { }

		internal override void WrapInContainer(AggregationContainer c) => c.CumulativeSum = this;
	}

	public class CumulativeSumAggregationDescriptor
		: PipelineAggregationDescriptorBase<CumulativeSumAggregationDescriptor, ICumulativeSumAggregation, SingleBucketsPath>
			, ICumulativeSumAggregation { }
}
