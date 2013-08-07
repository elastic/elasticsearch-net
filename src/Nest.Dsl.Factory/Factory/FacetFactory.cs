
namespace Nest.Dsl.Factory
{
    public static class FacetFactory
    {
        public static QueryFacetBuilder QueryFacet(string facetName)
        {
            return new QueryFacetBuilder(facetName);
        }

        public static QueryFacetBuilder QueryFacet(string facetName, IQueryBuilder query)
        {
            return new QueryFacetBuilder(facetName).Query(query);
        }

        public static FilterFacetBuilder FilterFacet(string facetName)
        {
            return new FilterFacetBuilder(facetName);
        }

        public static FilterFacetBuilder FilterFacet(string facetName, IFilterBuilder filter)
        {
            return new FilterFacetBuilder(facetName).Filter(filter);
        }

        public static TermsFacetBuilder TermsFacet(string facetName)
        {
            return new TermsFacetBuilder(facetName);
        }

        public static TermsStatsFacetBuilder TermsStatsFacet(string facetName)
        {
            return new TermsStatsFacetBuilder(facetName);
        }

        public static StatisticalFacetBuilder StatisticalFacet(string facetName)
        {
            return new StatisticalFacetBuilder(facetName);
        }

        public static StatisticalScriptFacetBuilder StatisticalScriptFacet(string facetName)
        {
            return new StatisticalScriptFacetBuilder(facetName);
        }

        public static HistogramFacetBuilder HistogramFacet(string facetName)
        {
            return new HistogramFacetBuilder(facetName);
        }

        public static DateHistogramFacetBuilder DateHistogramFacet(string facetName)
        {
            return new DateHistogramFacetBuilder(facetName);
        }

        public static HistogramScriptFacetBuilder HistogramScriptFacet(string facetName)
        {
            return new HistogramScriptFacetBuilder(facetName);
        }

        public static RangeFacetBuilder RangeFacet(string facetName)
        {
            return new RangeFacetBuilder(facetName);
        }

        public static RangeScriptFacetBuilder RangeScriptFacet(string facetName)
        {
            return new RangeScriptFacetBuilder(facetName);
        }

        public static GeoDistanceFacetBuilder GeoDistanceFacet(string facetName)
        {
            return new GeoDistanceFacetBuilder(facetName);
        }
    }
}
