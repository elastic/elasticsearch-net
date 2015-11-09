using System;
using System.Collections.Generic;
using System.Linq;
using Nest.DSL.Visitor;
using Newtonsoft.Json;
using Nest.QueryDsl.Visitor;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReserializeJsonConverter<QueryContainer, IQueryContainer>))]
	public interface IQueryContainer : ICustomJson
	{
		[JsonProperty(PropertyName = "bool")]
		IBoolQuery Bool { get; set; }

		[JsonIgnore]
		bool IsConditionless { get; set; }

		[JsonIgnore]
		bool IsStrict { get; set; }

		[JsonIgnore]
		bool IsVerbatim { get; set; }
		
		[JsonIgnore]
		string RawQuery { get; set; }
		
		[JsonProperty(PropertyName = "match_all")]
		IMatchAllQuery MatchAllQuery { get; set; }

		[JsonProperty(PropertyName = "term")]
		ITermQuery Term { get; set; }

		[JsonProperty(PropertyName = "wildcard")]
		IWildcardQuery Wildcard { get; set; }

		[JsonProperty(PropertyName = "prefix")]
		IPrefixQuery Prefix { get; set; }

		[JsonProperty(PropertyName = "boosting")]
		IBoostingQuery Boosting { get; set; }

		[JsonProperty(PropertyName = "ids")]
		IIdsQuery Ids { get; set; }

		[JsonProperty(PropertyName = "constant_score")]
		IConstantScoreQuery ConstantScore { get; set; }

		[JsonProperty(PropertyName = "dis_max")]
		IDisMaxQuery DisMax { get; set; }

		[JsonProperty(PropertyName = "filtered")]
		IFilteredQuery Filtered { get; set; }

		[JsonProperty(PropertyName = "multi_match")]
		IMultiMatchQuery MultiMatch { get; set; }

		[JsonProperty(PropertyName = "match")]
		IMatchQuery Match { get; set; }

		[JsonProperty(PropertyName = "fuzzy")]
		[JsonConverter(typeof(FuzzyQueryJsonConverter))]
		IFuzzyQuery Fuzzy { get; set; }

		[JsonProperty(PropertyName = "geo_shape")]
		IGeoShapeQuery GeoShape { get; set; }

		[JsonProperty(PropertyName = "common")]
		ICommonTermsQuery CommonTerms { get; set; }

		[JsonProperty(PropertyName = "terms")]
		ITermsQuery Terms { get; set; }

		[JsonProperty(PropertyName = "range")]
		IRangeQuery Range { get; set; }

		[JsonProperty(PropertyName = "regexp")]
		IRegexpQuery Regexp { get; set; }

		[JsonProperty(PropertyName = "has_child")]
		IHasChildQuery HasChild { get; set; }

		[JsonProperty(PropertyName = "has_parent")]
		IHasParentQuery HasParent { get; set; }
		
		[JsonProperty(PropertyName = "span_term")]
		ISpanTermQuery SpanTerm { get; set; }

		[JsonProperty(PropertyName = "simple_query_string")]
		ISimpleQueryStringQuery SimpleQueryString { get; set; }

		[JsonProperty(PropertyName = "query_string")]
		IQueryStringQuery QueryString { get; set; }

		[JsonProperty(PropertyName = "mlt")]
		IMoreLikeThisQuery MoreLikeThis { get; set; }

		[JsonProperty(PropertyName = "span_first")]
		ISpanFirstQuery SpanFirst { get; set; }

		[JsonProperty(PropertyName = "span_or")]
		ISpanOrQuery SpanOr { get; set; }

		[JsonProperty(PropertyName = "span_near")]
		ISpanNearQuery SpanNear { get; set; }

		[JsonProperty(PropertyName = "span_not")]
		ISpanNotQuery SpanNot { get; set; }

		[JsonProperty(PropertyName = "span_containing")]
		ISpanContainingQuery SpanContaining { get; set; }

		[JsonProperty(PropertyName = "span_within")]
		ISpanWithinQuery SpanWithin { get; set; }

		[JsonProperty(PropertyName = "span_multi")]
		ISpanMultiTermQuery SpanMultiTerm { get; set; }

		[JsonProperty(PropertyName = "nested")]
		INestedQuery Nested { get; set; }

		[JsonProperty(PropertyName = "indices")]
		IIndicesQuery Indices { get; set; }

		[JsonProperty(PropertyName = "function_score")]
		IFunctionScoreQuery FunctionScore { get; set; }

		[JsonProperty(PropertyName = "template")]
		ITemplateQuery Template { get; set; }

		[JsonProperty("geo_bounding_box")]
		IGeoBoundingBoxQuery GeoBoundingBox { get; set; }

		[JsonProperty("geo_distance")]
		IGeoDistanceQuery GeoDistance { get; set; }

		[JsonProperty("geo_polygon")]
		IGeoPolygonQuery GeoPolygon { get; set; }

		[JsonProperty("geo_distance_range")]
		IGeoDistanceRangeQuery GeoDistanceRange { get; set; }

		[JsonProperty("geohash_cell")]
		IGeoHashCellQuery GeoHashCell { get; set; }

		[JsonProperty("script")]
		IScriptQuery Script { get; set; }

		[JsonProperty("exists")]
		IExistsQuery Exists { get; set; }

		[JsonProperty("missing")]
		IMissingQuery Missing { get; set; }

		[JsonProperty("type")]
		ITypeQuery Type { get; set; }

		void Accept(IQueryVisitor visitor);
	}

	[JsonObject(MemberSerialization.OptIn)]
	public class QueryContainer : IQueryContainer
	{
		private IQueryContainer Self => this;

		IBoolQuery IQueryContainer.Bool { get; set; }

		string IQueryContainer.RawQuery { get; set; }

		IMatchAllQuery IQueryContainer.MatchAllQuery { get; set; }
		
		ITermQuery IQueryContainer.Term { get; set; }
		
		IWildcardQuery IQueryContainer.Wildcard { get; set; }
		
		IPrefixQuery IQueryContainer.Prefix { get; set; }

		IBoostingQuery IQueryContainer.Boosting { get; set; }
		
		IIdsQuery IQueryContainer.Ids { get; set; }

		IConstantScoreQuery IQueryContainer.ConstantScore { get; set; }
		
		IDisMaxQuery IQueryContainer.DisMax { get; set; }
		
		IFilteredQuery IQueryContainer.Filtered { get; set; }

		IMultiMatchQuery IQueryContainer.MultiMatch { get; set; }
		
		IMatchQuery IQueryContainer.Match { get; set; }
		
		IFuzzyQuery IQueryContainer.Fuzzy { get; set; }
		
		IGeoShapeQuery IQueryContainer.GeoShape { get; set; }
		
		ICommonTermsQuery IQueryContainer.CommonTerms { get; set; }
		
		ITermsQuery IQueryContainer.Terms { get; set; }
		
		IQueryStringQuery IQueryContainer.QueryString { get; set; }
		
		ISimpleQueryStringQuery IQueryContainer.SimpleQueryString { get; set; }
		
		IRegexpQuery IQueryContainer.Regexp { get; set; }

		IHasChildQuery IQueryContainer.HasChild { get; set; }
		
		IHasParentQuery IQueryContainer.HasParent { get; set; }
		
		IMoreLikeThisQuery IQueryContainer.MoreLikeThis { get; set; }
		
		IRangeQuery IQueryContainer.Range { get; set; }

		ISpanTermQuery IQueryContainer.SpanTerm { get; set; }
		
		ISpanFirstQuery IQueryContainer.SpanFirst { get; set; }
		
		ISpanOrQuery IQueryContainer.SpanOr { get; set; }
		
		ISpanNotQuery IQueryContainer.SpanNot { get; set; }
		
		ISpanNearQuery IQueryContainer.SpanNear { get; set; }

		ISpanContainingQuery IQueryContainer.SpanContaining { get; set; }

		ISpanWithinQuery IQueryContainer.SpanWithin { get; set; }

		ISpanMultiTermQuery IQueryContainer.SpanMultiTerm { get; set; }

		INestedQuery IQueryContainer.Nested { get; set; }
		
		IIndicesQuery IQueryContainer.Indices { get; set; }

		IFunctionScoreQuery IQueryContainer.FunctionScore { get; set; }

		ITemplateQuery IQueryContainer.Template { get; set; }

		IGeoBoundingBoxQuery IQueryContainer.GeoBoundingBox { get; set; }

		IGeoDistanceQuery IQueryContainer.GeoDistance { get; set; }

		IGeoPolygonQuery IQueryContainer.GeoPolygon { get; set; }

		IGeoDistanceRangeQuery IQueryContainer.GeoDistanceRange { get; set; }

		IGeoHashCellQuery IQueryContainer.GeoHashCell { get; set; }

		IScriptQuery IQueryContainer.Script { get; set; }

		IExistsQuery IQueryContainer.Exists { get; set; }

		IMissingQuery IQueryContainer.Missing { get; set; }

		ITypeQuery IQueryContainer.Type { get; set; }

		bool IQueryContainer.IsConditionless { get; set; }
		internal bool IsConditionless => Self.IsConditionless;

		bool IQueryContainer.IsStrict { get; set; }
		internal bool IsStrict => Self.IsStrict;

		bool IQueryContainer.IsVerbatim { get; set; }
		internal bool IsVerbatim => Self.IsVerbatim;

		public QueryContainer() {}
	
		public QueryContainer(QueryBase query)
		{
			QueryBase.ToContainer(query, this);
		}
	
		public static QueryContainer From(QueryBase query)
		{
			return QueryBase.ToContainer(query);
		}

		public static QueryContainer operator &(QueryContainer leftContainer, QueryContainer rightContainer)
		{
			QueryContainer queryContainer;
			if (IfEitherIsEmptyReturnTheOtherOrEmpty(leftContainer, rightContainer, out queryContainer)) return queryContainer;

			return leftContainer.MergeMustQueries(rightContainer);
		}
		
		public static QueryContainer operator |(QueryContainer leftContainer, QueryContainer rightContainer)
		{
			QueryContainer queryContainer;
			if (IfEitherIsEmptyReturnTheOtherOrEmpty(leftContainer, rightContainer, out queryContainer)) return queryContainer;

			return leftContainer.MergeShouldQueries(rightContainer);
		}

		private static bool IfEitherIsEmptyReturnTheOtherOrEmpty(QueryContainer leftContainer, QueryContainer rightContainer,
			out QueryContainer queryContainer)
		{
			var combined = new[] {leftContainer, rightContainer};
			queryContainer = !combined.Any(bf => bf == null || bf.IsConditionless)
				? null
				: combined.FirstOrDefault(bf => bf != null && !bf.IsConditionless) ?? CreateEmptyContainer();
			return queryContainer != null;
		}

		public static QueryContainer operator !(QueryContainer queryContainer)
		{
			if (queryContainer == null || queryContainer.IsConditionless) return CreateEmptyContainer();

			IQueryContainer f = new QueryContainer();
			f.Bool = new BoolQuery();
			f.Bool.MustNot = new[] { queryContainer };
			return f as QueryContainer;
		}

		public static bool operator false(QueryContainer a)
		{
			return false;
		}

		public static bool operator true(QueryContainer a)
		{
			return false;
		}

		public void Accept(IQueryVisitor visitor)
		{
			var walker = new QueryWalker();
			if (visitor.Scope == VisitorScope.Unknown) visitor.Scope = VisitorScope.Query;
			walker.Walk(this, visitor);
		}

		private readonly JsonSerializerSettings _jsonSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
		protected string CreateConditionlessWhenStrictExceptionMessage<TQuery>(TQuery query) where TQuery : IQuery =>
			"Query resulted in a conditionless {0} query (json by approx):\n{1}"
				.F(
					query.GetType().Name.Replace("Descriptor", "").Replace("`1", ""),
					JsonConvert.SerializeObject(this, Formatting.Indented, _jsonSettings)

				);
		protected static QueryContainer CreateEmptyContainer()
		{
			var q = new QueryContainer();
			((IQueryContainer)q).IsConditionless = true;
			return q;
		}

		//TODO remove rely on a custom serializer
		public object GetCustomJson()
		{	
			var f = ((IQueryContainer)this);
			if (f.RawQuery.IsNullOrEmpty()) return f; 
			return new RawJson(f.RawQuery);
		}
	}

}
