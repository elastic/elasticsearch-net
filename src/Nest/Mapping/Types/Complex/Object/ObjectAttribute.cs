using System;

namespace Nest
{
	public class ObjectAttribute : ElasticsearchCorePropertyAttributeBase, IObjectProperty
	{
		private IObjectProperty Self => this;

		public ObjectAttribute() : base(FieldType.Object) { }
#pragma warning disable 618
		protected ObjectAttribute(string typeName) : base(typeName) { }
		protected ObjectAttribute(Type type) : base(type) { }
#pragma warning restore 618

		Union<bool, DynamicMapping> IObjectProperty.Dynamic { get; set; }
		bool? IObjectProperty.Enabled { get; set; }
		bool? IObjectProperty.IncludeInAll { get; set; }
		IProperties IObjectProperty.Properties { get; set; }

		public bool Enabled { get { return Self.Enabled.GetValueOrDefault(); } set { Self.Enabled = value; } }
		public bool IncludeInAll { get { return Self.IncludeInAll.GetValueOrDefault(); } set { Self.IncludeInAll = value; } }

	}
}
