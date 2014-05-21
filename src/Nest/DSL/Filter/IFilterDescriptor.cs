using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Nest.DSL.Visitor;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<BaseFilterDescriptor>))]
	public interface IFilterDescriptor 
	{
		[JsonIgnore]
		string _Name { get; set; }
		[JsonIgnore]
		string _CacheKey { get; set; }
		[JsonIgnore]
		bool? _Cache { get; set; }

		[JsonIgnore]
		bool IsConditionless { get; }

		[JsonIgnore]
		string RawFilter { get; set; }

		[JsonIgnore]
		bool IsStrict { get; set; }

		[JsonIgnore]
		bool IsVerbatim { get; set; }

		[JsonProperty(PropertyName = "bool")]
		IBoolFilter Bool { get; set; }

		[JsonProperty(PropertyName = "exists")]
		IExistsFilter Exists { get; set; }

		[JsonProperty(PropertyName = "missing")]
		IMissingFilter Missing { get; set; }

		[JsonProperty(PropertyName = "ids")]
		IIdsFilter Ids { get; set; }

		[JsonProperty(PropertyName = "geo_bounding_box")]
		IGeoBoundingBoxFilter GeoBoundingBox { get; set; }

		[JsonProperty(PropertyName = "geo_distance")]
		IGeoDistanceFilter GeoDistance { get; set; }

		[JsonProperty(PropertyName = "geo_distance_range")]
		IGeoDistanceRangeFilter GeoDistanceRange { get; set; }

		[JsonProperty(PropertyName = "geo_polygon")]
		IGeoPolygonFilter GeoPolygon { get; set; }

		[JsonProperty(PropertyName = "geo_shape")]
		IGeoShapeBaseFilter GeoShape { get; set; }

		[JsonProperty(PropertyName = "limit")]
		ILimitFilter Limit { get; set; }

		[JsonProperty(PropertyName = "type")]
		ITypeFilter Type { get; set; }

		[JsonProperty(PropertyName = "match_all")]
		IMatchAllFilter MatchAll { get; set; }

		[JsonProperty(PropertyName = "has_child")]
		IHasChildFilter HasChild { get; set; }

		[JsonProperty(PropertyName = "has_parent")]
		IHasParentFilter HasParent { get; set; }

		[JsonProperty(PropertyName = "range")]
		IRangeFilter Range { get; set; }

		[JsonProperty(PropertyName = "prefix")]
		IPrefixFilter Prefix { get; set; }

		[JsonProperty(PropertyName = "term")]
		ITermFilter Term { get; set; }

		[JsonProperty(PropertyName = "terms")]
		ITermsBaseFilter Terms { get; set; }

		[JsonProperty(PropertyName = "fquery")]
		IQueryFilter Query { get; set; }

		[JsonProperty(PropertyName = "and")]
		IAndFilter And { get; set; }

		[JsonProperty(PropertyName = "or")]
		IOrFilter Or { get; set; }

		[JsonProperty(PropertyName = "not")]
		INotFilter Not { get; set; }

		[JsonProperty(PropertyName = "script")]
		IScriptFilter Script { get; set; }

		[JsonProperty(PropertyName = "nested")]
		INestedFilterDescriptor Nested { get; set; }

		[JsonProperty(PropertyName = "regexp")]
		IRegexpFilter Regexp { get; set; }



		void Accept(IQueryVisitor visitor);
	}

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
