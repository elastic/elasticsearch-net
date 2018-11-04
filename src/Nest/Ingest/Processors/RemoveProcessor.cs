using System;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(ProcessorJsonConverter<RemoveProcessor>))]
	public interface IRemoveProcessor : IProcessor
	{
		[JsonProperty("field")]
		Field Field { get; set; }
	}

	public class RemoveProcessor : ProcessorBase, IRemoveProcessor
	{
		public Field Field { get; set; }
		protected override string Name => "remove";
	}

	public class RemoveProcessorDescriptor<T>
		: ProcessorDescriptorBase<RemoveProcessorDescriptor<T>, IRemoveProcessor>, IRemoveProcessor
		where T : class
	{
		protected override string Name => "remove";

		Field IRemoveProcessor.Field { get; set; }

		public RemoveProcessorDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		public RemoveProcessorDescriptor<T> Field(Expression<Func<T, object>> objectPath) =>
			Assign(a => a.Field = objectPath);
	}
}
