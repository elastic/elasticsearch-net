namespace Nest
{
	public class GeoPointAttribute : ElasticsearchDocValuesPropertyAttributeBase, IGeoPointProperty
	{
		private IGeoPointProperty Self => this;

		public GeoPointAttribute() : base(FieldType.GeoPoint) { }

		bool? IGeoPointProperty.IgnoreMalformed { get; set; }

		bool? IGeoPointProperty.IgnoreZValue { get; set; }
		GeoLocation IGeoPointProperty.NullValue { get; set; }

		public bool IgnoreMalformed { get => Self.IgnoreMalformed.GetValueOrDefault(); set => Self.IgnoreMalformed = value; }
		public bool IgnoreZValue { get => Self.IgnoreZValue.GetValueOrDefault(); set => Self.IgnoreZValue= value; }
	}
}
