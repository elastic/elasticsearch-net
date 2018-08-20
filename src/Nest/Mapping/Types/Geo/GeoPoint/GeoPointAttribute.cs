namespace Nest
{
	public class GeoPointAttribute : ElasticsearchDocValuesPropertyAttributeBase, IGeoPointProperty
	{
		private IGeoPointProperty Self => this;

		public GeoPointAttribute() : base(FieldType.GeoPoint) { }

		bool? IGeoPointProperty.IgnoreMalformed { get; set; }
		bool? IGeoPointProperty.IgnoreZValue { get; set; }
		GeoLocation IGeoPointProperty.NullValue { get; set; }

		/// <inheritdoc cref="IGeoPointProperty.IgnoreMalformed"/>
		public bool IgnoreMalformed { get => Self.IgnoreMalformed.GetValueOrDefault(); set => Self.IgnoreMalformed = value; }

		/// <inheritdoc cref="IGeoPointProperty.IgnoreZValue"/>
		public bool IgnoreZValue { get => Self.IgnoreZValue.GetValueOrDefault(true); set => Self.IgnoreZValue= value; }
	}
}
