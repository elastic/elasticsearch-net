using Nest.FactoryDsl.Filter;
using Nest.FactoryDsl.Query;

namespace Nest.FactoryDsl
{
    public static class FilterFactory
    {
        /// <summary>
        /// A filter that matches all documents.
        /// </summary>
        /// <returns></returns>
        public static MatchAllFilterBuilder MatchAllFilter()
        {
            return new MatchAllFilterBuilder();
        }

        /// <summary>
        /// A filter that limits the results to the provided limit value (per shard!).
        /// </summary>
        /// <param name="limit">The limit</param>
        /// <returns></returns>
        public static LimitFilterBuilder LimitFilter(int limit)
        {
            return new LimitFilterBuilder(limit);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public static NestedFilterBuilder NestedFilter(string path, IQueryBuilder query)
        {
            return new NestedFilterBuilder(path, query);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static NestedFilterBuilder NestedFilter(string path, IFilterBuilder filter)
        {
            return new NestedFilterBuilder(path, filter);
        }

        /// <summary>
        /// Creates a new ids filter with the provided doc/mapping types.
        /// </summary>
        /// <param name="types">The types to match the ids against.</param>
        /// <returns></returns>
        public static IdsFilterBuilder IdsFilter(params string[] types)
        {
            return new IdsFilterBuilder(types);
        }

        /// <summary>
        /// A filter based on doc/mapping type.
        /// </summary>
        /// <param name="type">The field type</param>
        /// <returns></returns>
        public static TypeFilterBuilder TypeFilter(string type)
        {
            return new TypeFilterBuilder(type);
        }

        /// <summary>
        /// A filter for a field based on a term.
        /// </summary>
        /// <param name="name">The field name</param>
        /// <param name="value">The term value</param>
        /// <returns></returns>
        public static TermFilterBuilder TermFilter(string name, string value)
        {
            return new TermFilterBuilder(name, value);
        }

        /// <summary>
        /// A filter for a field based on a term.
        /// </summary>
        /// <param name="name">The field name</param>
        /// <param name="value">The term value</param>
        /// <returns></returns>
        public static TermFilterBuilder TermFilter(string name, int value)
        {
            return new TermFilterBuilder(name, value);
        }

        /// <summary>
        /// A filter for a field based on a term.
        /// </summary>
        /// <param name="name">The field name</param>
        /// <param name="value">The term value</param>
        /// <returns></returns>
        public static TermFilterBuilder TermFilter(string name, long value)
        {
            return new TermFilterBuilder(name, value);
        }

        /// <summary>
        /// A filter for a field based on a term.
        /// </summary>
        /// <param name="name">The field name</param>
        /// <param name="value">The term value</param>
        /// <returns></returns>
        public static TermFilterBuilder TermFilter(string name, float value)
        {
            return new TermFilterBuilder(name, value);
        }

        /// <summary>
        /// A filter for a field based on a term.
        /// </summary>
        /// <param name="name">The field name</param>
        /// <param name="value">The term value</param>
        /// <returns></returns>
        public static TermFilterBuilder TermFilter(string name, double value)
        {
            return new TermFilterBuilder(name, value);
        }

        /// <summary>
        /// A filter for a field based on a term.
        /// </summary>
        /// <param name="name">The field name</param>
        /// <param name="value">The term value</param>
        /// <returns></returns>
        public static TermFilterBuilder TermFilter(string name, object value)
        {
            return new TermFilterBuilder(name, value);
        }

        /// <summary>
        /// A filer for a field based on several terms matching on any of them.
        /// </summary>
        /// <param name="name">The field name</param>
        /// <param name="values">The terms</param>
        /// <returns></returns>
        public static TermsFilterBuilder TermsFilter(string name, params string[] values)
        {
            return new TermsFilterBuilder(name, values);
        }

        /// <summary>
        /// A filer for a field based on several terms matching on any of them.
        /// </summary>
        /// <param name="name">The field name</param>
        /// <param name="values">The terms</param>
        /// <returns></returns>
        public static TermsFilterBuilder TermsFilter(string name, params int[] values)
        {
            return new TermsFilterBuilder(name, values);
        }

        /// <summary>
        /// A filer for a field based on several terms matching on any of them.
        /// </summary>
        /// <param name="name">The field name</param>
        /// <param name="values">The terms</param>
        /// <returns></returns>
        public static TermsFilterBuilder TermsFilter(string name, params long[] values)
        {
            return new TermsFilterBuilder(name, values);
        }

        /// <summary>
        /// A filer for a field based on several terms matching on any of them.
        /// </summary>
        /// <param name="name">The field name</param>
        /// <param name="values">The terms</param>
        /// <returns></returns>
        public static TermsFilterBuilder TermsFilter(string name, params float[] values)
        {
            return new TermsFilterBuilder(name, values);
        }

        /// <summary>
        /// A filer for a field based on several terms matching on any of them.
        /// </summary>
        /// <param name="name">The field name</param>
        /// <param name="values">The terms</param>
        /// <returns></returns>
        public static TermsFilterBuilder TermsFilter(string name, params double[] values)
        {
            return new TermsFilterBuilder(name, values);
        }

        /// <summary>
        /// A filer for a field based on several terms matching on any of them.
        /// </summary>
        /// <param name="name">The field name</param>
        /// <param name="values">The terms</param>
        /// <returns></returns>
        public static TermsFilterBuilder TermsFilter(string name, params object[] values)
        {
            return new TermsFilterBuilder(name, values);
        }

        /// <summary>
        /// A filer for a field based on several terms matching on any of them.
        /// </summary>
        /// <param name="name">The field name</param>
        /// <param name="values">The terms</param>
        /// <returns></returns>
        public static TermsFilterBuilder InFilter(string name, params string[] values)
        {
            return new TermsFilterBuilder(name, values);
        }

        /// <summary>
        /// A filer for a field based on several terms matching on any of them.
        /// </summary>
        /// <param name="name">The field name</param>
        /// <param name="values">The terms</param>
        /// <returns></returns>
        public static TermsFilterBuilder InFilter(string name, params int[] values)
        {
            return new TermsFilterBuilder(name, values);
        }

        /// <summary>
        /// A filer for a field based on several terms matching on any of them.
        /// </summary>
        /// <param name="name">The field name</param>
        /// <param name="values">The terms</param>
        /// <returns></returns>
        public static TermsFilterBuilder InFilter(string name, params long[] values)
        {
            return new TermsFilterBuilder(name, values);
        }

        /// <summary>
        /// A filer for a field based on several terms matching on any of them.
        /// </summary>
        /// <param name="name">The field name</param>
        /// <param name="values">The terms</param>
        /// <returns></returns>
        public static TermsFilterBuilder InFilter(string name, params float[] values)
        {
            return new TermsFilterBuilder(name, values);
        }

        /// <summary>
        /// A filer for a field based on several terms matching on any of them.
        /// </summary>
        /// <param name="name">The field name</param>
        /// <param name="values">The terms</param>
        /// <returns></returns>
        public static TermsFilterBuilder InFilter(string name, params double[] values)
        {
            return new TermsFilterBuilder(name, values);
        }

        /// <summary>
        /// A filer for a field based on several terms matching on any of them.
        /// </summary>
        /// <param name="name">The field name</param>
        /// <param name="values">The terms</param>
        /// <returns></returns>
        public static TermsFilterBuilder InFilter(string name, params object[] values)
        {
            return new TermsFilterBuilder(name, values);
        }

        /// <summary>
        /// A filter that restricts search results to values that have a matching prefix in a given field.
        /// </summary>
        /// <param name="name">The field name</param>
        /// <param name="prefix">The prefix</param>
        /// <returns></returns>
        public static PrefixFilterBuilder PrefixFilter(string name, string prefix)
        {
            return new PrefixFilterBuilder(name, prefix);
        }

        /// <summary>
        /// A filter that restricts search results to values that are within the given range.
        /// </summary>
        /// <param name="name">The field name</param>
        /// <returns></returns>
        public static RangeFilterBuilder RangeFilter(string name)
        {
            return new RangeFilterBuilder(name);
        }

        /// <summary>
        /// A filter that restricts search results to values that are within the given numeric range. Uses the field data cache (loading all the values for the specified field into memory)
        /// </summary>
        /// <param name="name">The field name</param>
        /// <returns></returns>
        public static NumericRangeFilterBuilder NumericRangeFilter(string name)
        {
            return new NumericRangeFilterBuilder(name);
        }

        /// <summary>
        /// A filter that simply wraps a query.
        /// </summary>
        /// <param name="queryBuilder">The query to wrap as a filter</param>
        /// <returns></returns>
        public static QueryFilterBuilder QueryFilter(IQueryBuilder queryBuilder)
        {
            return new QueryFilterBuilder(queryBuilder);
        }

        /// <summary>
        /// A builder for filter based on a script.
        /// </summary>
        /// <param name="script">The script to filter by</param>
        /// <returns></returns>
        public static ScriptFilterBuilder ScriptFilter(string script)
        {
            return new ScriptFilterBuilder(script);
        }

        /// <summary>
        /// A filter to filter based on a specific distance from a specific geo location / point.
        /// </summary>
        /// <param name="name">The location field name</param>
        /// <returns></returns>
        public static GeoDistanceFilterBuilder GeoDistanceFilter(string name)
        {
            return new GeoDistanceFilterBuilder(name);
        }

        /// <summary>
        /// A filter to filter based on a specific range from a specific geo location / point.
        /// </summary>
        /// <param name="name">The location field name</param>
        /// <returns></returns>
        public static GeoDistanceRangeFilterBuilder GeoDistanceRangeFilter(string name)
        {
            return new GeoDistanceRangeFilterBuilder(name);
        }

        /// <summary>
        /// A filter to filter based on a bounding box defined by top left and bottom right locations / points
        /// </summary>
        /// <param name="name">The location field name</param>
        /// <returns></returns>
        public static GeoBoundingBoxFilterBuilder GeoBoundingBoxFilter(string name)
        {
            return new GeoBoundingBoxFilterBuilder(name);
        }

        /// <summary>
        /// A filter to filter based on a polygon defined by a set of locations  / points.
        /// </summary>
        /// <param name="name">The location field name</param>
        /// <returns></returns>
        public static GeoPolygonFilterBuilder GeoPolygonFilter(string name)
        {
            return new GeoPolygonFilterBuilder(name);
        }

        /// <summary>
        /// A filter to filter only documents where a field exists in them.
        /// </summary>
        /// <param name="name">The name of the field</param>
        /// <returns></returns>
        public static ExistsFilterBuilder ExistsFilter(string name)
        {
            return new ExistsFilterBuilder(name);
        }

        /// <summary>
        /// A filter to filter only documents where a field does not exists in them.
        /// </summary>
        /// <param name="name">The name of the field</param>
        /// <returns></returns>
        public static MissingFilterBuilder MissingFilter(string name)
        {
            return new MissingFilterBuilder(name);
        }

        /// <summary>
        /// Constructs a child filter, with the child type and the query to run against child documents, with
        /// the result of the filter being the *parent* documents.
        /// </summary>
        /// <param name="type">The child type</param>
        /// <param name="query">The query to run against the child type</param>
        /// <returns></returns>
        public static HasChildFilterBuilder HasChildFilter(string type, IQueryBuilder query)
        {
            return new HasChildFilterBuilder(type, query);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static BoolFilterBuilder BoolFilter()
        {
            return new BoolFilterBuilder();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        public static AndFilterBuilder AndFilter(params IFilterBuilder[] filters)
        {
            return new AndFilterBuilder(filters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        public static OrFilterBuilder OrFilter(params IFilterBuilder[] filters)
        {
            return new OrFilterBuilder(filters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static NotFilterBuilder NotFilter(IFilterBuilder filter)
        {
            return new NotFilterBuilder(filter);
        }
    }
}