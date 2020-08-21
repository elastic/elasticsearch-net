// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public partial class QueryContainer : IQueryContainer, IDescriptor
	{
		private IBoolQuery _bool;
		private IBoostingQuery _boosting;
#pragma warning disable 618
		private ICommonTermsQuery _commonTerms;
#pragma warning restore 618
		private IConstantScoreQuery _constantScore;
		private IDisMaxQuery _disMax;
		private IDistanceFeatureQuery _distanceFeature;
		private IExistsQuery _exists;
		private IFunctionScoreQuery _functionScore;
		private IFuzzyQuery _fuzzy;
		private IGeoBoundingBoxQuery _geoBoundingBox;
		private IGeoDistanceQuery _geoDistance;
		private IGeoPolygonQuery _geoPolygon;
		private IGeoShapeQuery _geoShape;
		private IShapeQuery _shape;
		private IHasChildQuery _hasChild;
		private IHasParentQuery _hasParent;
		private IIdsQuery _ids;
		private IIntervalsQuery _intervals;
		private IMatchQuery _match;
		private IMatchAllQuery _matchAllQuery;
		private IMatchBoolPrefixQuery _matchBoolPrefixQuery;
		private IMatchNoneQuery _matchNoneQuery;
		private IMatchPhraseQuery _matchPhrase;
		private IMatchPhrasePrefixQuery _matchPhrasePrefix;
		private IMoreLikeThisQuery _moreLikeThis;
		private IMultiMatchQuery _multiMatch;
		private INestedQuery _nested;
		private IParentIdQuery _parentId;
		private IPercolateQuery _percolate;
		private IPrefixQuery _prefix;
		private IQueryStringQuery _queryString;
		private IRangeQuery _range;
		private IRawQuery _raw;
		private IRegexpQuery _regexp;
		private IScriptQuery _script;
		private IScriptScoreQuery _scriptScore;
		private ISimpleQueryStringQuery _simpleQueryString;
		private ISpanContainingQuery _spanContaining;
		private ISpanFieldMaskingQuery _spanFieldMasking;
		private ISpanFirstQuery _spanFirst;
		private ISpanMultiTermQuery _spanMultiTerm;
		private ISpanNearQuery _spanNear;
		private ISpanNotQuery _spanNot;
		private ISpanOrQuery _spanOr;
		private ISpanTermQuery _spanTerm;
		private ISpanWithinQuery _spanWithin;
		private ITermQuery _term;
		private ITermsQuery _terms;
		private ITermsSetQuery _termsSet;
		private IWildcardQuery _wildcard;
		private IRankFeatureQuery _rankFeature;
		private IPinnedQuery _pinned;

		[IgnoreDataMember]
		private IQueryContainer Self => this;

		[IgnoreDataMember]
		internal IQuery ContainedQuery { get; set; }

		IBoolQuery IQueryContainer.Bool
		{
			get => _bool;
			set => _bool = Set(value);
		}

		IBoostingQuery IQueryContainer.Boosting
		{
			get => _boosting;
			set => _boosting = Set(value);
		}

#pragma warning disable 618
		ICommonTermsQuery IQueryContainer.CommonTerms

		{
			get => _commonTerms;
			set => _commonTerms = Set(value);
		}
#pragma warning restore 618

		IConstantScoreQuery IQueryContainer.ConstantScore
		{
			get => _constantScore;
			set => _constantScore = Set(value);
		}

		IDisMaxQuery IQueryContainer.DisMax
		{
			get => _disMax;
			set => _disMax = Set(value);
		}

		IDistanceFeatureQuery IQueryContainer.DistanceFeature
		{
			get => _distanceFeature;
			set => _distanceFeature = Set(value);
		}

		IExistsQuery IQueryContainer.Exists
		{
			get => _exists;
			set => _exists = Set(value);
		}

		IFunctionScoreQuery IQueryContainer.FunctionScore
		{
			get => _functionScore;
			set => _functionScore = Set(value);
		}

		IFuzzyQuery IQueryContainer.Fuzzy
		{
			get => _fuzzy;
			set => _fuzzy = Set(value);
		}

		IGeoBoundingBoxQuery IQueryContainer.GeoBoundingBox
		{
			get => _geoBoundingBox;
			set => _geoBoundingBox = Set(value);
		}

		IGeoDistanceQuery IQueryContainer.GeoDistance
		{
			get => _geoDistance;
			set => _geoDistance = Set(value);
		}

		IGeoPolygonQuery IQueryContainer.GeoPolygon
		{
			get => _geoPolygon;
			set => _geoPolygon = Set(value);
		}

		IGeoShapeQuery IQueryContainer.GeoShape
		{
			get => _geoShape;
			set => _geoShape = Set(value);
		}

		IShapeQuery IQueryContainer.Shape
		{
			get => _shape;
			set => _shape = Set(value);
		}

		IHasChildQuery IQueryContainer.HasChild
		{
			get => _hasChild;
			set => _hasChild = Set(value);
		}

		IHasParentQuery IQueryContainer.HasParent
		{
			get => _hasParent;
			set => _hasParent = Set(value);
		}

		IIdsQuery IQueryContainer.Ids
		{
			get => _ids;
			set => _ids = Set(value);
		}

		IIntervalsQuery IQueryContainer.Intervals
		{
			get => _intervals;
			set => _intervals = Set(value);
		}

		IMatchQuery IQueryContainer.Match
		{
			get => _match;
			set => _match = Set(value);
		}

		IMatchAllQuery IQueryContainer.MatchAll
		{
			get => _matchAllQuery;
			set => _matchAllQuery = Set(value);
		}

		IMatchBoolPrefixQuery IQueryContainer.MatchBoolPrefix
		{
			get => _matchBoolPrefixQuery;
			set => _matchBoolPrefixQuery = Set(value);
		}

		IMatchNoneQuery IQueryContainer.MatchNone
		{
			get => _matchNoneQuery;
			set => _matchNoneQuery = Set(value);
		}

		IMatchPhraseQuery IQueryContainer.MatchPhrase
		{
			get => _matchPhrase;
			set => _matchPhrase = Set(value);
		}

		IMatchPhrasePrefixQuery IQueryContainer.MatchPhrasePrefix
		{
			get => _matchPhrasePrefix;
			set => _matchPhrasePrefix = Set(value);
		}

		IMoreLikeThisQuery IQueryContainer.MoreLikeThis
		{
			get => _moreLikeThis;
			set => _moreLikeThis = Set(value);
		}

		IMultiMatchQuery IQueryContainer.MultiMatch
		{
			get => _multiMatch;
			set => _multiMatch = Set(value);
		}

		INestedQuery IQueryContainer.Nested
		{
			get => _nested;
			set => _nested = Set(value);
		}

		IParentIdQuery IQueryContainer.ParentId
		{
			get => _parentId;
			set => _parentId = Set(value);
		}

		IPercolateQuery IQueryContainer.Percolate
		{
			get => _percolate;
			set => _percolate = Set(value);
		}

		IPrefixQuery IQueryContainer.Prefix
		{
			get => _prefix;
			set => _prefix = Set(value);
		}

		IQueryStringQuery IQueryContainer.QueryString
		{
			get => _queryString;
			set => _queryString = Set(value);
		}

		IRangeQuery IQueryContainer.Range
		{
			get => _range;
			set => _range = Set(value);
		}

		IRawQuery IQueryContainer.RawQuery
		{
			get => _raw;
			set => _raw = Set(value);
		}

		IRegexpQuery IQueryContainer.Regexp
		{
			get => _regexp;
			set => _regexp = Set(value);
		}

		IScriptQuery IQueryContainer.Script
		{
			get => _script;
			set => _script = Set(value);
		}

		IScriptScoreQuery IQueryContainer.ScriptScore
		{
			get => _scriptScore;
			set => _scriptScore = Set(value);
		}

		ISimpleQueryStringQuery IQueryContainer.SimpleQueryString
		{
			get => _simpleQueryString;
			set => _simpleQueryString = Set(value);
		}

		ISpanContainingQuery IQueryContainer.SpanContaining
		{
			get => _spanContaining;
			set => _spanContaining = Set(value);
		}

		ISpanFieldMaskingQuery IQueryContainer.SpanFieldMasking
		{
			get => _spanFieldMasking;
			set => _spanFieldMasking = Set(value);
		}

		ISpanFirstQuery IQueryContainer.SpanFirst
		{
			get => _spanFirst;
			set => _spanFirst = Set(value);
		}

		ISpanMultiTermQuery IQueryContainer.SpanMultiTerm
		{
			get => _spanMultiTerm;
			set => _spanMultiTerm = Set(value);
		}

		ISpanNearQuery IQueryContainer.SpanNear
		{
			get => _spanNear;
			set => _spanNear = Set(value);
		}

		ISpanNotQuery IQueryContainer.SpanNot
		{
			get => _spanNot;
			set => _spanNot = Set(value);
		}

		ISpanOrQuery IQueryContainer.SpanOr
		{
			get => _spanOr;
			set => _spanOr = Set(value);
		}

		ISpanTermQuery IQueryContainer.SpanTerm
		{
			get => _spanTerm;
			set => _spanTerm = Set(value);
		}

		ISpanWithinQuery IQueryContainer.SpanWithin
		{
			get => _spanWithin;
			set => _spanWithin = Set(value);
		}

		ITermQuery IQueryContainer.Term
		{
			get => _term;
			set => _term = Set(value);
		}

		ITermsQuery IQueryContainer.Terms
		{
			get => _terms;
			set => _terms = Set(value);
		}

		ITermsSetQuery IQueryContainer.TermsSet
		{
			get => _termsSet;
			set => _termsSet = Set(value);
		}

		IWildcardQuery IQueryContainer.Wildcard
		{
			get => _wildcard;
			set => _wildcard = Set(value);
		}

		IRankFeatureQuery IQueryContainer.RankFeature
		{
			get => _rankFeature;
			set => _rankFeature = Set(value);
		}
		IPinnedQuery IQueryContainer.Pinned
		{
			get => _pinned;
			set => _pinned = Set(value);
		}


		private T Set<T>(T value) where T : IQuery
		{
			if (ContainedQuery != null)
				throw new Exception(
					$"Cannot assign {typeof(T).Name} to {nameof(QueryContainer)}. "
					+ $"It can only hold a single query and already contains a {ContainedQuery.GetType().Name}");

			ContainedQuery = value;
			return value;
		}
	}
}
