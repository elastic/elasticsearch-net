using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Nest.DSL.Query.Behaviour;
using Nest.Resolvers;
using Nest.Resolvers.Converters.Queries;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Nest.Resolvers.Converters;
using Elasticsearch.Net;

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

	public class TermQueryDescriptorBase<TDescriptor, T> : ITermQuery
		where TDescriptor : TermQueryDescriptorBase<TDescriptor, T>
		where T : class
	{
		PropertyPathMarker ITermQuery.Field { get; set; }
		object ITermQuery.Value { get; set; }
		double? ITermQuery.Boost { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				var tq = ((ITermQuery)this);
				return tq.Value == null || tq.Value.ToString().IsNullOrEmpty() || tq.Field.IsConditionless();
			}
		}

		public TDescriptor OnField(string field)
		{
			((ITermQuery)this).Field = field;
			return (TDescriptor)this;
		}
		public TDescriptor OnField(Expression<Func<T, object>> field)
		{
			((ITermQuery)this).Field = field;
			return (TDescriptor)this;
		}
		public TDescriptor OnField<K>(Expression<Func<T, K>> field)
		{
			((ITermQuery)this).Field = field;
			return (TDescriptor)this;
		}
		public TDescriptor Value(object value)
		{
			((ITermQuery)this).Value = value;
			return (TDescriptor)this;
		}
		public TDescriptor Boost(double boost)
		{
			((ITermQuery)this).Boost = boost;
			return (TDescriptor)this;
		}

		public PropertyPathMarker GetFieldName()
		{
			return ((ITermQuery)this).Field;
		}

		public void SetFieldName(string fieldName)
		{
			((ITermQuery)this).Field = fieldName;
		}
	}


	public class TermQueryDescriptor<T> : TermQueryDescriptorBase<TermQueryDescriptor<T>, T>
		where T : class
	{
		
	}
}
