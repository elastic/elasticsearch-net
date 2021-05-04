// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public interface IPipelineAggregation : IAggregation
	{
		[DataMember(Name ="buckets_path")]
		IBucketsPath BucketsPath { get; set; }

		[DataMember(Name ="format")]
		string Format { get; set; }

		[DataMember(Name ="gap_policy")]
		GapPolicy? GapPolicy { get; set; }
	}

	public abstract class PipelineAggregationBase : AggregationBase, IPipelineAggregation
	{
		internal PipelineAggregationBase() { }

		public PipelineAggregationBase(string name, IBucketsPath bucketsPath) : base(name) => BucketsPath = bucketsPath;

		public IBucketsPath BucketsPath { get; set; }
		public string Format { get; set; }
		public GapPolicy? GapPolicy { get; set; }
	}

	public abstract class PipelineAggregationDescriptorBase<TPipelineAggregation, TPipelineAggregationInterface, TBucketsPath>
		: DescriptorBase<TPipelineAggregation, TPipelineAggregationInterface>, IPipelineAggregation
		where TPipelineAggregation : PipelineAggregationDescriptorBase<TPipelineAggregation, TPipelineAggregationInterface, TBucketsPath>
		, TPipelineAggregationInterface, IPipelineAggregation
		where TPipelineAggregationInterface : class, IPipelineAggregation
		where TBucketsPath : IBucketsPath
	{
		IBucketsPath IPipelineAggregation.BucketsPath { get; set; }
		string IPipelineAggregation.Format { get; set; }
		GapPolicy? IPipelineAggregation.GapPolicy { get; set; }

		IDictionary<string, object> IAggregation.Meta { get; set; }

		string IAggregation.Name { get; set; }

		public TPipelineAggregation Format(string format) => Assign(format, (a, v) => a.Format = v);

		public TPipelineAggregation GapPolicy(GapPolicy? gapPolicy) => Assign(gapPolicy, (a, v) => a.GapPolicy = v);

		public TPipelineAggregation BucketsPath(TBucketsPath bucketsPath) => Assign(bucketsPath, (a, v) => a.BucketsPath = v);

		public TPipelineAggregation Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector) =>
			Assign(selector, (a, v) => a.Meta = v?.Invoke(new FluentDictionary<string, object>()));
	}
}
