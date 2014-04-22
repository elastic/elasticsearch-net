//using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Resolvers;
using Newtonsoft.Json;
using System.Globalization;
using System;

namespace Nest
{
	public interface IGeoDistanceFilter : IFilterBase
	{
		PropertyPathMarker Field { get; set; }
		string _Location { get; set; }
		
		[JsonProperty("distance")]
		object _Distance { get; set; }
		
		[JsonProperty("unit")]
		string _GeoUnit { get; set; }
	
		[JsonProperty("optimize_bbox")]
		string _GeoOptimizeBBox { get; set; }
	}

	public class GeoDistanceFilterDescriptor : FilterBase, IGeoDistanceFilter
	{
		PropertyPathMarker IGeoDistanceFilter.Field { get; set; }
		string IGeoDistanceFilter._Location { get; set; }
		object IGeoDistanceFilter._Distance { get; set; }
		string IGeoDistanceFilter._GeoUnit { get; set; }
		string IGeoDistanceFilter._GeoOptimizeBBox { get; set; }

		bool IFilterBase.IsConditionless
		{
			get
			{
				return ((IGeoDistanceFilter)this)._Location.IsNullOrEmpty() || ((IGeoDistanceFilter)this)._Distance == null;
			}
		}

		public GeoDistanceFilterDescriptor Location(double Lat, double Lon)
		{
			var c = CultureInfo.InvariantCulture;
			((IGeoDistanceFilter)this)._Location = "{0}, {1}".F(Lat.ToString(c), Lon.ToString(c));
			return this;
		}
		public GeoDistanceFilterDescriptor Location(string geoHash)
		{
			((IGeoDistanceFilter)this)._Location = geoHash;
			return this;
		}
		public GeoDistanceFilterDescriptor Distance(string distance)
		{
			((IGeoDistanceFilter)this)._Distance = distance;
			return this;
		}
		public GeoDistanceFilterDescriptor Distance(double distance, GeoUnit unit)
		{
			((IGeoDistanceFilter)this)._Distance = distance;
			((IGeoDistanceFilter)this)._GeoUnit = Enum.GetName(typeof(GeoUnit), unit);
			return this;
		}
		public GeoDistanceFilterDescriptor Optimize(GeoOptimizeBBox optimize)
		{
			((IGeoDistanceFilter)this)._GeoOptimizeBBox = Enum.GetName(typeof(GeoOptimizeBBox), optimize);
			return this;
		}
	}
}
