// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(SumBucketAggregation))]
	public interface ISumBucketAggregation : IPipelineAggregation { }

	public class SumBucketAggregation
		: PipelineAggregationBase, ISumBucketAggregation
	{
		internal SumBucketAggregation() { }

		public SumBucketAggregation(string name, SingleBucketsPath bucketsPath)
			: base(name, bucketsPath) { }

		internal override void WrapInContainer(AggregationContainer c) => c.SumBucket = this;
	}

	public class SumBucketAggregationDescriptor
		: PipelineAggregationDescriptorBase<SumBucketAggregationDescriptor, ISumBucketAggregation, SingleBucketsPath>
			, ISumBucketAggregation { }
}
