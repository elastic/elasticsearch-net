using System;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(ProcessorJsonConverter<UppercaseProcessor>))]
	public interface IUppercaseProcessor : IProcessor
	{
		[JsonProperty("field")]
		Field Field { get; set; }
	}

	public class UppercaseProcessor : ProcessorBase, IUppercaseProcessor
	{
		[JsonProperty("field")]
		public Field Field { get; set; }

		protected override string Name => "uppercase";
	}

	public class UppercaseProcessorDescriptor<T>
		: ProcessorDescriptorBase<UppercaseProcessorDescriptor<T>, IUppercaseProcessor>, IUppercaseProcessor
		where T : class
	{
		protected override string Name => "uppercase";

		Field IUppercaseProcessor.Field { get; set; }

		public UppercaseProcessorDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		public UppercaseProcessorDescriptor<T> Field(Expression<Func<T, object>> objectPath) =>
			Assign(a => a.Field = objectPath);
	}
}
