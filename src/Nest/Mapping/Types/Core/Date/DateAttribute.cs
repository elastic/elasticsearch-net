using System;

namespace Nest
{
	public class DateAttribute : ElasticsearchDocValuesPropertyAttributeBase, IDateProperty
	{
		public DateAttribute() : base(FieldType.Date) { }

		public double Boost
		{
			get => Self.Boost.GetValueOrDefault();
			set => Self.Boost = value;
		}

		public string Format
		{
			get => Self.Format;
			set => Self.Format = value;
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

		public DateTime NullValue
		{
			get => Self.NullValue.GetValueOrDefault();
			set => Self.NullValue = value;
		}

		double? IDateProperty.Boost { get; set; }
		INumericFielddata IDateProperty.Fielddata { get; set; }
		string IDateProperty.Format { get; set; }
		bool? IDateProperty.IgnoreMalformed { get; set; }

		/// <remarks>Removed in 6.x</remarks>
		bool? IDateProperty.IncludeInAll { get; set; }

		bool? IDateProperty.Index { get; set; }
		DateTime? IDateProperty.NullValue { get; set; }
		private IDateProperty Self => this;
	}
}
