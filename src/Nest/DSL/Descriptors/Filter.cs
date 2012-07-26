using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using System.Linq.Expressions;

namespace Nest
{

  public static class Filter<T> where T : class
  {
    public static BaseFilter And(params Func<FilterDescriptor<T>, BaseFilter>[] filters)
    {
      return new FilterDescriptor<T>().And(filters);
    }
    public static BaseFilter Bool(Action<BoolFilterDescriptor<T>> booleanFilter)
    {
      return new FilterDescriptor<T>().Bool(booleanFilter);
    }
    public static BaseFilter Exists(Expression<Func<T, object>> fieldDescriptor)
    {
      return new FilterDescriptor<T>().Exists(fieldDescriptor);
    }
    public static BaseFilter Exists(string field)
    {
      return new FilterDescriptor<T>().Exists(field);
    }
    public static BaseFilter GeoBoundingBox(Expression<Func<T, object>> fieldDescriptor, double topLeftX, double topLeftY, double bottomRightX, double bottomRightY, GeoExecution? Type = null)
    {
      return new FilterDescriptor<T>().GeoBoundingBox(fieldDescriptor, topLeftX, topLeftY, bottomRightX, bottomRightY, Type);
    }
    public static BaseFilter GeoBoundingBox(Expression<Func<T, object>> fieldDescriptor, string geoHashTopLeft, string geoHashBottomRight, GeoExecution? Type = null) { 
 return new FilterDescriptor<T>().GeoBoundingBox(fieldDescriptor, geoHashTopLeft, geoHashBottomRight, Type); 
 }
    public static BaseFilter GeoBoundingBox(string fieldName, double topLeftX, double topLeftY, double bottomRightX, double bottomRightY, GeoExecution? Type = null)
    {
      return new FilterDescriptor<T>().GeoBoundingBox(fieldName, topLeftX, topLeftY, bottomRightX, bottomRightY, Type);
    }
    public static BaseFilter GeoBoundingBox(string fieldName, string geoHashTopLeft, string geoHashBottomRight, GeoExecution? Type = null)
    {
      return new FilterDescriptor<T>().GeoBoundingBox(fieldName, geoHashTopLeft, geoHashBottomRight, Type);
    }
    public static BaseFilter GeoDistance(Expression<Func<T, object>> fieldDescriptor, Action<GeoDistanceFilterDescriptor> filterDescriptor)
    {
      return new FilterDescriptor<T>().GeoDistance(fieldDescriptor, filterDescriptor);
    }
    public static BaseFilter GeoDistance(string field, Action<GeoDistanceFilterDescriptor> filterDescriptor)
    {
      return new FilterDescriptor<T>().GeoDistance(field, filterDescriptor);
    }
    public static BaseFilter GeoDistanceRange(Expression<Func<T, object>> fieldDescriptor, Action<GeoDistanceRangeFilterDescriptor> filterDescriptor)
    {
      return new FilterDescriptor<T>().GeoDistanceRange(fieldDescriptor, filterDescriptor);
    }
    public static BaseFilter GeoDistanceRange(string field, Action<GeoDistanceRangeFilterDescriptor> filterDescriptor)
    {
      return new FilterDescriptor<T>().GeoDistanceRange(field, filterDescriptor);
    }
    public static BaseFilter GeoPolygon(Expression<Func<T, object>> fieldDescriptor, params string[] points)
    {
      return new FilterDescriptor<T>().GeoPolygon(fieldDescriptor, points);
    }
    public static BaseFilter GeoPolygon(Expression<Func<T, object>> fieldDescriptor, IEnumerable<Tuple<double, double>> points)
    {
      return new FilterDescriptor<T>().GeoPolygon(fieldDescriptor, points);
    }
    public static BaseFilter GeoPolygon(string field, IEnumerable<Tuple<double, double>> points)
    {
      return new FilterDescriptor<T>().GeoPolygon(field, points);
    }
    public static BaseFilter GeoPolygon(string fieldName, params string[] points)
    {
      return new FilterDescriptor<T>().GeoPolygon(fieldName, points);
    }
    public static BaseFilter HasChild<K>(Action<HasChildFilterDescriptor<K>> querySelector) where K : class
    {
      return new FilterDescriptor<T>().HasChild<K>(querySelector);
    }
    public static BaseFilter Ids(IEnumerable<string> types, IEnumerable<string> values)
    {
      return new FilterDescriptor<T>().Ids(types, values);
    }
    public static BaseFilter Ids(IEnumerable<string> values)
    {
      return new FilterDescriptor<T>().Ids(values);
    }
    public static BaseFilter Ids(string type, IEnumerable<string> values)
    {
      return new FilterDescriptor<T>().Ids(type, values);
    }
    public static BaseFilter Limit(int limit)
    {
      return new FilterDescriptor<T>().Limit(limit);
    }
    public static BaseFilter MatchAll()
    {
      return new FilterDescriptor<T>().MatchAll();
    }
    public static BaseFilter Missing(Expression<Func<T, object>> fieldDescriptor)
    {
      return new FilterDescriptor<T>().Missing(fieldDescriptor);
    }
    public static BaseFilter Missing(string field)
    {
      return new FilterDescriptor<T>().Missing(field);
    }
    public static BaseFilter Nested(Action<NestedFilterDescriptor<T>> selector)
    {
      return new FilterDescriptor<T>().Nested(selector);
    }
    public static BaseFilter Not(Func<FilterDescriptor<T>, BaseFilter> selector)
    {
      return new FilterDescriptor<T>().Not(selector);
    }
    public static BaseFilter NumericRange(Action<NumericRangeFilterDescriptor<T>> numericRangeSelector)
    {
      return new FilterDescriptor<T>().NumericRange(numericRangeSelector);
    }
    public static BaseFilter Or(params Func<FilterDescriptor<T>, BaseFilter>[] filters)
    {
      return new FilterDescriptor<T>().Or(filters);
    }
    public static BaseFilter Prefix(Expression<Func<T, object>> fieldDescriptor, string prefix)
    {
      return new FilterDescriptor<T>().Prefix(fieldDescriptor, prefix);
    }
    public static BaseFilter Prefix(string field, string prefix)
    {
      return new FilterDescriptor<T>().Prefix(field, prefix);
    }
    public static BaseFilter Query(Func<QueryDescriptor<T>, BaseQuery> querySelector)
    {
      return new FilterDescriptor<T>().Query(querySelector);
    }
    public static BaseFilter Range(Action<RangeFilterDescriptor<T>> rangeSelector)
    {
      return new FilterDescriptor<T>().Range(rangeSelector);
    }
    public static BaseFilter Script(Action<ScriptFilterDescriptor> scriptSelector)
    {
      return new FilterDescriptor<T>().Script(scriptSelector);
    }
    public static BaseFilter Term(Expression<Func<T, object>> fieldDescriptor, string term)
    {
      return new FilterDescriptor<T>().Term(fieldDescriptor, term);
    }
    public static BaseFilter Term(string field, string term)
    {
      return new FilterDescriptor<T>().Term(field, term);
    }
    public static BaseFilter Terms(Expression<Func<T, object>> fieldDescriptor, IEnumerable<string> terms, TermsExecution? Execution = null)
    {
      return new FilterDescriptor<T>().Terms(fieldDescriptor, terms, Execution);
    }
    public static BaseFilter Terms(string field, IEnumerable<string> terms, TermsExecution? Execution = null)
    {
      return new FilterDescriptor<T>().Terms(field, terms, Execution);
    }
    public static BaseFilter Type(string type)
    {
      return new FilterDescriptor<T>().Type(type);
    }
  }


