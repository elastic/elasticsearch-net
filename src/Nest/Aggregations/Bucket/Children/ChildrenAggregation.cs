using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<ChildrenAggregator>))]
	public interface IChildrenAggregator : IBucketAggregator
	{
		[JsonProperty("type")]
		TypeNameMarker Type { get; set; }
	}

	public class ChildrenAggregator : BucketAggregator, IChildrenAggregator
	{
		public TypeNameMarker Type { get; set; }
	}

	public class ChildrenAggregationDescriptor<T> 
		: BucketAggregationBaseDescriptor<ChildrenAggregationDescriptor<T>, IChildrenAggregator, T>, IChildrenAggregator
		where T : class
	{

		TypeNameMarker IChildrenAggregator.Type { get; set; } = typeof(T);

		public ChildrenAggregationDescriptor<T> Type(TypeNameMarker type) =>
			Assign(a => a.Type = type);

		public ChildrenAggregationDescriptor<T> Type<TChildType>() where TChildType : class =>
			Assign(a => a.Type = typeof(TChildType));
	}
}
