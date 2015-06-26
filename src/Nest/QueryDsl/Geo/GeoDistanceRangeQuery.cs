using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Globalization;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGeoDistanceRangeQuery : IFieldNameQuery
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
		GeoDistance? DistanceType { get; set; }

		[JsonProperty("optimize_bbox")]
		GeoOptimizeBBox? OptimizeBoundingBox { get; set; }
		
		[JsonProperty("include_lower")]
		bool? IncludeLower { get; set; }

		[JsonProperty("include_upper")]
		bool? IncludeUpper { get; set; }
	}

	// TODO : Finish implementing
	public class GeoDistanceRangeFilter : PlainQuery, IGeoDistanceRangeQuery
	{
		protected override void WrapInContainer(IQueryContainer container)
		{
			container.GeoDistanceRange = this;
		}

		public PropertyPathMarker GetFieldName()
		{
			return Field;
		}

		public void SetFieldName(string fieldName)
		{
			Field = fieldName;
		}

		public PropertyPathMarker Field { get; set; }
		public string Location { get; set; }
		public object From { get; set; }
		public object To { get; set; }
		public GeoUnit? Unit { get; set; }
		public GeoDistance? DistanceType { get; set; }
		public GeoOptimizeBBox? OptimizeBoundingBox { get; set; }
		public bool? IncludeLower { get; set; }
		public bool? IncludeUpper { get; set; }

		public string Name { get; set; }

		public bool IsConditionless { get { return QueryCondition.IsConditionless(this);  } }
	}

	public class GeoDistanceRangeQueryDescriptor : IGeoDistanceRangeQuery
	{
		PropertyPathMarker IGeoDistanceRangeQuery.Field { get; set; }
		string IGeoDistanceRangeQuery.Location { get; set; }
		object IGeoDistanceRangeQuery.From { get; set; }
		object IGeoDistanceRangeQuery.To { get; set; }
		bool? IGeoDistanceRangeQuery.IncludeLower { get; set; }
		bool? IGeoDistanceRangeQuery.IncludeUpper { get; set; }
		GeoDistance? IGeoDistanceRangeQuery.DistanceType { get; set; }
		GeoOptimizeBBox? IGeoDistanceRangeQuery.OptimizeBoundingBox { get; set; }
		GeoUnit? IGeoDistanceRangeQuery.Unit { get; set; }
		private IGeoDistanceRangeQuery _ { get { return this; } }

		bool IQuery.IsConditionless { get { return QueryCondition.IsConditionless(this); } }

		string IQuery.Name { get; set; }


		public GeoDistanceRangeQueryDescriptor Location(double Lat, double Lon)
		{
			var c = CultureInfo.InvariantCulture;
			_.Location = "{0}, {1}".F(Lat.ToString(c), Lon.ToString(c));
			return this;
		}

		public GeoDistanceRangeQueryDescriptor Location(string geoHash)
		{
			_.Location = geoHash;
			return this;
		}

		public GeoDistanceRangeQueryDescriptor Distance(string From, string To)
		{
			_.From = From;
			_.To = To;
			return this;
		}

		public GeoDistanceRangeQueryDescriptor Distance(double From, double To, GeoUnit Unit)
		{
			_.From = From;
			_.To = To;
			_.Unit = Unit;
			return this;
		}

		public GeoDistanceRangeQueryDescriptor Optimize(GeoOptimizeBBox optimize)
		{
			_.OptimizeBoundingBox = optimize;
			return this;
		}
		
		public GeoDistanceRangeQueryDescriptor DistanceType(GeoDistance geoDistance)
		{
			_.DistanceType = geoDistance;
			return this;
		}

		/// <summary>
		/// Forces the 'From()' to be exclusive (which is inclusive by default).
		/// </summary>
		public GeoDistanceRangeQueryDescriptor FromExclusive()
		{
			_.IncludeLower = false;
			return this;
		}

		/// <summary>
		/// Forces the 'To()' to be exclusive (which is inclusive by default).
		/// </summary>
		public GeoDistanceRangeQueryDescriptor ToExclusive()
		{
			_.IncludeUpper = false;
			return this;
		}

		public PropertyPathMarker GetFieldName()
		{
			return _.Field;
		}

		public void SetFieldName(string fieldName)
		{
			_.Field = fieldName;
		}
	}
}
