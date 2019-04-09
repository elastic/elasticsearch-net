using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
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
		string TimeZone { get; set; }
	}

	public class DateProcessor : ProcessorBase, IDateProcessor
	{
		public Field Field { get; set; }

		public IEnumerable<string> Formats { get; set; }

		public string Locale { get; set; }

		public Field TargetField { get; set; }

		public string TimeZone { get; set; }
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

		public DateProcessorDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		public DateProcessorDescriptor<T> Field(Expression<Func<T, object>> objectPath) =>
			Assign(objectPath, (a, v) => a.Field = v);

		public DateProcessorDescriptor<T> TargetField(Field field) => Assign(field, (a, v) => a.TargetField = v);

		public DateProcessorDescriptor<T> TargetField(Expression<Func<T, object>> objectPath) =>
			Assign(objectPath, (a, v) => a.TargetField = v);

		public DateProcessorDescriptor<T> Formats(IEnumerable<string> matchFormats) => Assign(matchFormats, (a, v) => a.Formats = v);

		public DateProcessorDescriptor<T> Formats(params string[] matchFormats) => Assign(matchFormats, (a, v) => a.Formats = v);

		public DateProcessorDescriptor<T> TimeZone(string timezone) => Assign(timezone, (a, v) => a.TimeZone = v);

		public DateProcessorDescriptor<T> Locale(string locale) => Assign(locale, (a, v) => a.Locale = v);
	}
}
