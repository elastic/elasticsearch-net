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
	[ReadAs(typeof(MissingAggregation))]
	public interface IMissingAggregation : IBucketAggregation
	{
		[DataMember(Name ="field")]
		Field Field { get; set; }
	}

	public class MissingAggregation : BucketAggregationBase, IMissingAggregation
	{
		internal MissingAggregation() { }

		public MissingAggregation(string name) : base(name) { }

		public Field Field { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.Missing = this;
	}

	public class MissingAggregationDescriptor<T>
		: BucketAggregationDescriptorBase<MissingAggregationDescriptor<T>, IMissingAggregation, T>
			, IMissingAggregation
		where T : class
	{
		Field IMissingAggregation.Field { get; set; }

		public MissingAggregationDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		public MissingAggregationDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> field) => Assign(field, (a, v) => a.Field = v);
	}
}
