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
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(VariableWidthHistogramAggregation))]
	public interface IVariableWidthHistogramAggregation : IBucketAggregation
	{
		/// <summary>
		/// The field to target.
		/// </summary>
		[DataMember(Name = "field")]
		Field Field { get; set; }

		[DataMember(Name = "buckets")]
		int? Buckets { get; set; }

		[DataMember(Name = "initial_buffer")]
		int? InitialBuffer { get; set; }
		
		[DataMember(Name = "shard_size")]
		int? ShardSize { get; set; }
	}

	public class VariableWidthHistogramAggregation : BucketAggregationBase, IVariableWidthHistogramAggregation
	{
		public VariableWidthHistogramAggregation(string name) : base(name) { }

		/// <inheritdoc />
		public Field Field { get; set; }
		/// <inheritdoc />
		public int? Buckets { get; set; }
		/// <inheritdoc />
		public int? InitialBuffer { get; set; }
		/// <inheritdoc />
		public int? ShardSize { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.VariableWidthHistogram = this;
	}

	public class VariableWidthHistogramAggregationDescriptor<T>
		: BucketAggregationDescriptorBase<VariableWidthHistogramAggregationDescriptor<T>, IVariableWidthHistogramAggregation, T>, IVariableWidthHistogramAggregation
		where T : class
	{
		Field IVariableWidthHistogramAggregation.Field { get; set; }
		int? IVariableWidthHistogramAggregation.Buckets { get; set; }
		int? IVariableWidthHistogramAggregation.InitialBuffer { get; set; }
		int? IVariableWidthHistogramAggregation.ShardSize { get; set; }

		/// <inheritdoc cref="IVariableWidthHistogramAggregation.Field" />
		public VariableWidthHistogramAggregationDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		/// <inheritdoc cref="IVariableWidthHistogramAggregation.Field" />
		public VariableWidthHistogramAggregationDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> field) => Assign(field, (a, v) => a.Field = v);

		/// <inheritdoc cref="IVariableWidthHistogramAggregation.Buckets" />
		public VariableWidthHistogramAggregationDescriptor<T> Buckets(int? buckets) => Assign(buckets, (a, v) => a.Buckets = v);

		/// <inheritdoc cref="IVariableWidthHistogramAggregation.InitialBuffer" />
		public VariableWidthHistogramAggregationDescriptor<T> InitialBuffer(int? initialBuffer) => Assign(initialBuffer, (a, v) => a.InitialBuffer = v);

		/// <inheritdoc cref="IVariableWidthHistogramAggregation.ShardSize" />
		public VariableWidthHistogramAggregationDescriptor<T> ShardSize(int? shardSize) => Assign(shardSize, (a, v) => a.ShardSize = v);
	}
}
