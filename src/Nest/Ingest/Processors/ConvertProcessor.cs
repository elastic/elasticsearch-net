using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(ProcessorJsonConverter<ConvertProcessor>))]
	public interface IConvertProcessor : IProcessor
	{
		[JsonProperty("field")]
		Field Field { get; set; }

		[JsonProperty("target_field")]
		Field TargetField { get; set; }

		[JsonProperty("type")]
		ConvertProcessorType? Type { get; set; }
	}

	public class ConvertProcessor : ProcessorBase, IConvertProcessor
	{
		protected override string Name => "convert";
		public Field Field { get; set; }
		public Field TargetField { get; set; }
		public ConvertProcessorType? Type { get; set; }
	}

	public class ConvertProcessorDescriptor<T> : ProcessorDescriptorBase<ConvertProcessorDescriptor<T>, IConvertProcessor>, IConvertProcessor
		where T : class
	{
		protected override string Name => "convert";
		Field IConvertProcessor.Field { get; set; }
		Field IConvertProcessor.TargetField { get; set; }
		ConvertProcessorType? IConvertProcessor.Type { get; set; }

		public ConvertProcessorDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		public ConvertProcessorDescriptor<T> Field(Expression<Func<T, object>> objectPath) =>
			Assign(a => a.Field = objectPath);

		public ConvertProcessorDescriptor<T> TargetField(Field field) => Assign(a => a.TargetField = field);

		public ConvertProcessorDescriptor<T> TargetField(Expression<Func<T, object>> objectPath) =>
			Assign(a => a.TargetField = objectPath);

		public ConvertProcessorDescriptor<T> Type(ConvertProcessorType? type) => Assign(a => a.Type = type);
	}

	[JsonConverter(typeof(StringEnumConverter))]
	public enum ConvertProcessorType
	{
		[EnumMember(Value="integer")]
		Integer,
		[EnumMember(Value="float")]
		Float,
		[EnumMember(Value="string")]
		String,
		[EnumMember(Value="boolean")]
		Boolean,
		[EnumMember(Value="auto")]
		Auto
	}

}
