namespace Nest
{
	public class GeoPointAttribute : ElasticsearchDocValuesPropertyAttributeBase, IGeoPointProperty
	{
		public GeoPointAttribute() : base(FieldType.GeoPoint) { }

		public bool IgnoreMalformed
		{
			get => Self.IgnoreMalformed.GetValueOrDefault();
			set => Self.IgnoreMalformed = value;
		}

		bool? IGeoPointProperty.IgnoreMalformed { get; set; }
		private IGeoPointProperty Self => this;
	}
}
