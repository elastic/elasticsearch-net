using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Elasticsearch.Net;


namespace Nest
{
	[InterfaceDataContract]
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

		public ConvertProcessorDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		public ConvertProcessorDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.Field = v);

		public ConvertProcessorDescriptor<T> TargetField(Field field) => Assign(field, (a, v) => a.TargetField = v);

		public ConvertProcessorDescriptor<T> TargetField<TValue>(Expression<Func<T, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.TargetField = v);

		public ConvertProcessorDescriptor<T> Type(ConvertProcessorType? type) => Assign(type, (a, v) => a.Type = v);
	}

	[StringEnum]
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
