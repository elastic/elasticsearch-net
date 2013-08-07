using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Globalization;

namespace Nest
{
	public class GeoDistanceRangeFilterDescriptor : FilterBase
	{
		internal string _Location { get; set; }
		internal object _FromDistance { get; set; }
		internal object _ToDistance { get; set; }
		internal string _GeoUnit { get; set; }
		internal string _GeoOptimizeBBox { get; set; }

		private bool IsValidDistance { get; set; }

		internal override bool IsConditionless
		{
			get
			{
				return this._Location.IsNullOrEmpty() || !this.IsValidDistance;
			}

		}


		public GeoDistanceRangeFilterDescriptor Location(double Lat, double Lon)
		{
			var c = CultureInfo.InvariantCulture;
			this._Location = "{0}, {1}".F(Lat.ToString(c), Lon.ToString(c));
			return this;
		}
		public GeoDistanceRangeFilterDescriptor Location(string geoHash)
		{
			this._Location = geoHash;
			return this;
		}
		public GeoDistanceRangeFilterDescriptor Distance(string From, string To)
		{
			this._FromDistance = From;
			this._ToDistance = To;
			this.IsValidDistance = !From.IsNullOrEmpty() && !To.IsNullOrEmpty();
			return this;
		}
		public GeoDistanceRangeFilterDescriptor Distance(double From, double To, GeoUnit Unit)
		{
			this._FromDistance = From;
			this._ToDistance = To;
			this._GeoUnit = Enum.GetName(typeof(GeoUnit), Unit);
			this.IsValidDistance = From != null && To != null;
			return this;
		}
		public GeoDistanceRangeFilterDescriptor Optimize(GeoOptimizeBBox optimize)
		{
			this._GeoOptimizeBBox = Enum.GetName(typeof(GeoOptimizeBBox), optimize);
			return this;
		}
	}
}
