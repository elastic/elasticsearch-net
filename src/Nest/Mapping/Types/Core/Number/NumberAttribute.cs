using Elasticsearch.Net;

namespace Nest
{
	/// <summary>
	/// Maps a property as a number type. If no type is specified,
	/// the default type is float (single precision floating point).
	/// </summary>
	public class NumberAttribute : ElasticsearchDocValuesPropertyAttributeBase, INumberProperty
	{
		private INumberProperty Self => this;

		public NumberAttribute() : base(FieldType.Float) { }
		public NumberAttribute(NumberType type) : base(type.ToFieldType()) { }

		bool? INumberProperty.Index { get; set; }
		double? INumberProperty.Boost { get; set; }
		double? INumberProperty.NullValue { get; set; }
		bool? INumberProperty.IgnoreMalformed { get; set; }
		bool? INumberProperty.Coerce { get; set; }
		INumericFielddata INumberProperty.Fielddata { get; set; }
		double? INumberProperty.ScalingFactor { get; set; }

		public bool Index { get => Self.Index.GetValueOrDefault(); set => Self.Index = value; }
		public double Boost { get => Self.Boost.GetValueOrDefault(); set => Self.Boost = value; }
		public double NullValue { get => Self.NullValue.GetValueOrDefault(); set => Self.NullValue = value; }
		public bool IgnoreMalformed { get => Self.IgnoreMalformed.GetValueOrDefault(); set => Self.IgnoreMalformed = value; }
		public bool Coerce { get => Self.Coerce.GetValueOrDefault(); set => Self.Coerce = value; }
		public double ScalingFactor { get => Self.ScalingFactor.GetValueOrDefault(); set => Self.ScalingFactor = value; }

	}
}
