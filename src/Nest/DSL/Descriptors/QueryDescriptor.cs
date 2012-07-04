using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using System.Linq.Expressions;

namespace Nest
{
	public class QueryDescriptor : QueryDescriptor<dynamic>
	{

	}
	public class QueryDescriptor<T>  : BaseQuery, IQueryDescriptor<T> where T : class
	{
		[JsonProperty(PropertyName = "match_all")]
		internal MatchAll MatchAllQuery { get; set; }
		[JsonProperty(PropertyName = "term")]
		internal Term TermQuery { get; set; }
		[JsonProperty(PropertyName = "wildcard")]
		internal Wildcard WildcardQuery { get; set; }
		[JsonProperty(PropertyName = "prefix")]
		internal Prefix PrefixQuery { get; set; }
		
		[JsonProperty(PropertyName = "boosting")]
		internal BoostingQueryDescriptor<T> BoostingQueryDescriptor { get; set; }
		[JsonProperty(PropertyName = "ids")]
		internal IdsQuery IdsQuery { get; set; }
		[JsonProperty(PropertyName = "custom_score")]
		internal CustomScoreQueryDescriptor<T> CustomScoreQueryDescriptor { get; set; }
		[JsonProperty(PropertyName = "custom_boost_factor")]
		internal CustomBoostFactorQueryDescriptor<T> CustomBoostFactorQueryDescriptor { get; set; }
		[JsonProperty(PropertyName = "constant_score")]
		internal ConstantScoreQueryDescriptor<T> ConstantScoreQueryDescriptor { get; set; }
		[JsonProperty(PropertyName = "dis_max")]
		internal DismaxQueryDescriptor<T> DismaxQueryDescriptor { get; set; }
		[JsonProperty(PropertyName = "filtered")]
		internal FilteredQueryDescriptor<T> FilteredQueryDescriptor { get; set; }
		[JsonProperty(PropertyName = "text")]
		internal IDictionary<string, object> TextQueryDescriptor { get; set; }
		[JsonProperty(PropertyName = "fuzzy")]
		internal IDictionary<string, object> FuzzyQueryDescriptor { get; set; }
		[JsonProperty(PropertyName = "terms")]
		internal IDictionary<string, object> TermsQueryDescriptor { get; set; }
		[JsonProperty(PropertyName = "query_string")]
		internal QueryStringDescriptor<T> QueryStringDescriptor { get; set; }

		[JsonProperty(PropertyName = "flt")]
		internal FuzzyLikeThisDescriptor<T> FuzzyLikeThisDescriptor { get; set; }
		[JsonProperty(PropertyName = "has_child")]
		internal object HasChildQueryDescriptor { get; set; }
		[JsonProperty(PropertyName = "mlt")]
		internal MoreLikeThisDescriptor<T> MoreLikeThisDescriptor { get; set; }
		[JsonProperty(PropertyName = "range")]
		internal IDictionary<string, object> RangeQueryDescriptor { get; set; }
		
		[JsonProperty(PropertyName = "span_term")]
		internal SpanTerm SpanTermQuery { get; set; }
		[JsonProperty(PropertyName = "span_first")]
		internal SpanFirstQueryDescriptor<T> SpanFirstQueryDescriptor { get; set; }
		[JsonProperty(PropertyName = "span_or")]
		internal SpanOrQueryDescriptor<T> SpanOrQueryDescriptor { get; set; }
		[JsonProperty(PropertyName = "span_near")]
		internal SpanNearQueryDescriptor<T> SpanNearQueryDescriptor { get; set; }
		[JsonProperty(PropertyName = "span_not")]
		internal SpanNotQueryDescriptor<T> SpanNotQueryDescriptor { get; set; }

