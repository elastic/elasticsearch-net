using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(TypeQuery))]
	public interface ITypeQuery : IQuery
	{
		[DataMember(Name = "value")]
		TypeName Value { get; set; }
	}

	public class TypeQuery : QueryBase, ITypeQuery
	{
		public TypeName Value { get; set; }
		protected override bool Conditionless => IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.Type = this;

		internal static bool IsConditionless(ITypeQuery q) => q.Value.IsConditionless();
	}

	public class TypeQueryDescriptor
		: QueryDescriptorBase<TypeQueryDescriptor, ITypeQuery>
			, ITypeQuery
	{
		protected override bool Conditionless => TypeQuery.IsConditionless(this);

		TypeName ITypeQuery.Value { get; set; }

		public TypeQueryDescriptor Value<T>() => Assign(a => a.Value = typeof(T));

		public TypeQueryDescriptor Value(TypeName type) => Assign(a => a.Value = type);
	}
}
