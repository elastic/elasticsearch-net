using System;

namespace Nest
{
	public class ObjectAttribute : ElasticsearchCorePropertyAttributeBase, IObjectProperty
	{
		public ObjectAttribute() : base(FieldType.Object) { }

		public bool Enabled
		{
			get => Self.Enabled.GetValueOrDefault();
			set => Self.Enabled = value;
		}

		/// <remarks>Removed in 6.x</remarks>
		public bool IncludeInAll
		{
			get => Self.IncludeInAll.GetValueOrDefault();
			set => Self.IncludeInAll = value;
		}

		Union<bool, DynamicMapping> IObjectProperty.Dynamic { get; set; }
		bool? IObjectProperty.Enabled { get; set; }

		/// <remarks>Removed in 6.x</remarks>
		bool? IObjectProperty.IncludeInAll { get; set; }

		IProperties IObjectProperty.Properties { get; set; }
		private IObjectProperty Self => this;
#pragma warning disable 618
		protected ObjectAttribute(string typeName) : base(typeName) { }

		protected ObjectAttribute(Type type) : base(type) { }
#pragma warning restore 618
	}
}
