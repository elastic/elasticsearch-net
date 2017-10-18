using Newtonsoft.Json;

// ReSharper disable UnusedMember.Global

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(AggregationJsonConverter<ChildrenAggregation>))]
	public interface IChildrenAggregation : IBucketAggregation
	{
		[JsonProperty("type")]
		RelationName Type { get; set; }
	}

	public class ChildrenAggregation : BucketAggregationBase, IChildrenAggregation
	{
		public RelationName Type { get; set; }

		internal ChildrenAggregation() { }

		public ChildrenAggregation(string name, RelationName type) : base(name)
		{
			this.Type = type;
		}

		internal override void WrapInContainer(AggregationContainer c) => c.Children = this;
	}

	public class ChildrenAggregationDescriptor<T>
		: BucketAggregationDescriptorBase<ChildrenAggregationDescriptor<T>, IChildrenAggregation, T>, IChildrenAggregation
		where T : class
	{
		RelationName IChildrenAggregation.Type { get; set; } = typeof(T);

		public ChildrenAggregationDescriptor<T> Type(RelationName type) =>
			Assign(a => a.Type = type);

		public ChildrenAggregationDescriptor<T> Type<TChildType>() where TChildType : class =>
			Assign(a => a.Type = typeof(TChildType));
	}
}
