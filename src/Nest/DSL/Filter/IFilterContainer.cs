using System;
using System.Collections.Generic;
using Nest.DSL.Visitor;
using Nest.Resolvers.Converters;
using Nest.Resolvers.Converters.Filters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<FilterContainer>))]
	public interface IFilterContainer 
	{
		[JsonIgnore]
		string FilterName { get; set; }
		[JsonIgnore]
		string CacheKey { get; set; }
		[JsonIgnore]
		bool? Cache { get; set; }

		[JsonIgnore]
		bool IsConditionless { get; set; }

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
		[JsonConverter(typeof(GeoBoundingFilterConverter))]
		IGeoBoundingBoxFilter GeoBoundingBox { get; set; }

		[JsonProperty(PropertyName = "geo_distance")]
		[JsonConverter(typeof(GeoDistanceFilterConverter))]
		IGeoDistanceFilter GeoDistance { get; set; }

        [JsonProperty(PropertyName = "geohash_cell")]
        [JsonConverter(typeof(GeoHashCellFilterConverter))]
        IGeoHashCellFilter GeoHashCell { get; set; }

		[JsonProperty(PropertyName = "geo_distance_range")]
		[JsonConverter(typeof(GeoDistanceRangeFilterConverter))]
		IGeoDistanceRangeFilter GeoDistanceRange { get; set; }

		[JsonProperty(PropertyName = "geo_polygon")]
		[JsonConverter(typeof(CompositeJsonConverter<GeoPolygonFilterJsonReader, FieldNameFilterConverter<GeoPolygonFilter>>))]
		IGeoPolygonFilter GeoPolygon { get; set; }

		[JsonProperty(PropertyName = "geo_shape")]
		[JsonConverter(typeof(CompositeJsonConverter<GeoShapeFilterJsonReader, FieldNameFilterConverter<GeoPolygonFilter>>))]
		IGeoShapeBaseFilter GeoShape { get; set; }

		[JsonProperty(PropertyName = "limit")]
		ILimitFilter Limit { get; set; }

		[JsonProperty(PropertyName = "indices")]
		IIndicesFilter Indices { get; set; }

		[JsonProperty(PropertyName = "type")]
		ITypeFilter Type { get; set; }

		[JsonProperty(PropertyName = "match_all")]
		IMatchAllFilter MatchAll { get; set; }

		[JsonProperty(PropertyName = "has_child")]
		IHasChildFilter HasChild { get; set; }

		[JsonProperty(PropertyName = "has_parent")]
		IHasParentFilter HasParent { get; set; }

		[JsonProperty(PropertyName = "range")]
		[JsonConverter(typeof(FieldNameFilterConverter<RangeFilter>))]
		IRangeFilter Range { get; set; }

		[JsonProperty(PropertyName = "prefix")]
		[JsonConverter(typeof(PrefixFilterConverter))]
		IPrefixFilter Prefix { get; set; }

		[JsonProperty(PropertyName = "term")]
		[JsonConverter(typeof (TermFilterConverter))]
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
		INestedFilter Nested { get; set; }

		[JsonProperty(PropertyName = "regexp")]
		[JsonConverter(typeof(FieldNameFilterConverter<RegexpFilter>))]
		IRegexpFilter Regexp { get; set; }

		void Accept(IQueryVisitor visitor);
	}
}
