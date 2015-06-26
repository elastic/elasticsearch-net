using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Globalization;
using System.Linq.Expressions;

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

	public class GeoDistanceRangeQuery : PlainQuery, IGeoDistanceRangeQuery
	{
		public string Name { get; set; }
		public bool IsConditionless { get { return QueryCondition.IsConditionless(this);  } }
		public PropertyPathMarker Field { get; set; }
		public string Location { get; set; }
		public object From { get; set; }
		public object To { get; set; }
		public GeoUnit? Unit { get; set; }
		public GeoDistance? DistanceType { get; set; }
		public GeoOptimizeBBox? OptimizeBoundingBox { get; set; }
		public bool? IncludeLower { get; set; }
		public bool? IncludeUpper { get; set; }

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
	}

	public class GeoDistanceRangeQueryDescriptor<T> : IGeoDistanceRangeQuery where T : class
	{
		private IGeoDistanceRangeQuery Self { get { return this; } }
		string IQuery.Name { get; set; }
		bool IQuery.IsConditionless { get { return QueryCondition.IsConditionless(this); } }
		PropertyPathMarker IGeoDistanceRangeQuery.Field { get; set; }
		string IGeoDistanceRangeQuery.Location { get; set; }
		object IGeoDistanceRangeQuery.From { get; set; }
		object IGeoDistanceRangeQuery.To { get; set; }
		bool? IGeoDistanceRangeQuery.IncludeLower { get; set; }
		bool? IGeoDistanceRangeQuery.IncludeUpper { get; set; }
		GeoDistance? IGeoDistanceRangeQuery.DistanceType { get; set; }
		GeoOptimizeBBox? IGeoDistanceRangeQuery.OptimizeBoundingBox { get; set; }
		GeoUnit? IGeoDistanceRangeQuery.Unit { get; set; }

		public GeoDistanceRangeQueryDescriptor<T> Field(string field)
		{
			Self.Field = field;
			return this;
		}

		public GeoDistanceRangeQueryDescriptor<T> Field(Expression<Func<T, object>> field)
		{
			Self.Field = field;
			return this;
		}

		public GeoDistanceRangeQueryDescriptor<T> Location(double Lat, double Lon)
		{
			var c = CultureInfo.InvariantCulture;
			Self.Location = "{0}, {1}".F(Lat.ToString(c), Lon.ToString(c));
			return this;
		}

		public GeoDistanceRangeQueryDescriptor<T> Location(string geoHash)
		{
			Self.Location = geoHash;
			return this;
		}

		public GeoDistanceRangeQueryDescriptor<T> Distance(string From, string To)
		{
			Self.From = From;
			Self.To = To;
			return this;
		}

		public GeoDistanceRangeQueryDescriptor<T> Distance(double From, double To, GeoUnit Unit)
		{
			Self.From = From;
			Self.To = To;
			Self.Unit = Unit;
			return this;
		}

		public GeoDistanceRangeQueryDescriptor<T> Optimize(GeoOptimizeBBox optimize)
		{
			Self.OptimizeBoundingBox = optimize;
			return this;
		}
		
		public GeoDistanceRangeQueryDescriptor<T> DistanceType(GeoDistance geoDistance)
		{
			Self.DistanceType = geoDistance;
			return this;
		}

		/// <summary>
		/// Forces the 'From()' to be exclusive (which is inclusive by default).
		/// </summary>
		public GeoDistanceRangeQueryDescriptor<T> FromExclusive()
		{
			Self.IncludeLower = false;
			return this;
		}

		/// <summary>
		/// Forces the 'To()' to be exclusive (which is inclusive by default).
		/// </summary>
		public GeoDistanceRangeQueryDescriptor<T> ToExclusive()
		{
			Self.IncludeUpper = false;
			return this;
		}

		public PropertyPathMarker GetFieldName()
		{
			return Self.Field;
		}

		public void SetFieldName(string fieldName)
		{
			Self.Field = fieldName;
		}
	}
}
