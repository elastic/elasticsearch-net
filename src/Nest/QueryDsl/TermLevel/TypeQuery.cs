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

	public class TypeFilter : PlainQuery, ITypeQuery
	{
		protected override void WrapInContainer(IQueryContainer container)
		{
			container.Type = this;
		}

		public TypeNameMarker Value { get; set; }

		public string Name { get; set; }

		bool IQuery.IsConditionless { get { return QueryCondition.IsConditionless(this); } }
	}
	
	// TODO: finish implementing
	public class TypeQueryDescriptor : ITypeQuery
	{
		string IQuery.Name { get; set; }

		bool IQuery.IsConditionless { get { return QueryCondition.IsConditionless(this); } }

		[JsonProperty(PropertyName = "value")]
		TypeNameMarker ITypeQuery.Value { get; set; }
	}
}
