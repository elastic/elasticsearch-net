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
		string _QueryString { get; set; }

		[JsonProperty(PropertyName = "default_field")]
		PropertyPathMarker _Field { get; set; }

		[JsonProperty(PropertyName = "fields")]
		IEnumerable<PropertyPathMarker> _Fields { get; set; }

		[JsonProperty(PropertyName = "default_operator")]
		[JsonConverter(typeof (StringEnumConverter))]
		Operator? _DefaultOperator { get; set; }

		[JsonProperty(PropertyName = "analyzer")]
		string _Analyzer { get; set; }

		[JsonProperty(PropertyName = "lowercase_expanded_terms")]
		bool? _LowercaseExpendedTerms { get; set; }

		[JsonProperty(PropertyName = "flags")]
		string _Flags { get; set; }

		[JsonProperty(PropertyName = "locale")]
		string _Locale { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class SimpleQueryStringQueryDescriptor<T> : IQuery, ISimpleQueryStringQuery where T : class
	{
		string ISimpleQueryStringQuery._QueryString { get; set; }
		
		PropertyPathMarker ISimpleQueryStringQuery._Field { get; set; }
		
		IEnumerable<PropertyPathMarker> ISimpleQueryStringQuery._Fields { get; set; }
		
		Operator? ISimpleQueryStringQuery._DefaultOperator { get; set; }
		
		string ISimpleQueryStringQuery._Analyzer { get; set; }
		
		bool? ISimpleQueryStringQuery._LowercaseExpendedTerms { get; set; }
		
		string ISimpleQueryStringQuery._Flags { get; set; }
		
		string ISimpleQueryStringQuery._Locale { get; set; }


		bool IQuery.IsConditionless
		{
			get
			{
				return ((ISimpleQueryStringQuery)this)._QueryString.IsNullOrEmpty();
			}
		}


		public SimpleQueryStringQueryDescriptor<T> OnField(string field)
		{
			((ISimpleQueryStringQuery)this)._Field = field;
			return this;
		}
		public SimpleQueryStringQueryDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			((ISimpleQueryStringQuery)this)._Field = objectPath;
			return this;
		}
		public SimpleQueryStringQueryDescriptor<T> OnFields(IEnumerable<string> fields)
		{
			((ISimpleQueryStringQuery)this)._Fields = fields.Select(f=>(PropertyPathMarker)f);
			return this;
		}
		public SimpleQueryStringQueryDescriptor<T> OnFields(
			params Expression<Func<T, object>>[] objectPaths)
		{
			((ISimpleQueryStringQuery)this)._Fields = objectPaths.Select(e=>(PropertyPathMarker)e);
			return this;
		}
		public SimpleQueryStringQueryDescriptor<T> OnFieldsWithBoost(Action<FluentDictionary<Expression<Func<T, object>>, double?>> boostableSelector)
		{
			var d = new FluentDictionary<Expression<Func<T, object>>, double?>();
			boostableSelector(d);
			((ISimpleQueryStringQuery)this)._Fields = d.Select(o => PropertyPathMarker.Create(o.Key, o.Value));
			return this;
		}
		public SimpleQueryStringQueryDescriptor<T> OnFieldsWithBoost(Action<FluentDictionary<string, double?>> boostableSelector) 
		{
			var d = new FluentDictionary<string, double?>();
			boostableSelector(d);
			((ISimpleQueryStringQuery)this)._Fields = d.Select(o => PropertyPathMarker.Create(o.Key, o.Value));
			return this;
		}

		public SimpleQueryStringQueryDescriptor<T> Query(string query)
		{
			((ISimpleQueryStringQuery)this)._QueryString = query;
			return this;
		}
		public SimpleQueryStringQueryDescriptor<T> DefaultOperator(Operator op)
		{
			((ISimpleQueryStringQuery)this)._DefaultOperator = op;
			return this;
		}
		public SimpleQueryStringQueryDescriptor<T> Analyzer(string analyzer)
		{
			((ISimpleQueryStringQuery)this)._Analyzer = analyzer;
			return this;
		}
		public SimpleQueryStringQueryDescriptor<T> Flags(string flags)
		{
			((ISimpleQueryStringQuery)this)._Flags = flags;
			return this;
		}
		public SimpleQueryStringQueryDescriptor<T> LowercaseExpendedTerms(bool lowercaseExpendedTerms)
		{
			((ISimpleQueryStringQuery)this)._LowercaseExpendedTerms = lowercaseExpendedTerms;
			return this;
		}
		public SimpleQueryStringQueryDescriptor<T> Locale(string locale)
		{
			((ISimpleQueryStringQuery)this)._Locale = locale;
			return this;
		}

	}
}
