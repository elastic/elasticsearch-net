using Nest.Resolvers.Converters;
using Newtonsoft.Json;

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
		: BucketAggregationBaseDescriptor<ChildrenAggregationDescriptor<T>, T>, IChildrenAggregator
		where T : class
	{
		public ChildrenAggregationDescriptor()
		{
			this.Self.Type = typeof(T);
		}

		IChildrenAggregator Self { get { return this; } }
		
		TypeNameMarker IChildrenAggregator.Type { get; set; }

		public ChildrenAggregationDescriptor<T> Type(TypeNameMarker type)
		{
			this.Self.Type = type;
			return this;
		}

		public ChildrenAggregationDescriptor<T> Type<K>() 
			where K : class
		{
			this.Self.Type = typeof(K);
			return this;
		}
	}
}
