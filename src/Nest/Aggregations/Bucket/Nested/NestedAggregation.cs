// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(NestedAggregation))]
	public interface INestedAggregation : IBucketAggregation
	{
		[DataMember(Name ="path")]
		Field Path { get; set; }
	}

	public class NestedAggregation : BucketAggregationBase, INestedAggregation
	{
		internal NestedAggregation() { }

		public NestedAggregation(string name) : base(name) { }

		public Field Path { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.Nested = this;
	}

	public class NestedAggregationDescriptor<T>
		: BucketAggregationDescriptorBase<NestedAggregationDescriptor<T>, INestedAggregation, T>
			, INestedAggregation
		where T : class
	{
		Field INestedAggregation.Path { get; set; }

		public NestedAggregationDescriptor<T> Path(Field path) => Assign(path, (a, v) => a.Path = v);

		public NestedAggregationDescriptor<T> Path<TValue>(Expression<Func<T, TValue>> path) => Assign(path, (a, v) => a.Path = v);
	}
}
