using System;
using Elasticsearch.Net_5_2_0;

namespace Nest_5_2_0
{
	public class DateRangeAttribute : RangePropertyAttributeBase, IDateRangeProperty
	{
		private IDateRangeProperty Self => this;

		public DateRangeAttribute() : base(RangeType.DateRange) { }

		string IDateRangeProperty.Format { get; set; }

		public string Format { get { return Self.Format; } set { Self.Format = value; } }
	}
}
