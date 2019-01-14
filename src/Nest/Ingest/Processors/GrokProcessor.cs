using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	public interface IGrokProcessor : IProcessor
	{
		[DataMember(Name ="field")]
		Field Field { get; set; }

		[DataMember(Name ="pattern_definitions")]
		IDictionary<string, string> PatternDefinitions { get; set; }

		[DataMember(Name ="patterns")]
		IEnumerable<string> Patterns { get; set; }

		[DataMember(Name ="trace_match")]
		bool? TraceMatch { get; set; }
	}

	public class GrokProcessor : ProcessorBase, IGrokProcessor
	{
		public Field Field { get; set; }

		public IDictionary<string, string> PatternDefinitions { get; set; }

		public IEnumerable<string> Patterns { get; set; }

		public bool? TraceMatch { get; set; }
		protected override string Name => "grok";
	}

	public class GrokProcessorDescriptor<T>
		: ProcessorDescriptorBase<GrokProcessorDescriptor<T>, IGrokProcessor>, IGrokProcessor
		where T : class
	{
		protected override string Name => "grok";

		Field IGrokProcessor.Field { get; set; }
		IDictionary<string, string> IGrokProcessor.PatternDefinitions { get; set; }
		IEnumerable<string> IGrokProcessor.Patterns { get; set; }
		bool? IGrokProcessor.TraceMatch { get; set; }

		public GrokProcessorDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		public GrokProcessorDescriptor<T> Field(Expression<Func<T, object>> objectPath) =>
			Assign(a => a.Field = objectPath);

		public GrokProcessorDescriptor<T> Patterns(IEnumerable<string> patterns) => Assign(a => a.Patterns = patterns);

		public GrokProcessorDescriptor<T> Patterns(params string[] patterns) => Assign(a => a.Patterns = patterns);

		public GrokProcessorDescriptor<T> PatternDefinitions(
			Func<FluentDictionary<string, string>, FluentDictionary<string, string>> patternDefinitions
		) =>
			Assign(a => a.PatternDefinitions = patternDefinitions?.Invoke(new FluentDictionary<string, string>()));

		public GrokProcessorDescriptor<T> TraceMatch(bool? traceMatch = true) =>
			Assign(a => a.TraceMatch = traceMatch);
	}
}
