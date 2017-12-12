using System;

namespace Nest
{
	public class ObjectAttribute : ElasticsearchCorePropertyAttributeBase, IObjectProperty
	{
		private IObjectProperty Self => this;

		public ObjectAttribute() : base(FieldType.Object) { }
		protected ObjectAttribute(FieldType type) : base(type) { }

		Union<bool, DynamicMapping> IObjectProperty.Dynamic { get; set; }
		bool? IObjectProperty.Enabled { get; set; }
		IProperties IObjectProperty.Properties { get; set; }

		public bool Enabled { get => Self.Enabled.GetValueOrDefault(); set => Self.Enabled = value; }
	}
}
