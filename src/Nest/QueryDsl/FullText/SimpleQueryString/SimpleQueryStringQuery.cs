using System;
using System.Collections.Generic;
using System.Globalization;
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

		[JsonProperty(PropertyName = "analyze_wildcard")]
		bool? AnalyzeWildcard { get; set; }

		[JsonProperty(PropertyName = "flags")]
		string Flags { get; set; }

		[JsonProperty(PropertyName = "locale")]
		string Locale { get; set; }

		[JsonProperty("minimum_should_match")]
		string MinimumShouldMatch { get; set; }
	}

	public class SimpleQueryStringQuery : Query, ISimpleQueryStringQuery
	{
		bool IQuery.Conditionless => IsConditionless(this);
		public string Query { get; set; }
		public PropertyPathMarker DefaultField { get; set; }
		public IEnumerable<PropertyPathMarker> Fields { get; set; }
		public Operator? DefaultOperator { get; set; }
		public string Analyzer { get; set; }
		public bool? LowercaseExpendedTerms { get; set; }
		public bool? AnalyzeWildcard { get; set; }
		public string Flags { get; set; }
		public string Locale { get; set; }
		public string MinimumShouldMatch { get; set; }

		protected override void WrapInContainer(IQueryContainer c) => c.SimpleQueryString = this;
		internal static bool IsConditionless(ISimpleQueryStringQuery q) => q.Query.IsNullOrEmpty();
	}

	public class SimpleQueryStringQueryDescriptor<T> : ISimpleQueryStringQuery where T : class
	{
		private SimpleQueryStringQueryDescriptor<T> _assign(Action<ISimpleQueryStringQuery> assigner) => Fluent.Assign(this, assigner);

		string IQuery.Name { get; set; }
		bool IQuery.Conditionless => SimpleQueryStringQuery.IsConditionless(this);
		string ISimpleQueryStringQuery.Query { get; set; }
		PropertyPathMarker ISimpleQueryStringQuery.DefaultField { get; set; }
		IEnumerable<PropertyPathMarker> ISimpleQueryStringQuery.Fields { get; set; }
		Operator? ISimpleQueryStringQuery.DefaultOperator { get; set; }
		string ISimpleQueryStringQuery.Analyzer { get; set; }
		bool? ISimpleQueryStringQuery.AnalyzeWildcard { get; set; }
		bool? ISimpleQueryStringQuery.LowercaseExpendedTerms { get; set; }
		string ISimpleQueryStringQuery.Flags { get; set; }
		string ISimpleQueryStringQuery.Locale { get; set; }
		string ISimpleQueryStringQuery.MinimumShouldMatch { get; set; }

		public SimpleQueryStringQueryDescriptor<T> Name(string name) => _assign(a => a.Name = name);

		public SimpleQueryStringQueryDescriptor<T> DefaultField(string field) => _assign(a => a.DefaultField = field);

		public SimpleQueryStringQueryDescriptor<T> DefaultField(Expression<Func<T, object>> objectPath) => 
			_assign(a => a.DefaultField = objectPath);

		public SimpleQueryStringQueryDescriptor<T> OnFields(IEnumerable<string> fields) =>
			_assign(a => a.Fields = fields?.Select(f => (PropertyPathMarker) f).ToListOrNullIfEmpty());

		public SimpleQueryStringQueryDescriptor<T> OnFields(params Expression<Func<T, object>>[] objectPaths) =>
			_assign(a => a.Fields = objectPaths?.Select(f => (PropertyPathMarker) f).ToListOrNullIfEmpty());

		public SimpleQueryStringQueryDescriptor<T> OnFieldsWithBoost(Func<
			FluentDictionary<Expression<Func<T, object>>, double?>, IDictionary<Expression<Func<T, object>>, double?>> boostableSelector) =>
				_assign(a => a.Fields = boostableSelector?
					.Invoke(new FluentDictionary<Expression<Func<T, object>>, double?>())
					.Select(o => PropertyPathMarker.Create(o.Key, o.Value))
					.ToListOrNullIfEmpty()
				);

		public SimpleQueryStringQueryDescriptor<T> OnFieldsWithBoost(
			Func<FluentDictionary<string, double?>, IDictionary<Expression<Func<T, object>>, double?>> boostableSelector) =>
				_assign(a => a.Fields = boostableSelector?
					.Invoke(new FluentDictionary<string, double?>())
					.Select(o => PropertyPathMarker.Create(o.Key, o.Value))
					.ToListOrNullIfEmpty()
				);

		public SimpleQueryStringQueryDescriptor<T> Query(string query) => _assign(a => a.Query = query);

		public SimpleQueryStringQueryDescriptor<T> DefaultOperator(Operator op) => _assign(a => a.DefaultOperator = op);

		public SimpleQueryStringQueryDescriptor<T> Analyzer(string analyzer) => _assign(a => a.Analyzer = analyzer);

		public SimpleQueryStringQueryDescriptor<T> Flags(string flags) => _assign(a => a.Flags = flags);

		public SimpleQueryStringQueryDescriptor<T> LowercaseExpendedTerms(bool lowercaseExpendedTerms = true) =>
			_assign(a => a.LowercaseExpendedTerms = lowercaseExpendedTerms);

		public SimpleQueryStringQueryDescriptor<T> AnalyzeWildcard(bool analyzeWildcard = true) =>
			_assign(a => a.AnalyzeWildcard = analyzeWildcard);

		public SimpleQueryStringQueryDescriptor<T> Locale(string locale) => _assign(a => a.Locale = locale);

		public SimpleQueryStringQueryDescriptor<T> MinimumShouldMatch(int minimumShouldMatches) =>
			_assign(a => a.MinimumShouldMatch = minimumShouldMatches.ToString(CultureInfo.InvariantCulture));

		public SimpleQueryStringQueryDescriptor<T> MinimumShouldMatch(string minimumShouldMatch) =>
			_assign(a => a.MinimumShouldMatch = minimumShouldMatch);

    }
}
