using System;
using System.Collections.Generic;
using System.Linq.Expressions;
namespace Nest
{
	interface IFilterDescriptor<T>
	 where T : class
	{
		BaseFilter And(params Func<FilterDescriptor<T>, BaseFilter>[] filters);
		BaseFilter Bool(Action<BoolFilterDescriptor<T>> booleanFilter);
		BaseFilter Exists(Expression<Func<T, object>> fieldDescriptor);
		BaseFilter Exists(string field);
		BaseFilter GeoBoundingBox(Expression<Func<T, object>> fieldDescriptor, double topLeftX, double topLeftY, double bottomRightX, double bottomRightY, GeoExecution? Type = null);
		BaseFilter GeoBoundingBox(Expression<Func<T, object>> fieldDescriptor, string geoHashTopLeft, string geoHashBottomRight, GeoExecution? Type = null);
		BaseFilter GeoBoundingBox(string fieldName, double topLeftX, double topLeftY, double bottomRightX, double bottomRightY, GeoExecution? Type = null);
		BaseFilter GeoBoundingBox(string fieldName, string geoHashTopLeft, string geoHashBottomRight, GeoExecution? Type = null);
		BaseFilter GeoDistance(Expression<Func<T, object>> fieldDescriptor, Action<GeoDistanceFilterDescriptor> filterDescriptor);
		BaseFilter GeoDistance(string field, Action<GeoDistanceFilterDescriptor> filterDescriptor);
		BaseFilter GeoDistanceRange(Expression<Func<T, object>> fieldDescriptor, Action<GeoDistanceRangeFilterDescriptor> filterDescriptor);
		BaseFilter GeoDistanceRange(string field, Action<GeoDistanceRangeFilterDescriptor> filterDescriptor);
		BaseFilter GeoPolygon(Expression<Func<T, object>> fieldDescriptor, params string[] points);
		BaseFilter GeoPolygon(Expression<Func<T, object>> fieldDescriptor, IEnumerable<Tuple<double, double>> points);
		BaseFilter GeoPolygon(string field, IEnumerable<Tuple<double, double>> points);
		BaseFilter GeoPolygon(string fieldName, params string[] points);
		BaseFilter HasChild<K>(Action<HasChildFilterDescriptor<K>> querySelector) where K : class;
		BaseFilter Ids(IEnumerable<string> types, IEnumerable<string> values);
		BaseFilter Ids(IEnumerable<string> values);
		BaseFilter Ids(string type, IEnumerable<string> values);
		BaseFilter Limit(int? limit);
		BaseFilter MatchAll();
		BaseFilter Missing(Expression<Func<T, object>> fieldDescriptor);
		BaseFilter Missing(string field);
		FilterDescriptor<T> Name(string name);
		BaseFilter Nested(Action<NestedFilterDescriptor<T>> selector);
		BaseFilter Not(Func<FilterDescriptor<T>, BaseFilter> selector);
		BaseFilter NumericRange(Action<NumericRangeFilterDescriptor<T>> numericRangeSelector);
		BaseFilter Or(params Func<FilterDescriptor<T>, BaseFilter>[] filters);
		BaseFilter Prefix(Expression<Func<T, object>> fieldDescriptor, string prefix);
		BaseFilter Prefix(string field, string prefix);
		BaseFilter Query(Func<QueryDescriptor<T>, BaseQuery> querySelector);
		BaseFilter Range(Action<RangeFilterDescriptor<T>> rangeSelector);
		BaseFilter Script(Action<ScriptFilterDescriptor> scriptSelector);
		BaseFilter Term(Expression<Func<T, object>> fieldDescriptor, string term);
		BaseFilter Term(string field, string term);
		BaseFilter Terms(Expression<Func<T, object>> fieldDescriptor, IEnumerable<string> terms, TermsExecution? Execution = null);
		BaseFilter Terms(string field, IEnumerable<string> terms, TermsExecution? Execution = null);
		BaseFilter Type(string type);
	}
}
