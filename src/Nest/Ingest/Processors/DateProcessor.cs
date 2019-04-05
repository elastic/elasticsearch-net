using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(ProcessorJsonConverter<DateProcessor>))]
	public interface IDateProcessor : IProcessor
	{
		/// <summary>
		/// The field to get the date from.
		/// </summary>
		[JsonProperty("field")]
		Field Field { get; set; }

		/// <summary>
		/// An array of the expected date formats. Can be a Joda pattern or one of
		/// the following formats: ISO8601, UNIX, UNIX_MS, or TAI64N.
		/// </summary>
		[JsonProperty("formats")]
		IEnumerable<string> Formats { get; set; }

		/// <summary>
		/// The locale to use when parsing the date, relevant when parsing month names or week days.
		/// Supports template snippets.
		/// </summary>
		[JsonProperty("locale")]
		string Locale { get; set; }

		/// <summary>
		/// The field that will hold the parsed date. Defaults to @timestamp
		/// </summary>
		[JsonProperty("target_field")]
		Field TargetField { get; set; }

		/// <summary>
		/// The timezone to use when parsing the date. Supports template snippets.
		/// </summary>
		[JsonProperty("timezone")]
		string Timezone { get; set; }
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
		public string Timezone { get; set; }

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
		string IDateProcessor.Timezone { get; set; }

		/// <inheritdoc cref="IDateProcessor.Field" />
		public DateProcessorDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		/// <inheritdoc cref="IDateProcessor.Field" />
		public DateProcessorDescriptor<T> Field(Expression<Func<T, object>> objectPath) =>
			Assign(objectPath, (a, v) => a.Field = v);

		/// <inheritdoc cref="IDateProcessor.TargetField" />
		public DateProcessorDescriptor<T> TargetField(Field field) => Assign(field, (a, v) => a.TargetField = v);

		/// <inheritdoc cref="IDateProcessor.TargetField" />
		public DateProcessorDescriptor<T> TargetField(Expression<Func<T, object>> objectPath) =>
			Assign(objectPath, (a, v) => a.TargetField = v);

		/// <inheritdoc cref="IDateProcessor.Formats" />
		public DateProcessorDescriptor<T> Formats(IEnumerable<string> matchFormats) => Assign(matchFormats, (a, v) => a.Formats = v);

		/// <inheritdoc cref="IDateProcessor.Formats" />
		public DateProcessorDescriptor<T> Formats(params string[] matchFormats) => Assign(matchFormats, (a, v) => a.Formats = v);

		/// <inheritdoc cref="IDateProcessor.Timezone" />
		public DateProcessorDescriptor<T> Timezone(string timezone) => Assign(timezone, (a, v) => a.Timezone = v);

		/// <inheritdoc cref="IDateProcessor.Locale" />
		public DateProcessorDescriptor<T> Locale(string locale) => Assign(locale, (a, v) => a.Locale = v);
	}
}
