using System;
using System.Collections.Generic;
using System.Linq;
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
		bool IQuery.Conditionless => IsConditionless(this);
		public TypeName Value { get; set; }

		protected override void WrapInContainer(IQueryContainer c) => c.Type = this;
		internal static bool IsConditionless(ITypeQuery q) => q.Value.IsConditionless();
	}
	
	public class TypeQueryDescriptor 
		: QueryDescriptorBase<TypeQueryDescriptor, ITypeQuery>
		, ITypeQuery
	{
		bool IQuery.Conditionless => TypeQuery.IsConditionless(this);

		[JsonProperty(PropertyName = "value")]
		TypeName ITypeQuery.Value { get; set; }

		public TypeQueryDescriptor Value<T>() => Assign(a => a.Value = typeof(T));

		public TypeQueryDescriptor Value(string type) => Assign(a => a.Value = type);
	}
}
