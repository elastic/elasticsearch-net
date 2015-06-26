using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Globalization;
using System;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGeoDistanceQuery : IQuery
	{
		PropertyPathMarker Field { get; set; }

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

	internal static class GeoDistanceCondition
	{
		public static bool IsConditionless(IGeoDistanceQuery self)
		{
			return self.Location.IsNullOrEmpty() || self.Distance == null;
		}
	}

	public class GeoDistanceFilter : PlainQuery, IGeoDistanceQuery
	{
		protected override void WrapInContainer(IQueryContainer container)
		{
			container.GeoDistance = this;
		}

		public string Name { get; set; }
		bool IQuery.IsConditionless { get { return GeoDistanceCondition.IsConditionless(this); } }

		public PropertyPathMarker Field { get; set; }
		public string Location { get; set; }
		public object Distance { get; set; }
		public GeoUnit? Unit { get; set; }
		public GeoOptimizeBBox? OptimizeBoundingBox { get; set; }
		public GeoDistance? DistanceType { get; set; }
	}

	// TODO : Finish implementing
	public class GeoDistanceQueryDescriptor : IGeoDistanceQuery
	{
		PropertyPathMarker IGeoDistanceQuery.Field { get; set; }
		string IGeoDistanceQuery.Location { get; set; }
		object IGeoDistanceQuery.Distance { get; set; }
		GeoUnit? IGeoDistanceQuery.Unit { get; set; }
		GeoDistance? IGeoDistanceQuery.DistanceType { get; set; }
		GeoOptimizeBBox? IGeoDistanceQuery.OptimizeBoundingBox { get; set; }

		string IQuery.Name { get; set; }
		bool IQuery.IsConditionless { get { return GeoDistanceCondition.IsConditionless(this); } }

		public GeoDistanceQueryDescriptor Location(double Lat, double Lon)
		{
			var c = CultureInfo.InvariantCulture;
			((IGeoDistanceQuery)this).Location = "{0}, {1}".F(Lat.ToString(c), Lon.ToString(c));
			return this;
		}
		public GeoDistanceQueryDescriptor Location(string geoHash)
		{
			((IGeoDistanceQuery)this).Location = geoHash;
			return this;
		}
		public GeoDistanceQueryDescriptor Distance(string distance)
		{
			((IGeoDistanceQuery)this).Distance = distance;
			return this;
		}
		public GeoDistanceQueryDescriptor Distance(double distance, GeoUnit unit)
		{
			((IGeoDistanceQuery)this).Distance = distance;
			((IGeoDistanceQuery)this).Unit = unit;
			return this;
		}
		public GeoDistanceQueryDescriptor Optimize(GeoOptimizeBBox optimize)
		{
			((IGeoDistanceQuery)this).OptimizeBoundingBox = optimize;
			return this;
		}
		public GeoDistanceQueryDescriptor DistanceType(GeoDistance type)
		{
			((IGeoDistanceQuery)this).DistanceType = type;
			return this;
		}
	}
}
