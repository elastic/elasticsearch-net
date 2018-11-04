using System.Diagnostics;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IGeoShapeProperty : IDocValuesProperty
	{
		[JsonProperty("distance_error_pct")]
		double? DistanceErrorPercentage { get; set; }

		[JsonProperty("orientation")]
		GeoOrientation? Orientation { get; set; }

		[JsonProperty("points_only")]
		bool? PointsOnly { get; set; }

		[JsonProperty("precision")]
		Distance Precision { get; set; }

		/// <summary>
		/// defines the approach for how to represent shapes at indexing and search time.
		/// It also influences the capabilities available so it is recommended to let
		/// Elasticsearch set this parameter automatically.
		/// </summary>
		[JsonProperty("strategy")]
		GeoStrategy? Strategy { get; set; }

		[JsonProperty("tree")]
		GeoTree? Tree { get; set; }

		[JsonProperty("tree_levels")]
		int? TreeLevels { get; set; }
	}

	[DebuggerDisplay("{DebugDisplay}")]
	public class GeoShapeProperty : DocValuesPropertyBase, IGeoShapeProperty
	{
		public GeoShapeProperty() : base(FieldType.GeoShape) { }

		public double? DistanceErrorPercentage { get; set; }

		public GeoOrientation? Orientation { get; set; }

		public bool? PointsOnly { get; set; }

		public Distance Precision { get; set; }

		public GeoStrategy? Strategy { get; set; }

		public GeoTree? Tree { get; set; }

		public int? TreeLevels { get; set; }
	}

	[DebuggerDisplay("{DebugDisplay}")]
	public class GeoShapePropertyDescriptor<T>
		: DocValuesPropertyDescriptorBase<GeoShapePropertyDescriptor<T>, IGeoShapeProperty, T>, IGeoShapeProperty
		where T : class
	{
		public GeoShapePropertyDescriptor() : base(FieldType.GeoShape) { }

		double? IGeoShapeProperty.DistanceErrorPercentage { get; set; }
		GeoOrientation? IGeoShapeProperty.Orientation { get; set; }
		bool? IGeoShapeProperty.PointsOnly { get; set; }
		Distance IGeoShapeProperty.Precision { get; set; }
		GeoStrategy? IGeoShapeProperty.Strategy { get; set; }
		GeoTree? IGeoShapeProperty.Tree { get; set; }
		int? IGeoShapeProperty.TreeLevels { get; set; }

		public GeoShapePropertyDescriptor<T> Tree(GeoTree tree) => Assign(a => a.Tree = tree);

		public GeoShapePropertyDescriptor<T> TreeLevels(int treeLevels) => Assign(a => a.TreeLevels = treeLevels);

		public GeoShapePropertyDescriptor<T> Strategy(GeoStrategy strategy) => Assign(a => a.Strategy = strategy);

		public GeoShapePropertyDescriptor<T> Precision(double precision, DistanceUnit unit) =>
			Assign(a => a.Precision = new Distance(precision, unit));

		public GeoShapePropertyDescriptor<T> Orientation(GeoOrientation orientation) => Assign(a => a.Orientation = orientation);

		public GeoShapePropertyDescriptor<T> DistanceErrorPercentage(double distanceErrorPercentage) =>
			Assign(a => a.DistanceErrorPercentage = distanceErrorPercentage);

		public GeoShapePropertyDescriptor<T> PointsOnly(bool pointsOnly = true) => Assign(a => a.PointsOnly = pointsOnly);
	}
}
