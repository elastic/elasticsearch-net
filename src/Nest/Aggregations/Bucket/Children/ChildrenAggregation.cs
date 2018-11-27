using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(ChildrenAggregation))]
	public interface IChildrenAggregation : IBucketAggregation
	{
		[DataMember(Name ="type")]
		RelationName Type { get; set; }
	}

	public class ChildrenAggregation : BucketAggregationBase, IChildrenAggregation
	{
		internal ChildrenAggregation() { }

		public ChildrenAggregation(string name, RelationName type) : base(name) => Type = type;

		public RelationName Type { get; set; }

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
