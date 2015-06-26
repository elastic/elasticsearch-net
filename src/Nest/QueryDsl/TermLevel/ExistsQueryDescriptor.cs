using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeConverter<ExistsQueryDescriptor>))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IExistsQuery : IFieldNameQuery
	{
		[JsonProperty(PropertyName = "field")]
		PropertyPathMarker Field { get; set; }
	}

	public class ExistsQuery : PlainQuery, IExistsQuery
	{
		protected override void WrapInContainer(IQueryContainer container)
		{
			container.Exists = this;
		}

		public PropertyPathMarker GetFieldName()
		{
			return Field;
		}

		public void SetFieldName(string fieldName)
		{
			Field = fieldName;
		}

		public PropertyPathMarker Field { get; set; }

		public string Name { get; set; }

		bool IQuery.IsConditionless { get { return QueryCondition.IsConditionless(this); } }
	}

	// TODO: finish implementation
	public class ExistsQueryDescriptor : IExistsQuery
	{
		private IExistsQuery Self { get { return this; } }

		bool IQuery.IsConditionless { get { return QueryCondition.IsConditionless(this); } }

		PropertyPathMarker IExistsQuery.Field { get; set;}

		string IQuery.Name { get; set; }

		public PropertyPathMarker GetFieldName()
		{
			return Self.Field;	
		}

		public void SetFieldName(string fieldName)
		{
			Self.Field = fieldName;
		}
	}
}
