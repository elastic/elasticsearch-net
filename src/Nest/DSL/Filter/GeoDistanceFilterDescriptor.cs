//using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

using Elasticsearch.Net;
using System.Globalization;
using System;

namespace Nest
{
	public class GeoDistanceFilterDescriptor : FilterBase
	{
		internal string _Location { get; set; }
		internal object _Distance { get; set; }
		internal string _GeoUnit { get; set; }
		internal string _GeoOptimizeBBox { get; set; }

		internal override bool IsConditionless
		{
			get
			{
				return this._Location.IsNullOrEmpty() || this._Distance == null;
			}

		}

		public GeoDistanceFilterDescriptor Location(double Lat, double Lon)
		{
			var c = CultureInfo.InvariantCulture;
			this._Location = "{0}, {1}".F(Lat.ToString(c), Lon.ToString(c));
			return this;
		}
		public GeoDistanceFilterDescriptor Location(string geoHash)
		{
			this._Location = geoHash;
			return this;
		}
		public GeoDistanceFilterDescriptor Distance(string distance)
		{
			this._Distance = distance;
			return this;
		}
		public GeoDistanceFilterDescriptor Distance(double distance, GeoUnit unit)
		{
			this._Distance = distance;
			this._GeoUnit = Enum.GetName(typeof(GeoUnit), unit);
			return this;
		}
		public GeoDistanceFilterDescriptor Optimize(GeoOptimizeBBox optimize)
		{
			this._GeoOptimizeBBox = Enum.GetName(typeof(GeoOptimizeBBox), optimize);
			return this;
		}
	}
}
