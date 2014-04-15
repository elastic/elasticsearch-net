using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Globalization;
using Newtonsoft.Json.Converters;
using Elasticsearch.Net;
using Nest.Resolvers;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ISimpleQueryStringQuery
	{
		[JsonProperty(PropertyName = "query")]
		string Query { get; set; }

		[JsonProperty(PropertyName = "default_field")]
		PropertyPathMarker DefaultField { get; set; }

		[JsonProperty(PropertyName = "fields")]
		IEnumerable<PropertyPathMarker> Fields { get; set; }

		[JsonProperty(PropertyName = "default_operator")]
		[JsonConverter(typeof (StringEnumConverter))]
		Operator? DefaultOperator { get; set; }

		[JsonProperty(PropertyName = "analyzer")]
		string Analyzer { get; set; }

		[JsonProperty(PropertyName = "lowercase_expanded_terms")]
		bool? LowercaseExpendedTerms { get; set; }

		[JsonProperty(PropertyName = "flags")]
		string Flags { get; set; }

		[JsonProperty(PropertyName = "locale")]
		string Locale { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class SimpleQueryStringQueryDescriptor<T> : IQuery, ISimpleQueryStringQuery where T : class
	{
		string ISimpleQueryStringQuery.Query { get; set; }
		
		PropertyPathMarker ISimpleQueryStringQuery.DefaultField { get; set; }
		
		IEnumerable<PropertyPathMarker> ISimpleQueryStringQuery.Fields { get; set; }
		
		Operator? ISimpleQueryStringQuery.DefaultOperator { get; set; }
		
		string ISimpleQueryStringQuery.Analyzer { get; set; }
		
		bool? ISimpleQueryStringQuery.LowercaseExpendedTerms { get; set; }
		
		string ISimpleQueryStringQuery.Flags { get; set; }
		
		string ISimpleQueryStringQuery.Locale { get; set; }


		bool IQuery.IsConditionless
		{
			get
			{
				return ((ISimpleQueryStringQuery)this).Query.IsNullOrEmpty();
			}
		}


		public SimpleQueryStringQueryDescriptor<T> OnField(string field)
		{
			((ISimpleQueryStringQuery)this).DefaultField = field;
			return this;
		}
		public SimpleQueryStringQueryDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			((ISimpleQueryStringQuery)this).DefaultField = objectPath;
			return this;
		}
		public SimpleQueryStringQueryDescriptor<T> OnFields(IEnumerable<string> fields)
		{
			((ISimpleQueryStringQuery)this).Fields = fields.Select(f=>(PropertyPathMarker)f);
			return this;
		}
		public SimpleQueryStringQueryDescriptor<T> OnFields(
			params Expression<Func<T, object>>[] objectPaths)
		{
			((ISimpleQueryStringQuery)this).Fields = objectPaths.Select(e=>(PropertyPathMarker)e);
			return this;
		}
		public SimpleQueryStringQueryDescriptor<T> OnFieldsWithBoost(Action<FluentDictionary<Expression<Func<T, object>>, double?>> boostableSelector)
		{
			var d = new FluentDictionary<Expression<Func<T, object>>, double?>();
			boostableSelector(d);
			((ISimpleQueryStringQuery)this).Fields = d.Select(o => PropertyPathMarker.Create(o.Key, o.Value));
			return this;
		}
		public SimpleQueryStringQueryDescriptor<T> OnFieldsWithBoost(Action<FluentDictionary<string, double?>> boostableSelector) 
		{
			var d = new FluentDictionary<string, double?>();
			boostableSelector(d);
			((ISimpleQueryStringQuery)this).Fields = d.Select(o => PropertyPathMarker.Create(o.Key, o.Value));
			return this;
		}

		public SimpleQueryStringQueryDescriptor<T> Query(string query)
		{
			((ISimpleQueryStringQuery)this).Query = query;
			return this;
		}
		public SimpleQueryStringQueryDescriptor<T> DefaultOperator(Operator op)
		{
			((ISimpleQueryStringQuery)this).DefaultOperator = op;
			return this;
		}
		public SimpleQueryStringQueryDescriptor<T> Analyzer(string analyzer)
		{
			((ISimpleQueryStringQuery)this).Analyzer = analyzer;
			return this;
		}
		public SimpleQueryStringQueryDescriptor<T> Flags(string flags)
		{
			((ISimpleQueryStringQuery)this).Flags = flags;
			return this;
		}
		public SimpleQueryStringQueryDescriptor<T> LowercaseExpendedTerms(bool lowercaseExpendedTerms)
		{
			((ISimpleQueryStringQuery)this).LowercaseExpendedTerms = lowercaseExpendedTerms;
			return this;
		}
		public SimpleQueryStringQueryDescriptor<T> Locale(string locale)
		{
			((ISimpleQueryStringQuery)this).Locale = locale;
			return this;
		}

	}
}
