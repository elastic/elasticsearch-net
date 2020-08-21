// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Nest
{
	public class DateRangeAttribute : RangePropertyAttributeBase, IDateRangeProperty
	{
		public DateRangeAttribute() : base(RangeType.DateRange) { }

		public string Format
		{
			get => Self.Format;
			set => Self.Format = value;
		}

		string IDateRangeProperty.Format { get; set; }
		private IDateRangeProperty Self => this;
	}
}
