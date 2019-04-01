using System;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(ProcessorJsonConverter<RenameProcessor>))]
	public interface IRenameProcessor : IProcessor
	{
		[JsonProperty("field")]
		Field Field { get; set; }

		[JsonProperty("target_field")]
		Field TargetField { get; set; }
	}

	public class RenameProcessor : ProcessorBase, IRenameProcessor
	{
		public Field Field { get; set; }
		public Field TargetField { get; set; }
		protected override string Name => "rename";
	}

	public class RenameProcessorDescriptor<T>
		: ProcessorDescriptorBase<RenameProcessorDescriptor<T>, IRenameProcessor>, IRenameProcessor
		where T : class
	{
		protected override string Name => "rename";
		Field IRenameProcessor.Field { get; set; }
		Field IRenameProcessor.TargetField { get; set; }

		public RenameProcessorDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		public RenameProcessorDescriptor<T> Field(Expression<Func<T, object>> objectPath) =>
			Assign(objectPath, (a, v) => a.Field = v);

		public RenameProcessorDescriptor<T> TargetField(Field field) => Assign(field, (a, v) => a.TargetField = v);

		public RenameProcessorDescriptor<T> TargetField(Expression<Func<T, object>> objectPath) =>
			Assign(objectPath, (a, v) => a.TargetField = v);
	}
}
