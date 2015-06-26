using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeConverter<MissingQueryDescriptor>))]
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

		public PropertyPathMarker Field { get; set; }

		public bool? Existence { get; set; }

		public bool? NullValue { get; set; }

		public string Name { get; set; }

		public bool IsConditionless => QueryCondition.IsConditionless(this);
	}

	// TODO: finish implementing
	public class MissingQueryDescriptor : IMissingQuery
	{
		private IMissingQuery Self => this;

		bool IQuery.IsConditionless => QueryCondition.IsConditionless(this);

		PropertyPathMarker IMissingQuery.Field { get; set;}
		bool? IMissingQuery.Existence { get; set; }
		bool? IMissingQuery.NullValue { get; set; }
		string IQuery.Name { get; set; }

		public MissingQueryDescriptor Existence(bool existence = true)
		{
			Self.Existence = existence;
			return this;
		}

		public MissingQueryDescriptor NullValue(bool nullValue = true)
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
