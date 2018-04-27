using System;
using System.Collections;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public class QueryContainerDescriptor<T> : QueryContainer where T : class
	{

		private QueryContainer WrapInContainer<TQuery, TQueryInterface>(
			Func<TQuery, TQueryInterface> create,
			Action<TQueryInterface, IQueryContainer> assign
			)
			where TQuery : class, TQueryInterface, IQuery, new()
			where TQueryInterface : class, IQuery
		{
			// Invoke the create delegate before assigning container; the create delegate
			// may mutate the current QueryContainerDescriptor<T> instance such that it
			// contains a query. See https://github.com/elastic/elasticsearch-net/issues/2875
			var query = create.InvokeOrDefault(new TQuery());

			var container = this.ContainedQuery == null
				? this
				: new QueryContainerDescriptor<T>();

			IQueryContainer c = container;
			c.IsVerbatim = query.IsVerbatim;
			c.IsStrict = query.IsStrict;
			assign(query, container);
			container.ContainedQuery = query;

			//if query is writable (not conditionless or verbatim): return a container that holds the query
			if (query.IsWritable)
				return container;

			//query is conditionless but marked as strict, throw exception
			if (query.IsStrict)
				throw new ArgumentException("Query is conditionless but strict is turned on");

			//query is conditionless return an empty container that can later be rewritten
			return null;
		}

		/// <summary>
		/// A query defined using a raw json string.
		/// <para>The query must be enclosed within '{' and '}'</para>
		/// </summary>
		/// <param name="rawJson">The query dsl json</param>
		public QueryContainer Raw(string rawJson) =>
			WrapInContainer((RawQueryDescriptor descriptor) => descriptor.Raw(rawJson), (query, container) => container.RawQuery = query);

		/// <summary>
		/// A query that uses a query parser in order to parse its content.
		/// </summary>
		public QueryContainer QueryString(Func<QueryStringQueryDescriptor<T>, IQueryStringQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.QueryString = query);

		/// <summary>
		/// A query that uses the SimpleQueryParser to parse its context.
		/// Unlike the regular query_string query, the simple_query_string query will
		/// never throw an exception, and discards invalid parts of the query.
		/// </summary>
		public QueryContainer SimpleQueryString(Func<SimpleQueryStringQueryDescriptor<T>, ISimpleQueryStringQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.SimpleQueryString = query);

		/// <summary>
		/// A query that match on any (configurable) of the provided terms.
		/// This is a simpler syntax query for using a bool query with several term queries in the should clauses.
		/// </summary>
		public QueryContainer Terms(Func<TermsQueryDescriptor<T>, ITermsQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.Terms = query);

		/// <summary>
		/// A fuzzy based query that uses similarity based on Levenshtein (edit distance) algorithm.
		/// Warning: this query is not very scalable with its default prefix length of 0 � in this case,
		/// every term will be enumerated and cause an edit score calculation or max_expansions is not set.
		/// </summary>
		public QueryContainer Fuzzy(Func<FuzzyQueryDescriptor<T>, IFuzzyQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.Fuzzy = query);

		public QueryContainer FuzzyNumeric(Func<FuzzyNumericQueryDescriptor<T>, IFuzzyQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.Fuzzy = query);

		public QueryContainer FuzzyDate(Func<FuzzyDateQueryDescriptor<T>, IFuzzyQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.Fuzzy = query);

		/// <summary>
		/// The default match query is of type boolean. It means that the text provided is analyzed and the analysis
		/// process constructs a boolean query from the provided text.
		/// </summary>
		public QueryContainer Match(Func<MatchQueryDescriptor<T>, IMatchQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.Match = query);

		/// <summary>
		/// The match_phrase query analyzes the match and creates a phrase query out of the analyzed text.
		/// </summary>
		public QueryContainer MatchPhrase(Func<MatchPhraseQueryDescriptor<T>, IMatchPhraseQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.MatchPhrase = query);

		/// <summary>
		/// The match_phrase_prefix is the same as match_phrase, expect it allows for prefix matches on the last term
		/// in the text
		/// </summary>
		public QueryContainer MatchPhrasePrefix(Func<MatchPhrasePrefixQueryDescriptor<T>, IMatchPhrasePrefixQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.MatchPhrasePrefix = query);

		/// <summary>
		/// The multi_match query builds further on top of the match query by allowing multiple fields to be specified.
		/// The idea here is to allow to more easily build a concise match type query over multiple fields instead of using a
		/// relatively more expressive query by using multiple match queries within a bool query.
		/// </summary>
		public QueryContainer MultiMatch(Func<MultiMatchQueryDescriptor<T>, IMultiMatchQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.MultiMatch = query);

		/// <summary>
		/// Nested query allows to query nested objects / docs (see nested mapping). The query is executed against the
		/// nested objects / docs as if they were indexed as separate docs (they are, internally) and resulting in the
		/// root parent doc (or parent nested mapping).
		/// </summary>
		public QueryContainer Nested(Func<NestedQueryDescriptor<T>, INestedQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.Nested = query);

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
		/// Matches documents with fields that have terms within a certain numeric range.
		/// </summary>
		public QueryContainer Range(Func<NumericRangeQueryDescriptor<T>, INumericRangeQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.Range = query);

		/// <summary>
		/// Matches documents with fields that have terms within a certain date range.
		/// </summary>
		public QueryContainer DateRange(Func<DateRangeQueryDescriptor<T>, IDateRangeQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.Range = query);

		/// <summary>
		/// Matches documents with fields that have terms within a certain term range.
		/// </summary>
		public QueryContainer TermRange(Func<TermRangeQueryDescriptor<T>, ITermRangeQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.Range = query);

		/// <summary>
		/// More like this query find documents that are �like� provided text by running it against one or more fields.
		/// </summary>
		public QueryContainer MoreLikeThis(Func<MoreLikeThisQueryDescriptor<T>, IMoreLikeThisQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.MoreLikeThis = query);

		/// <summary>
		/// A geo_shape query that finds documents
		/// that have a geometry that matches for the given spatial relation and input shape
		/// </summary>
		public QueryContainer GeoShape(Func<GeoShapeQueryDescriptor<T>, IGeoShapeQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.GeoShape = query);

		/// <summary>
		/// Matches documents with a geo_point type field that falls within a polygon of points
		/// </summary>
		public QueryContainer GeoPolygon(Func<GeoPolygonQueryDescriptor<T>, IGeoPolygonQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.GeoPolygon = query);

		/// <summary>
		/// Matches documents with a geo_point type field to include only those
		/// that exist within a specific distance from a given geo_point
		/// </summary>
		public QueryContainer GeoDistance(Func<GeoDistanceQueryDescriptor<T>, IGeoDistanceQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.GeoDistance = query);

		/// <summary>
		/// Matches documents with a geo_point type field to include only those that exist within a bounding box
		/// </summary>
		public QueryContainer GeoBoundingBox(Func<GeoBoundingBoxQueryDescriptor<T>, IGeoBoundingBoxQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.GeoBoundingBox = query);

		/// <summary>
		/// The common terms query is a modern alternative to stopwords which improves the precision and recall
		/// of search results (by taking stopwords into account), without sacrificing performance.
		/// </summary>
		public QueryContainer CommonTerms(Func<CommonTermsQueryDescriptor<T>, ICommonTermsQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.CommonTerms = query);

		/// <summary>
		/// The has_child query works the same as the has_child filter, by automatically wrapping the filter with a
		/// constant_score.
		/// </summary>
		/// <typeparam name="TChild">Type of the child</typeparam>
		public QueryContainer HasChild<TChild>(Func<HasChildQueryDescriptor<TChild>, IHasChildQuery> selector) where TChild : class =>
			WrapInContainer(selector, (query, container) => container.HasChild = query);

		/// <summary>
		/// The has_parent query works the same as the has_parent filter, by automatically wrapping the filter with a
		/// constant_score.
		/// </summary>
		/// <typeparam name="TParent">Type of the parent</typeparam>
		public QueryContainer HasParent<TParent>(Func<HasParentQueryDescriptor<TParent>, IHasParentQuery> selector) where TParent : class =>
			WrapInContainer(selector, (query, container) => container.HasParent = query);

		/// <summary>
		/// A query that generates the union of documents produced by its subqueries, and that scores each document
		/// with the maximum score for that document as produced by any subquery, plus a tie breaking increment for
		/// any additional matching subqueries.
		/// </summary>
		public QueryContainer DisMax(Func<DisMaxQueryDescriptor<T>, IDisMaxQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.DisMax = query);

		/// <summary>
		/// A query that wraps a filter or another query and simply returns a constant score equal to the query boost
		/// for every document in the filter. Maps to Lucene ConstantScoreQuery.
		/// </summary>
		public QueryContainer ConstantScore(Func<ConstantScoreQueryDescriptor<T>, IConstantScoreQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.ConstantScore = query);

		/// <summary>
		/// A query that matches documents matching boolean combinations of other queries. The bool query maps to
		/// Lucene BooleanQuery.
		/// It is built using one or more boolean clauses, each clause with a typed occurrence
		/// </summary>
		public QueryContainer Bool(Func<BoolQueryDescriptor<T>, IBoolQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.Bool = query);

		/// <summary>
		/// A query that can be used to effectively demote results that match a given query.
		/// Unlike the "must_not" clause in bool query, this still selects documents that contain
		/// undesirable terms, but reduces their overall score.
		/// </summary>
		public QueryContainer Boosting(Func<BoostingQueryDescriptor<T>, IBoostingQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.Boosting = query);

		/// <summary>
		/// A query that matches all documents. Maps to Lucene MatchAllDocsQuery.
		/// </summary>
		public QueryContainer MatchAll(Func<MatchAllQueryDescriptor, IMatchAllQuery> selector = null) =>
			WrapInContainer(selector, (query, container) => container.MatchAll = query ?? new MatchAllQuery());

		/// <summary>
		/// A query that matches no documents. This is the inverse of the match_all query.
		/// </summary>
		public QueryContainer MatchNone(Func<MatchNoneQueryDescriptor, IMatchNoneQuery> selector = null) =>
			WrapInContainer(selector, (query, container) => container.MatchNone = query ?? new MatchNoneQuery());

		/// <summary>
		/// Matches documents that have fields that contain a term (not analyzed).
		/// The term query maps to Lucene TermQuery.
		/// </summary>
		public QueryContainer Term(Expression<Func<T, object>> field, object value, double? boost = null, string name = null) =>
			this.Term(t => t.Field(field).Value(value).Boost(boost).Name(name));

		/// <summary>
		/// Helper method to easily filter on join relations
		/// </summary>
		public QueryContainer HasRelationName(Expression<Func<T, JoinField>> field, RelationName value) =>
			this.Term(t => t.Field(field).Value(value));

		/// <summary>Helper method to easily filter on join relations</summary>
		public QueryContainer HasRelationName<TRelation>(Expression<Func<T, JoinField>> field) =>
			this.Term(t => t.Field(field).Value(Infer.Relation<TRelation>()));

		/// <summary>
		/// Matches documents that have fields that contain a term (not analyzed).
		/// The term query maps to Lucene TermQuery.
		/// </summary>
		public QueryContainer Term(Field field, object value, double? boost = null, string name = null) =>
			this.Term(t => t.Field(field).Value(value).Boost(boost).Name(name));

		/// <summary>
		/// Matches documents that have fields that contain a term (not analyzed).
		/// The term query maps to Lucene TermQuery.
		/// </summary>
		public QueryContainer Term(Func<TermQueryDescriptor<T>, ITermQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.Term = query);

		/// <summary>
		/// Matches documents that have fields matching a wildcard expression (not analyzed).
		/// Supported wildcards are *, which matches any character sequence (including the empty one), and ?,
		/// which matches any single character. Note this query can be slow, as it needs to iterate
		/// over many terms. In order to prevent extremely slow wildcard queries, a wildcard term should
		/// not start with one of the wildcards * or ?. The wildcard query maps to Lucene WildcardQuery.
		/// </summary>
		public QueryContainer Wildcard(Expression<Func<T, object>> field, string value, double? boost = null, MultiTermQueryRewrite rewrite = null, string name = null) =>
			this.Wildcard(t => t.Field(field).Value(value).Rewrite(rewrite).Boost(boost).Name(name));

		/// <summary>
		/// Matches documents that have fields matching a wildcard expression (not analyzed).
		/// Supported wildcards are *, which matches any character sequence (including the empty one), and ?,
		/// which matches any single character. Note this query can be slow, as it needs to iterate over many terms.
		/// In order to prevent extremely slow wildcard queries, a wildcard term should not start with
		/// one of the wildcards * or ?. The wildcard query maps to Lucene WildcardQuery.
		/// </summary>
		public QueryContainer Wildcard(Field field, string value, double? boost = null, MultiTermQueryRewrite rewrite = null, string name = null) =>
			this.Wildcard(t => t.Field(field).Value(value).Rewrite(rewrite).Boost(boost).Name(name));

		/// <summary>
		/// Matches documents that have fields matching a wildcard expression (not analyzed).
		/// Supported wildcards are *, which matches any character sequence (including the empty one), and ?,
		/// which matches any single character. Note this query can be slow, as it needs to iterate over many terms.
		/// In order to prevent extremely slow wildcard queries, a wildcard term should not start with
		/// one of the wildcards * or ?. The wildcard query maps to Lucene WildcardQuery.
		/// </summary>
		public QueryContainer Wildcard(Func<WildcardQueryDescriptor<T>, IWildcardQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.Wildcard = query);

		/// <summary>
		/// Matches documents that have fields containing terms with a specified prefix (not analyzed).
		/// The prefix query maps to Lucene PrefixQuery.
		/// </summary>
		public QueryContainer Prefix(Expression<Func<T, object>> field, string value, double? boost = null, MultiTermQueryRewrite rewrite = null, string name = null) =>
			this.Prefix(t => t.Field(field).Value(value).Boost(boost).Rewrite(rewrite).Name(name));

		/// <summary>
		/// Matches documents that have fields containing terms with a specified prefix (not analyzed).
		/// The prefix query maps to Lucene PrefixQuery.
		/// </summary>
		public QueryContainer Prefix(Field field, string value, double? boost = null, MultiTermQueryRewrite rewrite = null, string name = null) =>
			this.Prefix(t => t.Field(field).Value(value).Boost(boost).Rewrite(rewrite).Name(name));

		/// <summary>
		/// Matches documents that have fields containing terms with a specified prefix (not analyzed).
		/// The prefix query maps to Lucene PrefixQuery.
		/// </summary>
		public QueryContainer Prefix(Func<PrefixQueryDescriptor<T>, IPrefixQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.Prefix = query);

		/// <summary>
		/// Matches documents that only have the provided ids.
		/// Note, this filter does not require the _id field to be indexed since
		/// it works using the _uid field.
		/// </summary>
		public QueryContainer Ids(Func<IdsQueryDescriptor, IIdsQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.Ids = query);

		/// <summary>
		/// Matches spans containing a term. The span term query maps to Lucene SpanTermQuery.
		/// </summary>
		public QueryContainer SpanTerm(Func<SpanTermQueryDescriptor<T>, ISpanTermQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.SpanTerm = query);

		/// <summary>
		/// Matches spans near the beginning of a field. The span first query maps to Lucene SpanFirstQuery.
		/// </summary>
		public QueryContainer SpanFirst(Func<SpanFirstQueryDescriptor<T>, ISpanFirstQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.SpanFirst = query);

		/// <summary>
		/// Matches spans which are near one another. One can specify slop, the maximum number of
		/// intervening unmatched positions, as well as whether matches are required to be in-order.
		/// The span near query maps to Lucene SpanNearQuery.
		/// </summary>
		public QueryContainer SpanNear(Func<SpanNearQueryDescriptor<T>, ISpanNearQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.SpanNear = query);

		/// <summary>
		/// Matches the union of its span clauses.
		/// The span or query maps to Lucene SpanOrQuery.
		/// </summary>
		public QueryContainer SpanOr(Func<SpanOrQueryDescriptor<T>, ISpanOrQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.SpanOr = query);

		/// <summary>
		/// Removes matches which overlap with another span query.
		/// The span not query maps to Lucene SpanNotQuery.
		/// </summary>
		public QueryContainer SpanNot(Func<SpanNotQueryDescriptor<T>, ISpanNotQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.SpanNot = query);

		/// <summary>
		/// Wrap a multi term query (one of fuzzy, prefix, term range or regexp query)
		/// as a span query so it can be nested.
		/// </summary>
		public QueryContainer SpanMultiTerm(Func<SpanMultiTermQueryDescriptor<T>, ISpanMultiTermQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.SpanMultiTerm = query);

		/// <summary>
		/// Returns matches which enclose another span query.
		/// The span containing query maps to Lucene SpanContainingQuery
		/// </summary>
		public QueryContainer SpanContaining(Func<SpanContainingQueryDescriptor<T>, ISpanContainingQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.SpanContaining = query);

		/// <summary>
		/// Returns Matches which are enclosed inside another span query.
		/// The span within query maps to Lucene SpanWithinQuery
		/// </summary>
		public QueryContainer SpanWithin(Func<SpanWithinQueryDescriptor<T>, ISpanWithinQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.SpanWithin = query);

		/// <summary>
		/// Wraps span queries to allow them to participate in composite single-field Span queries by 'lying' about their search field.
		/// That is, the masked span query will function as normal, but the field points back to the set field of the query.
		/// This can be used to support queries like SpanNearQuery or SpanOrQuery across different fields,
		/// which is not ordinarily permitted.
		/// </summary>
		public QueryContainer SpanFieldMasking(Func<SpanFieldMaskingQueryDescriptor<T>, ISpanFieldMaskingQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.SpanFieldMasking = query);

		/// <summary>
		/// Allows you to use regular expression term queries.
		/// "term queries" means that Elasticsearch will apply the regexp to the terms produced
		/// by the tokenizer for that field, and not to the original text of the field.
		/// </summary>
		public QueryContainer Regexp(Func<RegexpQueryDescriptor<T>, IRegexpQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.Regexp = query);

		/// <summary>
		/// The function_score query allows you to modify the score of documents that are retrieved by a query.
		/// This can be useful if, for example, a score function is computationally expensive and it is
		/// sufficient to compute the score on a filtered set of documents.
		/// </summary>
		/// <returns></returns>
		public QueryContainer FunctionScore(Func<FunctionScoreQueryDescriptor<T>, IFunctionScoreQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.FunctionScore = query);

		public QueryContainer Script(Func<ScriptQueryDescriptor<T>, IScriptQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.Script = query);

		public QueryContainer Exists(Func<ExistsQueryDescriptor<T>, IExistsQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.Exists = query);

		public QueryContainer Type(Func<TypeQueryDescriptor, ITypeQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.Type = query);

		public QueryContainer Type<TOther>() => this.Type(q => q.Value<TOther>());

		/// <summary>
		/// Used to match queries stored in an index.
		/// The percolate query itself contains the document that will be used as query
		/// to match with the stored queries.
		/// </summary>
		public QueryContainer Percolate(Func<PercolateQueryDescriptor<T>, IPercolateQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.Percolate = query);

		/// <summary>
		/// Used to find child documents which belong to a particular parent.
		/// </summary>
		public QueryContainer ParentId(Func<ParentIdQueryDescriptor<T>, IParentIdQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.ParentId = query);

		/// <summary>
		/// Returns any documents that match with at least one or more of the provided terms.
		/// The terms are not analyzed and thus must match exactly. The number of terms that must match varies
		/// per document and is either controlled by a minimum should match field or
		/// computed per document in a minimum should match script.
		/// </summary>
		public QueryContainer TermsSet(Func<TermsSetQueryDescriptor<T>, ITermsSetQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.TermsSet = query);
	}
}
