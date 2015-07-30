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
	public interface IGeoDistanceQuery : IFieldNameQuery
	{
		string Location { get; set; }
		
		[JsonProperty("distance")]
		object Distance { get; set; }
		
		[JsonProperty("unit")]
		[JsonConverter(typeof(StringEnumConverter))]
		GeoUnit? Unit { get; set; }
	
		[JsonProperty("optimize_bbox")]
		[JsonConverter(typeof(StringEnumConverter))]
		GeoOptimizeBBox? OptimizeBoundingBox { get; set; }

		[JsonProperty("distance_type")]
		[JsonConverter(typeof(StringEnumConverter))]
		GeoDistance? DistanceType { get; set; }
	}

	public class GeoDistanceQuery : FieldNameQueryBase, IGeoDistanceQuery
	{
		bool IQuery.Conditionless => IsConditionless(this);
		public string Location { get; set; }
		public object Distance { get; set; }
		public GeoUnit? Unit { get; set; }
		public GeoOptimizeBBox? OptimizeBoundingBox { get; set; }
		public GeoDistance? DistanceType { get; set; }

		protected override void WrapInContainer(IQueryContainer c) => c.GeoDistance = this;

		internal static bool IsConditionless(IGeoDistanceQuery q) => q.Location.IsNullOrEmpty() || q.Distance == null;
	}

	public class GeoDistanceQueryDescriptor<T> 
		: FieldNameQueryDescriptorBase<GeoDistanceQueryDescriptor<T>, IGeoDistanceQuery, T> 
		, IGeoDistanceQuery where T : class
	{
		bool IQuery.Conditionless => GeoDistanceQuery.IsConditionless(this);
		string IGeoDistanceQuery.Location { get; set; }
		object IGeoDistanceQuery.Distance { get; set; }
		GeoUnit? IGeoDistanceQuery.Unit { get; set; }
		GeoDistance? IGeoDistanceQuery.DistanceType { get; set; }
		GeoOptimizeBBox? IGeoDistanceQuery.OptimizeBoundingBox { get; set; }

		public GeoDistanceQueryDescriptor<T> Location(double Lat, double Lon) => Assign(a =>
		{
			var c = CultureInfo.InvariantCulture;
			a.Location = "{0}, {1}".F(Lat.ToString(c), Lon.ToString(c));
		});

		public GeoDistanceQueryDescriptor<T> Location(string geoHash) => Assign(a => a.Location = geoHash);

		public GeoDistanceQueryDescriptor<T> Distance(string distance) => Assign(a => a.Distance = distance);

		public GeoDistanceQueryDescriptor<T> Distance(double distance, GeoUnit unit) => Assign(a => { a.Distance = distance; a.Unit = unit; });

		public GeoDistanceQueryDescriptor<T> Optimize(GeoOptimizeBBox optimize) => Assign(a => a.OptimizeBoundingBox = optimize);

		public GeoDistanceQueryDescriptor<T> DistanceType(GeoDistance type) => Assign(a => a.DistanceType = type);
	}
}
