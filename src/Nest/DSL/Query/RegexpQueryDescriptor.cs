using System;
using System.Collections.Generic;
using System.Linq;
using Nest.DSL.Query.Behaviour;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IRegexpQuery : IFieldNameQuery
	{
		[JsonProperty("value")]
		string Value { get; set; }

		[JsonProperty("flags")]
		string Flags { get; set; }

		PropertyPathMarker Field { get; set; }

		[JsonProperty("boost")]
		double? Boost { get; set; }
	}

	public class RegexpQuery : PlainQuery, IRegexpQuery
	{
		protected override void WrapInContainer(IQueryContainer container)
		{
			container.Regexp = this;
		}

		bool IQuery.IsConditionless { get { return false; } }
		PropertyPathMarker IFieldNameQuery.GetFieldName()
		{
			return this.Field;
		}

		void IFieldNameQuery.SetFieldName(string fieldName)
		{
			this.Field = fieldName;
		}

		public string Value { get; set; }
		public string Flags { get; set; }
		public PropertyPathMarker Field { get; set; }
		public double? Boost { get; set; }
	}

	public class RegexpQueryDescriptor<T> : IRegexpQuery where T : class
	{
		string IRegexpQuery.Value { get; set; }

		string IRegexpQuery.Flags { get; set; }

		PropertyPathMarker IRegexpQuery.Field { get; set; }

		double? IRegexpQuery.Boost { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return ((IRegexpQuery)this).Field.IsConditionless() || ((IRegexpQuery)this).Value.IsNullOrEmpty();
			}
		}

		void IFieldNameQuery.SetFieldName(string fieldName)
		{
			((IRegexpQuery)this).Field = fieldName;
		}
		PropertyPathMarker IFieldNameQuery.GetFieldName()
		{
			return ((IRegexpQuery)this).Field;
		}

		public RegexpQueryDescriptor<T> Value(string regex)
		{
			((IRegexpQuery)this).Value = regex;
			return this;
		}
		public RegexpQueryDescriptor<T> Flags(string flags)
		{
			((IRegexpQuery)this).Flags = flags;
			return this;
		}
		public RegexpQueryDescriptor<T> OnField(string path)
		{
			((IRegexpQuery)this).Field = path;
			return this;
		}
		public RegexpQueryDescriptor<T> Boost(double boost)
		{
			((IRegexpQuery)this).Boost = boost;
			return this;
		}

		public RegexpQueryDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			((IRegexpQuery)this).Field = objectPath;
			return this;
		}

	}
}
