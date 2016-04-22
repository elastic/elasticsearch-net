using System;
using System.Collections;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public class QueryContainerDescriptor<T> : QueryContainer where T : class
	{
		QueryContainerDescriptor<T> Assign(Action<IQueryContainer> assigner) => Fluent.Assign(this, assigner);

		[Obsolete("Scheduled to be removed in 5.0.  Setting Strict() at the container level does is a noop and must be set on each individual query.")]
		public QueryContainerDescriptor<T> Strict(bool strict = true) => this;

		[Obsolete("Scheduled to be removed in 5.0.  Setting Verbatim() at the container level is a noop and must be set on each individual query.")]
		public QueryContainerDescriptor<T> Verbatim(bool verbatim = true) => this;

		private static QueryContainer WrapInContainer<TQuery, TQueryInterface>(
			Func<TQuery, TQueryInterface> create,
			Action<TQueryInterface, IQueryContainer> assign
			)
			where TQuery : class, TQueryInterface, IQuery, new()
			where TQueryInterface : class, IQuery
		{
			var query = create.InvokeOrDefault(new TQuery());
			var container = new QueryContainerDescriptor<T>();
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
		/// Insert raw query json at this position of the query
		/// <para>Be sure to start your json with '{'</para>
		/// </summary>
		/// <param name="rawJson"></param>
		/// <returns></returns>
		public QueryContainer Raw(string rawJson) => Assign(a => a.RawQuery = new RawQueryDescriptor().Raw(rawJson));

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
		/// A query that match on any (configurable) of the provided terms. This is a simpler syntax query for using a bool query with several term queries in the should clauses.
		/// </summary>
		public QueryContainer Terms(Func<TermsQueryDescriptor<T>, ITermsQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.Terms = query);

		/// <summary>
		/// A fuzzy based query that uses similarity based on Levenshtein (edit distance) algorithm.
		/// Warning: this query is not very scalable with its default prefix length of 0 – in this case,
		/// every term will be enumerated and cause an edit score calculation or max_expansions is not set.
		/// </summary>
		public QueryContainer Fuzzy(Func<FuzzyQueryDescriptor<T>, IFuzzyQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.Fuzzy = query);

		public QueryContainer FuzzyNumeric(Func<FuzzyNumericQueryDescriptor<T>, IFuzzyQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.Fuzzy = query);

		public QueryContainer FuzzyDate(Func<FuzzyDateQueryDescriptor<T>, IFuzzyQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.Fuzzy = query);

		/// <summary>
		/// The default text query is of type boolean. It means that the text provided is analyzed and the analysis
		/// process constructs a boolean query from the provided text.
		/// </summary>
		public QueryContainer Match(Func<MatchQueryDescriptor<T>, IMatchQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.Match = query);

		/// <summary>
		/// The text_phrase query analyzes the text and creates a phrase query out of the analyzed text.
		/// </summary>
		public QueryContainer MatchPhrase(Func<MatchPhraseQueryDescriptor<T>, IMatchQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.Match = query);

		/// <summary>
		/// The text_phrase_prefix is the same as text_phrase, expect it allows for prefix matches on the last term
		/// in the text
		/// </summary>
		public QueryContainer MatchPhrasePrefix(Func<MatchPhrasePrefixQueryDescriptor<T>, IMatchQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.Match = query);

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
		/// The indices query can be used when executed across multiple indices, allowing to have a query that executes
		/// only when executed on an index that matches a specific list of indices, and another query that executes
		/// when it is executed on an index that does not match the listed indices.
		/// </summary>
		public QueryContainer Indices(Func<IndicesQueryDescriptor<T>, IIndicesQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.Indices = query);

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
		/// More like this query find documents that are “like” provided text by running it against one or more fields.
		/// </summary>
		public QueryContainer MoreLikeThis(Func<MoreLikeThisQueryDescriptor<T>, IMoreLikeThisQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.MoreLikeThis = query);

		/// <summary>
		/// The geo_shape Filter uses the same grid square representation as the geo_shape mapping to find documents
		/// that have a shape that intersects with the envelope shape.
		/// It will also use the same PrefixTree configuration as defined for the field mapping.
		/// </summary>
		public QueryContainer GeoShapeEnvelope(Func<GeoShapeEnvelopeQueryDescriptor<T>, IGeoShapeEnvelopeQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.GeoShape = query);

		/// <summary>
		/// The geo_shape Filter uses the same grid square representation as the geo_shape mapping to find documents
		/// that have a shape that intersects with the circle shape.
		/// It will also use the same PrefixTree configuration as defined for the field mapping.
		/// </summary>
		public QueryContainer GeoShapeCircle(Func<GeoShapeCircleQueryDescriptor<T>, IGeoShapeCircleQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.GeoShape = query);

		/// <summary>
		/// Use an indexed shape for the geo shape query
		/// </summary>
		public QueryContainer GeoIndexedShape(Func<GeoIndexedShapeQueryDescriptor<T>, IGeoIndexedShapeQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.GeoShape = query);

		/// <summary>
		/// The geo_shape Filter uses the same grid square representation as the geo_shape mapping to find documents
		/// that have a shape that intersects with the line string shape.
		/// It will also use the same PrefixTree configuration as defined for the field mapping.
		/// </summary>
		public QueryContainer GeoShapeLineString(Func<GeoShapeLineStringQueryDescriptor<T>, IGeoShapeLineStringQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.GeoShape = query);

		/// <summary>
		/// The geo_shape circle Filter uses the same grid square representation as the geo_shape mapping to find documents
		/// that have a shape that intersects with the multi line string shape.
		/// It will also use the same PrefixTree configuration as defined for the field mapping.
		/// </summary>
		public QueryContainer GeoShapeMultiLineString(Func<GeoShapeMultiLineStringQueryDescriptor<T>, IGeoShapeMultiLineStringQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.GeoShape = query);

		/// <summary>
		/// The geo_shape circle Filter uses the same grid square representation as the geo_shape mapping to find documents
		/// that have a shape that intersects with the point shape.
		/// It will also use the same PrefixTree configuration as defined for the field mapping.
		/// </summary>
		public QueryContainer GeoShapePoint(Func<GeoShapePointQueryDescriptor<T>, IGeoShapePointQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.GeoShape = query);

		/// <summary>
		/// The geo_shape circle Filter uses the same grid square representation as the geo_shape mapping to find documents
		/// that have a shape that intersects with the multi point shape.
		/// It will also use the same PrefixTree configuration as defined for the field mapping.
		/// </summary>
		public QueryContainer GeoShapeMultiPoint(Func<GeoShapeMultiPointQueryDescriptor<T>, IGeoShapeMultiPointQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.GeoShape = query);

		/// <summary>
		/// The geo_shape circle Filter uses the same grid square representation as the geo_shape mapping to find documents
		/// that have a shape that intersects with the polygon shape.
		/// It will also use the same PrefixTree configuration as defined for the field mapping.
		/// </summary>
		public QueryContainer GeoShapePolygon(Func<GeoShapePolygonQueryDescriptor<T>, IGeoShapePolygonQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.GeoShape = query);

		/// <summary>
		/// The geo_shape circle Filter uses the same grid square representation as the geo_shape mapping to find documents
		/// that have a shape that intersects with the multi polygon shape.
		/// It will also use the same PrefixTree configuration as defined for the field mapping.
		/// </summary>
		public QueryContainer GeoShapeMultiPolygon(Func<GeoShapeMultiPolygonQueryDescriptor<T>, IGeoShapeMultiPolygonQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.GeoShape = query);

		public QueryContainer GeoPolygon(Func<GeoPolygonQueryDescriptor<T>, IGeoPolygonQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.GeoPolygon = query);

		public QueryContainer GeoHashCell(Func<GeoHashCellQueryDescriptor<T>, IGeoHashCellQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.GeoHashCell = query);

		public QueryContainer GeoDistanceRange(Func<GeoDistanceRangeQueryDescriptor<T>, IGeoDistanceRangeQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.GeoDistanceRange = query);

		public QueryContainer GeoDistance(Func<GeoDistanceQueryDescriptor<T>, IGeoDistanceQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.GeoDistance = query);

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

#pragma warning disable 618
		/// <summary>
		/// A query that applies a filter to the results of another query. This query maps to Lucene FilteredQuery.
		/// </summary>
		[Obsolete("Use the bool query instead with a must clause for the query and a filter clause for the filter.")]
		public QueryContainer Filtered(Func<FilteredQueryDescriptor<T>, IFilteredQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.Filtered = query);

		/// <summary>
		///A query that matches documents using the AND boolean operator on other queries.
		/// </summary>
		[Obsolete("Use the bool query instead")]
		public QueryContainer And(Func<AndQueryDescriptor<T>, IAndQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.And = query);

		/// <summary>
		/// A query that matches documents using the OR boolean operator on other queries
		/// </summary>
		[Obsolete("Use the should clause on the bool query instead, note that this bool query should not have other clauses to be semantically correct")]
		public QueryContainer Or(Func<OrQueryDescriptor<T>, IOrQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.Or = query);

		/// <summary>
		/// A query that filters out matched documents using a query.
		/// </summary>
		[Obsolete("Use the bool query with must_not clause instead..")]
		public QueryContainer Not(Func<NotQueryDescriptor<T>, INotQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.Not = query);

		/// <summary>
		/// A limit query limits the number of documents (per shard) to execute on. For example:
		/// </summary>
		public QueryContainer Limit(Func<LimitQueryDescriptor<T>, ILimitQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.Limit = query);

#pragma warning restore 618
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
		/// the boosting query can be used to effectively demote results that match a given query.
		/// Unlike the "NOT" clause in bool query, this still selects documents that contain
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
		/// Matches documents that have fields that contain a term (not analyzed).
		/// The term query maps to Lucene TermQuery.
		/// </summary>
		public QueryContainer Term(Expression<Func<T, object>> field, object value, double? boost = null, string name = null) =>
			this.Term(t => t.Field(field).Value(value).Boost(boost).Name(name));

		/// <summary>
		/// Matches documents that have fields that contain a term (not analyzed).
		/// The term query maps to Lucene TermQuery.
		/// </summary>
		public QueryContainer Term(string field, object value, double? boost = null, string name = null) =>
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
		public QueryContainer Wildcard(Expression<Func<T, object>> field, string value, double? boost = null, RewriteMultiTerm? rewrite = null, string name = null) =>
			this.Wildcard(t => t.Field(field).Value(value).Rewrite(rewrite).Boost(boost).Name(name));

		/// <summary>
		/// Matches documents that have fields matching a wildcard expression (not analyzed).
		/// Supported wildcards are *, which matches any character sequence (including the empty one), and ?,
		/// which matches any single character. Note this query can be slow, as it needs to iterate over many terms.
		/// In order to prevent extremely slow wildcard queries, a wildcard term should not start with
		/// one of the wildcards * or ?. The wildcard query maps to Lucene WildcardQuery.
		/// </summary>
		public QueryContainer Wildcard(string field, string value, double? boost = null, RewriteMultiTerm? rewrite = null, string name = null) =>
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
		public QueryContainer Prefix(Expression<Func<T, object>> field, string value, double? boost = null, RewriteMultiTerm? rewrite = null, string name = null) =>
			this.Prefix(t => t.Field(field).Value(value).Boost(boost).Rewrite(rewrite).Name(name));

		/// <summary>
		/// Matches documents that have fields containing terms with a specified prefix (not analyzed).
		/// The prefix query maps to Lucene PrefixQuery.
		/// </summary>
		public QueryContainer Prefix(string field, string value, double? boost = null, RewriteMultiTerm? rewrite = null, string name = null) =>
			this.Prefix(t => t.Field(field).Value(value).Boost(boost).Rewrite(rewrite).Name(name));

		/// <summary>
		/// Matches documents that have fields containing terms with a specified prefix (not analyzed).
		/// The prefix query maps to Lucene PrefixQuery.
		/// </summary>
		public QueryContainer Prefix(Func<PrefixQueryDescriptor<T>, IPrefixQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.Prefix = query);

		/// <summary>
		/// Filters documents that only have the provided ids.
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
		/// </summary>
		public QueryContainer SpanContaining(Func<SpanContainingQueryDescriptor<T>, ISpanContainingQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.SpanContaining = query);

		/// <summary>
		/// </summary>
		public QueryContainer SpanWithin(Func<SpanWithinQueryDescriptor<T>, ISpanWithinQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.SpanWithin = query);

		/// <summary>
		/// custom_score query allows to wrap another query and customize the scoring of it optionally with a
		/// computation derived from other field values in the doc (numeric ones) using script or boost expression
		/// </summary>
		public QueryContainer Regexp(Func<RegexpQueryDescriptor<T>, IRegexpQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.Regexp = query);

		/// <summary>
		/// Function score query
		/// </summary>
		/// <returns></returns>
		public QueryContainer FunctionScore(Func<FunctionScoreQueryDescriptor<T>, IFunctionScoreQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.FunctionScore = query);

		public QueryContainer Template(Func<TemplateQueryDescriptor<T>, ITemplateQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.Template = query);

		public QueryContainer Script(Func<ScriptQueryDescriptor<T>, IScriptQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.Script = query);

		public QueryContainer Exists(Func<ExistsQueryDescriptor<T>, IExistsQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.Exists = query);

		public QueryContainer Missing(Func<MissingQueryDescriptor<T>, IMissingQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.Missing = query);

		public QueryContainer Type(Func<TypeQueryDescriptor, ITypeQuery> selector) =>
			WrapInContainer(selector, (query, container) => container.Type = query);

		public QueryContainer Type<TOther>() => this.Type(q => q.Value<TOther>());

	}
}
