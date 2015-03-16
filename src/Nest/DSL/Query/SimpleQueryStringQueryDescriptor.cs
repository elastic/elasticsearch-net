using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<SimpleQueryStringQueryDescriptor<object>>))]
	public interface ISimpleQueryStringQuery : IQuery
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

	public class SimpleQueryStringQuery : PlainQuery, ISimpleQueryStringQuery
	{
		protected override void WrapInContainer(IQueryContainer container)
		{
			container.SimpleQueryString = this;
		}
		public string Name { get; set; }
		bool IQuery.IsConditionless { get { return false; } }
		public string Query { get; set; }
		public PropertyPathMarker DefaultField { get; set; }
		public IEnumerable<PropertyPathMarker> Fields { get; set; }
		public Operator? DefaultOperator { get; set; }
		public string Analyzer { get; set; }
		public bool? LowercaseExpendedTerms { get; set; }
		public string Flags { get; set; }
		public string Locale { get; set; }
	}

	public class SimpleQueryStringQueryDescriptor<T> : ISimpleQueryStringQuery where T : class
	{
		private ISimpleQueryStringQuery Self { get { return this; } }

		string ISimpleQueryStringQuery.Query { get; set; }
		
		PropertyPathMarker ISimpleQueryStringQuery.DefaultField { get; set; }
		
		IEnumerable<PropertyPathMarker> ISimpleQueryStringQuery.Fields { get; set; }
		
		Operator? ISimpleQueryStringQuery.DefaultOperator { get; set; }
		
		string ISimpleQueryStringQuery.Analyzer { get; set; }
		
		bool? ISimpleQueryStringQuery.LowercaseExpendedTerms { get; set; }
		
		string ISimpleQueryStringQuery.Flags { get; set; }
		
		string ISimpleQueryStringQuery.Locale { get; set; }

		string IQuery.Name { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return Self.Query.IsNullOrEmpty();
			}
		}

		public SimpleQueryStringQueryDescriptor<T> Name(string name)
		{
			Self.Name = name;
			return this;
		}

		public SimpleQueryStringQueryDescriptor<T> DefaultField(string field)
		{
			Self.DefaultField = field;
			return this;
		}
		public SimpleQueryStringQueryDescriptor<T> DefaultField(Expression<Func<T, object>> objectPath)
		{
			Self.DefaultField = objectPath;
			return this;
		}
		public SimpleQueryStringQueryDescriptor<T> OnFields(IEnumerable<string> fields)
		{
			Self.Fields = fields.Select(f=>(PropertyPathMarker)f);
			return this;
		}
		public SimpleQueryStringQueryDescriptor<T> OnFields(
			params Expression<Func<T, object>>[] objectPaths)
		{
			Self.Fields = objectPaths.Select(e=>(PropertyPathMarker)e);
			return this;
		}
		public SimpleQueryStringQueryDescriptor<T> OnFieldsWithBoost(Action<FluentDictionary<Expression<Func<T, object>>, double?>> boostableSelector)
		{
			var d = new FluentDictionary<Expression<Func<T, object>>, double?>();
			boostableSelector(d);
			Self.Fields = d.Select(o => PropertyPathMarker.Create(o.Key, o.Value));
			return this;
		}
		public SimpleQueryStringQueryDescriptor<T> OnFieldsWithBoost(Action<FluentDictionary<string, double?>> boostableSelector) 
		{
			var d = new FluentDictionary<string, double?>();
			boostableSelector(d);
			Self.Fields = d.Select(o => PropertyPathMarker.Create(o.Key, o.Value));
			return this;
		}

		public SimpleQueryStringQueryDescriptor<T> Query(string query)
		{
			Self.Query = query;
			return this;
		}
		public SimpleQueryStringQueryDescriptor<T> DefaultOperator(Operator op)
		{
			Self.DefaultOperator = op;
			return this;
		}
		public SimpleQueryStringQueryDescriptor<T> Analyzer(string analyzer)
		{
			Self.Analyzer = analyzer;
			return this;
		}
		public SimpleQueryStringQueryDescriptor<T> Flags(string flags)
		{
			Self.Flags = flags;
			return this;
		}
		public SimpleQueryStringQueryDescriptor<T> LowercaseExpendedTerms(bool lowercaseExpendedTerms= true)
		{
			Self.LowercaseExpendedTerms = lowercaseExpendedTerms;
			return this;
		}
		public SimpleQueryStringQueryDescriptor<T> Locale(string locale)
		{
			Self.Locale = locale;
			return this;
		}

	}
}
