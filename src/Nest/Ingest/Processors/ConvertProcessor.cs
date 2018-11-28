using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Runtime.Serialization;
using Utf8Json;


namespace Nest
{
	[InterfaceDataContract]
	[JsonFormatter(typeof(ProcessorFormatter<ConvertProcessor>))]
	public interface IConvertProcessor : IProcessor
	{
		[DataMember(Name ="field")]
		Field Field { get; set; }

		[DataMember(Name ="target_field")]
		Field TargetField { get; set; }

		[DataMember(Name ="type")]
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

		public ConvertProcessorDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		public ConvertProcessorDescriptor<T> Field(Expression<Func<T, object>> objectPath) =>
			Assign(a => a.Field = objectPath);

		public ConvertProcessorDescriptor<T> TargetField(Field field) => Assign(a => a.TargetField = field);

		public ConvertProcessorDescriptor<T> TargetField(Expression<Func<T, object>> objectPath) =>
			Assign(a => a.TargetField = objectPath);

		public ConvertProcessorDescriptor<T> Type(ConvertProcessorType? type) => Assign(a => a.Type = type);
	}


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
