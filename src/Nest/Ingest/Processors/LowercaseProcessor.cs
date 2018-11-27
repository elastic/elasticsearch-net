using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	[JsonConverter(typeof(ProcessorJsonConverter<LowercaseProcessor>))]
	public interface ILowercaseProcessor : IProcessor
	{
		[DataMember(Name ="field")]
		Field Field { get; set; }
	}

	public class LowercaseProcessor : ProcessorBase, ILowercaseProcessor
	{
		public Field Field { get; set; }
		protected override string Name => "lowercase";
	}

	public class LowercaseProcessorDescriptor<T>
		: ProcessorDescriptorBase<LowercaseProcessorDescriptor<T>, ILowercaseProcessor>, ILowercaseProcessor
		where T : class
	{
		protected override string Name => "lowercase";

		Field ILowercaseProcessor.Field { get; set; }

		public LowercaseProcessorDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		public LowercaseProcessorDescriptor<T> Field(Expression<Func<T, object>> objectPath) =>
			Assign(a => a.Field = objectPath);
	}
}