		[JsonProperty(PropertyName = "top_children")]
		internal object TopChildrenQueryDescriptor { get; set; }
		[JsonProperty(PropertyName = "nested")]
		internal NestedQueryDescriptor<T> NestedQueryDescriptor { get; set; }
		[JsonProperty(PropertyName = "indices")]
		internal IndicesQueryDescriptor<T> IndicesQueryDescriptor { get; set; }


		public QueryDescriptor()
		{
			
		}
		
		/// <summary>
		/// A query that uses a query parser in order to parse its content.
		/// </summary>
		public BaseQuery QueryString(Action<QueryStringDescriptor<T>> selector)
		{
			var query = new QueryStringDescriptor<T>();
			selector(query);
			this.QueryStringDescriptor = query;
			return this;
		}
		/// <summary>
		/// A query that match on any (configurable) of the provided terms. This is a simpler syntax query for using a bool query with several term queries in the should clauses.
		/// </summary>
		public BaseQuery Terms(string field, params string[] terms)
		{
			return this.TermsDescriptor(t => t
				.OnField(field)
				.Terms(terms)
			);
		}
		/// <summary>
		/// A query that match on any (configurable) of the provided terms. This is a simpler syntax query for using a bool query with several term queries in the should clauses.
		/// </summary>
		public BaseQuery Terms(Expression<Func<T, object>> objectPath, params string[] terms)
		{
			var fieldName = ElasticClient.PropertyNameResolver.Resolve(objectPath);
			return this.Terms(fieldName, terms);
		}
		/// <summary>
		/// A query that match on any (configurable) of the provided terms. This is a simpler syntax query for using a bool query with several term queries in the should clauses.
		/// </summary>
		public BaseQuery TermsDescriptor(Action<TermsQueryDescriptor<T>> selector)
		{
			var query = new TermsQueryDescriptor<T>();
			selector(query);
			if (string.IsNullOrWhiteSpace(query._Field))
				throw new DslException("Field name not set for terms query");
			this.TermsQueryDescriptor = new Dictionary<string, object>() 
			{
				{ query._Field, query._Terms}
			};
			if (query._MinMatch.HasValue)
			{
				this.TermsQueryDescriptor.Add("minimum_match", query._MinMatch);
			}
			return new QueryDescriptor<T> { TermsQueryDescriptor = this.TermsQueryDescriptor };
		}


		/// <summary>
		/// A fuzzy based query that uses similarity based on Levenshtein (edit distance) algorithm.
		/// Warning: this query is not very scalable with its default prefix length of 0 – in this case,
		/// every term will be enumerated and cause an edit score calculation or max_expansions is not set.
		/// </summary>
		public BaseQuery Fuzzy(Action<FuzzyQueryDescriptor<T>> selector)
		{
			var query = new FuzzyQueryDescriptor<T>();
			selector(query);
			if (string.IsNullOrWhiteSpace(query._Field))
				throw new DslException("Field name not set for fuzzy query");
			this.FuzzyQueryDescriptor = new Dictionary<string, object>() 
			{
				{ query._Field, query}
			};
			return new QueryDescriptor<T> { FuzzyQueryDescriptor = this.FuzzyQueryDescriptor };
		}
		/// <summary>
		/// fuzzy query on a numeric field will result in a range query “around” the value using the min_similarity value
		/// </summary>
		public BaseQuery FuzzyNumeric(Action<FuzzyNumericQueryDescriptor<T>> selector)
		{
			var query = new FuzzyNumericQueryDescriptor<T>();
			selector(query);
			if (string.IsNullOrWhiteSpace(query._Field))
				throw new DslException("Field name not set for fuzzy query");
			this.FuzzyQueryDescriptor = new Dictionary<string, object>() 
			{
				{ query._Field, query}
			};
			return new QueryDescriptor<T> { FuzzyQueryDescriptor = this.FuzzyQueryDescriptor };
		}
		/// <summary>
		/// fuzzy query on a numeric field will result in a range query “around” the value using the min_similarity value
		/// </summary>
		/// <param name="selector"></param>
		public BaseQuery FuzzyDate(Action<FuzzyDateQueryDescriptor<T>> selector)
		{
			var query = new FuzzyDateQueryDescriptor<T>();
			selector(query);
			if (string.IsNullOrWhiteSpace(query._Field))
				throw new DslException("Field name not set for fuzzy query");
			this.FuzzyQueryDescriptor = new Dictionary<string, object>() 
			{
				{ query._Field, query}
			};
			return new QueryDescriptor<T> { FuzzyQueryDescriptor = this.FuzzyQueryDescriptor };
		}
		
