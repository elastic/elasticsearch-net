namespace Nest
{
	public class GeoShapeAttribute : ElasticsearchDocValuesPropertyAttributeBase, IGeoShapeProperty
	{
		public GeoShapeAttribute() : base(FieldType.GeoShape) { }

		public double DistanceErrorPercentage
		{
			get => Self.DistanceErrorPercentage.GetValueOrDefault();
			set => Self.DistanceErrorPercentage = value;
		}

		public GeoOrientation Orientation
		{
			get => Self.Orientation.GetValueOrDefault(GeoOrientation.CounterClockWise);
			set => Self.Orientation = value;
		}

		public bool PointsOnly
		{
			get => Self.PointsOnly.GetValueOrDefault();
			set => Self.PointsOnly = value;
		}

		public GeoStrategy Strategy
		{
			get => Self.Strategy.GetValueOrDefault(GeoStrategy.Recursive);
			set => Self.Strategy = value;
		}

		public GeoTree Tree
		{
			get => Self.Tree.GetValueOrDefault(GeoTree.Geohash);
			set => Self.Tree = value;
		}

		public int TreeLevels
		{
			get => Self.TreeLevels.GetValueOrDefault(50);
			set => Self.TreeLevels = value;
		}

		double? IGeoShapeProperty.DistanceErrorPercentage { get; set; }
		GeoOrientation? IGeoShapeProperty.Orientation { get; set; }
		bool? IGeoShapeProperty.PointsOnly { get; set; }
		Distance IGeoShapeProperty.Precision { get; set; }
		private IGeoShapeProperty Self => this;
		GeoStrategy? IGeoShapeProperty.Strategy { get; set; }

		GeoTree? IGeoShapeProperty.Tree { get; set; }
		int? IGeoShapeProperty.TreeLevels { get; set; }
	}
}
