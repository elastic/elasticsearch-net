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
		[JsonProperty("field")]
		Field Field { get; set; }

		[JsonProperty("formats")]
		IEnumerable<string> Formats { get; set; }

		[JsonProperty("locale")]
		string Locale { get; set; }

		[JsonProperty("target_field")]
		Field TargetField { get; set; }

		[JsonProperty("timezone")]
		string Timezone { get; set; }
	}

	public class DateProcessor : ProcessorBase, IDateProcessor
	{
		public Field Field { get; set; }

		public IEnumerable<string> Formats { get; set; }

		public string Locale { get; set; }

		public Field TargetField { get; set; }

		public string Timezone { get; set; }
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

		public DateProcessorDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		public DateProcessorDescriptor<T> Field(Expression<Func<T, object>> objectPath) =>
			Assign(objectPath, (a, v) => a.Field = v);

		public DateProcessorDescriptor<T> TargetField(Field field) => Assign(field, (a, v) => a.TargetField = v);

		public DateProcessorDescriptor<T> TargetField(Expression<Func<T, object>> objectPath) =>
			Assign(objectPath, (a, v) => a.TargetField = v);

		public DateProcessorDescriptor<T> Formats(IEnumerable<string> matchFormats) => Assign(matchFormats, (a, v) => a.Formats = v);

		public DateProcessorDescriptor<T> Formats(params string[] matchFormats) => Assign(matchFormats, (a, v) => a.Formats = v);

		public DateProcessorDescriptor<T> Timezone(string timezone) => Assign(timezone, (a, v) => a.Timezone = v);

		public DateProcessorDescriptor<T> Locale(string locale) => Assign(locale, (a, v) => a.Locale = v);
	}
}
