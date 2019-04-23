using System;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(ProcessorJsonConverter<TrimProcessor>))]
	public interface ITrimProcessor : IProcessor
	{
		[JsonProperty("field")]
		Field Field { get; set; }
	}

	public class TrimProcessor : ProcessorBase, ITrimProcessor
	{
		public Field Field { get; set; }
		protected override string Name => "trim";
	}

	public class TrimProcessorDescriptor<T>
		: ProcessorDescriptorBase<TrimProcessorDescriptor<T>, ITrimProcessor>, ITrimProcessor
		where T : class
	{
		protected override string Name => "trim";

		Field ITrimProcessor.Field { get; set; }

		public TrimProcessorDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		public TrimProcessorDescriptor<T> Field(Expression<Func<T, object>> objectPath) =>
			Assign(objectPath, (a, v) => a.Field = v);
	}
}
