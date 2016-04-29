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
	[JsonConverter(typeof(ProcessorJsonConverter<GsubProcessor>))]
	public interface IGsubProcessor : IProcessor
	{
		[JsonProperty("field")]
		Field Field { get; set; }

		[JsonProperty("pattern")]
		string Pattern { get; set; }

		[JsonProperty("replacement")]
		string Replacement { get; set; }
	}

	public class GsubProcessor : ProcessorBase, IGsubProcessor
	{
		protected override string Name => "gsub";

		public Field Field { get; set; }

		public string Pattern { get; set; }

		public string Replacement { get; set; }
	}

	public class GsubProcessorDescriptor<T>
		: ProcessorDescriptorBase<GsubProcessorDescriptor<T>, IGsubProcessor>, IGsubProcessor
		where T : class
	{
		protected override string Name => "gsub";

		Field IGsubProcessor.Field { get; set; }
		string IGsubProcessor.Pattern { get; set; }
		string IGsubProcessor.Replacement { get; set; }

		public GsubProcessorDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		public GsubProcessorDescriptor<T> Field(Expression<Func<T, object>> objectPath) =>
			Assign(a => a.Field = objectPath);

		public GsubProcessorDescriptor<T> Pattern(string pattern) => Assign(a => a.Pattern = pattern);

		public GsubProcessorDescriptor<T> Replacement(string replacement) => Assign(a => a.Replacement = replacement);
	}
}
