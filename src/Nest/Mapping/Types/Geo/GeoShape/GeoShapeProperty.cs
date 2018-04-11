using System.Diagnostics;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Maps a property as a geo_shape field
	/// </summary>
	[JsonObject(MemberSerialization.OptIn)]
	public interface IGeoShapeProperty : IDocValuesProperty
	{
		/// <summary>
		/// Name of the PrefixTree implementation to be used.
		/// Defaults to <see cref="GeoTree.Geohash"/>
		/// </summary>
		[JsonProperty("tree")]
		GeoTree? Tree { get; set; }

		/// <summary>
		/// Used instead of <see cref="TreeLevels"/> to set an appropriate value for the <see cref="TreeLevels"/>
		/// parameter. The value specifies the desired precision and Elasticsearch will calculate
		/// the best tree_levels value to honor this precision.
		/// </summary>
		[JsonProperty("precision")]
		Distance Precision { get; set; }

		/// <summary>
		/// Defines how to interpret vertex order for polygons and multipolygons.
		/// Defaults to <see cref="GeoOrientation.CounterClockWise"/>
		/// </summary>
		[JsonProperty("orientation")]
		GeoOrientation? Orientation { get; set; }

		/// <summary>
		/// Maximum number of layers to be used by the Prefix<see cref="Tree"/>. This can be used to control the
		/// precision of shape representations and therefore how many terms are indexed.
		/// Defaults to the default value of the chosen Prefix<see cref="Tree"/> implementation. Since this parameter requires a
		/// certain level of understanding of the underlying implementation, users may use the
		/// <see cref="Precision"/> parameter instead.
		/// </summary>
		[JsonProperty("tree_levels")]
		int? TreeLevels { get; set; }

		/// <summary>
		/// Defines the approach for how to represent shapes at indexing and search time.
		/// It also influences the capabilities available so it is recommended to let
		/// Elasticsearch set this parameter automatically.
		/// </summary>
		[JsonProperty("strategy")]
		GeoStrategy? Strategy { get; set; }

		/// <summary>
		/// Used as a hint to the Prefix<see cref="Tree"/> about how precise it should be.
		/// Defaults to 0.025 (2.5%) with 0.5 as the maximum supported value.
		/// </summary>
		/// <remarks>
		/// NOTE: This value will default to 0 if a <see cref="Precision"/> or <see cref="TreeLevels"/> definition
		/// is explicitly defined. This guarantees spatial precision at the level defined in the mapping.
		/// This can lead to significant memory usage for high resolution shapes with low error
		/// (e.g. large shapes at 1m with &lt; 0.001 error).
		/// To improve indexing performance (at the cost of query accuracy) explicitly define <see cref="TreeLevels"/> or
		/// <see cref="Precision"/> along with a reasonable <see cref="DistanceErrorPercentage"/>,
		/// noting that large shapes will have greater false positives.
		/// </remarks>
		[JsonProperty("distance_error_pct")]
		double? DistanceErrorPercentage { get; set; }

		/// <summary>
		/// Configures the geo_shape field type for point shapes only. Defaults to <c>false</c>.
		/// This optimizes index and search performance
		/// for the geohash and quadtree when it is known that only points will be indexed.
		/// At present geo_shape queries can not be executed on geo_point field types.
		/// This option bridges the gap by improving point performance on a geo_shape field
		/// so that geo_shape queries are optimal on a point only field.
		/// </summary>
		[JsonProperty("points_only")]
		bool? PointsOnly { get; set; }

		/// <summary>
		/// If <c>true</c>, malformed geojson shapes are ignored. If false (default),
		/// malformed geojson shapes throw an exception and reject the whole document.
		/// </summary>
		/// <remarks>
		/// Valid for Elasticsearch 6.1.0+
		/// </remarks>
		[JsonProperty("ignore_malformed")]
		bool? IgnoreMalformed { get; set; }
	}

	/// <inheritdoc cref="IGeoShapeProperty" />
	[DebuggerDisplay("{DebugDisplay}")]
	public class GeoShapeProperty : DocValuesPropertyBase, IGeoShapeProperty
	{
		public GeoShapeProperty() : base(FieldType.GeoShape) { }

		/// <inheritdoc />
		public GeoTree? Tree { get; set; }

		/// <inheritdoc />
		public Distance Precision { get; set; }

		/// <inheritdoc />
		public GeoOrientation? Orientation { get; set; }

		/// <inheritdoc />
		public int? TreeLevels { get; set; }

		/// <inheritdoc />
		public GeoStrategy? Strategy { get; set; }

		/// <inheritdoc />
		public double? DistanceErrorPercentage { get; set; }

		/// <inheritdoc />
		public bool? PointsOnly { get; set; }

		/// <inheritdoc />
		public bool? IgnoreMalformed { get; set; }
	}

	/// <inheritdoc cref="IGeoShapeProperty"/>
	[DebuggerDisplay("{DebugDisplay}")]
	public class GeoShapePropertyDescriptor<T>
		: DocValuesPropertyDescriptorBase<GeoShapePropertyDescriptor<T>, IGeoShapeProperty, T>, IGeoShapeProperty
		where T : class
	{
		GeoTree? IGeoShapeProperty.Tree { get; set; }
		Distance IGeoShapeProperty.Precision { get; set; }
		GeoOrientation? IGeoShapeProperty.Orientation { get; set; }
		int? IGeoShapeProperty.TreeLevels { get; set; }
		GeoStrategy? IGeoShapeProperty.Strategy { get; set; }
		double? IGeoShapeProperty.DistanceErrorPercentage { get; set; }
		bool? IGeoShapeProperty.PointsOnly { get; set; }
		bool? IGeoShapeProperty.IgnoreMalformed { get; set; }

		public GeoShapePropertyDescriptor() : base(FieldType.GeoShape) { }

		/// <inheritdoc cref="IGeoShapeProperty.Tree"/>
		public GeoShapePropertyDescriptor<T> Tree(GeoTree? tree) => Assign(a => a.Tree = tree);

		/// <inheritdoc cref="IGeoShapeProperty.TreeLevels"/>
		public GeoShapePropertyDescriptor<T> TreeLevels(int? treeLevels) => Assign(a => a.TreeLevels = treeLevels);

		/// <inheritdoc cref="IGeoShapeProperty.Strategy"/>
		public GeoShapePropertyDescriptor<T> Strategy(GeoStrategy? strategy) => Assign(a => a.Strategy = strategy);

		/// <inheritdoc cref="IGeoShapeProperty.Precision"/>
		public GeoShapePropertyDescriptor<T> Precision(double precision, DistanceUnit unit) =>
			Assign(a => a.Precision = new Distance(precision, unit));

		/// <inheritdoc cref="IGeoShapeProperty.Orientation"/>
		public GeoShapePropertyDescriptor<T> Orientation(GeoOrientation? orientation) => Assign(a => a.Orientation = orientation);

		/// <inheritdoc cref="IGeoShapeProperty.DistanceErrorPercentage"/>
		public GeoShapePropertyDescriptor<T> DistanceErrorPercentage(double? distanceErrorPercentage) =>
			Assign(a => a.DistanceErrorPercentage = distanceErrorPercentage);

		/// <inheritdoc cref="IGeoShapeProperty.PointsOnly"/>
		public GeoShapePropertyDescriptor<T> PointsOnly(bool? pointsOnly = true) => Assign(a => a.PointsOnly = pointsOnly);

		/// <inheritdoc cref="IGeoShapeProperty.IgnoreMalformed"/>
		public GeoShapePropertyDescriptor<T> IgnoreMalformed(bool? ignoreMalformed = true) =>
			Assign(a => a.IgnoreMalformed = ignoreMalformed);
	}
}
