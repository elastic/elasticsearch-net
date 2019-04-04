using System;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(ProcessorJsonConverter<SetProcessor>))]
	public interface ISetProcessor : IProcessor
	{
		[JsonProperty("field")]
		Field Field { get; set; }

		[JsonProperty("value")]
		[JsonConverter(typeof(SourceValueWriteConverter))]
		object Value { get; set; }
	}

	public class SetProcessor : ProcessorBase, ISetProcessor
	{
		public Field Field { get; set; }
		public object Value { get; set; }
		protected override string Name => "set";
	}

	public class SetProcessorDescriptor<T> : ProcessorDescriptorBase<SetProcessorDescriptor<T>, ISetProcessor>, ISetProcessor
		where T : class
	{
		protected override string Name => "set";
		Field ISetProcessor.Field { get; set; }
		object ISetProcessor.Value { get; set; }

		public SetProcessorDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		public SetProcessorDescriptor<T> Field(Expression<Func<T, object>> objectPath) =>
			Assign(objectPath, (a, v) => a.Field = v);

		public SetProcessorDescriptor<T> Value<TValue>(TValue value) => Assign(value, (a, v) => a.Value = v);
	}
}
