// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	/// <summary>
	/// Maps a property as a number type. If no type is specified,
	/// the default type is float (single precision floating point).
	/// </summary>
	public class NumberAttribute : ElasticsearchDocValuesPropertyAttributeBase, INumberProperty
	{
		public NumberAttribute() : base(FieldType.Float) { }

		public NumberAttribute(NumberType type) : base(type.ToFieldType()) { }

		public bool Coerce
		{
			get => Self.Coerce.GetValueOrDefault();
			set => Self.Coerce = value;
		}

		public bool IgnoreMalformed
		{
			get => Self.IgnoreMalformed.GetValueOrDefault();
			set => Self.IgnoreMalformed = value;
		}

		public bool Index
		{
			get => Self.Index.GetValueOrDefault();
			set => Self.Index = value;
		}

		public double NullValue
		{
			get => Self.NullValue.GetValueOrDefault();
			set => Self.NullValue = value;
		}

		public double ScalingFactor
		{
			get => Self.ScalingFactor.GetValueOrDefault();
			set => Self.ScalingFactor = value;
		}

		bool? INumberProperty.Coerce { get; set; }
		INumericFielddata INumberProperty.Fielddata { get; set; }
		bool? INumberProperty.IgnoreMalformed { get; set; }

		bool? INumberProperty.Index { get; set; }
		double? INumberProperty.NullValue { get; set; }
		double? INumberProperty.ScalingFactor { get; set; }
		private INumberProperty Self => this;
	}
}
