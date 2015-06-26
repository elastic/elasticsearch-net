using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeConverter<MissingQuery>))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IMissingQuery : IFieldNameQuery
	{
		[JsonProperty(PropertyName = "field")]
		PropertyPathMarker Field { get; set; }

		[JsonProperty(PropertyName = "existence")]
		bool? Existence { get; set; }

		[JsonProperty(PropertyName = "null_value")]
		bool? NullValue { get; set; }
	}

	public class MissingQuery : PlainQuery, IMissingQuery
	{
		public string Name { get; set; }
		public bool IsConditionless { get { return QueryCondition.IsConditionless(this); } }
		public PropertyPathMarker Field { get; set; }
		public bool? Existence { get; set; }
		public bool? NullValue { get; set; }

		protected override void WrapInContainer(IQueryContainer container)
		{
			container.Missing = this;
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

	public class MissingQueryDescriptor<T> : IMissingQuery where T : class
	{
		private IMissingQuery Self { get { return this; } }
		string IQuery.Name { get; set; }
		bool IQuery.IsConditionless { get { return QueryCondition.IsConditionless(this); } }
		PropertyPathMarker IMissingQuery.Field { get; set;}
		bool? IMissingQuery.Existence { get; set; }
		bool? IMissingQuery.NullValue { get; set; }

		public MissingQueryDescriptor<T> Name(string name)
		{
			Self.Name = name;
			return this;
		}

		public MissingQueryDescriptor<T> Field(string field)
		{
			Self.Field = field;
			return this;
		}

		public MissingQueryDescriptor<T> Field(Expression<Func<T, object>> field)
		{
			Self.Field = field;
			return this;
		}

		public MissingQueryDescriptor<T> Existence(bool existence = true)
		{
			Self.Existence = existence;
			return this;
		}

		public MissingQueryDescriptor<T> NullValue(bool nullValue = true)
		{
			Self.NullValue = nullValue;
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
