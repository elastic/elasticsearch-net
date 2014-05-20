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
		public static BaseFilterDescriptor And(params Func<FilterDescriptorDescriptor<T>, BaseFilterDescriptor>[] filters)
		{
			return new FilterDescriptorDescriptor<T>().And(filters);
		}
		public static BaseFilterDescriptor Bool(Action<BoolFilterDescriptor<T>> booleanFilter)
		{
			return new FilterDescriptorDescriptor<T>().Bool(booleanFilter);
		}
		public static BaseFilterDescriptor Exists(Expression<Func<T, object>> fieldDescriptor)
		{
			return new FilterDescriptorDescriptor<T>().Exists(fieldDescriptor);
		}
		public static BaseFilterDescriptor Exists(string field)
		{
			return new FilterDescriptorDescriptor<T>().Exists(field);
		}
		public static BaseFilterDescriptor Conditionless(Action<ConditionlessFilterDescriptor<T>> conditionlessFilter)
		{
			return new FilterDescriptorDescriptor<T>().Conditionless(conditionlessFilter);
		}
		public static BaseFilterDescriptor GeoBoundingBox(Expression<Func<T, object>> fieldDescriptor, double topLeftX, double topLeftY, double bottomRightX, double bottomRightY, GeoExecution? Type = null)
		{
			return new FilterDescriptorDescriptor<T>().GeoBoundingBox(fieldDescriptor, topLeftX, topLeftY, bottomRightX, bottomRightY, Type);
		}
		public static BaseFilterDescriptor GeoBoundingBox(Expression<Func<T, object>> fieldDescriptor, string geoHashTopLeft, string geoHashBottomRight, GeoExecution? Type = null)
		{
			return new FilterDescriptorDescriptor<T>().GeoBoundingBox(fieldDescriptor, geoHashTopLeft, geoHashBottomRight, Type);
		}
		public static BaseFilterDescriptor GeoBoundingBox(string fieldName, double topLeftX, double topLeftY, double bottomRightX, double bottomRightY, GeoExecution? Type = null)
		{
			return new FilterDescriptorDescriptor<T>().GeoBoundingBox(fieldName, topLeftX, topLeftY, bottomRightX, bottomRightY, Type);
		}
		public static BaseFilterDescriptor GeoBoundingBox(string fieldName, string geoHashTopLeft, string geoHashBottomRight, GeoExecution? Type = null)
		{
			return new FilterDescriptorDescriptor<T>().GeoBoundingBox(fieldName, geoHashTopLeft, geoHashBottomRight, Type);
		}
		public static BaseFilterDescriptor GeoDistance(Expression<Func<T, object>> fieldDescriptor, Action<GeoDistanceFilterDescriptor> filterDescriptor)
		{
			return new FilterDescriptorDescriptor<T>().GeoDistance(fieldDescriptor, filterDescriptor);
		}
		public static BaseFilterDescriptor GeoDistance(string field, Action<GeoDistanceFilterDescriptor> filterDescriptor)
		{
			return new FilterDescriptorDescriptor<T>().GeoDistance(field, filterDescriptor);
		}
		public static BaseFilterDescriptor GeoDistanceRange(Expression<Func<T, object>> fieldDescriptor, Action<GeoDistanceRangeFilterDescriptor> filterDescriptor)
		{
			return new FilterDescriptorDescriptor<T>().GeoDistanceRange(fieldDescriptor, filterDescriptor);
		}
		public static BaseFilterDescriptor GeoDistanceRange(string field, Action<GeoDistanceRangeFilterDescriptor> filterDescriptor)
		{
			return new FilterDescriptorDescriptor<T>().GeoDistanceRange(field, filterDescriptor);
		}
		public static BaseFilterDescriptor GeoPolygon(Expression<Func<T, object>> fieldDescriptor, params string[] points)
		{
			return new FilterDescriptorDescriptor<T>().GeoPolygon(fieldDescriptor, points);
		}
		public static BaseFilterDescriptor GeoPolygon(Expression<Func<T, object>> fieldDescriptor, IEnumerable<Tuple<double, double>> points)
		{
			return new FilterDescriptorDescriptor<T>().GeoPolygon(fieldDescriptor, points);
		}
		public static BaseFilterDescriptor GeoPolygon(string field, IEnumerable<Tuple<double, double>> points)
		{
			return new FilterDescriptorDescriptor<T>().GeoPolygon(field, points);
		}
		public static BaseFilterDescriptor GeoPolygon(string fieldName, params string[] points)
		{
			return new FilterDescriptorDescriptor<T>().GeoPolygon(fieldName, points);
		}
		public static BaseFilterDescriptor HasChild<K>(Action<HasChildFilterDescriptor<K>> querySelector) where K : class
		{
			return new FilterDescriptorDescriptor<T>().HasChild<K>(querySelector);
		}
		public static BaseFilterDescriptor Ids(IEnumerable<string> types, IEnumerable<string> values)
		{
			return new FilterDescriptorDescriptor<T>().Ids(types, values);
		}
		public static BaseFilterDescriptor Ids(IEnumerable<string> values)
		{
			return new FilterDescriptorDescriptor<T>().Ids(values);
		}
		public static BaseFilterDescriptor Ids(string type, IEnumerable<string> values)
		{
			return new FilterDescriptorDescriptor<T>().Ids(type, values);
		}
		public static BaseFilterDescriptor Limit(int limit)
		{
			return new FilterDescriptorDescriptor<T>().Limit(limit);
		}
		public static BaseFilterDescriptor MatchAll()
		{
			return new FilterDescriptorDescriptor<T>().MatchAll();
		}
		public static BaseFilterDescriptor Missing(Expression<Func<T, object>> fieldDescriptor)
		{
			return new FilterDescriptorDescriptor<T>().Missing(fieldDescriptor);
		}
		public static BaseFilterDescriptor Missing(string field)
		{
			return new FilterDescriptorDescriptor<T>().Missing(field);
		}
		public static BaseFilterDescriptor Nested(Action<NestedFilterDescriptor<T>> selector)
		{
			return new FilterDescriptorDescriptor<T>().Nested(selector);
		}
		public static BaseFilterDescriptor Not(Func<FilterDescriptorDescriptor<T>, BaseFilterDescriptor> selector)
		{
			return new FilterDescriptorDescriptor<T>().Not(selector);
		}
		public static BaseFilterDescriptor Or(params Func<FilterDescriptorDescriptor<T>, BaseFilterDescriptor>[] filters)
		{
			return new FilterDescriptorDescriptor<T>().Or(filters);
		}
		public static BaseFilterDescriptor Prefix(Expression<Func<T, object>> fieldDescriptor, string prefix)
		{
			return new FilterDescriptorDescriptor<T>().Prefix(fieldDescriptor, prefix);
		}
		public static BaseFilterDescriptor Prefix(string field, string prefix)
		{
			return new FilterDescriptorDescriptor<T>().Prefix(field, prefix);
		}
		public static BaseFilterDescriptor Query(Func<QueryDescriptor<T>, BaseQuery> querySelector)
		{
			return new FilterDescriptorDescriptor<T>().Query(querySelector);
		}
		public static BaseFilterDescriptor Range(Action<RangeFilterDescriptor<T>> rangeSelector)
		{
			return new FilterDescriptorDescriptor<T>().Range(rangeSelector);
		}
		public static BaseFilterDescriptor Script(Action<ScriptFilterDescriptor> scriptSelector)
		{
			return new FilterDescriptorDescriptor<T>().Script(scriptSelector);
		}
		public static BaseFilterDescriptor Term<K>(Expression<Func<T, K>> fieldDescriptor, K term)
		{
			return new FilterDescriptorDescriptor<T>().Term(fieldDescriptor, term);
		}
		public static BaseFilterDescriptor Term<K>(string field, K term)
		{
			return new FilterDescriptorDescriptor<T>().Term(field, term);
		}
		public static BaseFilterDescriptor Terms(Expression<Func<T, object>> fieldDescriptor, IEnumerable<string> terms, TermsExecution? Execution = null)
		{
			return new FilterDescriptorDescriptor<T>().Terms(fieldDescriptor, terms, Execution);
		}
		public static BaseFilterDescriptor Terms(string field, IEnumerable<string> terms, TermsExecution? Execution = null)
		{
			return new FilterDescriptorDescriptor<T>().Terms(field, terms, Execution);
		}
		public static BaseFilterDescriptor Type(string type)
		{
			return new FilterDescriptorDescriptor<T>().Type(type);
		}
		public static BaseFilterDescriptor Regexp(Action<RegexpFilterDescriptor<T>> regexpSelector)
		{
			return new FilterDescriptorDescriptor<T>().Regexp(regexpSelector);
		}
		public static FilterDescriptorDescriptor<T>  Strict(bool strict = true)
		{	
			var f = new FilterDescriptorDescriptor<T>();
			((IFilterDescriptor)f).IsStrict = true;
			return f;
		}
		public static FilterDescriptorDescriptor<T>  Verbatim(bool verbatim = true)
		{
			var f = new FilterDescriptorDescriptor<T>();
			((IFilterDescriptor)f).IsVerbatim = true;
			return f;
		}
		
	}

}
