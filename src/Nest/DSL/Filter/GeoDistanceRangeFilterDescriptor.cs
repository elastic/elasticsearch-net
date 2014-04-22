using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Resolvers;
using Newtonsoft.Json;
using System.Globalization;
using Elasticsearch.Net;

namespace Nest
{
	public interface IGeoDistanceRangeFilter : IFilterBase
	{
		PropertyPathMarker Field { get; set; }
		string _Location { get; set; }
		object _FromDistance { get; set; }
		object _ToDistance { get; set; }
		string _GeoUnit { get; set; }
		string _GeoOptimizeBBox { get; set; }
	}

	public class GeoDistanceRangeFilterDescriptor : FilterBase, IGeoDistanceRangeFilter
	{
		PropertyPathMarker IGeoDistanceRangeFilter.Field { get; set; }
		string IGeoDistanceRangeFilter._Location { get; set; }
		object IGeoDistanceRangeFilter._FromDistance { get; set; }
		object IGeoDistanceRangeFilter._ToDistance { get; set; }
		string IGeoDistanceRangeFilter._GeoUnit { get; set; }
		string IGeoDistanceRangeFilter._GeoOptimizeBBox { get; set; }

		private bool IsValidDistance { get; set; }

		bool IFilterBase.IsConditionless
		{
			get
			{
				return ((IGeoDistanceRangeFilter)this)._Location.IsNullOrEmpty() || !this.IsValidDistance;
			}

		}


		public GeoDistanceRangeFilterDescriptor Location(double Lat, double Lon)
		{
			var c = CultureInfo.InvariantCulture;
			((IGeoDistanceRangeFilter)this)._Location = "{0}, {1}".F(Lat.ToString(c), Lon.ToString(c));
			return this;
		}
		public GeoDistanceRangeFilterDescriptor Location(string geoHash)
		{
			((IGeoDistanceRangeFilter)this)._Location = geoHash;
			return this;
		}
		public GeoDistanceRangeFilterDescriptor Distance(string From, string To)
		{
			((IGeoDistanceRangeFilter)this)._FromDistance = From;
			((IGeoDistanceRangeFilter)this)._ToDistance = To;
			this.IsValidDistance = !From.IsNullOrEmpty() && !To.IsNullOrEmpty();
			return this;
		}
		public GeoDistanceRangeFilterDescriptor Distance(double From, double To, GeoUnit Unit)
		{
			((IGeoDistanceRangeFilter)this)._FromDistance = From;
			((IGeoDistanceRangeFilter)this)._ToDistance = To;
			((IGeoDistanceRangeFilter)this)._GeoUnit = Enum.GetName(typeof(GeoUnit), Unit);
			this.IsValidDistance = From != null && To != null;
			return this;
		}
		public GeoDistanceRangeFilterDescriptor Optimize(GeoOptimizeBBox optimize)
		{
			((IGeoDistanceRangeFilter)this)._GeoOptimizeBBox = Enum.GetName(typeof(GeoOptimizeBBox), optimize);
			return this;
		}
	}
}
