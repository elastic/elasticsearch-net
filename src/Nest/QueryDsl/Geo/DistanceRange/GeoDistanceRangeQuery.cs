using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Globalization;
using System.Linq.Expressions;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof (VariableFieldNameQueryJsonConverter<GeoDistanceRangeQuery, IGeoDistanceRangeQuery>))]
	public interface IGeoDistanceRangeQuery : IFieldNameQuery
	{
		[VariableField]
		GeoLocation Location { get; set; }

		[JsonProperty("from")]
		GeoDistance From { get; set; }
		
		[JsonProperty("to")]
		GeoDistance To { get; set; }
		
		[JsonProperty("distance_type")]
		GeoDistanceType? DistanceType { get; set; }
		
		[JsonProperty("optimize_bbox")]
		GeoOptimizeBBox? OptimizeBoundingBox { get; set; }
		
		[JsonProperty("include_lower")]
		bool? IncludeLower { get; set; }

		[JsonProperty("include_upper")]
		bool? IncludeUpper { get; set; }

		[JsonProperty("coerce")]
		bool? Coerce { get; set; }

		[JsonProperty("ignore_malformed")]
		bool? IgnoreMalformed { get; set; }
	
		[JsonProperty("validation_method")]
		GeoValidationMethod? ValidationMethod { get; set; }
	
	}

	public class GeoDistanceRangeQuery : FieldNameQueryBase, IGeoDistanceRangeQuery
	{
		bool IQuery.Conditionless => IsConditionless(this);
		public GeoLocation Location { get; set; }
		public GeoDistance From { get; set; }
		public GeoDistance To { get; set; }
		public GeoDistanceType? DistanceType { get; set; }
		public GeoOptimizeBBox? OptimizeBoundingBox { get; set; }
		public bool? IncludeLower { get; set; }
		public bool? IncludeUpper { get; set; }
		public bool? Coerce { get; set; }
		public bool? IgnoreMalformed { get; set; }
		public GeoValidationMethod? ValidationMethod { get; set; }

		protected override void WrapInContainer(IQueryContainer c) => c.GeoDistanceRange = this;

		internal static bool IsConditionless(IGeoDistanceRangeQuery q) => q.Location == null || (q.To == null && q.From == null);
	}

	public class GeoDistanceRangeQueryDescriptor<T> : FieldNameQueryDescriptorBase<GeoDistanceRangeQueryDescriptor<T>, IGeoDistanceRangeQuery, T> 
		, IGeoDistanceRangeQuery where T : class
	{
		bool IQuery.Conditionless => GeoDistanceRangeQuery.IsConditionless(this);
		GeoLocation IGeoDistanceRangeQuery.Location { get; set; }
		GeoDistance IGeoDistanceRangeQuery.From { get; set; }
		GeoDistance IGeoDistanceRangeQuery.To { get; set; }
		bool? IGeoDistanceRangeQuery.IncludeLower { get; set; }
		bool? IGeoDistanceRangeQuery.IncludeUpper { get; set; }
		GeoDistanceType? IGeoDistanceRangeQuery.DistanceType { get; set; }
		GeoOptimizeBBox? IGeoDistanceRangeQuery.OptimizeBoundingBox { get; set; }
		bool? IGeoDistanceRangeQuery.Coerce { get; set; }
		bool? IGeoDistanceRangeQuery.IgnoreMalformed { get; set; }
		GeoValidationMethod? IGeoDistanceRangeQuery.ValidationMethod { get; set; }

		public GeoDistanceRangeQueryDescriptor<T> Location(GeoLocation geoLocation) => Assign(a => a.Location = geoLocation);
		public GeoDistanceRangeQueryDescriptor<T> Location(double lat, double lon) => Assign(a => a.Location = new GeoLocation(lat, lon));

		public GeoDistanceRangeQueryDescriptor<T> From(GeoDistance from) => Assign(a => a.From = from);
		public GeoDistanceRangeQueryDescriptor<T> From(double distance, GeoPrecision unit) => Assign(a => a.From = new GeoDistance(distance, unit));

		public GeoDistanceRangeQueryDescriptor<T> To(GeoDistance to) => Assign(a => a.To = to);
		public GeoDistanceRangeQueryDescriptor<T> To(double distance, GeoPrecision unit) => Assign(a => a.To = new GeoDistance(distance, unit));

		public GeoDistanceRangeQueryDescriptor<T> Optimize(GeoOptimizeBBox optimize) => Assign(a => a.OptimizeBoundingBox = optimize);

		public GeoDistanceRangeQueryDescriptor<T> DistanceType(GeoDistanceType geoDistance) => Assign(a => a.DistanceType = geoDistance);

		/// <summary>
		/// Forces the 'From()' to be exclusive (which is inclusive by default).
		/// </summary>
		public GeoDistanceRangeQueryDescriptor<T> FromExclusive() => Assign(a => a.IncludeLower = false);

		/// <summary>
		/// Forces the 'To()' to be exclusive (which is inclusive by default).
		/// </summary>
		public GeoDistanceRangeQueryDescriptor<T> ToExclusive() => Assign(a => a.IncludeUpper = false);

		public GeoDistanceRangeQueryDescriptor<T> Coerce(bool? coerce = true) => Assign(a => a.Coerce = coerce);

		public GeoDistanceRangeQueryDescriptor<T> IgnoreMalformed(bool? ignore = true) => Assign(a => a.IgnoreMalformed = ignore);

		public GeoDistanceRangeQueryDescriptor<T> ValidationMethod(GeoValidationMethod? validation) => Assign(a => a.ValidationMethod = validation);
	}
}
