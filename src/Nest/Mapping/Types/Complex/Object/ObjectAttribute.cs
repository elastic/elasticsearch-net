using System;

namespace Nest
{
	public class ObjectAttribute : ElasticsearchCorePropertyAttributeBase, IObjectProperty
	{
		IObjectProperty Self => this;

		Union<bool, DynamicMapping> IObjectProperty.Dynamic { get; set; }
		bool? IObjectProperty.Enabled { get; set; }
		bool? IObjectProperty.IncludeInAll { get; set; }
		IProperties IObjectProperty.Properties { get; set; }

		public bool Enabled { get { return Self.Enabled.GetValueOrDefault(); } set { Self.Enabled = value; } }
		public bool IncludeInAll { get { return Self.IncludeInAll.GetValueOrDefault(); } set { Self.IncludeInAll = value; } }

		public ObjectAttribute() : base("object") { }
		protected ObjectAttribute(string typeName) : base(typeName) { }
		protected ObjectAttribute(Type type) : base(type) { }
	}
}
