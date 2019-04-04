﻿using System;
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
		public Field Field { get; set; }
		public Field TargetField { get; set; }
		public ConvertProcessorType? Type { get; set; }
		protected override string Name => "convert";
	}

	public class ConvertProcessorDescriptor<T> : ProcessorDescriptorBase<ConvertProcessorDescriptor<T>, IConvertProcessor>, IConvertProcessor
		where T : class
	{
		protected override string Name => "convert";
		Field IConvertProcessor.Field { get; set; }
		Field IConvertProcessor.TargetField { get; set; }
		ConvertProcessorType? IConvertProcessor.Type { get; set; }

		public ConvertProcessorDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		public ConvertProcessorDescriptor<T> Field(Expression<Func<T, object>> objectPath) =>
			Assign(objectPath, (a, v) => a.Field = v);

		public ConvertProcessorDescriptor<T> TargetField(Field field) => Assign(field, (a, v) => a.TargetField = v);

		public ConvertProcessorDescriptor<T> TargetField(Expression<Func<T, object>> objectPath) =>
			Assign(objectPath, (a, v) => a.TargetField = v);

		public ConvertProcessorDescriptor<T> Type(ConvertProcessorType? type) => Assign(type, (a, v) => a.Type = v);
	}

	[JsonConverter(typeof(StringEnumConverter))]
	public enum ConvertProcessorType
	{
		[EnumMember(Value = "integer")]
		Integer,

		[EnumMember(Value = "float")]
		Float,

		[EnumMember(Value = "string")]
		String,

		[EnumMember(Value = "boolean")]
		Boolean,

		[EnumMember(Value = "auto")]
		Auto
	}
}
