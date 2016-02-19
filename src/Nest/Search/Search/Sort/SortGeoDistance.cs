using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	public interface IGeoDistanceSort : ISort
	{
		Field Field { get; set; }
		IEnumerable<GeoLocation> Points { get; set; }

		[JsonProperty(PropertyName = "unit")]
		DistanceUnit? GeoUnit { get; set; }

		[JsonProperty(PropertyName = "distance_type")]
		GeoDistanceType? DistanceType { get; set; }
	}

	public class GeoDistanceSort : SortBase, IGeoDistanceSort
	{
		protected override Field SortKey => "_geo_distance";
		public Field Field { get; set; }
		public IEnumerable<GeoLocation> Points { get; set; }
		public DistanceUnit? GeoUnit { get; set; }
		public GeoDistanceType? DistanceType { get; set; }

	}

	public class SortGeoDistanceDescriptor<T> : SortDescriptorBase<SortGeoDistanceDescriptor<T>, IGeoDistanceSort, T>, IGeoDistanceSort 
		where T : class
	{

		protected override Field SortKey => "_geo_distance";

		Field IGeoDistanceSort.Field { get; set; }
		IEnumerable<GeoLocation> IGeoDistanceSort.Points { get; set; }
		DistanceUnit? IGeoDistanceSort.GeoUnit { get; set; }
		GeoDistanceType? IGeoDistanceSort.DistanceType { get; set; }

		public SortGeoDistanceDescriptor<T> PinTo(params GeoLocation[] geoLocations) => Assign(a => a.Points = geoLocations);
		public SortGeoDistanceDescriptor<T> PinTo(IEnumerable<GeoLocation> geoLocations) => Assign(a => a.Points = geoLocations);

		public SortGeoDistanceDescriptor<T> Unit(DistanceUnit unit) => Assign(a => a.GeoUnit = unit);

		public SortGeoDistanceDescriptor<T> DistanceType(GeoDistanceType distanceType) => Assign(a => a.DistanceType = distanceType);

		public SortGeoDistanceDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		public SortGeoDistanceDescriptor<T> Field(Expression<Func<T, object>> objectPath) => Assign(a => a.Field = objectPath);

	}
}
