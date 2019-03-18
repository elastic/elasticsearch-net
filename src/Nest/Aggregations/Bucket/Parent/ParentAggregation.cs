using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(AggregationJsonConverter<ParentAggregation>))]
	public interface IParentAggregation : IBucketAggregation
	{
		/// <summary>
		/// The type for the child in the parent/child relationship
		/// </summary>
		[JsonProperty("type")]
		RelationName Type { get; set; }
	}

	public class ParentAggregation : BucketAggregationBase, IParentAggregation
	{
		internal ParentAggregation() { }

		public ParentAggregation(string name, RelationName type) : base(name) => Type = type;

		public RelationName Type { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.Parent = this;
	}

	public class ParentAggregationDescriptor<T, TParent>
		: BucketAggregationDescriptorBase<ParentAggregationDescriptor<T, TParent>, IParentAggregation, TParent>, IParentAggregation
		where T : class
		where TParent : class
	{
		RelationName IParentAggregation.Type { get; set; } = typeof(T);

		public ParentAggregationDescriptor<T, TParent> Type(RelationName type) =>
			Assign(a => a.Type = type);

		public ParentAggregationDescriptor<T, TParent> Type<TOtherParent>() =>
			Assign(a => a.Type = typeof(TOtherParent));
	}
}
