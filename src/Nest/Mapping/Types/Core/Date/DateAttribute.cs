using System;

namespace Nest
{
	public class DateAttribute : ElasticsearchPropertyAttributeBase, IDateProperty
	{
		IDateProperty Self => this;

		NonStringIndexOption? IDateProperty.Index { get; set; }
		double? IDateProperty.Boost { get; set; }
		DateTime? IDateProperty.NullValue { get; set; }
		bool? IDateProperty.IncludeInAll { get; set; }
		int? IDateProperty.PrecisionStep { get; set; }
		bool? IDateProperty.IgnoreMalformed { get; set; }
		string IDateProperty.Format { get; set; }
		NumericResolutionUnit? IDateProperty.NumericResolution { get; set; }
		INumericFielddata IDateProperty.Fielddata { get; set; }

		public NonStringIndexOption Index { get { return Self.Index.GetValueOrDefault(); } set { Self.Index = value; } }
		public double Boost { get { return Self.Boost.GetValueOrDefault(); } set { Self.Boost = value; } }
		public DateTime NullValue { get { return Self.NullValue.GetValueOrDefault(); } set { Self.NullValue = value; } }
		public bool IncludeInAll { get { return Self.IncludeInAll.GetValueOrDefault(); } set { Self.IncludeInAll = value; } }
		public int PrecisionStep { get { return Self.PrecisionStep.GetValueOrDefault(); } set { Self.PrecisionStep = value; } }
		public bool IgnoreMalformed { get { return Self.IgnoreMalformed.GetValueOrDefault(); } set { Self.IgnoreMalformed = value; } }
		public string Format { get { return Self.Format; } set { Self.Format = value; } }
		public NumericResolutionUnit NumericResolution { get { return Self.NumericResolution.GetValueOrDefault(); } set { Self.NumericResolution = value; } }

		public DateAttribute() : base("date") { }
	}	
}
