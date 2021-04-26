/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(DiversifiedSamplerAggregation))]
	public interface IDiversifiedSamplerAggregation : IBucketAggregation
	{
		[DataMember(Name ="execution_hint")]
		DiversifiedSamplerAggregationExecutionHint? ExecutionHint { get; set; }

		[DataMember(Name = "field")]
		Field Field { get; set; }

		[DataMember(Name ="max_docs_per_value")]
		int? MaxDocsPerValue { get; set; }

		[DataMember(Name ="script")]
		IScript Script { get; set; }

		[DataMember(Name ="shard_size")]
		int? ShardSize { get; set; }
	}

	public class DiversifiedSamplerAggregation : BucketAggregationBase, IDiversifiedSamplerAggregation
	{
		internal DiversifiedSamplerAggregation() { }

		public DiversifiedSamplerAggregation(string name) : base(name) { }

		public DiversifiedSamplerAggregationExecutionHint? ExecutionHint { get; set; }
		public Field Field { get; set; }
		public int? MaxDocsPerValue { get; set; }
		public IScript Script { get; set; }
		public int? ShardSize { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.DiversifiedSampler = this;
	}

	public class DiversifiedSamplerAggregationDescriptor<T>
		: BucketAggregationDescriptorBase<DiversifiedSamplerAggregationDescriptor<T>, IDiversifiedSamplerAggregation, T>, IDiversifiedSamplerAggregation
		where T : class
	{
		DiversifiedSamplerAggregationExecutionHint? IDiversifiedSamplerAggregation.ExecutionHint { get; set; }
		Field IDiversifiedSamplerAggregation.Field { get; set; }
		int? IDiversifiedSamplerAggregation.MaxDocsPerValue { get; set; }
		IScript IDiversifiedSamplerAggregation.Script { get; set; }
		int? IDiversifiedSamplerAggregation.ShardSize { get; set; }

		public DiversifiedSamplerAggregationDescriptor<T> ExecutionHint(DiversifiedSamplerAggregationExecutionHint? executionHint) =>
			Assign(executionHint, (a, v) => a.ExecutionHint = v);

		public DiversifiedSamplerAggregationDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		public DiversifiedSamplerAggregationDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> field) => Assign(field, (a, v) => a.Field = v);

		public DiversifiedSamplerAggregationDescriptor<T> MaxDocsPerValue(int? maxDocs) => Assign(maxDocs, (a, v) => a.MaxDocsPerValue = v);

		public DiversifiedSamplerAggregationDescriptor<T> Script(string script) => Assign((InlineScript)script, (a, v) => a.Script = v);

		public DiversifiedSamplerAggregationDescriptor<T> Script(Func<ScriptDescriptor, IScript> scriptSelector) =>
			Assign(scriptSelector, (a, v) => a.Script = v?.Invoke(new ScriptDescriptor()));

		public DiversifiedSamplerAggregationDescriptor<T> ShardSize(int? shardSize) => Assign(shardSize, (a, v) => a.ShardSize = v);
	}
}
