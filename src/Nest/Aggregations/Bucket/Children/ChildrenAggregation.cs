using Newtonsoft.Json;

// ReSharper disable UnusedMember.Global

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(AggregationJsonConverter<ChildrenAggregation>))]
	public interface IChildrenAggregation : IBucketAggregation
	{
		[JsonProperty("type")]
		TypeName Type { get; set; }
	}

	public class ChildrenAggregation : BucketAggregationBase, IChildrenAggregation
	{
		public TypeName Type { get; set; }

		internal ChildrenAggregation() { }

		public ChildrenAggregation(string name, TypeName type) : base(name)
		{
			this.Type = type;
		}

		internal override void WrapInContainer(AggregationContainer c) => c.Children = this;
	}

	public class ChildrenAggregationDescriptor<T> 
		: BucketAggregationDescriptorBase<ChildrenAggregationDescriptor<T>, IChildrenAggregation, T>, IChildrenAggregation
		where T : class
	{
		TypeName IChildrenAggregation.Type { get; set; } = typeof(T);

		public ChildrenAggregationDescriptor<T> Type(TypeName type) =>
			Assign(a => a.Type = type);

		public ChildrenAggregationDescriptor<T> Type<TChildType>() where TChildType : class =>
			Assign(a => a.Type = typeof(TChildType));
	}
}
