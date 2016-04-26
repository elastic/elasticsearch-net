using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IDateProcessor : IProcessor
	{
		[JsonProperty("field")]
		Field Field { get; set; }

		[JsonProperty("target_field")]
		Field TargetField { get; set; }

		[JsonProperty("match_formats")]
		string MatchFormats { get; set; }

		[JsonProperty("timezone")]
		string Timezone { get; set; }

		[JsonProperty("locale")]
		string Locale { get; set; }
	}

	public class DateProcessor : ProcessorBase, IDateProcessor
	{
		protected override string Name => "date";

		public Field Field { get; set; }

		public string Locale { get; set; }

		public string MatchFormats { get; set; }

		public Field TargetField { get; set; }

		public string Timezone { get; set; }
	}

	public class DateProcessorDescriptor<T>
		: ProcessorDescriptorBase<DateProcessorDescriptor<T>, IDateProcessor>, IDateProcessor
		where T : class
	{
		protected override string Name => "date";

		Field IDateProcessor.Field { get; set; }

		string IDateProcessor.Locale { get; set; }

		string IDateProcessor.MatchFormats { get; set; }

		Field IDateProcessor.TargetField { get; set; }

		string IDateProcessor.Timezone { get; set; }

		public DateProcessorDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		public DateProcessorDescriptor<T> Field(Expression<Func<T, object>> objectPath) =>
			Assign(a => a.Field = objectPath);

		public DateProcessorDescriptor<T> TargetField(Field field) => Assign(a => a.TargetField = field);

		public DateProcessorDescriptor<T> TargetField(Expression<Func<T, object>> objectPath) =>
			Assign(a => a.TargetField = objectPath);

		public DateProcessorDescriptor<T> MatchFormats(string matchFormats) => Assign(a => a.MatchFormats = matchFormats);

		public DateProcessorDescriptor<T> Timezone(string timezone) => Assign(a => a.Timezone = timezone);

		public DateProcessorDescriptor<T> Locale(string locale) => Assign(a => a.Locale = locale);
	}
}
