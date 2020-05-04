// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(SamplerAggregation))]
	public interface ISamplerAggregation : IBucketAggregation
	{
		[DataMember(Name ="execution_hint")]
		SamplerAggregationExecutionHint? ExecutionHint { get; set; }

		[DataMember(Name ="max_docs_per_value")]
		int? MaxDocsPerValue { get; set; }

		[DataMember(Name ="script")]
		IScript Script { get; set; }

		[DataMember(Name ="shard_size")]
		int? ShardSize { get; set; }
	}

	public class SamplerAggregation : BucketAggregationBase, ISamplerAggregation
	{
		internal SamplerAggregation() { }

		public SamplerAggregation(string name) : base(name) { }

		public SamplerAggregationExecutionHint? ExecutionHint { get; set; }
		public int? MaxDocsPerValue { get; set; }
		public IScript Script { get; set; }
		public int? ShardSize { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.Sampler = this;
	}

	public class SamplerAggregationDescriptor<T>
		: BucketAggregationDescriptorBase<SamplerAggregationDescriptor<T>, ISamplerAggregation, T>, ISamplerAggregation
		where T : class
	{
		SamplerAggregationExecutionHint? ISamplerAggregation.ExecutionHint { get; set; }
		int? ISamplerAggregation.MaxDocsPerValue { get; set; }
		IScript ISamplerAggregation.Script { get; set; }
		int? ISamplerAggregation.ShardSize { get; set; }

		public SamplerAggregationDescriptor<T> ExecutionHint(SamplerAggregationExecutionHint? executionHint) =>
			Assign(executionHint, (a, v) => a.ExecutionHint = v);

		public SamplerAggregationDescriptor<T> MaxDocsPerValue(int? maxDocs) => Assign(maxDocs, (a, v) => a.MaxDocsPerValue = v);

		public SamplerAggregationDescriptor<T> Script(string script) => Assign((InlineScript)script, (a, v) => a.Script = v);

		public SamplerAggregationDescriptor<T> Script(Func<ScriptDescriptor, IScript> scriptSelector) =>
			Assign(scriptSelector, (a, v) => a.Script = v?.Invoke(new ScriptDescriptor()));

		public SamplerAggregationDescriptor<T> ShardSize(int? shardSize) => Assign(shardSize, (a, v) => a.ShardSize = v);
	}
}
