using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeConverter<TypeQueryDescriptor>))]
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
		private ITypeQuery Self => this;

		bool IQuery.Conditionless => TypeQuery.IsConditionless(this);

		[JsonProperty(PropertyName = "value")]
		TypeName ITypeQuery.Value { get; set; }

		public TypeQueryDescriptor Value<T>()
		{
			Self.Value = typeof(T);
			return this;
		}

		public TypeQueryDescriptor Value(string type)
		{
			Self.Value = type;
			return this;
		}
	}
}
