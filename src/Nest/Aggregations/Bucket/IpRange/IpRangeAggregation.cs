// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(IpRangeAggregation))]
	public interface IIpRangeAggregation : IBucketAggregation
	{
		[DataMember(Name ="field")]
		Field Field { get; set; }

		[DataMember(Name ="ranges")]
		IEnumerable<IIpRangeAggregationRange> Ranges { get; set; }
	}

	public class IpRangeAggregation : BucketAggregationBase, IIpRangeAggregation
	{
		internal IpRangeAggregation() { }

		public IpRangeAggregation(string name) : base(name) { }

		public Field Field { get; set; }
		public IEnumerable<IIpRangeAggregationRange> Ranges { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.IpRange = this;
	}

	public class IpRangeAggregationDescriptor<T>
		: BucketAggregationDescriptorBase<IpRangeAggregationDescriptor<T>, IIpRangeAggregation, T>
			, IIpRangeAggregation
		where T : class
	{
		Field IIpRangeAggregation.Field { get; set; }

		IEnumerable<IIpRangeAggregationRange> IIpRangeAggregation.Ranges { get; set; }

		public IpRangeAggregationDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		public IpRangeAggregationDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> field) => Assign(field, (a, v) => a.Field = v);

		public IpRangeAggregationDescriptor<T> Ranges(params Func<IpRangeAggregationRangeDescriptor, IIpRangeAggregationRange>[] ranges) =>
			Assign(ranges?.Select(r => r(new IpRangeAggregationRangeDescriptor())), (a, v) => a.Ranges = v);
	}
}
