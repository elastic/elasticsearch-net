namespace Nest
{
	public class ObjectAttribute : ElasticsearchCorePropertyAttributeBase, IObjectProperty
	{
		public ObjectAttribute() : base(FieldType.Object) { }

		protected ObjectAttribute(FieldType type) : base(type) { }

		public bool Enabled
		{
			get => Self.Enabled.GetValueOrDefault();
			set => Self.Enabled = value;
		}

		Union<bool, DynamicMapping> IObjectProperty.Dynamic { get; set; }
		bool? IObjectProperty.Enabled { get; set; }
		IProperties IObjectProperty.Properties { get; set; }
		private IObjectProperty Self => this;
	}
}
