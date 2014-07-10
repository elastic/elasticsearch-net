//using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Resolvers;
using Nest.Resolvers.Converters;
using Nest.Resolvers.Converters.Filters;
using Newtonsoft.Json;
using System.Globalization;
using System;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGeoDistanceFilter : IFilter
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

	public class GeoDistanceFilter : PlainFilter, IGeoDistanceFilter
	{
		protected internal override void WrapInContainer(IFilterContainer container)
		{
			container.GeoDistance = this;
		}

		public PropertyPathMarker Field { get; set; }
		public string Location { get; set; }
		public object Distance { get; set; }
		public GeoUnit? Unit { get; set; }
		public GeoOptimizeBBox? OptimizeBoundingBox { get; set; }
		public GeoDistance? DistanceType { get; set; }
	}

	public class GeoDistanceFilterDescriptor : FilterBase, IGeoDistanceFilter
	{
		PropertyPathMarker IGeoDistanceFilter.Field { get; set; }
		string IGeoDistanceFilter.Location { get; set; }
		object IGeoDistanceFilter.Distance { get; set; }
		GeoUnit? IGeoDistanceFilter.Unit { get; set; }
		GeoDistance? IGeoDistanceFilter.DistanceType { get; set; }
		GeoOptimizeBBox? IGeoDistanceFilter.OptimizeBoundingBox { get; set; }

		bool IFilter.IsConditionless
		{
			get
			{
				return ((IGeoDistanceFilter)this).Location.IsNullOrEmpty() || ((IGeoDistanceFilter)this).Distance == null;
			}
		}

		public GeoDistanceFilterDescriptor Location(double Lat, double Lon)
		{
			var c = CultureInfo.InvariantCulture;
			((IGeoDistanceFilter)this).Location = "{0}, {1}".F(Lat.ToString(c), Lon.ToString(c));
			return this;
		}
		public GeoDistanceFilterDescriptor Location(string geoHash)
		{
			((IGeoDistanceFilter)this).Location = geoHash;
			return this;
		}
		public GeoDistanceFilterDescriptor Distance(string distance)
		{
			((IGeoDistanceFilter)this).Distance = distance;
			return this;
		}
		public GeoDistanceFilterDescriptor Distance(double distance, GeoUnit unit)
		{
			((IGeoDistanceFilter)this).Distance = distance;
			((IGeoDistanceFilter)this).Unit = unit;
			return this;
		}
		public GeoDistanceFilterDescriptor Optimize(GeoOptimizeBBox optimize)
		{
			((IGeoDistanceFilter)this).OptimizeBoundingBox = optimize;
			return this;
		}
		public GeoDistanceFilterDescriptor DistanceType(GeoDistance type)
		{
			((IGeoDistanceFilter)this).DistanceType = type;
			return this;
		}
	}
}
