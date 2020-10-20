// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(SamplerAggregation))]
	public interface ISamplerAggregation : IBucketAggregation
	{
		[DataMember(Name ="shard_size")]
		int? ShardSize { get; set; }
	}

	public class SamplerAggregation : BucketAggregationBase, ISamplerAggregation
	{
		internal SamplerAggregation() { }

		public SamplerAggregation(string name) : base(name) { }

		public int? ShardSize { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.Sampler = this;
	}

	public class SamplerAggregationDescriptor<T>
		: BucketAggregationDescriptorBase<SamplerAggregationDescriptor<T>, ISamplerAggregation, T>, ISamplerAggregation
		where T : class
	{
		int? ISamplerAggregation.ShardSize { get; set; }

		public SamplerAggregationDescriptor<T> ShardSize(int? shardSize) => Assign(shardSize, (a, v) => a.ShardSize = v);
	}
}
