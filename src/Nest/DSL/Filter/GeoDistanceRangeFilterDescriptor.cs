using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Resolvers;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System.Globalization;
using Elasticsearch.Net;

namespace Nest
{
	[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<GeoDistanceRangeFilterDescriptor>, CustomJsonConverter>))]
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGeoDistanceRangeFilter : IFilterBase, ICustomJson
	{
		PropertyPathMarker Field { get; set; }
		string Location { get; set; }

		[JsonProperty("from")]
		object From { get; set; }
		[JsonProperty("to")]
		object To { get; set; }
		[JsonProperty("distance_type")]
		string DistanceType { get; set; }
		[JsonProperty("optimize_bbox")]
		string OptimizeBoundingBox { get; set; }
	}

	public class GeoDistanceRangeFilterDescriptor : FilterBase, IGeoDistanceRangeFilter
	{
		PropertyPathMarker IGeoDistanceRangeFilter.Field { get; set; }
		string IGeoDistanceRangeFilter.Location { get; set; }
		object IGeoDistanceRangeFilter.From { get; set; }
		object IGeoDistanceRangeFilter.To { get; set; }
		string IGeoDistanceRangeFilter.DistanceType { get; set; }
		string IGeoDistanceRangeFilter.OptimizeBoundingBox { get; set; }

		private bool IsValidDistance { get; set; }

		bool IFilterBase.IsConditionless
		{
			get
			{
				return ((IGeoDistanceRangeFilter)this).Location.IsNullOrEmpty() || !this.IsValidDistance;
			}
		}

		public GeoDistanceRangeFilterDescriptor Location(double Lat, double Lon)
		{
			var c = CultureInfo.InvariantCulture;
			((IGeoDistanceRangeFilter)this).Location = "{0}, {1}".F(Lat.ToString(c), Lon.ToString(c));
			return this;
		}
		public GeoDistanceRangeFilterDescriptor Location(string geoHash)
		{
			((IGeoDistanceRangeFilter)this).Location = geoHash;
			return this;
		}
		public GeoDistanceRangeFilterDescriptor Distance(string From, string To)
		{
			((IGeoDistanceRangeFilter)this).From = From;
			((IGeoDistanceRangeFilter)this).To = To;
			this.IsValidDistance = !From.IsNullOrEmpty() && !To.IsNullOrEmpty();
			return this;
		}
		public GeoDistanceRangeFilterDescriptor Distance(double From, double To, GeoUnit Unit)
		{
			((IGeoDistanceRangeFilter)this).From = From;
			((IGeoDistanceRangeFilter)this).To = To;
			((IGeoDistanceRangeFilter)this).DistanceType = Enum.GetName(typeof(GeoUnit), Unit);
			this.IsValidDistance = true;
			return this;
		}
		public GeoDistanceRangeFilterDescriptor Optimize(GeoOptimizeBBox optimize)
		{
			((IGeoDistanceRangeFilter)this).OptimizeBoundingBox = Enum.GetName(typeof(GeoOptimizeBBox), optimize);
			return this;
		}

		object ICustomJson.GetCustomJson()
		{
			var f = (IGeoDistanceRangeFilter)this;
			var dict = this.FieldNameAsKeyFormat(f.Field, f.Location, d=>d
				.Add("from", f.From)
				.Add("to", f.To)
				.Add("distance_type", f.DistanceType)
				.Add("optimize_bbox", f.OptimizeBoundingBox)
			);
			return dict;
		}
	}
}
