// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(StatsBucketAggregation))]
	public interface IStatsBucketAggregation : IPipelineAggregation { }

	public class StatsBucketAggregation
		: PipelineAggregationBase, IStatsBucketAggregation
	{
		internal StatsBucketAggregation() { }

		public StatsBucketAggregation(string name, SingleBucketsPath bucketsPath)
			: base(name, bucketsPath) { }

		internal override void WrapInContainer(AggregationContainer c) => c.StatsBucket = this;
	}

	public class StatsBucketAggregationDescriptor
		: PipelineAggregationDescriptorBase<StatsBucketAggregationDescriptor, IStatsBucketAggregation, SingleBucketsPath>
			, IStatsBucketAggregation { }
}
