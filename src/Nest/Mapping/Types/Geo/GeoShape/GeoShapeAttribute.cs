namespace Nest
{
	/// <inheritdoc cref="IGeoShapeProperty"/>
	public class GeoShapeAttribute : ElasticsearchDocValuesPropertyAttributeBase, IGeoShapeProperty
	{
		private IGeoShapeProperty Self => this;

		public GeoShapeAttribute() : base(FieldType.GeoShape) { }

		GeoTree? IGeoShapeProperty.Tree { get; set; }
		Distance IGeoShapeProperty.Precision { get; set; }
		GeoOrientation? IGeoShapeProperty.Orientation { get; set; }
		int? IGeoShapeProperty.TreeLevels { get; set; }
		GeoStrategy? IGeoShapeProperty.Strategy { get; set; }
		double? IGeoShapeProperty.DistanceErrorPercentage { get; set; }
		bool? IGeoShapeProperty.PointsOnly { get; set; }
		bool? IGeoShapeProperty.IgnoreMalformed { get; set; }
		bool? IGeoShapeProperty.IgnoreZValue { get; set; }

		/// <inheritdoc cref="IGeoShapeProperty.Tree"/>
		public GeoTree Tree { get => Self.Tree.GetValueOrDefault(GeoTree.Geohash); set => Self.Tree = value; }
		/// <inheritdoc cref="IGeoShapeProperty.Orientation"/>
		public GeoOrientation Orientation { get => Self.Orientation.GetValueOrDefault(GeoOrientation.CounterClockWise); set => Self.Orientation = value; }
		/// <inheritdoc cref="IGeoShapeProperty.TreeLevels"/>
		public int TreeLevels { get => Self.TreeLevels.GetValueOrDefault(50); set => Self.TreeLevels = value; }
		/// <inheritdoc cref="IGeoShapeProperty.Strategy"/>
		public GeoStrategy Strategy {  get => Self.Strategy.GetValueOrDefault(GeoStrategy.Recursive); set => Self.Strategy = value; }
		/// <inheritdoc cref="IGeoShapeProperty.DistanceErrorPercentage"/>
		public double DistanceErrorPercentage
		{
			get => Self.Precision != null | Self.TreeLevels != null
				? Self.DistanceErrorPercentage.GetValueOrDefault(0)
				: Self.DistanceErrorPercentage.GetValueOrDefault(0.025);
			set => Self.DistanceErrorPercentage = value;
		}
		/// <inheritdoc cref="IGeoShapeProperty.PointsOnly"/>
		public bool PointsOnly { get => Self.PointsOnly.GetValueOrDefault(false); set => Self.PointsOnly = value; }
		/// <inheritdoc cref="IGeoShapeProperty.IgnoreMalformed"/>
		public bool IgnoreMalformed { get => Self.IgnoreMalformed.GetValueOrDefault(false); set => Self.IgnoreMalformed = value; }
		/// <inheritdoc cref="IGeoShapeProperty.IgnoreZValue"/>
		public bool IgnoreZValue { get => Self.IgnoreZValue.GetValueOrDefault(true); set => Self.IgnoreZValue = value; }
	}
}