		/// <summary>
		/// The default text query is of type boolean. It means that the text provided is analyzed and the analysis 
		/// process constructs a boolean query from the provided text.
		/// </summary>
		public BaseQuery Text(Action<TextQueryDescriptor<T>> selector)
		{
			var query = new TextQueryDescriptor<T>();
			selector(query);
			if (string.IsNullOrWhiteSpace(query._Field))
				throw new DslException("Field name not set for text query");
			this.TextQueryDescriptor = new Dictionary<string, object>() 
			{
				{ query._Field, query}
			};
			return new QueryDescriptor<T> { TextQueryDescriptor = this.TextQueryDescriptor };
		}
		/// <summary>
		/// The text_phrase query analyzes the text and creates a phrase query out of the analyzed text. 
		/// </summary>
		public BaseQuery TextPhrase(Action<TextPhraseQueryDescriptor<T>> selector)
		{
			var query = new TextPhraseQueryDescriptor<T>();
			selector(query);
			if (string.IsNullOrWhiteSpace(query._Field))
				throw new DslException("Field name not set for text_phrase query");
			this.TextQueryDescriptor = new Dictionary<string, object>() 
			{
				{ query._Field, query}
			};
			return new QueryDescriptor<T> { TextQueryDescriptor = this.TextQueryDescriptor };
		}
		/// <summary>
		/// The text_phrase_prefix is the same as text_phrase, expect it allows for prefix matches on the last term 
		/// in the text
		/// </summary>
		public BaseQuery TextPhrasePrefix(Action<TextPhrasePrefixQueryDescriptor<T>> selector)
		{
			var query = new TextPhrasePrefixQueryDescriptor<T>();
			selector(query);
			if (string.IsNullOrWhiteSpace(query._Field))
				throw new DslException("Field name not set for text_phrase query");
			this.TextQueryDescriptor = new Dictionary<string, object>() 
			{
				{ query._Field, query}
			};
			return new QueryDescriptor<T> { TextQueryDescriptor = this.TextQueryDescriptor };
		}

