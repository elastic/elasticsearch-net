using System;
using System.Collections.Generic;
using System.Linq.Expressions;
namespace Nest
{
	interface IFilterDescriptor<T>
	 where T : class
	{
		BaseFilterDescriptor And(params Func<FilterDescriptorDescriptor<T>, BaseFilterDescriptor>[] filters);
		BaseFilterDescriptor Bool(Action<BoolFilterDescriptor<T>> booleanFilter);
		BaseFilterDescriptor Exists(Expression<Func<T, object>> fieldDescriptor);
		BaseFilterDescriptor Exists(string field);
		BaseFilterDescriptor GeoBoundingBox(Expression<Func<T, object>> fieldDescriptor, double topLeftX, double topLeftY, double bottomRightX, double bottomRightY, GeoExecution? type = null);
		BaseFilterDescriptor GeoBoundingBox(Expression<Func<T, object>> fieldDescriptor, string geoHashTopLeft, string geoHashBottomRight, GeoExecution? type = null);
		BaseFilterDescriptor GeoBoundingBox(string fieldName, double topLeftX, double topLeftY, double bottomRightX, double bottomRightY, GeoExecution? type = null);
		BaseFilterDescriptor GeoBoundingBox(string fieldName, string geoHashTopLeft, string geoHashBottomRight, GeoExecution? type = null);
		BaseFilterDescriptor GeoDistance(Expression<Func<T, object>> fieldDescriptor, Action<GeoDistanceFilterDescriptor> filterDescriptor);
		BaseFilterDescriptor GeoDistance(string field, Action<GeoDistanceFilterDescriptor> filterDescriptor);
		BaseFilterDescriptor GeoDistanceRange(Expression<Func<T, object>> fieldDescriptor, Action<GeoDistanceRangeFilterDescriptor> filterDescriptor);
		BaseFilterDescriptor GeoDistanceRange(string field, Action<GeoDistanceRangeFilterDescriptor> filterDescriptor);
		BaseFilterDescriptor GeoPolygon(Expression<Func<T, object>> fieldDescriptor, params string[] points);
		BaseFilterDescriptor GeoPolygon(Expression<Func<T, object>> fieldDescriptor, IEnumerable<Tuple<double, double>> points);
		BaseFilterDescriptor GeoPolygon(string field, IEnumerable<Tuple<double, double>> points);
		BaseFilterDescriptor GeoPolygon(string fieldName, params string[] points);
		BaseFilterDescriptor HasChild<K>(Action<HasChildFilterDescriptor<K>> querySelector) where K : class;
		BaseFilterDescriptor HasParent<K>(Action<HasParentFilterDescriptor<K>> querySelector) where K : class;
		BaseFilterDescriptor Ids(IEnumerable<string> types, IEnumerable<string> values);
		BaseFilterDescriptor Ids(IEnumerable<string> values);
		BaseFilterDescriptor Ids(string type, IEnumerable<string> values);
		BaseFilterDescriptor Limit(int? limit);
		BaseFilterDescriptor MatchAll();
		BaseFilterDescriptor Missing(Expression<Func<T, object>> fieldDescriptor);
		BaseFilterDescriptor Missing(string field);
		FilterDescriptorDescriptor<T> Name(string name);
		BaseFilterDescriptor Nested(Action<NestedFilterDescriptor<T>> selector);
		BaseFilterDescriptor Not(Func<FilterDescriptorDescriptor<T>, BaseFilterDescriptor> selector);
		BaseFilterDescriptor NumericRange(Action<NumericRangeFilterDescriptor<T>> numericRangeSelector);
		BaseFilterDescriptor Or(params Func<FilterDescriptorDescriptor<T>, BaseFilterDescriptor>[] filters);
		BaseFilterDescriptor Prefix(Expression<Func<T, object>> fieldDescriptor, string prefix);
		BaseFilterDescriptor Prefix(string field, string prefix);
		BaseFilterDescriptor Query(Func<QueryDescriptor<T>, BaseQuery> querySelector);
		BaseFilterDescriptor Range(Action<RangeFilterDescriptor<T>> rangeSelector);
		BaseFilterDescriptor Script(Action<ScriptFilterDescriptor> scriptSelector);
		BaseFilterDescriptor Term<K>(Expression<Func<T, K>> fieldDescriptor, K term);
		BaseFilterDescriptor Term(Expression<Func<T, object>> fieldDescriptor, object term);
		BaseFilterDescriptor Term(string field, object term);
		BaseFilterDescriptor Terms<K>(Expression<Func<T, K>> fieldDescriptor, IEnumerable<K> terms, TermsExecution? Execution = null);
		BaseFilterDescriptor Terms(Expression<Func<T, object>> fieldDescriptor, IEnumerable<string> terms, TermsExecution? Execution = null);
		BaseFilterDescriptor Terms(string field, IEnumerable<string> terms, TermsExecution? Execution = null);
		BaseFilterDescriptor Type(string type);
	}
}
