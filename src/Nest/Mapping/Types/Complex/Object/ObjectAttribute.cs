// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
