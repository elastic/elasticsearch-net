namespace Nest.Dsl.Factory
{
    public static class NameRegistry
    {
        #region IQueryBuilders
        public const string MatchQueryBuilder = "match";
        public const string BoolQueryBuilder = "bool";
        public const string BoostingQueryBuilder = "boosting";
        public const string ConstantScoreQueryBuilder = "constant_score";
        public const string CustomBoostFactorQueryBuilder = "custom_boost_factor";
        public const string CustomFilterScoreQueryBuilder = "custom_filters_score";
        public const string CustomScoreQueryBuilder = "custom_score";
        public const string DisMaxQueryBuilder = "dis_max";
        public const string FieldMaskingSpanQueryBuilder = "field_masking_span";
        public const string FieldQueryBuilder = "field";
        public const string FilteredQueryBuilder = "filtered";
        public const string FuzzyLikeThisFieldQueryBuilder = "flt_field";
        public const string FuzzyLikeThisQueryBuilder = "flt";
        public const string FuzzyQueryBuilder = "fuzzy";
        public const string HasChildQueryBuilder = "has_child";
        public const string IdsQueryBuilder = "ids";
        public const string MatchAllQueryBuilder = "match_all";
        public const string MoreLikeThisFieldQueryBuilder = "mlt_field";
        public const string MoreLikeThisQueryBuilder = "mlt";
        public const string NestedQueryBuilder = "nested";
        public const string PrefixQueryBuilder = "prefix";
        public const string QuerystringQueryBuilder = "query_string";
        public const string RangeQueryBuilder = "range";
        public const string SpanFirstQueryBuilder = "span_first";
        public const string SpanNearQueryBuilder = "span_first";
        public const string SpanNotQueryBuilder = "span_not";
        public const string SpanOrQueryBuilder = "span_or";
        public const string SpanTermQueryBuilder = "span_term";
        public const string TermQueryBuilder = "term";
        public const string TermsQueryBuilder = "terms";
        public const string TextQueryBuilder = "text";
        public const string TopChildrenQueryBuilder = "top_children";
        public const string WildcardQueryBuilder = "wildcard";
        public const string WrapperQueryBuilder = "wrapper";
        public const string IndicesQueryBuilder = "indices";
        #endregion

        #region IFilterBuilders
        public const string AndFilterBuilder = "and";
        public const string BoolFilterBuilder = "bool";
        public const string ExistsFilterBuilder = "exists";
        public const string GeoBoundingBoxFilterBuilder = "geo_bbox";
        public const string GeoDistanceFilterBuilder = "geo_distance";
        public const string GeoDistanceRangeFilterBuilder = "geo_distance_range";
        public const string GeoPolygonFilterBuilder = "geo_polygon";
        public const string HasChildFilterBuilder = "has_child";
        public const string IdsFilterBuilder = "ids";
        public const string LimitFilterBuilder = "limit";
        public const string MatchAllFilterBuilder = "match_all";
        public const string MissingFilterBuilder = "missing";
        public const string NestedFilterBuilder = "nested";
        public const string NotFilterBuilder = "not";
        public const string NumericRangeFilterBuilder = "range";
        public const string OrFilterBuilder = "or";
        public const string PrefixFilterBuilder = "prefix";
        public const string QueryFilterBuilder = "query";
        public const string QueryFilterBuilderAlt = "fquery";
        public const string RangeFilterBuilder = "range";
        public const string ScriptFilterBuilder = "script";
        public const string TermFilterBuilder = "term";
        public const string TermsFilterBuilder = "terms";
        public const string TypeFilterBuilder = "type";
        #endregion

        #region FacetBuilders
        public const string AbstractFacetBuilder = "_global_";
        public const string DateHistogramFacetBuilder = "date_histogram";
        public const string FilterFacetBuilder = "filter";
        public const string GeoDistanceFacetBuilder = "geo_distance";
        public const string HistogramFacetBuilder = "histogram";
        public const string HistogramScriptFacetBuilder = "histogram";
        public const string QueryFacetBuilder = "query";
        public const string RangeFacetBuilder = "range";
        public const string RangeScriptFacetBuilder = "range";
        public const string StatisticalFacetBuilder = "statistical";
        public const string StatisticalScriptFacetBuilder = "statistical";
        public const string TermsFacetBuilder = "terms";
        public const string TermsStatsFacetBuilder = "terms_stats";  
        #endregion

        #region ISortBuilders
        public const string GeoDistanceSortBuilder = "_geo_distance";
        public const string ScoreSortBuilder = "_score";
        public const string ScriptSortBuilder = "_script";
        #endregion
    }
}
