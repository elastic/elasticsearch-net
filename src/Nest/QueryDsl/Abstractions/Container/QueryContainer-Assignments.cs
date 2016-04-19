using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public partial class QueryContainer : IQueryContainer, IDescriptor
	{
		private IBoolQuery _b;
		private IMatchAllQuery _matchAllQuery;
		private ITermQuery _term;
		private IWildcardQuery _wildcard;
		private IPrefixQuery _prefix;
		private IBoostingQuery _boosting;
		private IIdsQuery _ids;
		private IConstantScoreQuery _constantScore;
		private IDisMaxQuery _disMax;
#pragma warning disable 618
		private IFilteredQuery _filtered;
		private IAndQuery _and;
		private IOrQuery _or;
		private INotQuery _not;
		private ILimitQuery _limit;
#pragma warning restore 618
		private IMultiMatchQuery _multiMatch;
		private IMatchQuery _match;
		private IFuzzyQuery _fuzzy;
		private IGeoShapeQuery _geoShape;
		private ICommonTermsQuery _commonTerms;
		private ITermsQuery _terms;
		private IQueryStringQuery _queryString;
		private ISimpleQueryStringQuery _simpleQueryString;
		private IRegexpQuery _regexp;
		private IHasChildQuery _hasChild;
		private IHasParentQuery _hasParent;
		private IMoreLikeThisQuery _moreLikeThis;
		private IRangeQuery _range;
		private ISpanTermQuery _spanTerm;
		private ISpanFirstQuery _spanFirst;
		private ISpanOrQuery _spanOr;
		private ISpanNotQuery _spanNot;
		private ISpanNearQuery _spanNear;
		private ISpanContainingQuery _spanContaining;
		private ISpanWithinQuery _spanWithin;
		private ISpanMultiTermQuery _spanMultiTerm;
		private INestedQuery _nested;
		private IIndicesQuery _indices;
		private IFunctionScoreQuery _functionScore;
		private ITemplateQuery _template;
		private IGeoBoundingBoxQuery _geoBoundingBox;
		private IGeoDistanceQuery _geoDistance;
		private IGeoPolygonQuery _geoPolygon;
		private IGeoDistanceRangeQuery _geoDistanceRange;
		private IGeoHashCellQuery _geoHashCell;
		private IScriptQuery _script;
		private IExistsQuery _exists;
		private IMissingQuery _missing;
		private ITypeQuery _type;
		private IRawQuery _rawQuery;
		private IQueryContainer Self => this;

		internal IQuery ContainedQuery { get; set; }

		private T Set<T>(T value) where T : IQuery
		{
			if (this.ContainedQuery != null)
				throw new Exception($"QueryContainer can only hold a single query already contains a {this.ContainedQuery.GetType().Name}");
			this.ContainedQuery = value;
			return value;
		}

		IRawQuery IQueryContainer.RawQuery { get { return _rawQuery; } set { _rawQuery = Set(value); } }
		IBoolQuery IQueryContainer.Bool { get { return _b; } set { _b = Set(value); } }
		IMatchAllQuery IQueryContainer.MatchAll { get { return _matchAllQuery; } set { _matchAllQuery = Set(value); } }
		ITermQuery IQueryContainer.Term { get { return _term; } set { _term = Set(value); } }
		IWildcardQuery IQueryContainer.Wildcard { get { return _wildcard; } set { _wildcard = Set(value); } }
		IPrefixQuery IQueryContainer.Prefix { get { return _prefix; } set { _prefix = Set(value); } }
		IBoostingQuery IQueryContainer.Boosting { get { return _boosting; } set { _boosting = Set(value); } }
		IIdsQuery IQueryContainer.Ids { get { return _ids; } set { _ids = Set(value); } }

		IConstantScoreQuery IQueryContainer.ConstantScore { get { return _constantScore; } set { _constantScore = Set(value); } }
		IDisMaxQuery IQueryContainer.DisMax { get { return _disMax; } set { _disMax = Set(value); } }

#pragma warning disable 618
		IFilteredQuery IQueryContainer.Filtered { get { return _filtered; } set { _filtered = Set(value); } }
		IAndQuery IQueryContainer.And { get { return _and; } set { _and = Set(value); } }
		IOrQuery IQueryContainer.Or { get { return _or; } set { _or = Set(value); } }
		INotQuery IQueryContainer.Not { get { return _not; } set { _not = Set(value); } }
		ILimitQuery IQueryContainer.Limit { get { return _limit; } set { _limit = Set(value); } }
#pragma warning restore 618
		IMultiMatchQuery IQueryContainer.MultiMatch { get { return _multiMatch; } set { _multiMatch = Set(value); } }
		IMatchQuery IQueryContainer.Match { get { return _match; } set { _match = Set(value); } }
		IFuzzyQuery IQueryContainer.Fuzzy { get { return _fuzzy; } set { _fuzzy = Set(value); } }
		IGeoShapeQuery IQueryContainer.GeoShape { get { return _geoShape; } set { _geoShape = Set(value); } }
		ICommonTermsQuery IQueryContainer.CommonTerms { get { return _commonTerms; } set { _commonTerms = Set(value); } }
		ITermsQuery IQueryContainer.Terms { get { return _terms; } set { _terms = Set(value); } }
		IQueryStringQuery IQueryContainer.QueryString { get { return _queryString; } set { _queryString = Set(value); } }
		ISimpleQueryStringQuery IQueryContainer.SimpleQueryString { get { return _simpleQueryString; } set { _simpleQueryString = Set(value); } }
		IRegexpQuery IQueryContainer.Regexp { get { return _regexp; } set { _regexp = Set(value); } }
		IHasChildQuery IQueryContainer.HasChild { get { return _hasChild; } set { _hasChild = Set(value); } }
		IHasParentQuery IQueryContainer.HasParent { get { return _hasParent; } set { _hasParent = Set(value); } }
		IMoreLikeThisQuery IQueryContainer.MoreLikeThis { get { return _moreLikeThis; } set { _moreLikeThis = Set(value); } }
		IRangeQuery IQueryContainer.Range { get { return _range; } set { _range = Set(value); } }
		ISpanTermQuery IQueryContainer.SpanTerm { get { return _spanTerm; } set { _spanTerm = Set(value); } }
		ISpanFirstQuery IQueryContainer.SpanFirst { get { return _spanFirst; } set { _spanFirst = Set(value); } }
		ISpanOrQuery IQueryContainer.SpanOr { get { return _spanOr; } set { _spanOr = Set(value); } }
		ISpanNotQuery IQueryContainer.SpanNot { get { return _spanNot; } set { _spanNot = Set(value); } }
		ISpanNearQuery IQueryContainer.SpanNear { get { return _spanNear; } set { _spanNear = Set(value); } }
		ISpanContainingQuery IQueryContainer.SpanContaining { get { return _spanContaining; } set { _spanContaining = Set(value); } }
		ISpanWithinQuery IQueryContainer.SpanWithin { get { return _spanWithin; } set { _spanWithin = Set(value); } }
		ISpanMultiTermQuery IQueryContainer.SpanMultiTerm { get { return _spanMultiTerm; } set { _spanMultiTerm = Set(value); } }
		INestedQuery IQueryContainer.Nested { get { return _nested; } set { _nested = Set(value); } }
		IIndicesQuery IQueryContainer.Indices { get { return _indices; } set { _indices = Set(value); } }
		IFunctionScoreQuery IQueryContainer.FunctionScore { get { return _functionScore; } set { _functionScore = Set(value); } }
		ITemplateQuery IQueryContainer.Template { get { return _template; } set { _template = Set(value); } }
		IGeoBoundingBoxQuery IQueryContainer.GeoBoundingBox { get { return _geoBoundingBox; } set { _geoBoundingBox = Set(value); } }
		IGeoDistanceQuery IQueryContainer.GeoDistance { get { return _geoDistance; } set { _geoDistance = Set(value); } }
		IGeoPolygonQuery IQueryContainer.GeoPolygon { get { return _geoPolygon; } set { _geoPolygon = Set(value); } }
		IGeoDistanceRangeQuery IQueryContainer.GeoDistanceRange { get { return _geoDistanceRange; } set { _geoDistanceRange = Set(value); } }
		IGeoHashCellQuery IQueryContainer.GeoHashCell { get { return _geoHashCell; } set { _geoHashCell = Set(value); } }
		IScriptQuery IQueryContainer.Script { get { return _script; } set { _script = Set(value); } }
		IExistsQuery IQueryContainer.Exists { get { return _exists; } set { _exists = Set(value); } }
		IMissingQuery IQueryContainer.Missing { get { return _missing; } set { _missing = Set(value); } }
		ITypeQuery IQueryContainer.Type { get { return _type; } set { _type = Set(value); } }


	}

}
