using System;
using Elasticsearch.Net;

namespace Nest
{
	public class DateRangeAttribute : RangePropertyAttributeBase, IDateRangeProperty
	{
		private IDateRangeProperty Self => this;

		public DateRangeAttribute() : base(RangeType.DateRange) { }

		DateTime? IDateRangeProperty.NullValue { get; set; }
		bool? IDateRangeProperty.IgnoreMalformed { get; set; }
		string IDateRangeProperty.Format { get; set; }
		INumericFielddata IDateRangeProperty.Fielddata { get; set; }

		public DateTime NullValue { get { return Self.NullValue.GetValueOrDefault(); } set { Self.NullValue = value; } }
		public bool IgnoreMalformed { get { return Self.IgnoreMalformed.GetValueOrDefault(); } set { Self.IgnoreMalformed = value; } }
		public string Format { get { return Self.Format; } set { Self.Format = value; } }
	}
}
