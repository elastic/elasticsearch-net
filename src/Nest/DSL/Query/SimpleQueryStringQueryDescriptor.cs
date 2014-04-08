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
	public class SimpleQueryStringQueryDescriptor<T> : IQuery where T : class
	{
		[JsonProperty(PropertyName = "query")]
		internal string _QueryString { get; set; }
		[JsonProperty(PropertyName = "default_field")]
		internal PropertyPathMarker _Field { get; set; }
		[JsonProperty(PropertyName = "fields")]
		internal IEnumerable<PropertyPathMarker> _Fields { get; set; }
		[JsonProperty(PropertyName = "default_operator")]
		[JsonConverter(typeof(StringEnumConverter))]
		internal Operator? _DefaultOperator { get; set; }
		[JsonProperty(PropertyName = "analyzer")]
		internal string _Analyzer { get; set; }
		[JsonProperty(PropertyName = "lowercase_expanded_terms")]
		internal bool? _LowercaseExpendedTerms { get; set; }
		[JsonProperty(PropertyName = "flags")]
		internal string _Flags { get; set; }
		[JsonProperty(PropertyName = "locale")]
		internal string _Locale { get; set; }


		bool IQuery.IsConditionless
		{
			get
			{
				return this._QueryString.IsNullOrEmpty();
			}
		}


		public SimpleQueryStringQueryDescriptor<T> OnField(string field)
		{
			this._Field = field;
			return this;
		}
		public SimpleQueryStringQueryDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			this._Field = objectPath;
			return this;
		}
		public SimpleQueryStringQueryDescriptor<T> OnFields(IEnumerable<string> fields)
		{
			this._Fields = fields.Select(f=>(PropertyPathMarker)f);
			return this;
		}
		public SimpleQueryStringQueryDescriptor<T> OnFields(
			params Expression<Func<T, object>>[] objectPaths)
		{
			this._Fields = objectPaths.Select(e=>(PropertyPathMarker)e);
			return this;
		}
		public SimpleQueryStringQueryDescriptor<T> OnFieldsWithBoost(Action<FluentDictionary<Expression<Func<T, object>>, double?>> boostableSelector)
		{
			var d = new FluentDictionary<Expression<Func<T, object>>, double?>();
			boostableSelector(d);
			this._Fields = d.Select(o => PropertyPathMarker.Create(o.Key, o.Value));
			return this;
		}
		public SimpleQueryStringQueryDescriptor<T> OnFieldsWithBoost(Action<FluentDictionary<string, double?>> boostableSelector) 
		{
			var d = new FluentDictionary<string, double?>();
			boostableSelector(d);
			this._Fields = d.Select(o => PropertyPathMarker.Create(o.Key, o.Value));
			return this;
		}

		public SimpleQueryStringQueryDescriptor<T> Query(string query)
		{
			this._QueryString = query;
			return this;
		}
		public SimpleQueryStringQueryDescriptor<T> DefaultOperator(Operator op)
		{
			this._DefaultOperator = op;
			return this;
		}
		public SimpleQueryStringQueryDescriptor<T> Analyzer(string analyzer)
		{
			this._Analyzer = analyzer;
			return this;
		}
		public SimpleQueryStringQueryDescriptor<T> Flags(string flags)
		{
			this._Flags = flags;
			return this;
		}
		public SimpleQueryStringQueryDescriptor<T> LowercaseExpendedTerms(bool lowercaseExpendedTerms)
		{
			this._LowercaseExpendedTerms = lowercaseExpendedTerms;
			return this;
		}
		public SimpleQueryStringQueryDescriptor<T> Locale(string locale)
		{
			this._Locale = locale;
			return this;
		}

	}
}
