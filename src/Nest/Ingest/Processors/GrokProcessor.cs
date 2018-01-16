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
	[JsonConverter(typeof(ProcessorJsonConverter<GrokProcessor>))]
	public interface IGrokProcessor : IProcessor
	{
		[JsonProperty("field")]
		Field Field { get; set; }

		[JsonProperty("patterns")]
		IEnumerable<string> Patterns { get; set; }

		[JsonProperty("pattern_definitions")]
		IDictionary<string, string> PatternDefinitions { get; set; }

		[JsonProperty("trace_match")]
		bool? TraceMatch { get; set; }
	}

	public class GrokProcessor : ProcessorBase, IGrokProcessor
	{
		protected override string Name => "grok";

		public Field Field { get; set; }

		public IEnumerable<string> Patterns { get; set; }

		public IDictionary<string, string> PatternDefinitions { get; set; }

		public bool? TraceMatch { get; set; }
	}

	public class GrokProcessorDescriptor<T>
		: ProcessorDescriptorBase<GrokProcessorDescriptor<T>, IGrokProcessor>, IGrokProcessor
		where T : class
	{
		protected override string Name => "grok";

		Field IGrokProcessor.Field { get; set; }
		IEnumerable<string> IGrokProcessor.Patterns { get; set; }
		IDictionary<string, string> IGrokProcessor.PatternDefinitions { get; set; }
		bool? IGrokProcessor.TraceMatch { get; set; }

		public GrokProcessorDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		public GrokProcessorDescriptor<T> Field(Expression<Func<T, object>> objectPath) =>
			Assign(a => a.Field = objectPath);

		public GrokProcessorDescriptor<T> Patterns(IEnumerable<string> patterns) => Assign(a => a.Patterns = patterns);

		public GrokProcessorDescriptor<T> Patterns(params string[] patterns) => Assign(a => a.Patterns = patterns);

		public GrokProcessorDescriptor<T> PatternDefinitions(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> patternDefinitions) =>
			Assign(a => a.PatternDefinitions = patternDefinitions?.Invoke(new FluentDictionary<string, string>()));

		public GrokProcessorDescriptor<T> TraceMatch(bool? traceMatch = true) =>
			Assign(a => a.TraceMatch = traceMatch);
	}
}
