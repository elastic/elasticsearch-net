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

	public class GeoDistanceRangeQuery : FieldNameQueryBase, IGeoDistanceRangeQuery
	{
		bool IQuery.Conditionless => IsConditionless(this);
		public string Location { get; set; }
		public object From { get; set; }
		public object To { get; set; }
		public GeoUnit? Unit { get; set; }
		public GeoDistance? DistanceType { get; set; }
		public GeoOptimizeBBox? OptimizeBoundingBox { get; set; }
		public bool? IncludeLower { get; set; }
		public bool? IncludeUpper { get; set; }

		protected override void WrapInContainer(IQueryContainer c) => c.GeoDistanceRange = this;

		internal static bool IsConditionless(IGeoDistanceRangeQuery q) => q.Location.IsNullOrEmpty() || (q.To == null && q.From == null);
	}

	public class GeoDistanceRangeQueryDescriptor<T> 
		: FieldNameQueryDescriptorBase<GeoDistanceRangeQueryDescriptor<T>, IGeoDistanceRangeQuery, T> 
		, IGeoDistanceRangeQuery where T : class
	{
		private IGeoDistanceRangeQuery Self => this;
		bool IQuery.Conditionless => GeoDistanceRangeQuery.IsConditionless(this);
		string IGeoDistanceRangeQuery.Location { get; set; }
		object IGeoDistanceRangeQuery.From { get; set; }
		object IGeoDistanceRangeQuery.To { get; set; }
		bool? IGeoDistanceRangeQuery.IncludeLower { get; set; }
		bool? IGeoDistanceRangeQuery.IncludeUpper { get; set; }
		GeoDistance? IGeoDistanceRangeQuery.DistanceType { get; set; }
		GeoOptimizeBBox? IGeoDistanceRangeQuery.OptimizeBoundingBox { get; set; }
		GeoUnit? IGeoDistanceRangeQuery.Unit { get; set; }

		public GeoDistanceRangeQueryDescriptor<T> Location(double Lat, double Lon) => Assign(a =>
		{
			var c = CultureInfo.InvariantCulture;
			a.Location = "{0}, {1}".F(Lat.ToString(c), Lon.ToString(c));
		});

		public GeoDistanceRangeQueryDescriptor<T> Location(string geoHash) => Assign(a => a.Location = geoHash);

		public GeoDistanceRangeQueryDescriptor<T> Distance(string from, string to) => Assign(a => { a.From = from; a.To = to; });

		public GeoDistanceRangeQueryDescriptor<T> Distance(double from, double to, GeoUnit unit) => 
			Assign(a => { a.From = from; a.To = to; a.Unit = unit; });


		public GeoDistanceRangeQueryDescriptor<T> Optimize(GeoOptimizeBBox optimize) => Assign(a => a.OptimizeBoundingBox = optimize);

		public GeoDistanceRangeQueryDescriptor<T> DistanceType(GeoDistance geoDistance) => Assign(a => a.DistanceType = geoDistance);

		/// <summary>
		/// Forces the 'From()' to be exclusive (which is inclusive by default).
		/// </summary>
		public GeoDistanceRangeQueryDescriptor<T> FromExclusive() => Assign(a => a.IncludeLower = false);

		/// <summary>
		/// Forces the 'To()' to be exclusive (which is inclusive by default).
		/// </summary>
		public GeoDistanceRangeQueryDescriptor<T> ToExclusive() => Assign(a => a.IncludeUpper = false);
	}
}
