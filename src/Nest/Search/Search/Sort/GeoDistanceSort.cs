// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Nest.Utf8Json;

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
		DistanceUnit? Unit { get; set; }

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

		/// <inheritdoc cref="IGeoDistanceSort.Unit" />
		public DistanceUnit? Unit { get; set; }

		/// <inheritdoc cref="IGeoDistanceSort.IgnoreUnmapped" />
		public bool? IgnoreUnmapped { get; set; }

		public IEnumerable<GeoLocation> Points { get; set; }
		protected override Field SortKey => "_geo_distance";
	}

	/// <inheritdoc cref="IGeoDistanceSort" />
	public class GeoDistanceSortDescriptor<T> : SortDescriptorBase<GeoDistanceSortDescriptor<T>, IGeoDistanceSort, T>, IGeoDistanceSort
		where T : class
	{
		protected override Field SortKey => "_geo_distance";
		GeoDistanceType? IGeoDistanceSort.DistanceType { get; set; }

		Field IGeoDistanceSort.Field { get; set; }
		DistanceUnit? IGeoDistanceSort.Unit { get; set; }
		bool? IGeoDistanceSort.IgnoreUnmapped { get; set; }
		IEnumerable<GeoLocation> IGeoDistanceSort.Points { get; set; }

		public GeoDistanceSortDescriptor<T> Points(params GeoLocation[] geoLocations) => Assign(geoLocations, (a, v) => a.Points = v);

		public GeoDistanceSortDescriptor<T> Points(IEnumerable<GeoLocation> geoLocations) => Assign(geoLocations, (a, v) => a.Points = v);

		/// <inheritdoc cref="IGeoDistanceSort.Unit" />
		public GeoDistanceSortDescriptor<T> Unit(DistanceUnit? unit) => Assign(unit, (a, v) => a.Unit = v);

		/// <inheritdoc cref="IGeoDistanceSort.DistanceType" />
		public GeoDistanceSortDescriptor<T> DistanceType(GeoDistanceType? distanceType) => Assign(distanceType, (a, v) => a.DistanceType = v);

		public GeoDistanceSortDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		public GeoDistanceSortDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> objectPath) => Assign(objectPath, (a, v) => a.Field = v);

		/// <inheritdoc cref="IGeoDistanceSort.IgnoreUnmapped" />
		public GeoDistanceSortDescriptor<T> IgnoreUnmapped(bool? ignoreUnmapped = true) => Assign(ignoreUnmapped, (a, v) => a.IgnoreUnmapped = v);
	}
}
