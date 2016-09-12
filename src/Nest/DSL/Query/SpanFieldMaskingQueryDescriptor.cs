using System;
using System.Linq.Expressions;

using Nest.DSL.Query.Behaviour;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<SpanFieldMaskingQueryDescriptor<object>>))]
	public interface ISpanFieldMaskingQuery : ISpanSubQuery, IFieldNameQuery
	{
		[JsonProperty(PropertyName = "field")]
		PropertyPathMarker Field { get; set; }

		[JsonProperty(PropertyName = "query")]
		ISpanQuery Query { get; set; }
	}

	public class SpanFieldMaskingQuery : PlainQuery, ISpanFieldMaskingQuery
	{
		protected override void WrapInContainer(IQueryContainer container)
		{
			container.SpanFieldMasking = this;
		}

		bool IQuery.IsConditionless { get { return false; } }

		public string Name { get; set; }
		public PropertyPathMarker Field { get; set; }
		public ISpanQuery Query { get; set; }

		PropertyPathMarker IFieldNameQuery.GetFieldName()
		{
			return this.Field;
		}

		void IFieldNameQuery.SetFieldName(string fieldName)
		{
			this.Field = fieldName;
		}
	}

	public class SpanFieldMaskingQueryDescriptor<T> : ISpanFieldMaskingQuery where T : class
	{
		ISpanFieldMaskingQuery Self { get { return this; } }
		bool IQuery.IsConditionless { get { return false; } }

		ISpanQuery ISpanFieldMaskingQuery.Query { get; set; }
		PropertyPathMarker ISpanFieldMaskingQuery.Field { get; set; }
		string IQuery.Name { get; set; }
		
		public SpanFieldMaskingQueryDescriptor<T> Name(string name)
		{
			Self.Name = name;
			return this;
		}

		public SpanFieldMaskingQueryDescriptor<T> Query(Func<SpanQuery<T>, SpanQuery<T>> selector)
		{
			if (selector == null) return this;
			var span = new SpanQuery<T>();
			Self.Query = selector(span); ;
			return this;
		}

		public SpanFieldMaskingQueryDescriptor<T> OnField(string field)
		{
			Self.Field = field;
			return this;
		}

		public SpanFieldMaskingQueryDescriptor<T> OnField(Expression<Func<T, object>> field)
		{
			Self.Field = field;
			return this;
		}

		public SpanFieldMaskingQueryDescriptor<T> OnField<K>(Expression<Func<T, K>> field)
		{
			Self.Field = field;
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
