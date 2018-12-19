using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Similar to the Grok Processor, dissect also extracts structured fields out of a single text field
	/// within a document. However unlike the Grok Processor, dissect does not use Regular Expressions.
	/// This allows dissect’s syntax to be simple and for some cases faster than the Grok Processor.
	/// </summary>
	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(ProcessorJsonConverter<DissectProcessor>))]
	public interface IDissectProcessor : IProcessor
	{
		/// <summary> The field to dissect </summary>
		[JsonProperty("field")]
		Field Field { get; set; }

		/// <summary> The pattern to apply to the field </summary>
		[JsonProperty("pattern")]
		string Pattern { get; set; }

		/// <summary
		/// If true and field does not exist or is null, the processor quietly exits without modifying the document>
		/// </summary>
		[JsonProperty("ignore_missing")]
		bool? IgnoreMissing { get; set; }

		/// <summary> The character(s) that separate the appended fields. </summary>
		[JsonProperty("append_separator")]
		string AppendSeparator { get; set; }
	}

	/// <inheritdoc cref="IDissectProcessor"/>
	public class DissectProcessor : ProcessorBase, IDissectProcessor
	{
		protected override string Name => "dissect";

		/// <inheritdoc cref="IDissectProcessor.Field">
		public Field Field { get; set; }

		/// <inheritdoc cref="IDissectProcessor.Pattern">
		public string Pattern { get; set; }

		/// <inheritdoc cref="IDissectProcessor.IgnoreMissing">
		public bool? IgnoreMissing { get; set; }

		/// <inheritdoc cref="IDissectProcessor.AppendSeparator">
		public string AppendSeparator { get; set; }
	}

	/// <inheritdoc cref="IDissectProcessor"/>
	public class DissectProcessorDescriptor<T>
		: ProcessorDescriptorBase<DissectProcessorDescriptor<T>, IDissectProcessor>, IDissectProcessor
		where T : class
	{
		protected override string Name => "dissect";

		Field IDissectProcessor.Field { get; set; }
		string IDissectProcessor.Pattern { get; set; }
		bool? IDissectProcessor.IgnoreMissing { get; set; }
		string IDissectProcessor.AppendSeparator { get; set; }

		/// <inheritdoc cref="IDissectProcessor.Field">
		public DissectProcessorDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		/// <inheritdoc cref="IDissectProcessor.Field">
		public DissectProcessorDescriptor<T> Field(Expression<Func<T, object>> objectPath) =>
			Assign(a => a.Field = objectPath);

		/// <inheritdoc cref="IDissectProcessor.Pattern">
		public DissectProcessorDescriptor<T> Pattern(string pattern) =>
			Assign(a => a.Pattern = pattern);

		/// <inheritdoc cref="IDissectProcessor.IgnoreMissing">
		public DissectProcessorDescriptor<T> IgnoreMissing(bool? traceMatch = true) =>
			Assign(a => a.IgnoreMissing = traceMatch);

		/// <inheritdoc cref="IDissectProcessor.AppendSeparator">
		public DissectProcessorDescriptor<T> AppendSeparator(string appendSeparator) =>
			Assign(a => a.AppendSeparator = appendSeparator);

	}
}
