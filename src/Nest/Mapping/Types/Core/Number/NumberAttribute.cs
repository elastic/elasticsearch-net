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
#pragma warning disable 618
		protected NumberAttribute(string type) : base(type) { }
#pragma warning restore 618
		public double Boost
		{
			get => Self.Boost.GetValueOrDefault();
			set => Self.Boost = value;
		}

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

		/// <remarks>Removed in 6.x</remarks>
		public bool IncludeInAll
		{
			get => Self.IncludeInAll.GetValueOrDefault();
			set => Self.IncludeInAll = value;
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

		double? INumberProperty.Boost { get; set; }
		bool? INumberProperty.Coerce { get; set; }
		INumericFielddata INumberProperty.Fielddata { get; set; }
		bool? INumberProperty.IgnoreMalformed { get; set; }

		/// <remarks>Removed in 6.x</remarks>
		bool? INumberProperty.IncludeInAll { get; set; }

		bool? INumberProperty.Index { get; set; }
		double? INumberProperty.NullValue { get; set; }
		double? INumberProperty.ScalingFactor { get; set; }
		private INumberProperty Self => this;
	}
}
