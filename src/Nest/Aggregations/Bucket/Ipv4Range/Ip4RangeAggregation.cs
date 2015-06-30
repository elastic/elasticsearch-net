using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<Ip4RangeAggregator>))]
	public interface IIp4RangeAggregator : IBucketAggregator
	{
		[JsonProperty("field")]
		PropertyPathMarker Field { get; set; }

		[JsonProperty(PropertyName = "ranges")]
		IEnumerable<Ip4ExpressionRange> Ranges { get; set; }
	}

	public class Ip4RangeAggregator : BucketAggregator, IIp4RangeAggregator
	{
		public PropertyPathMarker Field { get; set; }
		public IEnumerable<Ip4ExpressionRange> Ranges { get; set; }
	}

	public class Ip4RangeAggregatorDescriptor<T> : 
		BucketAggregatorBaseDescriptor<Ip4RangeAggregatorDescriptor<T>,IIp4RangeAggregator, T>
			, IIp4RangeAggregator 
		where T : class
	{

		PropertyPathMarker IIp4RangeAggregator.Field { get; set; }

		IEnumerable<Ip4ExpressionRange> IIp4RangeAggregator.Ranges { get; set; }

		public Ip4RangeAggregatorDescriptor<T> Field(string field) => Assign(a => a.Field = field);

		public Ip4RangeAggregatorDescriptor<T> Field(Expression<Func<T, object>> field) => Assign(a => a.Field = field);

		public Ip4RangeAggregatorDescriptor<T> Ranges(params Func<Ip4ExpressionRange, Ip4ExpressionRange>[] ranges) =>
			Assign(a => a.Ranges = (from range in ranges let r = new Ip4ExpressionRange() select range(r)).ToListOrNullIfEmpty());

	}
}