using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<TypeQueryDescriptor>))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ITypeQuery : IQuery
	{
		[JsonProperty(PropertyName = "value")]
		TypeName Value { get; set; }
	}

	public class TypeQuery : QueryBase, ITypeQuery
	{
		protected override bool Conditionless => IsConditionless(this);
		public TypeName Value { get; set; }

		internal override void InternalWrapInContainer(IQueryContainer c) => c.Type = this;
		internal static bool IsConditionless(ITypeQuery q) => q.Value.IsConditionless();
	}
	
	public class TypeQueryDescriptor 
		: QueryDescriptorBase<TypeQueryDescriptor, ITypeQuery>
		, ITypeQuery
	{
		protected override bool Conditionless => TypeQuery.IsConditionless(this);

		[JsonProperty(PropertyName = "value")]
		TypeName ITypeQuery.Value { get; set; }

		public TypeQueryDescriptor Value<T>() => Assign(a => a.Value = typeof(T));

		public TypeQueryDescriptor Value(TypeName type) => Assign(a => a.Value = type);
	}
}
