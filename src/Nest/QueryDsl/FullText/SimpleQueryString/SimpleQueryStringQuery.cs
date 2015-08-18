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
	[JsonConverter(typeof(ReadAsTypeJsonConverter<SimpleQueryStringQueryDescriptor<object>>))]
	public interface ISimpleQueryStringQuery : IQuery
	{
		[JsonProperty(PropertyName = "query")]
		string Query { get; set; }

		[JsonProperty(PropertyName = "default_field")]
		FieldName DefaultField { get; set; }

		[JsonProperty(PropertyName = "fields")]
		IEnumerable<FieldName> Fields { get; set; }

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

	public class SimpleQueryStringQuery : QueryBase, ISimpleQueryStringQuery
	{
		bool IQuery.Conditionless => IsConditionless(this);
		public string Query { get; set; }
		public FieldName DefaultField { get; set; }
		public IEnumerable<FieldName> Fields { get; set; }
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

	public class SimpleQueryStringQueryDescriptor<T> 
		: QueryDescriptorBase<SimpleQueryStringQueryDescriptor<T>, ISimpleQueryStringQuery> 
		, ISimpleQueryStringQuery where T : class
	{
		bool IQuery.Conditionless => SimpleQueryStringQuery.IsConditionless(this);
		string ISimpleQueryStringQuery.Query { get; set; }
		FieldName ISimpleQueryStringQuery.DefaultField { get; set; }
		IEnumerable<FieldName> ISimpleQueryStringQuery.Fields { get; set; }
		Operator? ISimpleQueryStringQuery.DefaultOperator { get; set; }
		string ISimpleQueryStringQuery.Analyzer { get; set; }
		bool? ISimpleQueryStringQuery.AnalyzeWildcard { get; set; }
		bool? ISimpleQueryStringQuery.LowercaseExpendedTerms { get; set; }
		string ISimpleQueryStringQuery.Flags { get; set; }
		string ISimpleQueryStringQuery.Locale { get; set; }
		string ISimpleQueryStringQuery.MinimumShouldMatch { get; set; }

		public SimpleQueryStringQueryDescriptor<T> DefaultField(string field) => Assign(a => a.DefaultField = field);

		public SimpleQueryStringQueryDescriptor<T> DefaultField(Expression<Func<T, object>> objectPath) => 
			Assign(a => a.DefaultField = objectPath);

		public SimpleQueryStringQueryDescriptor<T> OnFields(IEnumerable<string> fields) =>
			Assign(a => a.Fields = fields?.Select(f => (FieldName) f).ToListOrNullIfEmpty());

		public SimpleQueryStringQueryDescriptor<T> OnFields(params Expression<Func<T, object>>[] objectPaths) =>
			Assign(a => a.Fields = objectPaths?.Select(f => (FieldName) f).ToListOrNullIfEmpty());

		public SimpleQueryStringQueryDescriptor<T> OnFieldsWithBoost(Func<
			FluentDictionary<Expression<Func<T, object>>, double?>, IDictionary<Expression<Func<T, object>>, double?>> boostableSelector) =>
				Assign(a => a.Fields = boostableSelector?
					.Invoke(new FluentDictionary<Expression<Func<T, object>>, double?>())
					.Select(o => FieldName.Create(o.Key, o.Value))
					.ToListOrNullIfEmpty()
				);

		public SimpleQueryStringQueryDescriptor<T> OnFieldsWithBoost(
			Func<FluentDictionary<string, double?>, IDictionary<Expression<Func<T, object>>, double?>> boostableSelector) =>
				Assign(a => a.Fields = boostableSelector?
					.Invoke(new FluentDictionary<string, double?>())
					.Select(o => FieldName.Create(o.Key, o.Value))
					.ToListOrNullIfEmpty()
				);

		public SimpleQueryStringQueryDescriptor<T> Query(string query) => Assign(a => a.Query = query);

		public SimpleQueryStringQueryDescriptor<T> DefaultOperator(Operator op) => Assign(a => a.DefaultOperator = op);

		public SimpleQueryStringQueryDescriptor<T> Analyzer(string analyzer) => Assign(a => a.Analyzer = analyzer);

		public SimpleQueryStringQueryDescriptor<T> Flags(string flags) => Assign(a => a.Flags = flags);

		public SimpleQueryStringQueryDescriptor<T> LowercaseExpendedTerms(bool lowercaseExpendedTerms = true) =>
			Assign(a => a.LowercaseExpendedTerms = lowercaseExpendedTerms);

		public SimpleQueryStringQueryDescriptor<T> AnalyzeWildcard(bool analyzeWildcard = true) =>
			Assign(a => a.AnalyzeWildcard = analyzeWildcard);

		public SimpleQueryStringQueryDescriptor<T> Locale(string locale) => Assign(a => a.Locale = locale);

		public SimpleQueryStringQueryDescriptor<T> MinimumShouldMatch(int minimumShouldMatches) =>
			Assign(a => a.MinimumShouldMatch = minimumShouldMatches.ToString(CultureInfo.InvariantCulture));

		public SimpleQueryStringQueryDescriptor<T> MinimumShouldMatch(string minimumShouldMatch) =>
			Assign(a => a.MinimumShouldMatch = minimumShouldMatch);
    }
}
