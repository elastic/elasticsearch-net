using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Resolvers;
using Nest.Resolvers.Converters;
using Nest.Resolvers.Converters.Filters;
using Newtonsoft.Json;
using System.Globalization;
using Elasticsearch.Net;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGeoDistanceRangeFilter : IFilter
	{
		PropertyPathMarker Field { get; set; }
		string Location { get; set; }

		[JsonProperty("from")]
		object From { get; set; }
		
		[JsonProperty("to")]
		object To { get; set; }
		
		[JsonProperty("unit")]
		GeoUnit? Unit { get; set; }

		[JsonProperty("distance_type")]
		GeoDistanceType? DistanceType { get; set; }

		[JsonProperty("optimize_bbox")]
		GeoOptimizeBBox? OptimizeBoundingBox { get; set; }
		
		[JsonProperty("include_lower")]
		bool? IncludeLower { get; set; }

		[JsonProperty("include_upper")]
		bool? IncludeUpper { get; set; }
	}

	public class GeoDistanceRangeFilter : PlainFilter, IGeoDistanceRangeFilter
	{
		protected override void WrapInContainer(IFilterContainer container)
		{
			container.GeoDistanceRange = this;
		}

		public PropertyPathMarker Field { get; set; }
		public string Location { get; set; }
		public object From { get; set; }
		public object To { get; set; }
		public GeoUnit? Unit { get; set; }
		public GeoDistanceType? DistanceType { get; set; }
		public GeoOptimizeBBox? OptimizeBoundingBox { get; set; }
		public bool? IncludeLower { get; set; }
		public bool? IncludeUpper { get; set; }
	}

	public class GeoDistanceRangeFilterDescriptor : FilterBase, IGeoDistanceRangeFilter
	{
		PropertyPathMarker IGeoDistanceRangeFilter.Field { get; set; }
		string IGeoDistanceRangeFilter.Location { get; set; }
		object IGeoDistanceRangeFilter.From { get; set; }
		object IGeoDistanceRangeFilter.To { get; set; }
		bool? IGeoDistanceRangeFilter.IncludeLower { get; set; }
		bool? IGeoDistanceRangeFilter.IncludeUpper { get; set; }
		GeoDistanceType? IGeoDistanceRangeFilter.DistanceType { get; set; }
		GeoOptimizeBBox? IGeoDistanceRangeFilter.OptimizeBoundingBox { get; set; }
		GeoUnit? IGeoDistanceRangeFilter.Unit { get; set; }

		private bool IsValidDistance { get; set; }
		private IGeoDistanceRangeFilter _ { get { return this; }}

		bool IFilter.IsConditionless
		{
			get
			{
				return _.Location.IsNullOrEmpty() || !this.IsValidDistance;
			}
		}

		public GeoDistanceRangeFilterDescriptor Location(double Lat, double Lon)
		{
			var c = CultureInfo.InvariantCulture;
			_.Location = "{0}, {1}".F(Lat.ToString(c), Lon.ToString(c));
			return this;
		}

		public GeoDistanceRangeFilterDescriptor Location(string geoHash)
		{
			_.Location = geoHash;
			return this;
		}

		public GeoDistanceRangeFilterDescriptor Distance(string From, string To)
		{
			_.From = From;
			_.To = To;
			this.IsValidDistance = !From.IsNullOrEmpty() && !To.IsNullOrEmpty();
			return this;
		}

		public GeoDistanceRangeFilterDescriptor Distance(double From, double To, GeoUnit Unit)
		{
			_.From = From;
			_.To = To;
			_.Unit = Unit;
			this.IsValidDistance = true;
			return this;
		}

		public GeoDistanceRangeFilterDescriptor Optimize(GeoOptimizeBBox optimize)
		{
			_.OptimizeBoundingBox = optimize;
			return this;
		}
		
		public GeoDistanceRangeFilterDescriptor DistanceType(GeoDistanceType geoDistanceType)
		{
			_.DistanceType = geoDistanceType;
			return this;
		}

		/// <summary>
		/// Forces the 'From()' to be exclusive (which is inclusive by default).
		/// </summary>
		public GeoDistanceRangeFilterDescriptor FromExclusive()
		{
			_.IncludeLower = false;
			return this;
		}

		/// <summary>
		/// Forces the 'To()' to be exclusive (which is inclusive by default).
		/// </summary>
		public GeoDistanceRangeFilterDescriptor ToExclusive()
		{
			_.IncludeUpper = false;
			return this;
		}

	}
}
