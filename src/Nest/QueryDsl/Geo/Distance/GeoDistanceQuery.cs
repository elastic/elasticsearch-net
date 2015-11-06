using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Globalization;
using System;
using Newtonsoft.Json.Converters;
using System.Linq.Expressions;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof (VariableFieldNameQueryJsonConverter<GeoDistanceQuery, IGeoDistanceQuery>))]
	public interface IGeoDistanceQuery : IFieldNameQuery
	{
		[VariableField]
		GeoLocation Location { get; set; }
		
		[JsonProperty("distance")]
		GeoDistance Distance { get; set; }
		
		[JsonProperty("optimize_bbox")]
		[JsonConverter(typeof(StringEnumConverter))]
		GeoOptimizeBBox? OptimizeBoundingBox { get; set; }

		[JsonProperty("distance_type")]
		[JsonConverter(typeof(StringEnumConverter))]
		GeoDistanceType? DistanceType { get; set; }
	}

	public class GeoDistanceQuery : FieldNameQueryBase, IGeoDistanceQuery
	{
		bool IQuery.Conditionless => IsConditionless(this);
		public GeoLocation Location { get; set; }
		public GeoDistance Distance { get; set; }
		public GeoOptimizeBBox? OptimizeBoundingBox { get; set; }
		public GeoDistanceType? DistanceType { get; set; }

		protected override void WrapInContainer(IQueryContainer c) => c.GeoDistance = this;

		internal static bool IsConditionless(IGeoDistanceQuery q) => q.Location == null || q.Distance == null;
	}

	public class GeoDistanceQueryDescriptor<T> 
		: FieldNameQueryDescriptorBase<GeoDistanceQueryDescriptor<T>, IGeoDistanceQuery, T> 
		, IGeoDistanceQuery where T : class
	{
		bool IQuery.Conditionless => GeoDistanceQuery.IsConditionless(this);
		GeoLocation IGeoDistanceQuery.Location { get; set; }
		GeoDistance IGeoDistanceQuery.Distance { get; set; }
		GeoDistanceType? IGeoDistanceQuery.DistanceType { get; set; }
		GeoOptimizeBBox? IGeoDistanceQuery.OptimizeBoundingBox { get; set; }

		public GeoDistanceQueryDescriptor<T> Location(double lat, double lon) => Assign(a => a.Location = new GeoLocation(lat, lon));

		public GeoDistanceQueryDescriptor<T> Distance(GeoDistance distance) => Assign(a => a.Distance = distance);

		public GeoDistanceQueryDescriptor<T> Distance(double distance, GeoPrecisionUnit unit) => Assign(a => a.Distance = new GeoDistance(distance, unit));

		public GeoDistanceQueryDescriptor<T> Optimize(GeoOptimizeBBox optimize) => Assign(a => a.OptimizeBoundingBox = optimize);

		public GeoDistanceQueryDescriptor<T> DistanceType(GeoDistanceType type) => Assign(a => a.DistanceType = type);
	}
}
