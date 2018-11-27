using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// A range of date values represented as unsigned 64-bit integer milliseconds elapsed since system epoch.
	/// </summary>
	[DataContract]
	public interface IDateRangeProperty : IRangeProperty
	{
		/// <summary>
		/// The date format(s) that can be parsed. Defaults to strict_date_optional_time||epoch_millis.
		/// <see cref="DateFormat" />
		/// </summary>
		[DataMember(Name ="format")]
		string Format { get; set; }
	}

	/// <inheritdoc />
	public class DateRangeProperty : RangePropertyBase, IDateRangeProperty
	{
		public DateRangeProperty() : base(RangeType.DateRange) { }

		/// <inheritdoc />
		public string Format { get; set; }
	}

	/// <inheritdoc />
	public class DateRangePropertyDescriptor<T>
		: RangePropertyDescriptorBase<DateRangePropertyDescriptor<T>, IDateRangeProperty, T>, IDateRangeProperty
		where T : class
	{
		public DateRangePropertyDescriptor() : base(RangeType.DateRange) { }

		string IDateRangeProperty.Format { get; set; }

		/// <inheritdoc />
		public DateRangePropertyDescriptor<T> Format(string format) => Assign(a => a.Format = format);
	}
}
