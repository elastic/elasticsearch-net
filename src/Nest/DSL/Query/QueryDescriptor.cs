using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public class QueryDescriptor<T> : QueryContainer where T : class
	{
		


		public QueryDescriptor()
		{
		}

		internal QueryDescriptor(bool forceConditionless)
		{
			((IQueryContainer)this).IsConditionless = forceConditionless;
		}

		public QueryDescriptor<T> Strict(bool strict = true)
		{
			var q = new QueryDescriptor<T>();
			((IQueryContainer)q).IsStrict = strict;
			return q;
		}

		public QueryDescriptor<T> Verbatim(bool verbatim = true)
		{
			var q = new QueryDescriptor<T>();
			((IQueryContainer)q).IsStrict = verbatim;
			((IQueryContainer)q).IsVerbatim = verbatim;
			return q;
		}

		private QueryDescriptor<T> CreateConditionlessQueryDescriptor(IQuery query)
		{
			if (this.IsStrict && !this.IsVerbatim)
				throw new DslException("Query resulted in a conditionless {0} query (json by approx):\n{1}"
					.F(
						query.GetType().Name.Replace("Descriptor", "").Replace("`1", "")
						, JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore })
					)
				);
			return new QueryDescriptor<T>(forceConditionless: true);
		}

		private QueryDescriptor<T> New(IQuery query, Action<IQueryContainer> fillProperty)
		{
			if (query.IsConditionless && !this.IsVerbatim)
				return CreateConditionlessQueryDescriptor(query);

			var q = this.Clone();

			if (fillProperty != null)
				fillProperty(q);

			return q;
		}

		protected virtual QueryDescriptor<T> Clone()
		{
			var q = new QueryDescriptor<T>();
			((IQueryContainer)q).IsStrict = this.IsStrict;
			((IQueryContainer)q).IsVerbatim = this.IsVerbatim;
			return q;
		}
		
		/// <summary>
		/// Insert raw query json at this position of the query
		/// <para>Be sure to start your json with '{'</para>
		/// </summary>
		/// <param name="rawJson"></param>
		/// <returns></returns>
		public QueryContainer Raw(string rawJson)
		{
			var f = new QueryDescriptor<T>();
			((IQueryContainer)f).IsStrict = this.IsStrict;
			((IQueryContainer)f).IsVerbatim = this.IsVerbatim;
			((IQueryContainer)f).RawQuery = rawJson;
			return f;
		}

		/// <summary>
		/// A query that uses a query parser in order to parse its content.
		/// </summary>
		public QueryContainer QueryString(Action<QueryStringQueryDescriptor<T>> selector)
		{
			var query = new QueryStringQueryDescriptor<T>();
			selector(query);
			return this.New(query, q => ((IQueryContainer)q).QueryString = query);
		}

		/// <summary>
		/// A query that uses the SimpleQueryParser to parse its context. 
		/// Unlike the regular query_string query, the simple_query_string query will 
		/// never throw an exception, and discards invalid parts of the query. 
		/// </summary>
		public QueryContainer SimpleQueryString(Action<SimpleQueryStringQueryDescriptor<T>> selector)
		{
			var query = new SimpleQueryStringQueryDescriptor<T>();
			selector(query);
			return this.New(query, q => ((IQueryContainer)q).SimpleQueryString = query);
		}
		
		/// <summary>
		/// A query that match on any (configurable) of the provided terms. This is a simpler syntax query for using a bool query with several term queries in the should clauses.
		/// </summary>
		public QueryContainer Terms(string field, IEnumerable<string> terms)
		{
			return this.TermsDescriptor(t => t
				.OnField(field)
				.Terms(terms)
			);
		}
		/// <summary>
		/// A query that match on any (configurable) of the provided terms. This is a simpler syntax query for using a bool query with several term queries in the should clauses.
		/// </summary>
		public QueryContainer Terms<K>(Expression<Func<T, K>> objectPath, IEnumerable<K> terms)
		{
			return this.TermsDescriptor<K>(t => t
				.OnField(objectPath)
				.Terms(terms)
			);
		}
		/// <summary>
		/// A query that match on any (configurable) of the provided terms. This is a simpler syntax query for using a bool query with several term queries in the should clauses.
		/// </summary>
		public QueryContainer Terms(Expression<Func<T, object>> objectPath, IEnumerable<string> terms)
		{
			return this.TermsDescriptor(t => t
				.OnField(objectPath)
				.Terms(terms)
			);
		}

		/// <summary>
		/// A query that match on any (configurable) of the provided terms. This is a simpler syntax query for using a bool query with several term queries in the should clauses.
		/// </summary>
		public QueryContainer TermsDescriptor(Action<TermsQueryDescriptor<T, object>> selector)
		{
			return this.TermsDescriptor<object>(selector);

		}
		/// <summary>
		/// A query that match on any (configurable) of the provided terms. This is a simpler syntax query for using a bool query with several term queries in the should clauses.
		/// </summary>
		public QueryContainer TermsDescriptor<K>(Action<TermsQueryDescriptor<T, K>> selector)
		{
			var query = new TermsQueryDescriptor<T, K>();
			selector(query);

			return this.New(query, q => q.Terms = query);
		}


		/// <summary>
		/// A fuzzy based query that uses similarity based on Levenshtein (edit distance) algorithm.
		/// Warning: this query is not very scalable with its default prefix length of 0 – in this case,
		/// every term will be enumerated and cause an edit score calculation or max_expansions is not set.
		/// </summary>
		public QueryContainer Fuzzy(Action<FuzzyQueryDescriptor<T>> selector)
		{
			var query = new FuzzyQueryDescriptor<T>();
			selector(query);

			return this.New(query, q => q.Fuzzy = query);
		}
		/// <summary>
		/// fuzzy query on a numeric field will result in a range query “around” the value using the min_similarity value
		/// </summary>
		public QueryContainer FuzzyNumeric(Action<FuzzyNumericQueryDescriptor<T>> selector)
		{
			var query = new FuzzyNumericQueryDescriptor<T>();
			selector(query);

			return this.New(query, q => q.Fuzzy = query);
		}
		/// <summary>
		/// fuzzy query on a numeric field will result in a range query “around” the value using the min_similarity value
		/// </summary>
		/// <param name="selector"></param>
		public QueryContainer FuzzyDate(Action<FuzzyDateQueryDescriptor<T>> selector)
		{
			var query = new FuzzyDateQueryDescriptor<T>();
			selector(query);

			return this.New(query, q => q.Fuzzy = query);
		}


		/// <summary>
		/// The default text query is of type boolean. It means that the text provided is analyzed and the analysis 
		/// process constructs a boolean query from the provided text.
		/// </summary>
		public QueryContainer Match(Action<MatchQueryDescriptor<T>> selector)
		{
			var query = new MatchQueryDescriptor<T>();
			selector(query);

			return this.New(query, q => q.Match = query);
		}

		/// <summary>
		/// The text_phrase query analyzes the text and creates a phrase query out of the analyzed text. 
		/// </summary>
		public QueryContainer MatchPhrase(Action<MatchPhraseQueryDescriptor<T>> selector)
		{
			var query = new MatchPhraseQueryDescriptor<T>();
			selector(query);

			return this.New(query, q => q.Match = query);
		}

		/// <summary>
		/// The text_phrase_prefix is the same as text_phrase, expect it allows for prefix matches on the last term 
		/// in the text
		/// </summary>
		public QueryContainer MatchPhrasePrefix(Action<MatchPhrasePrefixQueryDescriptor<T>> selector)
		{
			var query = new MatchPhrasePrefixQueryDescriptor<T>();
			selector(query);

			return this.New(query, q => q.Match = query);
		}

		/// <summary>
		/// The multi_match query builds further on top of the match query by allowing multiple fields to be specified. 
		/// The idea here is to allow to more easily build a concise match type query over multiple fields instead of using a 
		/// relatively more expressive query by using multiple match queries within a bool query.
		/// </summary>
		public QueryContainer MultiMatch(Action<MultiMatchQueryDescriptor<T>> selector)
		{
			var query = new MultiMatchQueryDescriptor<T>();
			selector(query);

			return this.New(query, q => q.MultiMatch = query);
		}

		/// <summary>
		/// Nested query allows to query nested objects / docs (see nested mapping). The query is executed against the 
		/// nested objects / docs as if they were indexed as separate docs (they are, internally) and resulting in the
		/// root parent doc (or parent nested mapping).
		/// </summary>
		public QueryContainer Nested(Action<NestedQueryDescriptor<T>> selector)
		{
			var query = new NestedQueryDescriptor<T>();
			selector(query);

			return this.New(query, q => q.Nested = query);
		}

		/// <summary>
		/// A thin wrapper allowing fined grained control what should happen if a query is conditionless
		/// if you need to fallback to something other than a match_all query
		/// </summary>
		public QueryContainer Conditionless(Action<ConditionlessQueryDescriptor<T>> selector)
		{
			var query = new ConditionlessQueryDescriptor<T>();
			selector(query);

			return (query._Query == null || query._Query.IsConditionless) ? query._Fallback : query._Query;
		}


		/// <summary>
		/// The indices query can be used when executed across multiple indices, allowing to have a query that executes
		/// only when executed on an index that matches a specific list of indices, and another query that executes 
		/// when it is executed on an index that does not match the listed indices.
		/// </summary>
		public QueryContainer Indices(Action<IndicesQueryDescriptor<T>> selector)
		{
			var query = new IndicesQueryDescriptor<T>();
			selector(query);

			return this.New(query, q => q.Indices = query);
		}
		/// <summary>
		/// Matches documents with fields that have terms within a certain range. The type of the Lucene query depends
		/// on the field type, for string fields, the TermRangeQuery, while for number/date fields, the query is
		/// a NumericRangeQuery
		/// </summary>
		public QueryContainer Range(Action<RangeQueryDescriptor<T>> selector)
		{
			var query = new RangeQueryDescriptor<T>();
			selector(query);
			
			return this.New(query, q => q.Range = query);
		}
		/// <summary>
		/// Fuzzy like this query find documents that are “like” provided text by running it against one or more fields.
		/// </summary>
		public QueryContainer FuzzyLikeThis(Action<FuzzyLikeThisQueryDescriptor<T>> selector)
		{
			var query = new FuzzyLikeThisQueryDescriptor<T>();
			selector(query);

			return this.New(query, q => q.FuzzyLikeThis = query);
		}
		/// <summary>
		/// More like this query find documents that are “like” provided text by running it against one or more fields.
		/// </summary>
		public QueryContainer MoreLikeThis(Action<MoreLikeThisQueryDescriptor<T>> selector)
		{
			var query = new MoreLikeThisQueryDescriptor<T>();
			selector(query);

			return this.New(query, q => q.MoreLikeThis = query);
		}
		
		/// <summary>
		/// The geo_shape Filter uses the same grid square representation as the geo_shape mapping to find documents 
		/// that have a shape that intersects with the envelope shape. 
		/// It will also use the same PrefixTree configuration as defined for the field mapping.
		/// </summary>
		public QueryContainer GeoShapeEnvelope(Action<GeoShapeEnvelopeQueryDescriptor<T>> selector)
		{
			var query = new GeoShapeEnvelopeQueryDescriptor<T>();
			selector(query);
			return this.New(query, q => q.GeoShape = query);
		}

		/// <summary>
		/// The geo_shape Filter uses the same grid square representation as the geo_shape mapping to find documents 
		/// that have a shape that intersects with the circle shape. 
		/// It will also use the same PrefixTree configuration as defined for the field mapping.
		/// </summary>
		public QueryContainer GeoShapeCircle(Action<GeoShapeCircleQueryDescriptor<T>> selector)
		{
			var query = new GeoShapeCircleQueryDescriptor<T>();
			selector(query);
			return this.New(query, q => q.GeoShape = query);
		}

		/// <summary>
		/// The geo_shape Filter uses the same grid square representation as the geo_shape mapping to find documents 
		/// that have a shape that intersects with the line string shape. 
		/// It will also use the same PrefixTree configuration as defined for the field mapping.
		/// </summary>
		public QueryContainer GeoShapeLineString(Action<GeoShapeLineStringQueryDescriptor<T>> selector)
		{
			var query = new GeoShapeLineStringQueryDescriptor<T>();
			selector(query);
			return this.New(query, q => q.GeoShape = query);
		}

		/// <summary>
		/// The geo_shape circle Filter uses the same grid square representation as the geo_shape mapping to find documents 
		/// that have a shape that intersects with the multi line string shape. 
		/// It will also use the same PrefixTree configuration as defined for the field mapping.
		/// </summary>
		public QueryContainer GeoShapeMultiLineString(Action<GeoShapeMultiLineStringQueryDescriptor<T>> selector)
		{
			var query = new GeoShapeMultiLineStringQueryDescriptor<T>();
			selector(query);
			return this.New(query, q => q.GeoShape = query);
		}

		/// <summary>
		/// The geo_shape circle Filter uses the same grid square representation as the geo_shape mapping to find documents 
		/// that have a shape that intersects with the point shape. 
		/// It will also use the same PrefixTree configuration as defined for the field mapping.
		/// </summary>
		public QueryContainer GeoShapePoint(Action<GeoShapePointQueryDescriptor<T>> selector)
		{
			var query = new GeoShapePointQueryDescriptor<T>();
			selector(query);
			return this.New(query, q => q.GeoShape = query);
		}

		/// <summary>
		/// The geo_shape circle Filter uses the same grid square representation as the geo_shape mapping to find documents 
		/// that have a shape that intersects with the multi point shape. 
		/// It will also use the same PrefixTree configuration as defined for the field mapping.
		/// </summary>
		public QueryContainer GeoShapeMultiPoint(Action<GeoShapeMultiPointQueryDescriptor<T>> selector)
		{
			var query = new GeoShapeMultiPointQueryDescriptor<T>();
			selector(query);
			return this.New(query, q => q.GeoShape = query);
		}

		/// <summary>
		/// The geo_shape circle Filter uses the same grid square representation as the geo_shape mapping to find documents 
		/// that have a shape that intersects with the polygon shape. 
		/// It will also use the same PrefixTree configuration as defined for the field mapping.
		/// </summary>
		public QueryContainer GeoShapePolygon(Action<GeoShapePolygonQueryDescriptor<T>> selector)
		{
			var query = new GeoShapePolygonQueryDescriptor<T>();
			selector(query);
			return this.New(query, q => q.GeoShape = query);
		}

		/// <summary>
		/// The geo_shape circle Filter uses the same grid square representation as the geo_shape mapping to find documents 
		/// that have a shape that intersects with the multi polygon shape. 
		/// It will also use the same PrefixTree configuration as defined for the field mapping.
		/// </summary>
		public QueryContainer GeoShapeMultiPolygon(Action<GeoShapeMultiPolygonQueryDescriptor<T>> selector)
		{
			var query = new GeoShapeMultiPolygonQueryDescriptor<T>();
			selector(query);
			return this.New(query, q => q.GeoShape = query);
		}

		/// <summary>
		/// The common terms query is a modern alternative to stopwords which improves the precision and recall 
		/// of search results (by taking stopwords into account), without sacrificing performance.
		/// </summary>
		public QueryContainer CommonTerms(Action<CommonTermsQueryDescriptor<T>> selector)
		{
			var query = new CommonTermsQueryDescriptor<T>();
			selector(query);
			
			return this.New(query, q => q.CommonTerms = query);
		}

		/// <summary>
		/// The has_child query works the same as the has_child filter, by automatically wrapping the filter with a 
		/// constant_score.
		/// </summary>
		/// <typeparam name="K">Type of the child</typeparam>
		public QueryContainer HasChild<K>(Action<HasChildQueryDescriptor<K>> selector) where K : class
		{
			var query = new HasChildQueryDescriptor<K>();
			selector(query);

			return this.New(query, q => q.HasChild = query);
		}
		/// <summary>
		/// The has_child query works the same as the has_child filter, by automatically wrapping the filter with a 
		/// constant_score.
		/// </summary>
		/// <typeparam name="K">Type of the child</typeparam>
		public QueryContainer HasParent<K>(Action<HasParentQueryDescriptor<K>> selector) where K : class
		{
			var query = new HasParentQueryDescriptor<K>();
			selector(query);

			return this.New(query, q => q.HasParent = query);
		}
		/// <summary>
		/// The top_children query runs the child query with an estimated hits size, and out of the hit docs, aggregates 
		/// it into parent docs. If there aren’t enough parent docs matching the requested from/size search request, 
		/// then it is run again with a wider (more hits) search.
		/// </summary>
		/// <typeparam name="K">Type of the child</typeparam>
		public QueryContainer TopChildren<K>(Action<TopChildrenQueryDescriptor<K>> selector) where K : class
		{
			var query = new TopChildrenQueryDescriptor<K>();
			selector(query);

			return this.New(query, q => q.TopChildren = query);
		}
		/// <summary>
		/// A query that applies a filter to the results of another query. This query maps to Lucene FilteredQuery.
		/// </summary>
		public QueryContainer Filtered(Action<FilteredQueryDescriptor<T>> selector)
		{
			var query = new FilteredQueryDescriptor<T>();
			selector(query);

			var filtered = query as IFilteredQuery;
			
			if (filtered.Query != null && filtered.Query.IsConditionless)
				filtered.Query = null;
			
			if (filtered.Filter != null && filtered.Filter.IsConditionless)
				filtered.Filter = null;

			return this.New(query, q => q.Filtered = query);
		}

		/// <summary>
		/// A query that generates the union of documents produced by its subqueries, and that scores each document 
		/// with the maximum score for that document as produced by any subquery, plus a tie breaking increment for 
		/// any additional matching subqueries.
		/// </summary>
		public QueryContainer Dismax(Action<DisMaxQueryDescriptor<T>> selector)
		{
			var query = new DisMaxQueryDescriptor<T>();
			selector(query);

			return this.New(query, q => q.DisMax = query);
		}
		/// <summary>
		/// A query that wraps a filter or another query and simply returns a constant score equal to the query boost 
		/// for every document in the filter. Maps to Lucene ConstantScoreQuery.
		/// </summary>
		public QueryContainer ConstantScore(Action<ConstantScoreQueryDescriptor<T>> selector)
		{
			var query = new ConstantScoreQueryDescriptor<T>();
			selector(query);

			return this.New(query, q => q.ConstantScore = query);
		}
		/// <summary>
		/// custom_boost_factor query allows to wrap another query and multiply its score by the provided boost_factor.
		/// This can sometimes be desired since boost value set on specific queries gets normalized, while this 
		/// query boost factor does not.
		/// </summary>
		[Obsolete("Custom boost factor has been removed in 1.1")]
		public QueryContainer CustomBoostFactor(Action<CustomBoostFactorQueryDescriptor<T>> selector)
		{
			var query = new CustomBoostFactorQueryDescriptor<T>();
			selector(query);

			return this.New(query, q => q.CustomBoostFactor = query);
		}
		/// <summary>
		/// custom_score query allows to wrap another query and customize the scoring of it optionally with a 
		/// computation derived from other field values in the doc (numeric ones) using script expression
		/// </summary>
		[Obsolete("Custom score has been removed in 1.1")]
		public QueryContainer CustomScore(Action<CustomScoreQueryDescriptor<T>> customScoreQuery)
		{
			var query = new CustomScoreQueryDescriptor<T>();
			customScoreQuery(query);

			return this.New(query, q => q.CustomScore = query);
		}
		/// <summary>
		/// custom_score query allows to wrap another query and customize the scoring of it optionally with a 
		/// computation derived from other field values in the doc (numeric ones) using script or boost expression
		/// </summary>
		public QueryContainer CustomFiltersScore(Action<CustomFiltersScoreQueryDescriptor<T>> customFiltersScoreQuery)
		{
			var query = new CustomFiltersScoreQueryDescriptor<T>();
			customFiltersScoreQuery(query);

			return this.New(query, q => q.CustomFiltersScore = query);
		}
		/// <summary>
		/// A query that matches documents matching boolean combinations of other queries. The bool query maps to 
		/// Lucene BooleanQuery. 
		/// It is built using one or more boolean clauses, each clause with a typed occurrence
		/// </summary>
		public QueryContainer Bool(Action<BoolQueryDescriptor<T>> booleanQuery)
		{
			var query = new BoolQueryDescriptor<T>();
			booleanQuery(query);
			return this.New(query, q => q.Bool = query);
		}

		/// <summary>
		/// the boosting query can be used to effectively demote results that match a given query. 
		/// Unlike the “NOT” clause in bool query, this still selects documents that contain
		/// undesirable terms, but reduces their overall score.
		/// </summary>
		/// <param name="boostingQuery"></param>
		public QueryContainer Boosting(Action<BoostingQueryDescriptor<T>> boostingQuery)
		{
			var query = new BoostingQueryDescriptor<T>();
			boostingQuery(query);

			return this.New(query, q => q.Boosting = query);
		}

		/// <summary>
		/// A query that matches all documents. Maps to Lucene MatchAllDocsQuery.
		/// </summary>
		/// <param name="Boost">An optional boost to associate with this match_all</param>
		/// <param name="NormField">
		/// When indexing, a boost value can either be associated on the document level, or per field. 
		/// The match all query does not take boosting into account by default. In order to take 
		/// boosting into account, the norms_field needs to be provided in order to explicitly specify which
		/// field the boosting will be done on (Note, this will result in slower execution time).
		/// </param>
		public QueryContainer MatchAll(double? Boost = null, string NormField = null)
		{
			var query = new MatchAllQuery() { NormField = NormField };
			if (Boost.HasValue)
				query.Boost = Boost.Value;

			return this.New(query, q => q.MatchAllQuery = query);
		}

		/// <summary>
		/// Matches documents that have fields that contain a term (not analyzed). 
		/// The term query maps to Lucene TermQuery. 
		/// </summary>
		public QueryContainer Term<K>(Expression<Func<T, K>> fieldDescriptor, K value, double? Boost = null)
		{
			return this.Term(t =>
			{
				t.OnField(fieldDescriptor).Value(value);
				if (Boost.HasValue)
					t.Boost(Boost.Value);
			});
		}
		/// <summary>
		/// Matches documents that have fields that contain a term (not analyzed). 
		/// The term query maps to Lucene TermQuery. 
		/// </summary>
		public QueryContainer Term(Expression<Func<T, object>> fieldDescriptor, object value, double? Boost = null)
		{
			return this.Term(t =>
			{
				t.OnField(fieldDescriptor).Value(value);
				if (Boost.HasValue)
					t.Boost(Boost.Value);
			});
		}
		/// <summary>
		/// Matches documents that have fields that contain a term (not analyzed). 
		/// The term query maps to Lucene TermQuery. 
		/// </summary>
		public QueryContainer Term(string field, object value, double? Boost = null)
		{
			return this.Term(t =>
			{
				t.OnField(field).Value(value);
				if (Boost.HasValue)
					t.Boost(Boost.Value);
			});
		}
		/// <summary>
		/// Matches documents that have fields that contain a term (not analyzed). 
		/// The term query maps to Lucene TermQuery. 
		/// </summary>
		public QueryContainer Term(Action<TermQueryDescriptor<T>> termSelector)
		{
			var termQuery = new TermQueryDescriptor<T>();
			termSelector(termQuery);
			return this.New(termQuery, q => q.Term = termQuery);
		}
		/// <summary>
		/// Matches documents that have fields matching a wildcard expression (not analyzed). 
		/// Supported wildcards are *, which matches any character sequence (including the empty one), and ?, 
		/// which matches any single character. Note this query can be slow, as it needs to iterate 
		/// over many terms. In order to prevent extremely slow wildcard queries, a wildcard term should 
		/// not start with one of the wildcards * or ?. The wildcard query maps to Lucene WildcardQuery.
		/// </summary>
		public QueryContainer Wildcard(Expression<Func<T, object>> fieldDescriptor, string value, double? Boost = null, RewriteMultiTerm? Rewrite = null)
		{
			return this.Wildcard(t =>
			{
				t.OnField(fieldDescriptor).Value(value);
				if (Boost.HasValue) t.Boost(Boost.Value);
				if (Rewrite.HasValue) t.Rewrite(Rewrite.Value);
			});
		}
		/// <summary>
		/// Matches documents that have fields matching a wildcard expression (not analyzed). 
		/// Supported wildcards are *, which matches any character sequence (including the empty one), and ?,
		/// which matches any single character. Note this query can be slow, as it needs to iterate over many terms. 
		/// In order to prevent extremely slow wildcard queries, a wildcard term should not start with 
		/// one of the wildcards * or ?. The wildcard query maps to Lucene WildcardQuery.
		/// </summary>
		public QueryContainer Wildcard(string field, string value, double? Boost = null, RewriteMultiTerm? Rewrite = null)
		{
			return this.Wildcard(t =>
			{
				t.OnField(field).Value(value);
				if (Boost.HasValue) t.Boost(Boost.Value);
				if (Rewrite.HasValue) t.Rewrite(Rewrite.Value);
			});
		}
		
		/// <summary>
		/// Matches documents that have fields matching a wildcard expression (not analyzed). 
		/// Supported wildcards are *, which matches any character sequence (including the empty one), and ?,
		/// which matches any single character. Note this query can be slow, as it needs to iterate over many terms. 
		/// In order to prevent extremely slow wildcard queries, a wildcard term should not start with 
		/// one of the wildcards * or ?. The wildcard query maps to Lucene WildcardQuery.
		/// </summary>
		public QueryContainer Wildcard(Action<WildcardQueryDescriptor<T>> wildcardSelector)
		{
			var wildcardQuery = new WildcardQueryDescriptor<T>();
			wildcardSelector(wildcardQuery);
			return this.New(wildcardQuery, q => q.Wildcard = wildcardQuery);
		}
		/// <summary>
		/// Matches documents that have fields containing terms with a specified prefix (not analyzed). 
		/// The prefix query maps to Lucene PrefixQuery. 
		/// </summary>
		public QueryContainer Prefix(Expression<Func<T, object>> fieldDescriptor, string value, double? Boost = null, RewriteMultiTerm? Rewrite = null)
		{
			return this.Prefix(t =>
			{
				t.OnField(fieldDescriptor).Value(value);
				if (Boost.HasValue) t.Boost(Boost.Value);
				if (Rewrite.HasValue) t.Rewrite(Rewrite.Value);
			});
		}

		/// <summary>
		/// Matches documents that have fields containing terms with a specified prefix (not analyzed). 
		/// The prefix query maps to Lucene PrefixQuery. 
		/// </summary>	
		public QueryContainer Prefix(string field, string value, double? Boost = null, RewriteMultiTerm? Rewrite = null)
		{
			return this.Prefix(t =>
			{
				t.OnField(field).Value(value);
				if (Boost.HasValue) t.Boost(Boost.Value);
				if (Rewrite.HasValue) t.Rewrite(Rewrite.Value);
			});
		}
		
		/// <summary>
		/// Matches documents that have fields containing terms with a specified prefix (not analyzed). 
		/// The prefix query maps to Lucene PrefixQuery. 
		/// </summary>	
		public QueryContainer Prefix(Action<PrefixQueryDescriptor<T>> prefixSelector)
		{
			var prefixQuery = new PrefixQueryDescriptor<T>();
			prefixSelector(prefixQuery);
			return this.New(prefixQuery, q => q.Prefix = prefixQuery);
		}
		/// <summary>
		/// Filters documents that only have the provided ids. Note, this filter does not require 
		/// the _id field to be indexed since it works using the _uid field.
		/// </summary>
		public QueryContainer Ids(IEnumerable<string> values)
		{
			var ids = new IdsQueryDescriptor { Values = values };
			return this.New(ids, q => q.Ids = ids);
		}

		/// <summary>
		/// Filters documents that only have the provided ids. 
		/// Note, this filter does not require the _id field to be indexed since
		/// it works using the _uid field.
		/// </summary>
		public QueryContainer Ids(string type, IEnumerable<string> values)
		{
			type.ThrowIfNullOrEmpty("type");
			var ids = new IdsQueryDescriptor { Values = values, Type = new[] { type } };
			return this.New(ids, q => q.Ids = ids);
		}

		/// <summary>
		/// Filters documents that only have the provided ids. 
		/// Note, this filter does not require the _id field to be indexed since 
		/// it works using the _uid field.
		/// </summary>
		public QueryContainer Ids(IEnumerable<string> types, IEnumerable<string> values)
		{
			var ids = new IdsQueryDescriptor { Values = values, Type = types };
			return this.New(ids, q => q.Ids = ids);
		}

		/// <summary>
		/// Matches spans containing a term. The span term query maps to Lucene SpanTermQuery. 
		/// </summary>
		public QueryContainer SpanTerm(Expression<Func<T, object>> fieldDescriptor , string value , double? Boost = null)
		{
			return this.SpanTerm(t =>
			{
				t.OnField(fieldDescriptor).Value(value);
				if (Boost.HasValue) t.Boost(Boost.Value);
			});
		}

		/// <summary>
		/// Matches spans containing a term. The span term query maps to Lucene SpanTermQuery. 
		/// </summary>
		public QueryContainer SpanTerm(string field, string value, double? Boost = null)
		{
			return this.SpanTerm(t =>
			{
				t.OnField(field).Value(value);
				if (Boost.HasValue) t.Boost(Boost.Value);
			});
		}

		/// <summary>
		/// Matches spans containing a term. The span term query maps to Lucene SpanTermQuery. 
		/// </summary>
		public QueryContainer SpanTerm(Action<SpanTermQueryDescriptor<T>> spanTermSelector)
		{
			var spanTerm = new SpanTermQueryDescriptor<T>();
			spanTermSelector(spanTerm);
			return this.New(spanTerm, q => q.SpanTerm = spanTerm);
		}
		/// <summary>
		/// Matches spans near the beginning of a field. The span first query maps to Lucene SpanFirstQuery. 
		/// </summary>
		public QueryContainer SpanFirst(Action<SpanFirstQueryDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			var span = new SpanFirstQueryDescriptor<T>();
			selector(span);
			return this.New(span, q => q.SpanFirst = span);
		}

		/// <summary>
		/// Matches spans which are near one another. One can specify slop, the maximum number of 
		/// intervening unmatched positions, as well as whether matches are required to be in-order.
		/// The span near query maps to Lucene SpanNearQuery.
		/// </summary>
		public QueryContainer SpanNear(Action<SpanNearQueryDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			var span = new SpanNearQueryDescriptor<T>();
			selector(span);

			return this.New(span, q => q.SpanNear = span);
		}

		/// <summary>
		/// Matches the union of its span clauses. 
		/// The span or query maps to Lucene SpanOrQuery. 
		/// </summary>
		public QueryContainer SpanOr(Action<SpanOrQueryDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			var span = new SpanOrQueryDescriptor<T>();
			selector(span);

			return this.New(span, q => q.SpanOr = span);
		}

		/// <summary>
		/// Removes matches which overlap with another span query. 
		/// The span not query maps to Lucene SpanNotQuery.
		/// </summary>
		public QueryContainer SpanNot(Action<SpanNotQuery<T>> selector)
		{
			selector.ThrowIfNull("selector");
			var span = new SpanNotQuery<T>();
			selector(span);

			return this.New(span, q => q.SpanNot = span);
		}

		/// <summary>
		/// Wrap a multi term query (one of fuzzy, prefix, term range or regexp query) 
		/// as a span query so it can be nested.
		/// </summary>
		public QueryContainer SpanMultiTerm(Action<SpanMultiTermQueryDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			var span = new SpanMultiTermQueryDescriptor<T>();
			selector(span);

			return this.New(span, q => q.SpanMultiTerm = span);
		}

		/// <summary>
		/// custom_score query allows to wrap another query and customize the scoring of it optionally with a 
		/// computation derived from other field values in the doc (numeric ones) using script or boost expression
		/// </summary>
		public QueryContainer Regexp(Action<RegexpQueryDescriptor<T>> regexpSelector)
		{
			var query = new RegexpQueryDescriptor<T>();
			regexpSelector(query);
			return this.New(query, q => q.Regexp = query);
		}

		/// <summary>
		/// Function score query
		/// </summary>
		/// <returns></returns>
		public QueryContainer FunctionScore(Action<FunctionScoreQueryDescriptor<T>> functionScoreQuery)
		{
			var query = new FunctionScoreQueryDescriptor<T>();
			functionScoreQuery(query);
			return this.New(query, q => q.FunctionScore = query);

		}
	}
}
