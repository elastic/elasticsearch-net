using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	/// <summary> Allows you to sort based on a proximity to one or more <see cref="GeoLocation" /> </summary>
	[InterfaceDataContract]
	public interface IGeoDistanceSort : ISort
	{
		/// <summary>
		/// How to compute the distance. Can either be arc (default), or plane (faster, but
		/// inaccurate on long distances and close to the poles).
		/// </summary>
		[DataMember(Name ="distance_type")]
		GeoDistanceType? DistanceType { get; set; }

		Field Field { get; set; }

		/// <summary> The unit to use when computing sort values. The default is m (meters) </summary>
		[DataMember(Name ="unit")]
		DistanceUnit? GeoUnit { get; set; }

		/// <summary>
		/// Indicates if the unmapped field should be treated as a missing value. Setting it to `true` is equivalent to specifying
		/// an `unmapped_type` in the field sort. The default is `false` (unmapped field are causing the search to fail)
		/// </summary>
		[DataMember(Name ="ignore_unmapped")]
		bool? IgnoreUnmapped { get; set; }

		IEnumerable<GeoLocation> Points { get; set; }
	}

	/// <inheritdoc cref="IGeoDistanceSort" />
	public class GeoDistanceSort : SortBase, IGeoDistanceSort
	{
		/// <inheritdoc cref="IGeoDistanceSort.DistanceType" />
		public GeoDistanceType? DistanceType { get; set; }

		public Field Field { get; set; }

		/// <inheritdoc cref="IGeoDistanceSort.GeoUnit" />
		public DistanceUnit? GeoUnit { get; set; }

		/// <inheritdoc cref="IGeoDistanceSort.IgnoreUnmapped" />
		public bool? IgnoreUnmapped { get; set; }

		public IEnumerable<GeoLocation> Points { get; set; }
		protected override Field SortKey => "_geo_distance";
	}

	/// <inheritdoc cref="IGeoDistanceSort" />
	public class SortGeoDistanceDescriptor<T> : SortDescriptorBase<SortGeoDistanceDescriptor<T>, IGeoDistanceSort, T>, IGeoDistanceSort
		where T : class
	{
		protected override Field SortKey => "_geo_distance";
		GeoDistanceType? IGeoDistanceSort.DistanceType { get; set; }

		Field IGeoDistanceSort.Field { get; set; }
		DistanceUnit? IGeoDistanceSort.GeoUnit { get; set; }
		bool? IGeoDistanceSort.IgnoreUnmapped { get; set; }
		IEnumerable<GeoLocation> IGeoDistanceSort.Points { get; set; }

		public SortGeoDistanceDescriptor<T> Points(params GeoLocation[] geoLocations) => Assign(a => a.Points = geoLocations);

		public SortGeoDistanceDescriptor<T> Points(IEnumerable<GeoLocation> geoLocations) => Assign(a => a.Points = geoLocations);

		/// <inheritdoc cref="IGeoDistanceSort.GeoUnit" />
		public SortGeoDistanceDescriptor<T> Unit(DistanceUnit? unit) => Assign(a => a.GeoUnit = unit);

		/// <inheritdoc cref="IGeoDistanceSort.DistanceType" />
		public SortGeoDistanceDescriptor<T> DistanceType(GeoDistanceType? distanceType) => Assign(a => a.DistanceType = distanceType);

		public SortGeoDistanceDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		public SortGeoDistanceDescriptor<T> Field(Expression<Func<T, object>> objectPath) => Assign(a => a.Field = objectPath);

		/// <inheritdoc cref="IGeoDistanceSort.IgnoreUnmapped" />
		public SortGeoDistanceDescriptor<T> IgnoreUnmapped(bool? ignoreUnmapped = true) => Assign(a => a.IgnoreUnmapped = ignoreUnmapped);
	}
}
