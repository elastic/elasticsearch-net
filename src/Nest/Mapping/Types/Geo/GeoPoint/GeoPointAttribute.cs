namespace Nest
{
	public class GeoPointAttribute : ElasticsearchDocValuesPropertyAttributeBase, IGeoPointProperty
	{
		IGeoPointProperty Self => this;

		bool? IGeoPointProperty.IgnoreMalformed { get; set; }

		public bool IgnoreMalformed { get { return Self.IgnoreMalformed.GetValueOrDefault(); } set { Self.IgnoreMalformed = value; } }
		public GeoPointAttribute() : base("geo_point") { }
	}
}
