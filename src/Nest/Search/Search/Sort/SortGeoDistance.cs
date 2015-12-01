using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Globalization;

namespace Nest
{
	public interface IGeoDistanceSort : ISort
	{
		Field Field { get; set; }
		IEnumerable<GeoLocation> Points { get; set; }
		GeoPrecision? GeoUnit { get; set; }
		GeoDistanceType? DistanceType { get; set; }
	}

	public class GeoDistanceSort : SortBase, IGeoDistanceSort
	{
		protected override Field SortKey => "_geo_distance";
		public Field Field { get; set; }
		public IEnumerable<GeoLocation> Points { get; set; }
		public GeoPrecision? GeoUnit { get; set; }
		public GeoDistanceType? DistanceType { get; set; }

	}

	public class SortGeoDistanceDescriptor<T> : SortDescriptorBase<SortGeoDistanceDescriptor<T>, IGeoDistanceSort, T>, IGeoDistanceSort 
		where T : class
	{

		protected override Field SortKey => "_geo_distance";

		Field IGeoDistanceSort.Field { get; set; }
		IEnumerable<GeoLocation> IGeoDistanceSort.Points { get; set; }
		GeoPrecision? IGeoDistanceSort.GeoUnit { get; set; }
		GeoDistanceType? IGeoDistanceSort.DistanceType { get; set; }

		public SortGeoDistanceDescriptor<T> PinTo(params GeoLocation[] geoLocations) => Assign(a => a.Points = geoLocations);
		public SortGeoDistanceDescriptor<T> PinTo(IEnumerable<GeoLocation> geoLocations) => Assign(a => a.Points = geoLocations);

		public SortGeoDistanceDescriptor<T> Unit(GeoPrecision unit) => Assign(a => a.GeoUnit = unit);

		public SortGeoDistanceDescriptor<T> DistanceType(GeoDistanceType distanceType) => Assign(a => a.DistanceType = distanceType);

		public SortGeoDistanceDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		public SortGeoDistanceDescriptor<T> Field(Expression<Func<T, object>> objectPath) => Assign(a => a.Field = objectPath);

		public SortGeoDistanceDescriptor<T> MissingLast() => Assign(a => a.Missing = "_last");

		public SortGeoDistanceDescriptor<T> MissingFirst() => Assign(a => a.Missing = "_first");

		public SortGeoDistanceDescriptor<T> MissingValue(string value) => Assign(a => a.Missing = value);

	}
}
