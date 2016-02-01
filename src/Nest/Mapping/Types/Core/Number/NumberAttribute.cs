namespace Nest
{
	public class NumberAttribute : ElasticsearchPropertyAttributeBase, INumberProperty
	{
		INumberProperty Self => this;

		NonStringIndexOption? INumberProperty.Index { get; set; }
		double? INumberProperty.Boost { get; set; }
		double? INumberProperty.NullValue { get; set; }
		bool? INumberProperty.IncludeInAll { get; set; }
		int? INumberProperty.PrecisionStep { get; set; }
		bool? INumberProperty.IgnoreMalformed { get; set; }
		bool? INumberProperty.Coerce { get; set; }
		INumericFielddata INumberProperty.Fielddata { get; set; }

		public NonStringIndexOption Index { get { return Self.Index.GetValueOrDefault(); } set { Self.Index = value; } }
		public double Boost { get { return Self.Boost.GetValueOrDefault(); } set { Self.Boost = value; } }
		public double NullValue { get { return Self.NullValue.GetValueOrDefault(); } set { Self.NullValue = value; } }
		public bool IncludeInAll { get { return Self.IncludeInAll.GetValueOrDefault(); } set { Self.IncludeInAll = value; } }
		public int PrecisionStep { get { return Self.PrecisionStep.GetValueOrDefault(); } set { Self.PrecisionStep = value; } }
		public bool IgnoreMalformed { get { return Self.IgnoreMalformed.GetValueOrDefault(); } set { Self.IgnoreMalformed = value; } }
		public bool Coerce { get { return Self.Coerce.GetValueOrDefault(); } set { Self.Coerce = value; } }

		public NumberAttribute(NumberType type) : base(type.GetStringValue()) { }
		protected NumberAttribute(string type) : base(type) { }
		public NumberAttribute() : base(NumberType.Double.GetStringValue()) { }
	}
}
