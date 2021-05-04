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
