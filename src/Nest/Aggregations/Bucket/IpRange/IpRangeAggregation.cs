using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(AggregationJsonConverter<IpRangeAggregation>))]
	public interface IIpRangeAggregation : IBucketAggregation
	{
		[JsonProperty("field")]
		Field Field { get; set; }

		[JsonProperty(PropertyName = "ranges")]
		IEnumerable<IIpRange> Ranges { get; set; }
	}

	public class IpRangeAggregation : BucketAggregationBase, IIpRangeAggregation
	{
		public Field Field { get; set; }
		public IEnumerable<IIpRange> Ranges { get; set; }

		internal IpRangeAggregation() { }

		public IpRangeAggregation(string name) : base(name) { }

		internal override void WrapInContainer(AggregationContainer c) => c.IpRange = this;
	}

	public class IpRangeAggregationDescriptor<T> :
		BucketAggregationDescriptorBase<IpRangeAggregationDescriptor<T>,IIpRangeAggregation, T>
			, IIpRangeAggregation
		where T : class
	{
		Field IIpRangeAggregation.Field { get; set; }

		IEnumerable<IIpRange> IIpRangeAggregation.Ranges { get; set; }

		public IpRangeAggregationDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		public IpRangeAggregationDescriptor<T> Field(Expression<Func<T, object>> field) => Assign(a => a.Field = field);

		public IpRangeAggregationDescriptor<T> Ranges(params Func<IpRangeDescriptor, IIpRange>[] ranges) =>
			Assign(a => a.Ranges = ranges?.Select(r => r(new IpRangeDescriptor())));
	}
}
