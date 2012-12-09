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

		internal override bool IsConditionless
		{
			get
			{
				return this._Location.IsNullOrEmpty() || 
					(this._FromDistance == null && this._ToDistance == null)
					|| (((string)this._FromDistance).IsNullOrEmpty() 
						&& ((string)this._ToDistance).IsNullOrEmpty() 
					    );
			}

		}


		public GeoDistanceRangeFilterDescriptor Location(double X, double Y)
		{
			var c = CultureInfo.InvariantCulture;
			this._Location = "{0}, {1}".F(X.ToString(c), Y.ToString(c));
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
			return this;
		}
		public GeoDistanceRangeFilterDescriptor Distance(double From, double To, GeoUnit Unit)
		{
			this._FromDistance = From;
			this._ToDistance = To;
			this._GeoUnit = Enum.GetName(typeof(GeoUnit), Unit);
			return this;
		}
		public GeoDistanceRangeFilterDescriptor Optimize(GeoOptimizeBBox optimize)
		{
			this._GeoOptimizeBBox = Enum.GetName(typeof(GeoOptimizeBBox), optimize);
			return this;
		}
	}
}
