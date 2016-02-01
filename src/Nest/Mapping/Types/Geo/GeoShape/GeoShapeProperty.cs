using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IGeoShapeProperty : IProperty
	{
		[JsonProperty("tree")]
		GeoTree? Tree { get; set; }

		[JsonProperty("precision")]
		Distance Precision { get; set; }

		[JsonProperty("orientation")]
		GeoOrientation? Orientation { get; set; }

		[JsonProperty("tree_levels")]
		int? TreeLevels { get; set; }

		[JsonProperty("distance_error_pct")]
		double? DistanceErrorPercentage { get; set; }

		[JsonProperty("points_only")]
		bool? PointsOnly { get; set; }
	}

	public class GeoShapeProperty : PropertyBase, IGeoShapeProperty
	{
		public GeoShapeProperty() : base("geo_shape") { }

		public GeoTree? Tree { get; set; }

		public Distance Precision { get; set; }

		public GeoOrientation? Orientation { get; set; }

		public int? TreeLevels { get; set; }

		public double? DistanceErrorPercentage { get; set; }

		public bool? PointsOnly { get; set; }
	}

	public class GeoShapePropertyDescriptor<T>
		: PropertyDescriptorBase<GeoShapePropertyDescriptor<T>, IGeoShapeProperty, T>, IGeoShapeProperty
		where T : class
	{
		GeoTree? IGeoShapeProperty.Tree { get; set; }
		Distance IGeoShapeProperty.Precision { get; set; }
		GeoOrientation? IGeoShapeProperty.Orientation { get; set; }
		int? IGeoShapeProperty.TreeLevels { get; set; }
		double? IGeoShapeProperty.DistanceErrorPercentage { get; set; }
		bool? IGeoShapeProperty.PointsOnly { get; set; }

		public GeoShapePropertyDescriptor() : base("geo_shape") { }

		public GeoShapePropertyDescriptor<T> Tree(GeoTree tree) => Assign(a => a.Tree = tree);

		public GeoShapePropertyDescriptor<T> TreeLevels(int treeLevels) => Assign(a => a.TreeLevels = treeLevels);

		public GeoShapePropertyDescriptor<T> Precision(double precision, DistanceUnit unit) =>
			Assign(a => a.Precision = new Distance(precision, unit));

		public GeoShapePropertyDescriptor<T> Orientation(GeoOrientation orientation) => Assign(a => a.Orientation = orientation);

		public GeoShapePropertyDescriptor<T> DistanceErrorPercentage(double distanceErrorPercentage) => 
			Assign(a => a.DistanceErrorPercentage = distanceErrorPercentage);

		public GeoShapePropertyDescriptor<T> PointsOnly(bool pointsOnly = true) => Assign(a => a.PointsOnly = pointsOnly);
	}
}