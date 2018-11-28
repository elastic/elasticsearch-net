using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[JsonFormatter(typeof(ProcessorFormatter<DateProcessor>))]
	public interface IDateProcessor : IProcessor
	{
		[DataMember(Name ="field")]
		Field Field { get; set; }

		[DataMember(Name ="formats")]
		IEnumerable<string> Formats { get; set; }

		[DataMember(Name ="locale")]
		string Locale { get; set; }

		[DataMember(Name ="target_field")]
		Field TargetField { get; set; }

		[DataMember(Name ="timezone")]
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

		public DateProcessorDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		public DateProcessorDescriptor<T> Field(Expression<Func<T, object>> objectPath) =>
			Assign(a => a.Field = objectPath);

		public DateProcessorDescriptor<T> TargetField(Field field) => Assign(a => a.TargetField = field);

		public DateProcessorDescriptor<T> TargetField(Expression<Func<T, object>> objectPath) =>
			Assign(a => a.TargetField = objectPath);

		public DateProcessorDescriptor<T> Formats(IEnumerable<string> matchFormats) => Assign(a => a.Formats = matchFormats);

		public DateProcessorDescriptor<T> Formats(params string[] matchFormats) => Assign(a => a.Formats = matchFormats);

		public DateProcessorDescriptor<T> Timezone(string timezone) => Assign(a => a.Timezone = timezone);

		public DateProcessorDescriptor<T> Locale(string locale) => Assign(a => a.Locale = locale);
	}
}
