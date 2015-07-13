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
		TypeName Type { get; set; }
	}

	public class ChildrenAggregator : BucketAggregator, IChildrenAggregator
	{
		public TypeName Type { get; set; }
	}

	public class ChildrenAgg : BucketAgg, IChildrenAggregator
	{
		public TypeName Type { get; set; }

		public ChildrenAgg(string name) : base(name) { }
	}

	public class ChildrenAggregatorDescriptor<T> 
		: BucketAggregatorBaseDescriptor<ChildrenAggregatorDescriptor<T>, IChildrenAggregator, T>, IChildrenAggregator
		where T : class
	{
		TypeName IChildrenAggregator.Type { get; set; } = typeof(T);

		public ChildrenAggregatorDescriptor<T> Type(TypeName type) =>
			Assign(a => a.Type = type);

		public ChildrenAggregatorDescriptor<T> Type<TChildType>() where TChildType : class =>
			Assign(a => a.Type = typeof(TChildType));
	}
}
