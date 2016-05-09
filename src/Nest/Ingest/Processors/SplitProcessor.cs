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
	[JsonConverter(typeof(ProcessorJsonConverter<SplitProcessor>))]
	public interface ISplitProcessor : IProcessor
	{
		[JsonProperty("field")]
		Field Field { get; set; }

		[JsonProperty("separator")]
		string Separator { get; set; }
	}

	public class SplitProcessor : ProcessorBase, ISplitProcessor
	{
		protected override string Name => "split";

		public Field Field { get; set; }

		public string Separator { get; set; }
	}

	public class SplitProcessorDescriptor<T>
		: ProcessorDescriptorBase<SplitProcessorDescriptor<T>, ISplitProcessor>, ISplitProcessor
		where T : class
	{
		protected override string Name => "split";

		Field ISplitProcessor.Field { get; set; }
		string ISplitProcessor.Separator { get; set; }

		public SplitProcessorDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		public SplitProcessorDescriptor<T> Field(Expression<Func<T, object>> objectPath) =>
			Assign(a => a.Field = objectPath);

		public SplitProcessorDescriptor<T> Separator(string separator) => Assign(a => a.Separator = separator);
	}
}
