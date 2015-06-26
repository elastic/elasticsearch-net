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
		TypeNameMarker Value { get; set; }
	}

	public class TypeQuery : PlainQuery, ITypeQuery
	{
		public string Name { get; set; }
		bool IQuery.IsConditionless { get { return QueryCondition.IsConditionless(this); } }
		public TypeNameMarker Value { get; set; }

		protected override void WrapInContainer(IQueryContainer container)
		{
			container.Type = this;
		}
	}
	
	public class TypeQueryDescriptor : ITypeQuery
	{
		private ITypeQuery Self { get { return this; } }

		string IQuery.Name { get; set; }

		bool IQuery.IsConditionless { get { return QueryCondition.IsConditionless(this); } }

		[JsonProperty(PropertyName = "value")]
		TypeNameMarker ITypeQuery.Value { get; set; }

		public TypeQueryDescriptor Name(string name)
		{
			Self.Name = name;
			return this;
		}

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
