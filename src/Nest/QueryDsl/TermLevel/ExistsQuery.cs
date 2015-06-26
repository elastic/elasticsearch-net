using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeConverter<ExistsQuery>))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IExistsQuery : IFieldNameQuery
	{
		[JsonProperty(PropertyName = "field")]
		PropertyPathMarker Field { get; set; }
	}

	public class ExistsQuery : PlainQuery, IExistsQuery
	{
		public string Name { get; set; }
		bool IQuery.IsConditionless { get { return QueryCondition.IsConditionless(this); } }
		public PropertyPathMarker Field { get; set; }

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
	}

	public class ExistsQueryDescriptor<T> : IExistsQuery where T : class
	{
		private IExistsQuery Self { get { return this; } }
		string IQuery.Name { get; set; }
		bool IQuery.IsConditionless { get { return QueryCondition.IsConditionless(this); } }
		PropertyPathMarker IExistsQuery.Field { get; set;}

		public ExistsQueryDescriptor<T> Field (string field)
		{
			Self.Field = field;
			return this;
		}

		public ExistsQueryDescriptor<T> Field(Expression<Func<T, object>> field)
		{
			Self.Field = field;
			return this;
		}

		public ExistsQueryDescriptor<T> Name(string name)
		{
			Self.Name = name;
			return this;
		}

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
