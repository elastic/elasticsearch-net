using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(ProcessorJsonConverter<ForeachProcessor>))]
	public interface IForeachProcessor : IProcessor
	{
		[JsonProperty("field")]
		Field Field { get; set; }

		[JsonProperty("processor")]
		IProcessor Processor { get; set; }
	}

	public class ForeachProcessor : ProcessorBase, IForeachProcessor
	{
		protected override string Name => "foreach";
		public Field Field { get; set; }
		public IProcessor Processor { get; set; }
	}

	public class ForeachProcessorDescriptor<T>
		: ProcessorDescriptorBase<ForeachProcessorDescriptor<T>, IForeachProcessor>, IForeachProcessor
		where T : class
	{
		protected override string Name => "foreach";

		Field IForeachProcessor.Field { get; set; }

		IProcessor IForeachProcessor.Processor { get; set; }

		public ForeachProcessorDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		public ForeachProcessorDescriptor<T> Field(Expression<Func<T, object>> objectPath) =>
			Assign(a => a.Field = objectPath);

		public ForeachProcessorDescriptor<T> Processor(Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> selector) =>
			Assign(a => a.Processor = selector?.Invoke(new ProcessorsDescriptor())?.Value?.FirstOrDefault());
	}
}
