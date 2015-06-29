using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Globalization;
using System;
using Newtonsoft.Json.Converters;
using System.Linq.Expressions;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGeoDistanceQuery : IFieldNameQuery
	{
		string Location { get; set; }
		
		[JsonProperty("distance")]
		object Distance { get; set; }
		
		[JsonProperty("unit")]
		[JsonConverter(typeof(StringEnumConverter))]
		GeoUnit? Unit { get; set; }
	
		[JsonProperty("optimize_bbox")]
		[JsonConverter(typeof(StringEnumConverter))]
		GeoOptimizeBBox? OptimizeBoundingBox { get; set; }

		[JsonProperty("distance_type")]
		[JsonConverter(typeof(StringEnumConverter))]
		GeoDistance? DistanceType { get; set; }
	}

	public class GeoDistanceQuery : FieldNameQuery, IGeoDistanceQuery
	{
		bool IQuery.Conditionless => IsConditionless(this);
		public string Location { get; set; }
		public object Distance { get; set; }
		public GeoUnit? Unit { get; set; }
		public GeoOptimizeBBox? OptimizeBoundingBox { get; set; }
		public GeoDistance? DistanceType { get; set; }

		protected override void WrapInContainer(IQueryContainer c) => c.GeoDistance = this;

		internal static bool IsConditionless(IGeoDistanceQuery q) => q.Location.IsNullOrEmpty() || q.Distance == null;
	}

	public class GeoDistanceQueryDescriptor<T> : IGeoDistanceQuery where T : class
	{
		private IGeoDistanceQuery Self => this;
		string IQuery.Name { get; set; }
		bool IQuery.Conditionless => GeoDistanceQuery.IsConditionless(this);
		PropertyPathMarker IFieldNameQuery.Field { get; set; }
		string IGeoDistanceQuery.Location { get; set; }
		object IGeoDistanceQuery.Distance { get; set; }
		GeoUnit? IGeoDistanceQuery.Unit { get; set; }
		GeoDistance? IGeoDistanceQuery.DistanceType { get; set; }
		GeoOptimizeBBox? IGeoDistanceQuery.OptimizeBoundingBox { get; set; }

		public GeoDistanceQueryDescriptor<T> Field(string field)
		{
			Self.Field = field;
			return this;
		}

		public GeoDistanceQueryDescriptor<T> Field(Expression<Func<T, object>> field)
		{
			Self.Field = field;
			return this;
		}

		public GeoDistanceQueryDescriptor<T> Location(double Lat, double Lon)
		{
			var c = CultureInfo.InvariantCulture;
			Self.Location = "{0}, {1}".F(Lat.ToString(c), Lon.ToString(c));
			return this;
		}

		public GeoDistanceQueryDescriptor<T> Location(string geoHash)
		{
			Self.Location = geoHash;
			return this;
		}

		public GeoDistanceQueryDescriptor<T> Distance(string distance)
		{
			Self.Distance = distance;
			return this;
		}

		public GeoDistanceQueryDescriptor<T> Distance(double distance, GeoUnit unit)
		{
			Self.Distance = distance;
			Self.Unit = unit;
			return this;
		}

		public GeoDistanceQueryDescriptor<T> Optimize(GeoOptimizeBBox optimize)
		{
			Self.OptimizeBoundingBox = optimize;
			return this;
		}

		public GeoDistanceQueryDescriptor<T> DistanceType(GeoDistance type)
		{
			Self.DistanceType = type;
			return this;
		}
	}
}
