using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Nest.DSL;

namespace Nest
{
	public class GeoDistanceRangeFilterDescriptor : FilterBase
	{
		internal string _Location { get; set;}
		internal object _FromDistance { get; set; }
		internal object _ToDistance { get; set; }
		internal string _GeoUnit { get; set; }
		internal string _GeoOptimizeBBox { get; set; }

		public GeoDistanceRangeFilterDescriptor Location(double X, double Y)
		{
			this._Location = "{0}, {1}".F(X, Y);
			return this;
		}
		public GeoDistanceRangeFilterDescriptor Location(string geoHash)
		{
			this._Location = geoHash;
			return this;
		}
		public GeoDistanceRangeFilterDescriptor Distance(string From, string To)
		{
			From.ThrowIfNullOrEmpty("From");
			To.ThrowIfNullOrEmpty("To");
			this._FromDistance = From;
			this._ToDistance = To;
			return this;
		}
		public GeoDistanceRangeFilterDescriptor Distance(double From, double To, GeoUnit Unit)
		{
			From.ThrowIfNull("From");
			To.ThrowIfNull("To");
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
