using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Runtime.InteropServices;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public class QueryContainerDescriptor<T> : QueryContainer where T : class
	{
		private IQueryContainer Self => this;

		QueryContainerDescriptor<T> _assign(Action<IQueryContainer> assigner) =>
			Fluent.Assign<QueryContainerDescriptor<T>, IQueryContainer>(new QueryContainerDescriptor<T>(), a =>
			{
				a.IsStrict = this.IsStrict;
				a.IsVerbatim = this.IsVerbatim;
				assigner(a);
			});

		//TODO remove all the shortcuts into descriptors except for .Term(s)()
		public QueryContainerDescriptor() { }

		internal QueryContainerDescriptor(bool forceConditionless)
		{
			Self.IsConditionless = forceConditionless;
		}

		public QueryContainerDescriptor<T> Strict(bool strict = true) => _assign(a => a.IsStrict = strict);

		public QueryContainerDescriptor<T> Verbatim(bool verbatim = true) => _assign(a =>
		{
			//TODO do we need to set IsStrict to verbatim value here?
			a.IsStrict = verbatim;
			a.IsVerbatim = verbatim;
		});

		private QueryContainer _assignSelector<TQuery, TQueryInterface>(
			Func<TQuery, TQueryInterface> create,
			Action<TQueryInterface, IQueryContainer> assign
			)
			where TQuery : TQueryInterface, IQuery, new()
			where TQueryInterface : IQuery
		{
			var query = create(new TQuery());

			//if query is not conditionless or is verbatim: return a container that holds the query
			if (!query.Conditionless || this.IsVerbatim)
				return _assign(a => assign(query, a));

			//query is conditionless but the container is marked as strict, throw exception
			if (this.IsStrict)
				throw new DslException(CreateConditionlessWhenStrictExceptionMessage(query));

			//query is conditionless return an empty container that can later be rewritten
			return CreateEmptyContainer();
		}

		/// <summary>
		/// Insert raw query json at this position of the query
		/// <para>Be sure to start your json with '{'</para>
		/// </summary>
		/// <param name="rawJson"></param>
		/// <returns></returns>
		public QueryContainer Raw(string rawJson) => _assign(a => a.RawQuery = rawJson);

		/// <summary>
		/// A query that uses a query parser in order to parse its content.
		/// </summary>
		public QueryContainer QueryString(Func<QueryStringQueryDescriptor<T>, IQueryStringQuery> selector) =>
			this._assignSelector(selector, (query, container) => container.QueryString = query);

		/// <summary>
		/// A query that uses the SimpleQueryParser to parse its context. 
		/// Unlike the regular query_string query, the simple_query_string query will 
		/// never throw an exception, and discards invalid parts of the query. 
		/// </summary>
		public QueryContainer SimpleQueryString(Func<SimpleQueryStringQueryDescriptor<T>, ISimpleQueryStringQuery> selector) =>
			this._assignSelector(selector, (query, container) => container.SimpleQueryString = query);

		/// <summary>
		/// A query that match on any (configurable) of the provided terms. This is a simpler syntax query for using a bool query with several term queries in the should clauses.
		/// </summary>
		public QueryContainer Terms(string field, IEnumerable<string> terms) =>
			this.Terms(t => t.Field(field).Terms(terms));

		/// <summary>
		/// A query that match on any (configurable) of the provided terms. This is a simpler syntax query for using a bool query with several term queries in the should clauses.
		/// </summary>
		public QueryContainer Terms<K>(Expression<Func<T, object>> objectPath, IEnumerable<K> terms) =>
			this.Terms<K>(t => t.Field(objectPath).Terms(terms));

		/// <summary>
		/// A query that match on any (configurable) of the provided terms. This is a simpler syntax query for using a bool query with several term queries in the should clauses.
		/// </summary>
		public QueryContainer Terms(Func<TermsQueryDescriptor<T, object>, ITermsQuery> selector) =>
			this.Terms<object>(selector);

		/// <summary>
		/// A query that match on any (configurable) of the provided terms. This is a simpler syntax query for using a bool query with several term queries in the should clauses.
		/// </summary>
		public QueryContainer Terms(Expression<Func<T, object>> objectPath, IEnumerable<string> terms) =>
			this.Terms(t => t.Field(objectPath).Terms(terms));
		/// <summary>
		/// A query that match on any (configurable) of the provided terms. This is a simpler syntax query for using a bool query with several term queries in the should clauses.
		/// </summary>
		public QueryContainer Terms<K>(Func<TermsQueryDescriptor<T, K>, ITermsQuery> selector) =>
			this._assignSelector(selector, (query, container) => container.Terms = query);

		/// <summary>
		/// A fuzzy based query that uses similarity based on Levenshtein (edit distance) algorithm.
		/// Warning: this query is not very scalable with its default prefix length of 0 – in this case,
		/// every term will be enumerated and cause an edit score calculation or max_expansions is not set.
		/// </summary>
		public QueryContainer Fuzzy(Func<FuzzyQueryDescriptor<T>, IFuzzyQuery> selector) =>
			this._assignSelector(selector, (query, container) => container.Fuzzy = query);

		/// <summary>
		/// The default text query is of type boolean. It means that the text provided is analyzed and the analysis 
		/// process constructs a boolean query from the provided text.
		/// </summary>
		public QueryContainer Match(Func<MatchQueryDescriptor<T>, IMatchQuery> selector) =>
			this._assignSelector(selector, (query, container) => container.Match = query);

		/// <summary>
		/// The text_phrase query analyzes the text and creates a phrase query out of the analyzed text. 
		/// </summary>
		public QueryContainer MatchPhrase(Func<MatchPhraseQueryDescriptor<T>, IMatchQuery> selector) =>
			this._assignSelector(selector, (query, container) => container.Match = query);

		/// <summary>
		/// The text_phrase_prefix is the same as text_phrase, expect it allows for prefix matches on the last term 
		/// in the text
		/// </summary>
		public QueryContainer MatchPhrasePrefix(Func<MatchPhrasePrefixQueryDescriptor<T>, IMatchQuery> selector) =>
			this._assignSelector(selector, (query, container) => container.Match = query);

		/// <summary>
		/// The multi_match query builds further on top of the match query by allowing multiple fields to be specified. 
		/// The idea here is to allow to more easily build a concise match type query over multiple fields instead of using a 
		/// relatively more expressive query by using multiple match queries within a bool query.
		/// </summary>
		public QueryContainer MultiMatch(Func<MultiMatchQueryDescriptor<T>, IMultiMatchQuery> selector) =>
			this._assignSelector(selector, (query, container) => container.MultiMatch = query);

		/// <summary>
		/// Nested query allows to query nested objects / docs (see nested mapping). The query is executed against the 
		/// nested objects / docs as if they were indexed as separate docs (they are, internally) and resulting in the
		/// root parent doc (or parent nested mapping).
		/// </summary>
		public QueryContainer Nested(Func<NestedQueryDescriptor<T>, INestedQuery> selector) =>
			this._assignSelector(selector, (query, container) => container.Nested = query);

		/// <summary>
		/// A thin wrapper allowing fined grained control what should happen if a query is conditionless
		/// if you need to fallback to something other than a match_all query
		/// </summary>
		public QueryContainer Conditionless(Func<ConditionlessQueryDescriptor<T>, IConditionlessQuery> selector)
		{
			var query = selector(new ConditionlessQueryDescriptor<T>());
			return query?.Query ?? query?.Fallback;
		}

		/// <summary>
		/// The indices query can be used when executed across multiple indices, allowing to have a query that executes
		/// only when executed on an index that matches a specific list of indices, and another query that executes 
		/// when it is executed on an index that does not match the listed indices.
		/// </summary>
		public QueryContainer Indices(Func<IndicesQueryDescriptor<T>, IIndicesQuery> selector) =>
			this._assignSelector(selector, (query, container) => container.Indices = query);

		/// <summary>
		/// Matches documents with fields that have terms within a certain range. The type of the Lucene query depends
		/// on the field type, for string fields, the TermRangeQuery, while for number/date fields, the query is
		/// a NumericRangeQuery
		/// </summary>
		public QueryContainer Range(Func<RangeQueryDescriptor<T>, IRangeQuery> selector) =>
			this._assignSelector(selector, (query, container) => container.Range = query);

		/// <summary>
		/// More like this query find documents that are “like” provided text by running it against one or more fields.
		/// </summary>
		public QueryContainer MoreLikeThis(Func<MoreLikeThisQueryDescriptor<T>, IMoreLikeThisQuery> selector) =>
			this._assignSelector(selector, (query, container) => container.MoreLikeThis = query);

		/// <summary>
		/// The geo_shape Filter uses the same grid square representation as the geo_shape mapping to find documents 
		/// that have a shape that intersects with the envelope shape. 
		/// It will also use the same PrefixTree configuration as defined for the field mapping.
		/// </summary>
		public QueryContainer GeoShapeEnvelope(Func<GeoShapeEnvelopeQueryDescriptor<T>, IGeoShapeEnvelopeQuery> selector) =>
			this._assignSelector(selector, (query, container) => container.GeoShape = query);

		/// <summary>
		/// The geo_shape Filter uses the same grid square representation as the geo_shape mapping to find documents 
		/// that have a shape that intersects with the circle shape. 
		/// It will also use the same PrefixTree configuration as defined for the field mapping.
		/// </summary>
		public QueryContainer GeoShapeCircle(Func<GeoShapeCircleQueryDescriptor<T>, IGeoShapeCircleQuery> selector) =>
			this._assignSelector(selector, (query, container) => container.GeoShape = query);

		/// <summary>
		/// Use an indexed shape for the geo shape query
		/// </summary>
		public QueryContainer GeoIndexedShape(Func<GeoIndexedShapeQueryDescriptor<T>, IGeoIndexedShapeQuery> selector) =>
			this._assignSelector(selector, (query, container) => container.GeoShape = query);

		/// <summary>
		/// The geo_shape Filter uses the same grid square representation as the geo_shape mapping to find documents 
		/// that have a shape that intersects with the line string shape. 
		/// It will also use the same PrefixTree configuration as defined for the field mapping.
		/// </summary>
		public QueryContainer GeoShapeLineString(Func<GeoShapeLineStringQueryDescriptor<T>, IGeoShapeLineStringQuery> selector) =>
			this._assignSelector(selector, (query, container) => container.GeoShape = query);

		/// <summary>
		/// The geo_shape circle Filter uses the same grid square representation as the geo_shape mapping to find documents 
		/// that have a shape that intersects with the multi line string shape. 
		/// It will also use the same PrefixTree configuration as defined for the field mapping.
		/// </summary>
		public QueryContainer GeoShapeMultiLineString(Func<GeoShapeMultiLineStringQueryDescriptor<T>, IGeoShapeMultiLineStringQuery> selector) =>
			this._assignSelector(selector, (query, container) => container.GeoShape = query);

		/// <summary>
		/// The geo_shape circle Filter uses the same grid square representation as the geo_shape mapping to find documents 
		/// that have a shape that intersects with the point shape. 
		/// It will also use the same PrefixTree configuration as defined for the field mapping.
		/// </summary>
		public QueryContainer GeoShapePoint(Func<GeoShapePointQueryDescriptor<T>, IGeoShapePointQuery> selector) =>
			this._assignSelector(selector, (query, container) => container.GeoShape = query);

		/// <summary>
		/// The geo_shape circle Filter uses the same grid square representation as the geo_shape mapping to find documents 
		/// that have a shape that intersects with the multi point shape. 
		/// It will also use the same PrefixTree configuration as defined for the field mapping.
		/// </summary>
		public QueryContainer GeoShapeMultiPoint(Func<GeoShapeMultiPointQueryDescriptor<T>, IGeoShapeMultiPointQuery> selector) =>
			this._assignSelector(selector, (query, container) => container.GeoShape = query);

		/// <summary>
		/// The geo_shape circle Filter uses the same grid square representation as the geo_shape mapping to find documents 
		/// that have a shape that intersects with the polygon shape. 
		/// It will also use the same PrefixTree configuration as defined for the field mapping.
		/// </summary>
		public QueryContainer GeoShapePolygon(Func<GeoShapePolygonQueryDescriptor<T>, IGeoShapePolygonQuery> selector) =>
			this._assignSelector(selector, (query, container) => container.GeoShape = query);

		/// <summary>
		/// The geo_shape circle Filter uses the same grid square representation as the geo_shape mapping to find documents 
		/// that have a shape that intersects with the multi polygon shape. 
		/// It will also use the same PrefixTree configuration as defined for the field mapping.
		/// </summary>
		public QueryContainer GeoShapeMultiPolygon(Func<GeoShapeMultiPolygonQueryDescriptor<T>, IGeoShapeMultiPolygonQuery> selector) =>
			this._assignSelector(selector, (query, container) => container.GeoShape = query);

		public QueryContainer GeoPolygon(Func<GeoPolygonQueryDescriptor<T>, IGeoPolygonQuery> selector) =>
			this._assignSelector(selector, (query, container) => container.GeoPolygon = query);

		public QueryContainer GeoHashCell(Func<GeoHashCellQueryDescriptor<T>, IGeoHashCellQuery> selector) =>
			this._assignSelector(selector, (query, container) => container.GeoHashCell = query);

		public QueryContainer GeoDistanceRange(Func<GeoDistanceRangeQueryDescriptor<T>, IGeoDistanceRangeQuery> selector) =>
			this._assignSelector(selector, (query, container) => container.GeoDistanceRange = query);

		public QueryContainer GeoDistance(Func<GeoDistanceQueryDescriptor<T>, IGeoDistanceQuery> selector) =>
			this._assignSelector(selector, (query, container) => container.GeoDistance = query);

		public QueryContainer GeoBoundingBox(Func<GeoBoundingBoxQueryDescriptor<T>, IGeoBoundingBoxQuery> selector) =>
			this._assignSelector(selector, (query, container) => container.GeoBoundingBox = query);

		/// <summary>
		/// The common terms query is a modern alternative to stopwords which improves the precision and recall 
		/// of search results (by taking stopwords into account), without sacrificing performance.
		/// </summary>
		public QueryContainer CommonTerms(Func<CommonTermsQueryDescriptor<T>, ICommonTermsQuery> selector) =>
			this._assignSelector(selector, (query, container) => container.CommonTerms = query);

		/// <summary>
		/// The has_child query works the same as the has_child filter, by automatically wrapping the filter with a 
		/// constant_score.
		/// </summary>
		/// <typeparam name="K">Type of the child</typeparam>
		public QueryContainer HasChild<K>(Func<HasChildQueryDescriptor<K>, IHasChildQuery> selector) where K : class =>
			this._assignSelector(selector, (query, container) => container.HasChild = query);

		/// <summary>
		/// The has_child query works the same as the has_child filter, by automatically wrapping the filter with a 
		/// constant_score.
		/// </summary>
		/// <typeparam name="K">Type of the child</typeparam>
		public QueryContainer HasParent<K>(Func<HasParentQueryDescriptor<K>, IHasParentQuery> selector) where K : class =>
			this._assignSelector(selector, (query, container) => container.HasParent = query);

		/// <summary>
		/// A query that applies a filter to the results of another query. This query maps to Lucene FilteredQuery.
		/// </summary>
		public QueryContainer Filtered(Func<FilteredQueryDescriptor<T>, IFilteredQuery> selector) =>
			this._assignSelector(selector, (query, container) =>
			{
				//TODO THIS SHOULD BE HANDLED by the filtered descriptor
				//this is done because filter and query can not be send as {} here
				if (query.Query != null && query.Query.IsConditionless)
					query.Query = null;

				if (query.Filter != null && query.Filter.IsConditionless)
					query.Filter = null;

				container.Filtered = query;
			});

		/// <summary>
		/// A query that generates the union of documents produced by its subqueries, and that scores each document 
		/// with the maximum score for that document as produced by any subquery, plus a tie breaking increment for 
		/// any additional matching subqueries.
		/// </summary>
		public QueryContainer Dismax(Func<DisMaxQueryDescriptor<T>, IDisMaxQuery> selector) =>
			this._assignSelector(selector, (query, container) => container.DisMax = query);

		/// <summary>
		/// A query that wraps a filter or another query and simply returns a constant score equal to the query boost 
		/// for every document in the filter. Maps to Lucene ConstantScoreQuery.
		/// </summary>
		public QueryContainer ConstantScore(Func<ConstantScoreQueryDescriptor<T>, IConstantScoreQuery> selector) =>
			this._assignSelector(selector, (query, container) => container.ConstantScore = query);

		/// <summary>
		/// A query that matches documents matching boolean combinations of other queries. The bool query maps to 
		/// Lucene BooleanQuery. 
		/// It is built using one or more boolean clauses, each clause with a typed occurrence
		/// </summary>
		public QueryContainer Bool(Func<BoolQueryDescriptor<T>, IBoolQuery> selector) =>
			this._assignSelector(selector, (query, container) => container.Bool = query);

		/// <summary>
		/// the boosting query can be used to effectively demote results that match a given query. 
		/// Unlike the “NOT” clause in bool query, this still selects documents that contain
		/// undesirable terms, but reduces their overall score.
		/// </summary>
		/// <param name="boostingQuery"></param>
		public QueryContainer Boosting(Func<BoostingQueryDescriptor<T>, IBoostingQuery> selector) =>
			this._assignSelector(selector, (query, container) => container.Boosting = query);

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
		public QueryContainer MatchAll(Func<MatchAllQueryDescriptor, IMatchAllQuery> selector = null) =>
			_assign(a => a.MatchAllQuery = selector?.Invoke(new MatchAllQueryDescriptor()) ?? new MatchAllQuery());

		/// <summary>
		/// Matches documents that have fields that contain a term (not analyzed). 
		/// The term query maps to Lucene TermQuery. 
		/// </summary>
		public QueryContainer Term<K>(Expression<Func<T, object>> fieldDescriptor, K value, double? Boost = null)
		{
			return this.Term(t =>
			{
				t.Field(fieldDescriptor).Value(value);
				if (Boost.HasValue)
					t.Boost(Boost.Value);
				return t;
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
				t.Field(fieldDescriptor).Value(value);
				if (Boost.HasValue)
					t.Boost(Boost.Value);
				return t;
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
				t.Field(field).Value(value);
				if (Boost.HasValue)
					t.Boost(Boost.Value);
				return t;
			});
		}

		/// <summary>
		/// Matches documents that have fields that contain a term (not analyzed). 
		/// The term query maps to Lucene TermQuery. 
		/// </summary>
		public QueryContainer Term(Func<TermQueryDescriptor<T>, ITermQuery> selector) =>
			this._assignSelector(selector, (query, container) => container.Term = query);

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
				t.Field(fieldDescriptor).Value(value);
				if (Boost.HasValue) t.Boost(Boost.Value);
				if (Rewrite.HasValue) t.Rewrite(Rewrite.Value);
				return t;
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
				t.Field(field).Value(value);
				if (Boost.HasValue) t.Boost(Boost.Value);
				if (Rewrite.HasValue) t.Rewrite(Rewrite.Value);
				return t;
			});
		}

		/// <summary>
		/// Matches documents that have fields matching a wildcard expression (not analyzed). 
		/// Supported wildcards are *, which matches any character sequence (including the empty one), and ?,
		/// which matches any single character. Note this query can be slow, as it needs to iterate over many terms. 
		/// In order to prevent extremely slow wildcard queries, a wildcard term should not start with 
		/// one of the wildcards * or ?. The wildcard query maps to Lucene WildcardQuery.
		/// </summary>
		public QueryContainer Wildcard(Func<WildcardQueryDescriptor<T>, IWildcardQuery> selector) =>
			this._assignSelector(selector, (query, container) => container.Wildcard = query);

		/// <summary>
		/// Matches documents that have fields containing terms with a specified prefix (not analyzed). 
		/// The prefix query maps to Lucene PrefixQuery. 
		/// </summary>
		public QueryContainer Prefix(Expression<Func<T, object>> fieldDescriptor, string value, double? Boost = null, RewriteMultiTerm? Rewrite = null)
		{
			return this.Prefix(t =>
			{
				t.Field(fieldDescriptor).Value(value);
				if (Boost.HasValue) t.Boost(Boost.Value);
				if (Rewrite.HasValue) t.Rewrite(Rewrite.Value);
				return t;
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
				t.Field(field).Value(value);
				if (Boost.HasValue) t.Boost(Boost.Value);
				if (Rewrite.HasValue) t.Rewrite(Rewrite.Value);
				return t;
			});
		}

		/// <summary>
		/// Matches documents that have fields containing terms with a specified prefix (not analyzed). 
		/// The prefix query maps to Lucene PrefixQuery. 
		/// </summary>	
		public QueryContainer Prefix(Func<PrefixQueryDescriptor<T>, IPrefixQuery> selector) =>
			this._assignSelector(selector, (query, container) => container.Prefix = query);

		/// <summary>
		/// Filters documents that only have the provided ids. 
		/// Note, this filter does not require the _id field to be indexed since 
		/// it works using the _uid field.
		/// </summary>
		public QueryContainer Ids(Func<IdsQueryDescriptor, IIdsQuery> selector) =>
			this._assignSelector(selector, (query, container) => container.Ids = query);

		/// <summary>
		/// Matches spans containing a term. The span term query maps to Lucene SpanTermQuery. 
		/// </summary>
		public QueryContainer SpanTerm(Expression<Func<T, object>> fieldDescriptor, string value, double? Boost = null)
		{
			return this.SpanTerm(t =>
			{
				t.Field(fieldDescriptor).Value(value);
				if (Boost.HasValue) t.Boost(Boost.Value);
				return t;
			});
		}

		/// <summary>
		/// Matches spans containing a term. The span term query maps to Lucene SpanTermQuery. 
		/// </summary>
		public QueryContainer SpanTerm(string field, string value, double? Boost = null)
		{
			return this.SpanTerm(t =>
			{
				t.Field(field).Value(value);
				if (Boost.HasValue) t.Boost(Boost.Value);
				return t;
			});
		}

		/// <summary>
		/// Matches spans containing a term. The span term query maps to Lucene SpanTermQuery. 
		/// </summary>
		public QueryContainer SpanTerm(Func<SpanTermQueryDescriptor<T>, ISpanTermQuery> selector) =>
			this._assignSelector(selector, (query, container) => container.SpanTerm = query);

		/// <summary>
		/// Matches spans near the beginning of a field. The span first query maps to Lucene SpanFirstQuery. 
		/// </summary>
		public QueryContainer SpanFirst(Func<SpanFirstQueryDescriptor<T>, ISpanFirstQuery> selector) =>
			this._assignSelector(selector, (query, container) => container.SpanFirst = query);

		/// <summary>
		/// Matches spans which are near one another. One can specify slop, the maximum number of 
		/// intervening unmatched positions, as well as whether matches are required to be in-order.
		/// The span near query maps to Lucene SpanNearQuery.
		/// </summary>
		public QueryContainer SpanNear(Func<SpanNearQueryDescriptor<T>, ISpanNearQuery> selector) =>
			this._assignSelector(selector, (query, container) => container.SpanNear = query);

		/// <summary>
		/// Matches the union of its span clauses. 
		/// The span or query maps to Lucene SpanOrQuery. 
		/// </summary>
		public QueryContainer SpanOr(Func<SpanOrQueryDescriptor<T>, ISpanOrQuery> selector) =>
			this._assignSelector(selector, (query, container) => container.SpanOr = query);

		/// <summary>
		/// Removes matches which overlap with another span query. 
		/// The span not query maps to Lucene SpanNotQuery.
		/// </summary>
		public QueryContainer SpanNot(Func<SpanNotQueryDescriptor<T>, ISpanNotQuery> selector) =>
			this._assignSelector(selector, (query, container) => container.SpanNot = query);

		/// <summary>
		/// Wrap a multi term query (one of fuzzy, prefix, term range or regexp query) 
		/// as a span query so it can be nested.
		/// </summary>
		public QueryContainer SpanMultiTerm(Func<SpanMultiTermQueryDescriptor<T>, ISpanMultiTermQuery> selector) =>
			this._assignSelector(selector, (query, container) => container.SpanMultiTerm = query);

		/// <summary>
		/// </summary>
		public QueryContainer SpanContaining(Func<SpanContainingQueryDescriptor<T>, ISpanContainingQuery> selector) =>
			this._assignSelector(selector, (query, container) => container.SpanContaining = query);

		/// <summary>
		/// </summary>
		public QueryContainer SpanWithin(Func<SpanWithinQueryDescriptor<T>, ISpanWithinQuery> selector) =>
			this._assignSelector(selector, (query, container) => container.SpanWithin = query);

		/// <summary>
		/// custom_score query allows to wrap another query and customize the scoring of it optionally with a 
		/// computation derived from other field values in the doc (numeric ones) using script or boost expression
		/// </summary>
		public QueryContainer Regexp(Func<RegexpQueryDescriptor<T>, IRegexpQuery> selector) =>
			this._assignSelector(selector, (query, container) => container.Regexp = query);

		/// <summary>
		/// Function score query
		/// </summary>
		/// <returns></returns>
		public QueryContainer FunctionScore(Func<FunctionScoreQueryDescriptor<T>, IFunctionScoreQuery> selector) =>
			this._assignSelector(selector, (query, container) => container.FunctionScore = query);

		public QueryContainer Template(Func<TemplateQueryDescriptor<T>, ITemplateQuery> selector) =>
			this._assignSelector(selector, (query, container) => container.Template = query);
	}
}
