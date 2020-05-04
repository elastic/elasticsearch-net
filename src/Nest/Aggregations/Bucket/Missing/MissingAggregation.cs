// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
