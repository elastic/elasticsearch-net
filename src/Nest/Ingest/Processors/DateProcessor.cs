// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	public interface IDateProcessor : IProcessor
	{
		/// <summary>
		/// The field to get the date from.
		/// </summary>
		[DataMember(Name ="field")]
		Field Field { get; set; }

		/// <summary>
		/// An array of the expected date formats. Can be a Joda pattern or one of
		/// the following formats: ISO8601, UNIX, UNIX_MS, or TAI64N.
		/// </summary>
		[DataMember(Name ="formats")]
		IEnumerable<string> Formats { get; set; }

		/// <summary>
		/// The locale to use when parsing the date, relevant when parsing month names or week days.
		/// Supports template snippets.
		/// </summary>
		[DataMember(Name ="locale")]
		string Locale { get; set; }

		/// <summary>
		/// The field that will hold the parsed date. Defaults to @timestamp
		/// </summary>
		[DataMember(Name ="target_field")]
		Field TargetField { get; set; }

		/// <summary>
		/// The timezone to use when parsing the date. Supports template snippets.
		/// </summary>
		[DataMember(Name ="timezone")]
		string TimeZone { get; set; }
	}

	public class DateProcessor : ProcessorBase, IDateProcessor
	{
		/// <inheritdoc />
		public Field Field { get; set; }

		/// <inheritdoc />
		public IEnumerable<string> Formats { get; set; }

		/// <inheritdoc />
		public string Locale { get; set; }

		/// <inheritdoc />
		public Field TargetField { get; set; }

		/// <inheritdoc />
		public string TimeZone { get; set; }

		/// <inheritdoc />
		protected override string Name => "date";
	}

	public class DateProcessorDescriptor<T>
		: ProcessorDescriptorBase<DateProcessorDescriptor<T>, IDateProcessor>, IDateProcessor
		where T : class
	{
		protected override string Name => "date";

		Field IDateProcessor.Field { get; set; }
		IEnumerable<string> IDateProcessor.Formats { get; set; }
		string IDateProcessor.Locale { get; set; }
		Field IDateProcessor.TargetField { get; set; }
		string IDateProcessor.TimeZone { get; set; }

		/// <inheritdoc cref="IDateProcessor.Field" />
		public DateProcessorDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		/// <inheritdoc cref="IDateProcessor.Field" />
		public DateProcessorDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.Field = v);

		/// <inheritdoc cref="IDateProcessor.TargetField" />
		public DateProcessorDescriptor<T> TargetField(Field field) => Assign(field, (a, v) => a.TargetField = v);

		/// <inheritdoc cref="IDateProcessor.TargetField" />
		public DateProcessorDescriptor<T> TargetField<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.TargetField = v);

		/// <inheritdoc cref="IDateProcessor.Formats" />
		public DateProcessorDescriptor<T> Formats(IEnumerable<string> matchFormats) => Assign(matchFormats, (a, v) => a.Formats = v);

		/// <inheritdoc cref="IDateProcessor.Formats" />
		public DateProcessorDescriptor<T> Formats(params string[] matchFormats) => Assign(matchFormats, (a, v) => a.Formats = v);

		/// <inheritdoc cref="IDateProcessor.TimeZone" />
		public DateProcessorDescriptor<T> TimeZone(string timezone) => Assign(timezone, (a, v) => a.TimeZone = v);

		/// <inheritdoc cref="IDateProcessor.Locale" />
		public DateProcessorDescriptor<T> Locale(string locale) => Assign(locale, (a, v) => a.Locale = v);
	}
}
