using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Nest.DSL.Visitor;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<FilterContainer>))]
	public interface IFilterContainer 
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
}