  public static class Filter
  {

    public static BaseFilter And(params Func<FilterDescriptor<dynamic>, BaseFilter>[] filters)
    {
      return new FilterDescriptor<dynamic>().And(filters);
    }
    public static BaseFilter Bool(Action<BoolFilterDescriptor<dynamic>> booleanFilter)
    {
      return new FilterDescriptor<dynamic>().Bool(booleanFilter);
    }
    public static BaseFilter Exists(Expression<Func<dynamic, object>> fieldDescriptor)
    {
      return new FilterDescriptor<dynamic>().Exists(fieldDescriptor);
    }
    public static BaseFilter Exists(string field)
    {
      return new FilterDescriptor<dynamic>().Exists(field);
    }
    public static BaseFilter GeoBoundingBox(Expression<Func<dynamic, object>> fieldDescriptor, double topLeftX, double topLeftY, double bottomRightX, double bottomRightY, GeoExecution? Type = null)
    {
      return new FilterDescriptor<dynamic>().GeoBoundingBox(fieldDescriptor, topLeftX, topLeftY, bottomRightX, bottomRightY, Type);
    }
    public static BaseFilter GeoBoundingBox(Expression<Func<dynamic, object>> fieldDescriptor, string geoHashTopLeft, string geoHashBottomRight, GeoExecution? Type = null)
    {
      return new FilterDescriptor<dynamic>().GeoBoundingBox(fieldDescriptor, geoHashTopLeft, geoHashBottomRight, Type);
    }
    public static BaseFilter GeoBoundingBox(string fieldName, double topLeftX, double topLeftY, double bottomRightX, double bottomRightY, GeoExecution? Type = null)
    {
      return new FilterDescriptor<dynamic>().GeoBoundingBox(fieldName, topLeftX, topLeftY, bottomRightX, bottomRightY, Type);
    }
    public static BaseFilter GeoBoundingBox(string fieldName, string geoHashTopLeft, string geoHashBottomRight, GeoExecution? Type = null)
    {
      return new FilterDescriptor<dynamic>().GeoBoundingBox(fieldName, geoHashTopLeft, geoHashBottomRight, Type);
    }
    public static BaseFilter GeoDistance(Expression<Func<dynamic, object>> fieldDescriptor, Action<GeoDistanceFilterDescriptor> filterDescriptor)
    {
      return new FilterDescriptor<dynamic>().GeoDistance(fieldDescriptor, filterDescriptor);
    }
    public static BaseFilter GeoDistance(string field, Action<GeoDistanceFilterDescriptor> filterDescriptor)
    {
      return new FilterDescriptor<dynamic>().GeoDistance(field, filterDescriptor);
    }
    public static BaseFilter GeoDistanceRange(Expression<Func<dynamic, object>> fieldDescriptor, Action<GeoDistanceRangeFilterDescriptor> filterDescriptor)
    {
      return new FilterDescriptor<dynamic>().GeoDistanceRange(fieldDescriptor, filterDescriptor);
    }
    public static BaseFilter GeoDistanceRange(string field, Action<GeoDistanceRangeFilterDescriptor> filterDescriptor)
    {
      return new FilterDescriptor<dynamic>().GeoDistanceRange(field, filterDescriptor);
    }
    public static BaseFilter GeoPolygon(Expression<Func<dynamic, object>> fieldDescriptor, params string[] points)
    {
      return new FilterDescriptor<dynamic>().GeoPolygon(fieldDescriptor, points);
    }
    public static BaseFilter GeoPolygon(Expression<Func<dynamic, object>> fieldDescriptor, IEnumerable<Tuple<double, double>> points)
    {
      return new FilterDescriptor<dynamic>().GeoPolygon(fieldDescriptor, points);
    }
    public static BaseFilter GeoPolygon(string field, IEnumerable<Tuple<double, double>> points)
    {
      return new FilterDescriptor<dynamic>().GeoPolygon(field, points);
    }
    public static BaseFilter GeoPolygon(string fieldName, params string[] points)
    {
      return new FilterDescriptor<dynamic>().GeoPolygon(fieldName, points);
    }
    public static BaseFilter HasChild<K>(Action<HasChildFilterDescriptor<K>> querySelector) where K : class
    {
      return new FilterDescriptor<dynamic>().HasChild<K>(querySelector);
    }
    public static BaseFilter Ids(IEnumerable<string> types, IEnumerable<string> values)
    {
      return new FilterDescriptor<dynamic>().Ids(types, values);
    }
    public static BaseFilter Ids(IEnumerable<string> values)
    {
      return new FilterDescriptor<dynamic>().Ids(values);
    }
    public static BaseFilter Ids(string type, IEnumerable<string> values)
    {
      return new FilterDescriptor<dynamic>().Ids(type, values);
    }
    public static BaseFilter Limit(int limit)
    {
      return new FilterDescriptor<dynamic>().Limit(limit);
    }
    public static BaseFilter MatchAll()
    {
      return new FilterDescriptor<dynamic>().MatchAll();
    }
    public static BaseFilter Missing(Expression<Func<dynamic, object>> fieldDescriptor)
    {
      return new FilterDescriptor<dynamic>().Missing(fieldDescriptor);
    }
    public static BaseFilter Missing(string field)
    {
      return new FilterDescriptor<dynamic>().Missing(field);
    }
    public static BaseFilter Nested(Action<NestedFilterDescriptor<dynamic>> selector)
    {
      return new FilterDescriptor<dynamic>().Nested(selector);
    }
    public static BaseFilter Not(Func<FilterDescriptor<dynamic>, BaseFilter> selector)
    {
      return new FilterDescriptor<dynamic>().Not(selector);
    }
    public static BaseFilter NumericRange(Action<NumericRangeFilterDescriptor<dynamic>> numericRangeSelector)
    {
      return new FilterDescriptor<dynamic>().NumericRange(numericRangeSelector);
    }
    public static BaseFilter Or(params Func<FilterDescriptor<dynamic>, BaseFilter>[] filters)
    {
      return new FilterDescriptor<dynamic>().Or(filters);
    }
    public static BaseFilter Prefix(Expression<Func<dynamic, object>> fieldDescriptor, string prefix)
    {
      return new FilterDescriptor<dynamic>().Prefix(fieldDescriptor, prefix);
    }
    public static BaseFilter Prefix(string field, string prefix)
    {
      return new FilterDescriptor<dynamic>().Prefix(field, prefix);
    }
    public static BaseFilter Query(Func<QueryDescriptor<dynamic>, BaseQuery> querySelector)
    {
      return new FilterDescriptor<dynamic>().Query(querySelector);
    }
    public static BaseFilter Range(Action<RangeFilterDescriptor<dynamic>> rangeSelector)
    {
      return new FilterDescriptor<dynamic>().Range(rangeSelector);
    }
    public static BaseFilter Script(Action<ScriptFilterDescriptor> scriptSelector)
    {
      return new FilterDescriptor<dynamic>().Script(scriptSelector);
    }
    public static BaseFilter Term(Expression<Func<dynamic, object>> fieldDescriptor, string term)
    {
      return new FilterDescriptor<dynamic>().Term(fieldDescriptor, term);
    }
    public static BaseFilter Term(string field, string term)
    {
      return new FilterDescriptor<dynamic>().Term(field, term);
    }
    public static BaseFilter Terms(Expression<Func<dynamic, object>> fieldDescriptor, IEnumerable<string> terms, TermsExecution? Execution = null)
    {
      return new FilterDescriptor<dynamic>().Terms(fieldDescriptor, terms, Execution);
    }
    public static BaseFilter Terms(string field, IEnumerable<string> terms, TermsExecution? Execution = null)
    {
      return new FilterDescriptor<dynamic>().Terms(field, terms, Execution);
    }
    public static BaseFilter Type(string type)
    {
      return new FilterDescriptor<dynamic>().Type(type);
    }

  }
}
