using System;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{

	/// <summary>
	/// A range of date values represented as unsigned 64-bit integer milliseconds elapsed since system epoch.
	/// </summary>
	[JsonObject(MemberSerialization.OptIn)]
	public interface IDateRangeProperty : IRangeProperty
	{
		/// <summary>
		/// Accepts a date value in one of the configured format's
		/// as the field which is substituted for any explicit null values. Defaults to null,
		/// which means the field is treated as missing.
		/// </summary>
		[JsonProperty("null_value")]
        DateTime? NullValue { get; set; }

		/// <summary>
		/// If true, malformed numbers are ignored. If false (default), malformed numbers throw an exception
		/// and reject the whole document.
		/// </summary>
		[JsonProperty("ignore_malformed")]
		bool? IgnoreMalformed { get; set; }

		/// <summary>
		/// The date format(s) that can be parsed. Defaults to strict_date_optional_time||epoch_millis.
		/// <see cref="DateFormat"/>
		/// </summary>
		[JsonProperty("format")]
		string Format { get; set; }

		[JsonProperty("fielddata")]
		INumericFielddata Fielddata { get; set; }
	}

	/// <inheritdoc/>
	public class DateRangeProperty : RangePropertyBase, IDateRangeProperty
	{
		public DateRangeProperty() : base(RangeType.DateRange) { }

		/// <inheritdoc/>
		public DateTime? NullValue { get; set; }

		/// <inheritdoc/>
		public bool? IgnoreMalformed { get; set; }

		/// <inheritdoc/>
		public string Format { get; set; }

		/// <inheritdoc/>
		public INumericFielddata Fielddata { get; set; }
	}

	/// <inheritdoc/>
	public class DateRangePropertyDescriptor<T>
		: RangePropertyDescriptorBase<DateRangePropertyDescriptor<T>, IDateRangeProperty, T>, IDateRangeProperty
		where T : class
	{
		public DateRangePropertyDescriptor() : base(RangeType.DateRange) { }

		DateTime? IDateRangeProperty.NullValue { get; set; }
		bool? IDateRangeProperty.IgnoreMalformed { get; set; }
		string IDateRangeProperty.Format { get; set; }
		INumericFielddata IDateRangeProperty.Fielddata { get; set; }

		/// <inheritdoc/>
		public DateRangePropertyDescriptor<T> NullValue(DateTime? nullValue) => Assign(a => a.NullValue = nullValue);
		/// <inheritdoc/>
		public DateRangePropertyDescriptor<T> IgnoreMalformed(bool? ignore = true) => Assign(a => a.IgnoreMalformed = ignore);
		/// <inheritdoc/>
		public DateRangePropertyDescriptor<T> Format(string format) => Assign(a => a.Format = format);
		/// <inheritdoc/>
		public DateRangePropertyDescriptor<T> Fielddata(INumericFielddata fielddata) => Assign(a => a.Fielddata = fielddata);
	}
}
