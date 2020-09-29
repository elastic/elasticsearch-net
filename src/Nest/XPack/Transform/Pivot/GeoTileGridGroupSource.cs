using System;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// The geotile grid value source works on geo_point fields and groups points into buckets that represent
	/// cells in a grid. The resulting grid can be sparse and only contains cells that have matching data.
	/// <para />
	/// Available in Elasticsearch 7.9.0+
	/// </summary>
	[InterfaceDataContract]
	public interface IGeoTileGridGroupSource : ISingleGroupSource
	{
		/// <summary>
		/// The highest-precision geotile of length 29 produces cells that cover less than 10cm by 10cm of land.
		/// This precision is uniquely suited for composite aggregations as each tile does not have to be
		/// generated and loaded in memory.
		/// </summary>
		[DataMember(Name = "precision")]
		GeoTilePrecision? Precision { get; set; }

		/// <summary>
		/// Constrained to a specific geo bounding box, which reduces the range of tiles used.
		/// These bounds are useful when only a specific part of a geographical area needs high precision tiling.
		/// <para />
		/// Available in Elasticsearch 7.6.0+.
		/// </summary>
		[DataMember(Name = "bounds")]
		IBoundingBox Bounds { get; set; }
	}

	/// <inheritdoc cref="IGeoTileGridGroupSource" />
	public class GeoTileGridGroupSource : SingleGroupSourceBase, IGeoTileGridGroupSource
	{
		/// <inheritdoc />
		public GeoTilePrecision? Precision { get; set; }
		/// <inheritdoc />
		public IBoundingBox Bounds { get; set; }
	}

	/// <inheritdoc cref="IGeoTileGridGroupSource" />
	public class GeoTileGridGroupSourceDescriptor<T>
		: SingleGroupSourceDescriptorBase<GeoTileGridGroupSourceDescriptor<T>, IGeoTileGridGroupSource, T>,
			IGeoTileGridGroupSource
	{
		GeoTilePrecision? IGeoTileGridGroupSource.Precision { get; set; }
		IBoundingBox IGeoTileGridGroupSource.Bounds { get; set; }

		/// <inheritdoc cref="IGeoTileGridGroupSource.Precision"/>
		public GeoTileGridGroupSourceDescriptor<T> Precision(GeoTilePrecision? precision) =>
			Assign(precision, (a, v) => a.Precision = v);

		/// <inheritdoc cref="IGeoTileGridGroupSource.Bounds"/>
		public GeoTileGridGroupSourceDescriptor<T> Bounds(Func<BoundingBoxDescriptor, IBoundingBox> selector) =>
			Assign(selector, (a, v) => a.Bounds = v?.Invoke(new BoundingBoxDescriptor()));
	}
}
