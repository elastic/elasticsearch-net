// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(MaxBucketAggregation))]
	public interface IMaxBucketAggregation : IPipelineAggregation { }

	public class MaxBucketAggregation
		: PipelineAggregationBase, IMaxBucketAggregation
	{
		internal MaxBucketAggregation() { }

		public MaxBucketAggregation(string name, SingleBucketsPath bucketsPath)
			: base(name, bucketsPath) { }

		internal override void WrapInContainer(AggregationContainer c) => c.MaxBucket = this;
	}

	public class MaxBucketAggregationDescriptor
		: PipelineAggregationDescriptorBase<MaxBucketAggregationDescriptor, IMaxBucketAggregation, SingleBucketsPath>
			, IMaxBucketAggregation { }
}