		/// <summary>
		/// Nested query allows to query nested objects / docs (see nested mapping). The query is executed against the 
		/// nested objects / docs as if they were indexed as separate docs (they are, internally) and resulting in the
		/// root parent doc (or parent nested mapping).
		/// </summary>
		public BaseQuery Nested(Action<NestedQueryDescriptor<T>> selector)
		{
			var query = new NestedQueryDescriptor<T>();
			selector(query);
			this.NestedQueryDescriptor = query;
			return new QueryDescriptor<T> { TextQueryDescriptor = this.TextQueryDescriptor };
		}
		/// <summary>
		/// The indices query can be used when executed across multiple indices, allowing to have a query that executes
		/// only when executed on an index that matches a specific list of indices, and another query that executes 
		/// when it is executed on an index that does not match the listed indices.
		/// </summary>
		public BaseQuery Indices(Action<IndicesQueryDescriptor<T>> selector)
		{
			var query = new IndicesQueryDescriptor<T>();
			selector(query);
			this.IndicesQueryDescriptor = query;
			return new QueryDescriptor<T> { IndicesQueryDescriptor = this.IndicesQueryDescriptor };
		}
		/// <summary>
		/// Matches documents with fields that have terms within a certain range. The type of the Lucene query depends
		/// on the field type, for string fields, the TermRangeQuery, while for number/date fields, the query is
		/// a NumericRangeQuery
		/// </summary>
		public BaseQuery Range(Action<RangeQueryDescriptor<T>> selector)
		{
			var query = new RangeQueryDescriptor<T>();
			selector(query);
			if (string.IsNullOrWhiteSpace(query._Field))
				throw new DslException("Field name not set for range query");
			this.RangeQueryDescriptor = new Dictionary<string, object>() 
			{
				{ query._Field, query}
			};
			return new QueryDescriptor<T> { RangeQueryDescriptor = this.RangeQueryDescriptor };
		}
		/// <summary>
		/// Fuzzy like this query find documents that are “like” provided text by running it against one or more fields.
		/// </summary>
		public BaseQuery FuzzyLikeThis(Action<FuzzyLikeThisDescriptor<T>> selector)
		{
			var query = new FuzzyLikeThisDescriptor<T>();
			selector(query);
			this.FuzzyLikeThisDescriptor = query;
			return new QueryDescriptor<T> { FuzzyLikeThisDescriptor = this.FuzzyLikeThisDescriptor };
		}
		/// <summary>
		/// More like this query find documents that are “like” provided text by running it against one or more fields.
		/// </summary>
		public BaseQuery MoreLikeThis(Action<MoreLikeThisDescriptor<T>> selector)
		{
			var query = new MoreLikeThisDescriptor<T>();
			selector(query);
			this.MoreLikeThisDescriptor = query;
			return new QueryDescriptor<T> { MoreLikeThisDescriptor = this.MoreLikeThisDescriptor };
		}
		/// <summary>
		/// The has_child query works the same as the has_child filter, by automatically wrapping the filter with a 
		/// constant_score.
		/// </summary>
		/// <typeparam name="K">Type of the child</typeparam>
		public BaseQuery HasChild<K>(Action<HasChildQueryDescriptor<K>> selector) where K : class
		{
			var query = new HasChildQueryDescriptor<K>();
			selector(query);
			this.HasChildQueryDescriptor = query;
			return new QueryDescriptor<T> { HasChildQueryDescriptor = this.HasChildQueryDescriptor };
		}
		/// <summary>
		/// The top_children query runs the child query with an estimated hits size, and out of the hit docs, aggregates 
		/// it into parent docs. If there aren’t enough parent docs matching the requested from/size search request, 
		/// then it is run again with a wider (more hits) search.
		/// </summary>
		/// <typeparam name="K">Type of the child</typeparam>
		public BaseQuery TopChildren<K>(Action<TopChildrenQueryDescriptor<K>> selector) where K : class
		{
			var query = new TopChildrenQueryDescriptor<K>();
			selector(query);
			this.TopChildrenQueryDescriptor = query;
			return new QueryDescriptor<T> { TopChildrenQueryDescriptor = this.TopChildrenQueryDescriptor };
		}
		/// <summary>
		/// A query that applies a filter to the results of another query. This query maps to Lucene FilteredQuery.
		/// </summary>
		public BaseQuery Filtered(Action<FilteredQueryDescriptor<T>> selector)
		{
			var query = new FilteredQueryDescriptor<T>();
			selector(query);
			this.FilteredQueryDescriptor = query;
			return new QueryDescriptor<T> { FilteredQueryDescriptor = this.FilteredQueryDescriptor };
		}

