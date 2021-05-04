// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// The date nanos datatype is similar to <see cref="IDateProperty"/>, except that
	/// internally, the date is stored with nanosecond resolution. This limits its range of
	/// dates from roughly 1970 to 2262.
	/// </summary>
	[InterfaceDataContract]
	public interface IDateNanosProperty : IDocValuesProperty
	{
		/// <summary>
		/// The date format(s) that can be parsed. Defaults to strict_date_optional_time||epoch_millis.
		/// <see cref="DateFormat" />
		/// </summary>
		[DataMember(Name = "format")]
		string Format { get; set; }

		/// <summary>
		/// If true, malformed numbers are ignored. If false (default), malformed numbers throw an exception
		/// and reject the whole document.
		/// </summary>
		[DataMember(Name = "ignore_malformed")]
		bool? IgnoreMalformed { get; set; }

		/// <summary>
		/// Should the field be searchable? Accepts true (default) and false.
		/// </summary>
		[DataMember(Name = "index")]
		bool? Index { get; set; }

		/// <summary>
		/// Accepts a date value in one of the configured format's
		/// as the field which is substituted for any explicit null values. Defaults to null,
		/// which means the field is treated as missing.
		/// </summary>
		[DataMember(Name = "null_value")]
		DateTime? NullValue { get; set; }
	}

	[DebuggerDisplay("{DebugDisplay}")]
	public class DateNanosProperty : DocValuesPropertyBase, IDateNanosProperty
	{
		public DateNanosProperty() : base(FieldType.DateNanos) { }

		/// <inheritdoc />
		public string Format { get; set; }

		/// <inheritdoc />
		public bool? IgnoreMalformed { get; set; }

		/// <inheritdoc />
		public bool? Index { get; set; }

		/// <inheritdoc />
		public DateTime? NullValue { get; set; }

		/// <inheritdoc />
		public int? PrecisionStep { get; set; }
	}

	[DebuggerDisplay("{DebugDisplay}")]
	public class DateNanosPropertyDescriptor<T>
		: DocValuesPropertyDescriptorBase<DateNanosPropertyDescriptor<T>, IDateNanosProperty, T>, IDateNanosProperty
		where T : class
	{
		public DateNanosPropertyDescriptor() : base(FieldType.DateNanos) { }

		string IDateNanosProperty.Format { get; set; }

		bool? IDateNanosProperty.IgnoreMalformed { get; set; }

		bool? IDateNanosProperty.Index { get; set; }

		DateTime? IDateNanosProperty.NullValue { get; set; }

		/// <inheritdoc cref="IDateNanosProperty.Index"/>
		public DateNanosPropertyDescriptor<T> Index(bool? index = true) => Assign(index, (a, v) => a.Index = v);

		/// <inheritdoc cref="IDateNanosProperty.NullValue"/>
		public DateNanosPropertyDescriptor<T> NullValue(DateTime? nullValue) => Assign(nullValue, (a, v) => a.NullValue = v);

		/// <inheritdoc cref="IDateNanosProperty.IgnoreMalformed"/>
		public DateNanosPropertyDescriptor<T> IgnoreMalformed(bool? ignoreMalformed = true) => Assign(ignoreMalformed, (a, v) => a.IgnoreMalformed = v);

		/// <inheritdoc cref="IDateNanosProperty.Format"/>
		public DateNanosPropertyDescriptor<T> Format(string format) => Assign(format, (a, v) => a.Format = v);
	}
}
