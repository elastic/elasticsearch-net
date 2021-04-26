/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// The date datatype maps a field as a date in Elasticsearch.
	/// </summary>
	[InterfaceDataContract]
	public interface IDateProperty : IDocValuesProperty
	{
		[DataMember(Name = "fielddata")]
		INumericFielddata Fielddata { get; set; }

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
	public class DateProperty : DocValuesPropertyBase, IDateProperty
	{
		public DateProperty() : base(FieldType.Date) { }

		/// <inheritdoc />
		public INumericFielddata Fielddata { get; set; }

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
	public class DatePropertyDescriptor<T>
		: DocValuesPropertyDescriptorBase<DatePropertyDescriptor<T>, IDateProperty, T>, IDateProperty
		where T : class
	{
		public DatePropertyDescriptor() : base(FieldType.Date) { }

		INumericFielddata IDateProperty.Fielddata { get; set; }
		string IDateProperty.Format { get; set; }
		bool? IDateProperty.IgnoreMalformed { get; set; }
		bool? IDateProperty.Index { get; set; }
		DateTime? IDateProperty.NullValue { get; set; }

		/// <inheritdoc />
		public DatePropertyDescriptor<T> Index(bool? index = true) => Assign(index, (a, v) => a.Index = v);


		/// <inheritdoc />
		public DatePropertyDescriptor<T> NullValue(DateTime? nullValue) => Assign(nullValue, (a, v) => a.NullValue = v);

		/// <inheritdoc />
		public DatePropertyDescriptor<T> IgnoreMalformed(bool? ignoreMalformed = true) => Assign(ignoreMalformed, (a, v) => a.IgnoreMalformed = v);

		/// <inheritdoc />
		public DatePropertyDescriptor<T> Format(string format) => Assign(format, (a, v) => a.Format = v);

		/// <inheritdoc />
		public DatePropertyDescriptor<T> Fielddata(Func<NumericFielddataDescriptor, INumericFielddata> selector) =>
			Assign(selector(new NumericFielddataDescriptor()), (a, v) => a.Fielddata = v);
	}
}
