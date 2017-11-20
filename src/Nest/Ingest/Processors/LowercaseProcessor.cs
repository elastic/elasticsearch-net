using Newtonsoft.Json;
using System;
using System.Linq.Expressions;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(ProcessorJsonConverter<LowercaseProcessor>))]
	public interface ILowercaseProcessor : IProcessor
	{
		[JsonProperty("field")]
		Field Field { get; set; }
	}

	public class LowercaseProcessor : ProcessorBase, ILowercaseProcessor
	{
		protected override string Name => "lowercase";
		public Field Field { get; set; }
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
