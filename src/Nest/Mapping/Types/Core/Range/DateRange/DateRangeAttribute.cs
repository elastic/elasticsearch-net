using System;
using Elasticsearch.Net;

namespace Nest
{
	public class DateRangeAttribute : RangePropertyAttributeBase, IDateRangeProperty
	{
		private IDateRangeProperty Self => this;

		public DateRangeAttribute() : base(RangeType.DateRange) { }

		string IDateRangeProperty.Format { get; set; }

		public string Format { get => Self.Format; set => Self.Format = value; }
	}
}