		/// <summary>
		/// A query that generates the union of documents produced by its subqueries, and that scores each document 
		/// with the maximum score for that document as produced by any subquery, plus a tie breaking increment for 
		/// any additional matching subqueries.
		/// </summary>
		public BaseQuery Dismax(Action<DismaxQueryDescriptor<T>> selector)
		{
			var query = new DismaxQueryDescriptor<T>();
			selector(query);
			this.DismaxQueryDescriptor = query;
			return new QueryDescriptor<T> { DismaxQueryDescriptor = this.DismaxQueryDescriptor };
		}
		/// <summary>
		/// A query that wraps a filter or another query and simply returns a constant score equal to the query boost 
		/// for every document in the filter. Maps to Lucene ConstantScoreQuery.
		/// </summary>
		public BaseQuery ConstantScore(Action<ConstantScoreQueryDescriptor<T>> selector)
		{
			var query = new ConstantScoreQueryDescriptor<T>();
			selector(query);
			this.ConstantScoreQueryDescriptor = query;
			return new QueryDescriptor<T> { ConstantScoreQueryDescriptor = this.ConstantScoreQueryDescriptor };
		}
		/// <summary>
		/// custom_boost_factor query allows to wrap another query and multiply its score by the provided boost_factor.
		/// This can sometimes be desired since boost value set on specific queries gets normalized, while this 
		/// query boost factor does not.
		/// </summary>
		public BaseQuery CustomBoostFactor(Action<CustomBoostFactorQueryDescriptor<T>> selector)
		{
			var query = new CustomBoostFactorQueryDescriptor<T>();
			selector(query);
			this.CustomBoostFactorQueryDescriptor = query;
			return new QueryDescriptor<T> { CustomBoostFactorQueryDescriptor = this.CustomBoostFactorQueryDescriptor };
		}
		/// <summary>
		/// custom_score query allows to wrap another query and customize the scoring of it optionally with a 
		/// computation derived from other field values in the doc (numeric ones) using script expression
		/// </summary>
		public BaseQuery CustomScore(Action<CustomScoreQueryDescriptor<T>> customScoreQuery)
		{
			var query = new CustomScoreQueryDescriptor<T>();
			customScoreQuery(query);
			this.CustomScoreQueryDescriptor = query;
			return new QueryDescriptor<T> { CustomScoreQueryDescriptor = this.CustomScoreQueryDescriptor };
		}
		/// <summary>
		/// A query that matches documents matching boolean combinations of other queries. The bool query maps to 
		/// Lucene BooleanQuery. 
		/// It is built using one or more boolean clauses, each clause with a typed occurrence
		/// </summary>
		public BaseQuery Bool(Action<BoolQueryDescriptor<T>> booleanQuery)
		{
			var query = new BoolQueryDescriptor<T>();
			booleanQuery(query);
			this.BoolQueryDescriptor = query;
			return new QueryDescriptor<T> { BoolQueryDescriptor = this.BoolQueryDescriptor };
		}
		/// <summary>
		/// he boosting query can be used to effectively demote results that match a given query. 
		/// Unlike the “NOT” clause in bool query, this still selects documents that contain
		/// undesirable terms, but reduces their overall score.
		/// </summary>
		/// <param name="boostingQuery"></param>
		public BaseQuery Boosting(Action<BoostingQueryDescriptor<T>> boostingQuery)
		{
			var query = new BoostingQueryDescriptor<T>();
			boostingQuery(query);
			this.BoostingQueryDescriptor = query;
			return new QueryDescriptor<T> { BoostingQueryDescriptor = this.BoostingQueryDescriptor };
		}
		/// <summary>
		/// A query that matches all documents. Maps to Lucene MatchAllDocsQuery.
		/// </summary>
		/// <param name="NormField">
		/// When indexing, a boost value can either be associated on the document level, or per field. 
		/// The match all query does not take boosting into account by default. In order to take 
		/// boosting into account, the norms_field needs to be provided in order to explicitly specify which
		/// field the boosting will be done on (Note, this will result in slower execution time).
		/// </param>
		public BaseQuery MatchAll(double? Boost = null, string NormField = null)
		{
			this.MatchAllQuery = new MatchAll() { NormField = NormField };
			if (Boost.HasValue)
				this.MatchAllQuery.Boost = Boost.Value;

			return new QueryDescriptor<T> { MatchAllQuery = this.MatchAllQuery };
		}

