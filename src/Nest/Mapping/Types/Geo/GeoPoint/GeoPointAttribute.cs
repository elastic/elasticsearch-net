namespace Nest
{
	public class GeoPointAttribute : ElasticsearchDocValuesPropertyAttributeBase, IGeoPointProperty
	{
		private IGeoPointProperty Self => this;

		public GeoPointAttribute() : base(FieldType.GeoPoint) { }

		bool? IGeoPointProperty.IgnoreMalformed { get; set; }

		public bool IgnoreMalformed { get => Self.IgnoreMalformed.GetValueOrDefault(); set => Self.IgnoreMalformed = value; }
	}
}
