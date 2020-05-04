// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(AverageBucketAggregation))]
	public interface IAverageBucketAggregation : IPipelineAggregation { }

	public class AverageBucketAggregation
		: PipelineAggregationBase, IAverageBucketAggregation
	{
		internal AverageBucketAggregation() { }

		public AverageBucketAggregation(string name, SingleBucketsPath bucketsPath)
			: base(name, bucketsPath) { }

		internal override void WrapInContainer(AggregationContainer c) => c.AverageBucket = this;
	}

	public class AverageBucketAggregationDescriptor
		: PipelineAggregationDescriptorBase<AverageBucketAggregationDescriptor, IAverageBucketAggregation, SingleBucketsPath>
			, IAverageBucketAggregation { }
}
