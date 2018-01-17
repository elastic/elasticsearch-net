using System;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IDateProperty : IDocValuesProperty
	{
		/// <summary>
		/// Should the field be searchable? Accepts true (default) and false.
		/// </summary>
		[JsonProperty("index")]
		bool? Index { get; set; }

		/// <summary>
		/// Mapping field-level query time boosting. Accepts a floating point number, defaults to 1.0.
		/// </summary>
		[JsonProperty("boost")]
        double? Boost { get; set; }

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

	[DebuggerDisplay("{DebugDisplay}")]
	public class DateProperty : DocValuesPropertyBase, IDateProperty
	{
		public DateProperty() : base(FieldType.Date) { }

		/// <inheritdoc/>
		public bool? Index { get; set; }
		/// <inheritdoc/>
		public double? Boost { get; set; }
		/// <inheritdoc/>
		public DateTime? NullValue { get; set; }
		/// <inheritdoc/>
		public int? PrecisionStep { get; set; }
		/// <inheritdoc/>
		public bool? IgnoreMalformed { get; set; }
		/// <inheritdoc/>
		public string Format { get; set; }
		/// <inheritdoc/>
		public INumericFielddata Fielddata { get; set; }
	}

	[DebuggerDisplay("{DebugDisplay}")]
	public class DatePropertyDescriptor<T>
		: DocValuesPropertyDescriptorBase<DatePropertyDescriptor<T>, IDateProperty, T>, IDateProperty
		where T : class
	{
		bool? IDateProperty.Index { get; set; }
		double? IDateProperty.Boost { get; set; }
		DateTime? IDateProperty.NullValue { get; set; }
		bool? IDateProperty.IgnoreMalformed { get; set; }
		string IDateProperty.Format { get; set; }
		INumericFielddata IDateProperty.Fielddata { get; set; }

		public DatePropertyDescriptor() : base(FieldType.Date) { }

		/// <inheritdoc/>
		public DatePropertyDescriptor<T> Index(bool? index = true) => Assign(a => a.Index = index);
		/// <inheritdoc/>
		public DatePropertyDescriptor<T> Boost(double? boost) => Assign(a => a.Boost = boost);
		/// <inheritdoc/>
		public DatePropertyDescriptor<T> NullValue(DateTime? nullValue) => Assign(a => a.NullValue = nullValue);
		/// <inheritdoc/>
		public DatePropertyDescriptor<T> IgnoreMalformed(bool? ignoreMalformed = true) => Assign(a => a.IgnoreMalformed = ignoreMalformed);
		/// <inheritdoc/>
		public DatePropertyDescriptor<T> Format(string format) => Assign(a => a.Format = format);
		/// <inheritdoc/>
		public DatePropertyDescriptor<T> Fielddata(Func<NumericFielddataDescriptor, INumericFielddata> selector) =>
			Assign(a => a.Fielddata = selector(new NumericFielddataDescriptor()));
	}
}
