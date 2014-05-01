//using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Resolvers;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System.Globalization;
using System;

namespace Nest
{
	[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<GeoDistanceFilterDescriptor>, CustomJsonConverter>))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGeoDistanceFilter : IFilterBase, ICustomJson
	{
		PropertyPathMarker Field { get; set; }
		string Location { get; set; }
		
		[JsonProperty("distance")]
		object Distance { get; set; }
		
		[JsonProperty("unit")]
		string Unit { get; set; }
	
		[JsonProperty("optimize_bbox")]
		string OptimizeBoundingBox { get; set; }
	}

	public class GeoDistanceFilterDescriptor : FilterBase, IGeoDistanceFilter
	{
		PropertyPathMarker IGeoDistanceFilter.Field { get; set; }
		string IGeoDistanceFilter.Location { get; set; }
		object IGeoDistanceFilter.Distance { get; set; }
		string IGeoDistanceFilter.Unit { get; set; }
		string IGeoDistanceFilter.OptimizeBoundingBox { get; set; }

		bool IFilterBase.IsConditionless
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
			((IGeoDistanceFilter)this).Unit = Enum.GetName(typeof(GeoUnit), unit);
			return this;
		}
		public GeoDistanceFilterDescriptor Optimize(GeoOptimizeBBox optimize)
		{
			((IGeoDistanceFilter)this).OptimizeBoundingBox = Enum.GetName(typeof(GeoOptimizeBBox), optimize);
			return this;
		}
		
		object ICustomJson.GetCustomJson()
		{
			var gbf = (IGeoDistanceFilter)this;
			var dict = this.FieldNameAsKeyFormat(gbf.Field, gbf.Location, d => d
				.Add("distance", gbf.Distance)
				.Add("unit", gbf.Unit)
				.Add("optimize_bbox", gbf.OptimizeBoundingBox)
			);
			return dict;
		}
	
	}
}
