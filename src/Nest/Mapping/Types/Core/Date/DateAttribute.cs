using System;

namespace Nest
{
	public class DateAttribute : ElasticsearchDocValuesPropertyAttributeBase, IDateProperty
	{
		private IDateProperty Self => this;

		public DateAttribute() : base(FieldType.Date) { }

		bool? IDateProperty.Index { get; set; }
		double? IDateProperty.Boost { get; set; }
		DateTime? IDateProperty.NullValue { get; set; }
		bool? IDateProperty.IgnoreMalformed { get; set; }
		string IDateProperty.Format { get; set; }
		INumericFielddata IDateProperty.Fielddata { get; set; }

		public bool Index { get => Self.Index.GetValueOrDefault(); set => Self.Index = value; }
		public double Boost { get => Self.Boost.GetValueOrDefault(); set => Self.Boost = value; }
		public DateTime NullValue { get => Self.NullValue.GetValueOrDefault(); set => Self.NullValue = value; }
		public bool IgnoreMalformed { get => Self.IgnoreMalformed.GetValueOrDefault(); set => Self.IgnoreMalformed = value; }
		public string Format { get => Self.Format; set => Self.Format = value; }

	}
}