		/// <summary>
		/// Matches documents that have fields that contain a term (not analyzed). 
		/// The term query maps to Lucene TermQuery. 
		/// </summary>
		public BaseQuery Term(Expression<Func<T, object>> fieldDescriptor
			, string value
			, double? Boost = null)
		{
			var field = ElasticClient.PropertyNameResolver.Resolve(fieldDescriptor);
			return this.Term(field, value, Boost);
		}
		/// <summary>
		/// Matches documents that have fields that contain a term (not analyzed). 
		/// The term query maps to Lucene TermQuery. 
		/// </summary>
		public BaseQuery Term(string field, string value, double? Boost = null)
		{
			var term = new Term() { Field = field, Value = value };
			if (Boost.HasValue)
				term.Boost = Boost;
			this.TermQuery = term;
			return new QueryDescriptor<T> { TermQuery = this.TermQuery };
		}

		/// <summary>
		/// Matches documents that have fields matching a wildcard expression (not analyzed). 
		/// Supported wildcards are *, which matches any character sequence (including the empty one), and ?, 
		/// which matches any single character. Note this query can be slow, as it needs to iterate 
		/// over many terms. In order to prevent extremely slow wildcard queries, a wildcard term should 
		/// not start with one of the wildcards * or ?. The wildcard query maps to Lucene WildcardQuery.
		/// </summary>
		public BaseQuery Wildcard(Expression<Func<T, object>> fieldDescriptor
			, string value
			, double? Boost = null)
		{
			var field = ElasticClient.PropertyNameResolver.Resolve(fieldDescriptor);
			return this.Wildcard(field, value, Boost);
		}
		/// <summary>
		/// Matches documents that have fields matching a wildcard expression (not analyzed). 
		/// Supported wildcards are *, which matches any character sequence (including the empty one), and ?,
		/// which matches any single character. Note this query can be slow, as it needs to iterate over many terms. 
		/// In order to prevent extremely slow wildcard queries, a wildcard term should not start with 
		/// one of the wildcards * or ?. The wildcard query maps to Lucene WildcardQuery.
		/// </summary>
		public BaseQuery Wildcard(string field, string value, double? Boost = null)
		{
			var wildcard = new Wildcard() { Field = field, Value = value };
			if (Boost.HasValue)
				wildcard.Boost = Boost;
			this.WildcardQuery = wildcard;
			return new QueryDescriptor<T> { WildcardQuery = this.WildcardQuery };
		}
		
		/// <summary>
		/// Matches documents that have fields containing terms with a specified prefix (not analyzed). 
		/// The prefix query maps to Lucene PrefixQuery. 
		/// </summary>
		public BaseQuery Prefix(Expression<Func<T, object>> fieldDescriptor
			, string value
			, double? Boost = null)
		{
			var field = ElasticClient.PropertyNameResolver.Resolve(fieldDescriptor);

			return this.Prefix(field, value, Boost);
		}
		/// <summary>
		/// Matches documents that have fields containing terms with a specified prefix (not analyzed). 
		/// The prefix query maps to Lucene PrefixQuery. 
		/// </summary>	
		public BaseQuery Prefix(string field, string value, double? Boost = null)
		{
			var prefix = new Prefix() { Field = field, Value = value };
			if (Boost.HasValue)
				prefix.Boost = Boost;
			this.PrefixQuery = prefix;
			return new QueryDescriptor<T> { PrefixQuery = this.PrefixQuery };
		}
		/// <summary>
		/// Filters documents that only have the provided ids. Note, this filter does not require 
		/// the _id field to be indexed since it works using the _uid field.
		/// </summary>
		public BaseQuery Ids(IEnumerable<string> values)
		{
			this.IdsQuery = new IdsQuery { Values = values };
			return new QueryDescriptor<T> { IdsQuery = this.IdsQuery };
		}
		/// <summary>
		/// Filters documents that only have the provided ids. 
		/// Note, this filter does not require the _id field to be indexed since
		/// it works using the _uid field.
		/// </summary>
		public BaseQuery Ids(string type, IEnumerable<string> values)
		{
			type.ThrowIfNullOrEmpty("type");
			this.IdsQuery = new IdsQuery { Values = values, Type = new[] { type } };
			return new QueryDescriptor<T> { IdsQuery = this.IdsQuery };
		}
		/// <summary>
		/// Filters documents that only have the provided ids. 
		/// Note, this filter does not require the _id field to be indexed since 
		/// it works using the _uid field.
		/// </summary>
		public BaseQuery Ids(IEnumerable<string> types, IEnumerable<string> values)
		{
			this.IdsQuery = new IdsQuery { Values = values, Type = types };
			return new QueryDescriptor<T> { IdsQuery = this.IdsQuery };
		}

