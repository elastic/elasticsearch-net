using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Nest.DSL.Query.Behaviour;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ITermQuery : IFieldNameQuery
	{
		PropertyPathMarker Field { get; set; }
		[JsonProperty("value")]
		object Value { get; set; }
		[JsonProperty("boost")]
		double? Boost { get; set; }
	}

	public class TermQuery : PlainQuery, ITermQuery
	{
		protected override void WrapInContainer(IQueryContainer container)
		{
			container.Term = this;
		}

		public string Name { get; set; }
		bool IQuery.IsConditionless { get { return false; } }
		PropertyPathMarker IFieldNameQuery.GetFieldName()
		{
			return this.Field;
		}

		void IFieldNameQuery.SetFieldName(string fieldName)
		{
			this.Field = fieldName;
		}

		public PropertyPathMarker Field { get; set; }
		public object Value { get; set; }
		public double? Boost { get; set; }
	}

	public class TermQueryDescriptorBase<TDescriptor, T> : ITermQuery
		where TDescriptor : TermQueryDescriptorBase<TDescriptor, T>
		where T : class
	{
		private ITermQuery Self { get { return this; }}

		PropertyPathMarker ITermQuery.Field { get; set; }
		object ITermQuery.Value { get; set; }
		double? ITermQuery.Boost { get; set; }

		string IQuery.Name { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return Self.Value == null || Self.Value.ToString().IsNullOrEmpty() || Self.Field.IsConditionless();
			}
		}

		public TDescriptor Name(string name)
		{
			Self.Name = name;
			return (TDescriptor) this;
		}

		public TDescriptor OnField(string field)
		{
			Self.Field = field;
			return (TDescriptor)this;
		}
		public TDescriptor OnField(Expression<Func<T, object>> field)
		{
			Self.Field = field;
			return (TDescriptor)this;
		}
		public TDescriptor OnField<K>(Expression<Func<T, K>> field)
		{
			Self.Field = field;
			return (TDescriptor)this;
		}
		public TDescriptor Value(object value)
		{
			Self.Value = value;
			return (TDescriptor)this;
		}
		public TDescriptor Boost(double boost)
		{
			Self.Boost = boost;
			return (TDescriptor)this;
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

	public class TermQueryDescriptor<T> : TermQueryDescriptorBase<TermQueryDescriptor<T>, T>
		where T : class
	{
		
	}
}
