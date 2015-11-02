using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<Ip4RangeAggregation>))]
	public interface IIp4RangeAggregation : IBucketAggregation
	{
		[JsonProperty("field")]
		FieldName Field { get; set; }

		[JsonProperty(PropertyName = "ranges")]
		IEnumerable<Ip4ExpressionRange> Ranges { get; set; }
	}

	public class Ip4RangeAggregation : BucketAggregationBase, IIp4RangeAggregation
	{
		public FieldName Field { get; set; }
		public IEnumerable<Ip4ExpressionRange> Ranges { get; set; }

		internal Ip4RangeAggregation() { }

		public Ip4RangeAggregation(string name) : base(name) { }

		internal override void WrapInContainer(AggregationContainer c) => c.IpRange = this;
	}

	public class Ip4RangeAggregationDescriptor<T> : 
		BucketAggregationDescriptorBase<Ip4RangeAggregationDescriptor<T>,IIp4RangeAggregation, T>
			, IIp4RangeAggregation 
		where T : class
	{

		FieldName IIp4RangeAggregation.Field { get; set; }

		IEnumerable<Ip4ExpressionRange> IIp4RangeAggregation.Ranges { get; set; }

		public Ip4RangeAggregationDescriptor<T> Field(string field) => Assign(a => a.Field = field);

		public Ip4RangeAggregationDescriptor<T> Field(Expression<Func<T, object>> field) => Assign(a => a.Field = field);

		public Ip4RangeAggregationDescriptor<T> Ranges(params Func<Ip4ExpressionRange, Ip4ExpressionRange>[] ranges) =>
			Assign(a => a.Ranges = (from range in ranges let r = new Ip4ExpressionRange() select range(r)).ToListOrNullIfEmpty());

	}
}