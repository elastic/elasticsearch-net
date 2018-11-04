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
