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
	[ReadAs(typeof(ReverseNestedAggregation))]
	public interface IReverseNestedAggregation : IBucketAggregation
	{
		[DataMember(Name ="path")]
		Field Path { get; set; }
	}

	public class ReverseNestedAggregation : BucketAggregationBase, IReverseNestedAggregation
	{
		internal ReverseNestedAggregation() { }

		public ReverseNestedAggregation(string name) : base(name) { }

		[DataMember(Name ="path")]
		public Field Path { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.ReverseNested = this;
	}

	public class ReverseNestedAggregationDescriptor<T>
		: BucketAggregationDescriptorBase<ReverseNestedAggregationDescriptor<T>, IReverseNestedAggregation, T>
			, IReverseNestedAggregation
		where T : class
	{
		Field IReverseNestedAggregation.Path { get; set; }

		public ReverseNestedAggregationDescriptor<T> Path(Field path) => Assign(path, (a, v) => a.Path = v);

		public ReverseNestedAggregationDescriptor<T> Path<TValue>(Expression<Func<T, TValue>> path) => Assign(path, (a, v) => a.Path = v);
	}
}
