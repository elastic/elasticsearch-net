using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
		protected override string Name => "set";
		public Field Field { get; set; }
		public object Value { get; set; }
	}

	public class SetProcessorDescriptor<T> : ProcessorDescriptorBase<SetProcessorDescriptor<T>, ISetProcessor>, ISetProcessor
		where T : class
	{
		protected override string Name => "set";
		Field ISetProcessor.Field { get; set; }
		object ISetProcessor.Value { get; set; }

		public SetProcessorDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		public SetProcessorDescriptor<T> Field(Expression<Func<T, object>> objectPath) =>
			Assign(a => a.Field = objectPath);

		public SetProcessorDescriptor<T> Value<TValue>(TValue value) => Assign(a => a.Value = value);
	}
}
