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
	public interface IForeachProcessor : IProcessor
	{
		[JsonProperty("field")]
		Field Field { get; set; }

		[JsonProperty("processors")]
		IEnumerable<IProcessor> Processors { get; set; }
	}

	public class ForeachProcessor : ProcessorBase, IForeachProcessor
	{
		protected override string Name => "foreach";
		public Field Field { get; set; }
		public IEnumerable<IProcessor> Processors { get; set; }
	}

	public class ForeachProcessorDescriptor<T>
		: ProcessorDescriptorBase<ForeachProcessorDescriptor<T>, IForeachProcessor>, IForeachProcessor
		where T : class
	{
		protected override string Name => "foreach";

		Field IForeachProcessor.Field { get; set; }

		IEnumerable<IProcessor> IForeachProcessor.Processors { get; set; }

		public ForeachProcessorDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		public ForeachProcessorDescriptor<T> Field(Expression<Func<T, object>> objectPath) =>
			Assign(a => a.Field = objectPath);

		public ForeachProcessorDescriptor<T> Processors(Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> selector) =>
			Assign(a => a.Processors = selector?.Invoke(new ProcessorsDescriptor())?.Value);
	}
}
