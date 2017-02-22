namespace Nest_5_2_0
{
	public class GeoPointAttribute : ElasticsearchDocValuesPropertyAttributeBase, IGeoPointProperty
	{
		private IGeoPointProperty Self => this;

		public GeoPointAttribute() : base(FieldType.GeoPoint) { }

		bool? IGeoPointProperty.IgnoreMalformed { get; set; }

		public bool IgnoreMalformed { get { return Self.IgnoreMalformed.GetValueOrDefault(); } set { Self.IgnoreMalformed = value; } }
	}
}
