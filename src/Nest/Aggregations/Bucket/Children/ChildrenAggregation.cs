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

using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(ChildrenAggregation))]
	public interface IChildrenAggregation : IBucketAggregation
	{
		[DataMember(Name ="type")]
		RelationName Type { get; set; }
	}

	public class ChildrenAggregation : BucketAggregationBase, IChildrenAggregation
	{
		internal ChildrenAggregation() { }

		public ChildrenAggregation(string name, RelationName type) : base(name) => Type = type;

		public RelationName Type { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.Children = this;
	}

	public class ChildrenAggregationDescriptor<T>
		: BucketAggregationDescriptorBase<ChildrenAggregationDescriptor<T>, IChildrenAggregation, T>, IChildrenAggregation
		where T : class
	{
		RelationName IChildrenAggregation.Type { get; set; } = typeof(T);

		public ChildrenAggregationDescriptor<T> Type(RelationName type) =>
			Assign(type, (a, v) => a.Type = v);

		public ChildrenAggregationDescriptor<T> Type<TChildType>() where TChildType : class =>
			Assign(typeof(TChildType), (a, v) => a.Type = v);
	}
}
