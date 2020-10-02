// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	public class BooleanAttribute : ElasticsearchDocValuesPropertyAttributeBase, IBooleanProperty
	{
		public BooleanAttribute() : base(FieldType.Boolean) { }

		public bool Index
		{
			get => Self.Index.GetValueOrDefault();
			set => Self.Index = value;
		}

		public bool NullValue
		{
			get => Self.NullValue.GetValueOrDefault();
			set => Self.NullValue = value;
		}

		INumericFielddata IBooleanProperty.Fielddata { get; set; }

		bool? IBooleanProperty.Index { get; set; }
		bool? IBooleanProperty.NullValue { get; set; }
		private IBooleanProperty Self => this;
	}
}