		/// <summary>
		/// Matches spans containing a term. The span term query maps to Lucene SpanTermQuery. 
		/// </summary>
		public BaseQuery SpanTerm(Expression<Func<T, object>> fieldDescriptor
				, string value
				, double? Boost = null)
		{
			var field = ElasticClient.PropertyNameResolver.Resolve(fieldDescriptor);
			return this.SpanTerm(field, value, Boost);
		}
		/// <summary>
		/// Matches spans containing a term. The span term query maps to Lucene SpanTermQuery. 
		/// </summary>
		public BaseQuery SpanTerm(string field, string value, double? Boost = null)
		{
			var spanTerm = new SpanTerm() { Field = field, Value = value };
			if (Boost.HasValue)
				spanTerm.Boost = Boost;
			this.SpanTermQuery = spanTerm;
			return new QueryDescriptor<T> { SpanTermQuery = this.SpanTermQuery };
		}
		/// <summary>
		/// Matches spans near the beginning of a field. The span first query maps to Lucene SpanFirstQuery. 
		/// </summary>
		public BaseQuery SpanFirst(Action<SpanFirstQueryDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			this.SpanFirstQueryDescriptor = new SpanFirstQueryDescriptor<T>();
			selector(this.SpanFirstQueryDescriptor);
			return new QueryDescriptor<T> { SpanFirstQueryDescriptor = this.SpanFirstQueryDescriptor };
		}
		/// <summary>
		/// Matches spans which are near one another. One can specify slop, the maximum number of 
		/// intervening unmatched positions, as well as whether matches are required to be in-order.
		/// The span near query maps to Lucene SpanNearQuery.
		/// </summary>
		public BaseQuery SpanNear(Action<SpanNearQueryDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			this.SpanNearQueryDescriptor = new SpanNearQueryDescriptor<T>();
			selector(this.SpanNearQueryDescriptor);
			return new QueryDescriptor<T> { SpanNearQueryDescriptor = this.SpanNearQueryDescriptor };
		}
		/// <summary>
		/// Matches the union of its span clauses. 
		/// The span or query maps to Lucene SpanOrQuery. 
		/// </summary>
		public BaseQuery SpanOr(Action<SpanOrQueryDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			this.SpanOrQueryDescriptor = new SpanOrQueryDescriptor<T>();
			selector(this.SpanOrQueryDescriptor);
			return new QueryDescriptor<T> { SpanOrQueryDescriptor = this.SpanOrQueryDescriptor };
		}
		/// <summary>
		/// Removes matches which overlap with another span query. 
		/// The span not query maps to Lucene SpanNotQuery.
		/// </summary>
		public BaseQuery SpanNot(Action<SpanNotQueryDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			this.SpanNotQueryDescriptor = new SpanNotQueryDescriptor<T>();
			selector(this.SpanNotQueryDescriptor);
			return new QueryDescriptor<T> { SpanNotQueryDescriptor = this.SpanNotQueryDescriptor };
		}
	
	}
}
