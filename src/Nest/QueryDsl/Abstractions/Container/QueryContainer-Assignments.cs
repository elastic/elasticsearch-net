using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public partial class QueryContainer : IQueryContainer, IDescriptor
	{
		private IBoolQuery _bool;
		private IMatchAllQuery _matchAllQuery;
		private IMatchNoneQuery _matchNoneQuery;
		private ITermQuery _term;
		private IWildcardQuery _wildcard;
		private IPrefixQuery _prefix;
		private IBoostingQuery _boosting;
		private IIdsQuery _ids;
		private IConstantScoreQuery _constantScore;
		private IDisMaxQuery _disMax;
		private IMultiMatchQuery _multiMatch;
		private IMatchQuery _match;
		private IMatchPhraseQuery _matchPhrase;
		private IMatchPhrasePrefixQuery _matchPhrasePrefix;
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
		private ISpanFieldMaskingQuery _spanFieldMasking;
		private INestedQuery _nested;
		private IFunctionScoreQuery _functionScore;
		private IGeoBoundingBoxQuery _geoBoundingBox;
		private IGeoDistanceQuery _geoDistance;
		private IGeoPolygonQuery _geoPolygon;
		private IScriptQuery _script;
		private IExistsQuery _exists;
		private ITypeQuery _type;
		private IRawQuery _raw;
		private IPercolateQuery _percolate;
		private IParentIdQuery _parentId;
		private ITermsSetQuery _termsSet;
		private IQueryContainer Self => this;

		internal IQuery ContainedQuery { get; set; }

		private T Set<T>(T value) where T : IQuery
		{
			if (this.ContainedQuery != null)
				throw new Exception($"{nameof(QueryContainer)} can only hold a single query; Instance already contains a {this.ContainedQuery.GetType().Name}");
			this.ContainedQuery = value;
			return value;
		}

		IRawQuery IQueryContainer.RawQuery { get => _raw; set => _raw = Set(value); }
		IBoolQuery IQueryContainer.Bool { get => _bool; set => _bool = Set(value); }
		IMatchAllQuery IQueryContainer.MatchAll { get => _matchAllQuery; set => _matchAllQuery = Set(value); }
		IMatchNoneQuery IQueryContainer.MatchNone { get => _matchNoneQuery; set => _matchNoneQuery = Set(value); }
		ITermQuery IQueryContainer.Term { get => _term; set => _term = Set(value); }
		IWildcardQuery IQueryContainer.Wildcard { get => _wildcard; set => _wildcard = Set(value); }
		IPrefixQuery IQueryContainer.Prefix { get => _prefix; set => _prefix = Set(value); }
		IBoostingQuery IQueryContainer.Boosting { get => _boosting; set => _boosting = Set(value); }
		IIdsQuery IQueryContainer.Ids { get => _ids; set => _ids = Set(value); }
		IConstantScoreQuery IQueryContainer.ConstantScore { get => _constantScore; set => _constantScore = Set(value); }
		IDisMaxQuery IQueryContainer.DisMax { get => _disMax; set => _disMax = Set(value); }
		IMultiMatchQuery IQueryContainer.MultiMatch { get => _multiMatch; set => _multiMatch = Set(value); }
		IMatchQuery IQueryContainer.Match { get => _match; set => _match = Set(value); }
		IMatchPhraseQuery IQueryContainer.MatchPhrase { get => _matchPhrase; set => _matchPhrase = Set(value); }
		IMatchPhrasePrefixQuery IQueryContainer.MatchPhrasePrefix { get => _matchPhrasePrefix; set => _matchPhrasePrefix = Set(value); }
		IFuzzyQuery IQueryContainer.Fuzzy { get => _fuzzy; set => _fuzzy = Set(value); }
		IGeoShapeQuery IQueryContainer.GeoShape { get => _geoShape; set => _geoShape = Set(value); }
		ICommonTermsQuery IQueryContainer.CommonTerms { get => _commonTerms; set => _commonTerms = Set(value); }
		ITermsQuery IQueryContainer.Terms { get => _terms; set => _terms = Set(value); }
		IQueryStringQuery IQueryContainer.QueryString { get => _queryString; set => _queryString = Set(value); }
		ISimpleQueryStringQuery IQueryContainer.SimpleQueryString { get => _simpleQueryString; set => _simpleQueryString = Set(value); }
		IRegexpQuery IQueryContainer.Regexp { get => _regexp; set => _regexp = Set(value); }
		IHasChildQuery IQueryContainer.HasChild { get => _hasChild; set => _hasChild = Set(value); }
		IHasParentQuery IQueryContainer.HasParent { get => _hasParent; set => _hasParent = Set(value); }
		IMoreLikeThisQuery IQueryContainer.MoreLikeThis { get => _moreLikeThis; set => _moreLikeThis = Set(value); }
		IRangeQuery IQueryContainer.Range { get => _range; set => _range = Set(value); }
		ISpanTermQuery IQueryContainer.SpanTerm { get => _spanTerm; set => _spanTerm = Set(value); }
		ISpanFirstQuery IQueryContainer.SpanFirst { get => _spanFirst; set => _spanFirst = Set(value); }
		ISpanOrQuery IQueryContainer.SpanOr { get => _spanOr; set => _spanOr = Set(value); }
		ISpanNotQuery IQueryContainer.SpanNot { get => _spanNot; set => _spanNot = Set(value); }
		ISpanNearQuery IQueryContainer.SpanNear { get => _spanNear; set => _spanNear = Set(value); }
		ISpanContainingQuery IQueryContainer.SpanContaining { get => _spanContaining; set => _spanContaining = Set(value); }
		ISpanWithinQuery IQueryContainer.SpanWithin { get => _spanWithin; set => _spanWithin = Set(value); }
		ISpanMultiTermQuery IQueryContainer.SpanMultiTerm { get => _spanMultiTerm; set => _spanMultiTerm = Set(value); }
		ISpanFieldMaskingQuery IQueryContainer.SpanFieldMasking { get => _spanFieldMasking; set => _spanFieldMasking = Set(value); }
		INestedQuery IQueryContainer.Nested { get => _nested; set => _nested = Set(value); }
		IFunctionScoreQuery IQueryContainer.FunctionScore { get => _functionScore; set => _functionScore = Set(value); }
		IGeoBoundingBoxQuery IQueryContainer.GeoBoundingBox { get => _geoBoundingBox; set => _geoBoundingBox = Set(value); }
		IGeoDistanceQuery IQueryContainer.GeoDistance { get => _geoDistance; set => _geoDistance = Set(value); }
		IGeoPolygonQuery IQueryContainer.GeoPolygon { get => _geoPolygon; set => _geoPolygon = Set(value); }
		IScriptQuery IQueryContainer.Script { get => _script; set => _script = Set(value); }
		IExistsQuery IQueryContainer.Exists { get => _exists; set => _exists = Set(value); }
		ITypeQuery IQueryContainer.Type { get => _type; set => _type = Set(value); }
		IPercolateQuery IQueryContainer.Percolate { get => _percolate; set => _percolate = Set(value); }
		IParentIdQuery IQueryContainer.ParentId { get => _parentId; set => _parentId = Set(value); }
		ITermsSetQuery IQueryContainer.TermsSet { get => _termsSet; set => _termsSet = Set(value); }
	}
}
