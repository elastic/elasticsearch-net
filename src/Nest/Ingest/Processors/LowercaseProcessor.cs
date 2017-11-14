using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
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
