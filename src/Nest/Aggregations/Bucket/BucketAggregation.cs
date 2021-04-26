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
using System.Collections.Generic;

namespace Nest
{
	public interface IBucketAggregation : IAggregation
	{
		AggregationDictionary Aggregations { get; set; }
	}

	public abstract class BucketAggregationBase : AggregationBase, IBucketAggregation
	{
		internal BucketAggregationBase() { }

		protected BucketAggregationBase(string name) : base(name) { }

		public AggregationDictionary Aggregations { get; set; }
	}

	public abstract class BucketAggregationDescriptorBase<TBucketAggregation, TBucketAggregationInterface, T>
		: IBucketAggregation, IDescriptor
		where TBucketAggregation : BucketAggregationDescriptorBase<TBucketAggregation, TBucketAggregationInterface, T>
		, TBucketAggregationInterface, IBucketAggregation
		where T : class
		where TBucketAggregationInterface : class, IBucketAggregation
	{
		protected TBucketAggregationInterface Self => (TBucketAggregation)this;
		AggregationDictionary IBucketAggregation.Aggregations { get; set; }

		IDictionary<string, object> IAggregation.Meta { get; set; }

		string IAggregation.Name { get; set; }

		protected TBucketAggregation Assign<TValue>(TValue value, Action<TBucketAggregationInterface, TValue> assigner) =>
			Fluent.Assign((TBucketAggregation)this, value, assigner);

		public TBucketAggregation Aggregations(Func<AggregationContainerDescriptor<T>, IAggregationContainer> selector) =>
			Assign(selector, (a, v) => a.Aggregations = v?.Invoke(new AggregationContainerDescriptor<T>())?.Aggregations);

		public TBucketAggregation Aggregations(AggregationDictionary aggregations) =>
			Assign(aggregations, (a, v) => a.Aggregations = v);

		public TBucketAggregation Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector) =>
			Assign(selector, (a, v) => a.Meta = v?.Invoke(new FluentDictionary<string, object>()));
	}
}
