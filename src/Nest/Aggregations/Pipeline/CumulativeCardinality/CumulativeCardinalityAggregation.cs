// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(CumulativeCardinalityAggregation))]
	public interface ICumulativeCardinalityAggregation : IPipelineAggregation { }

	public class CumulativeCardinalityAggregation
		: PipelineAggregationBase, ICumulativeCardinalityAggregation
	{
		internal CumulativeCardinalityAggregation() { }

		public CumulativeCardinalityAggregation(string name, SingleBucketsPath bucketsPath)
			: base(name, bucketsPath) { }

		internal override void WrapInContainer(AggregationContainer c) => c.CumulativeCardinality = this;
	}

	public class CumulativeCardinalityAggregationDescriptor
		: PipelineAggregationDescriptorBase<CumulativeCardinalityAggregationDescriptor, ICumulativeCardinalityAggregation, SingleBucketsPath>
			, ICumulativeCardinalityAggregation { }
}
