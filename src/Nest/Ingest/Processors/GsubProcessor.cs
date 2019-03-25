using System;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Converts a string field by applying a regular expression and a replacement.
	/// If the field is not a string, the processor will throw an exception.
	/// </summary>
	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(ProcessorJsonConverter<GsubProcessor>))]
	public interface IGsubProcessor : IProcessor
	{
		/// <summary>
		/// The field to apply the replacement to
		/// </summary>
		[JsonProperty("field")]
		Field Field { get; set; }

		/// <summary>
		/// The field to assign the converted value to, by default field is updated in-place
		/// </summary>
		[JsonProperty("target_field")]
		Field TargetField { get; set; }

		/// <summary>
		/// The pattern to be replaced
		/// </summary>
		[JsonProperty("pattern")]
		string Pattern { get; set; }

		/// <summary>
		/// The string to replace the matching patterns with
		/// </summary>
		[JsonProperty("replacement")]
		string Replacement { get; set; }

		/// <summary>
		/// If <c>true</c> and <see cref="Field" /> does not exist or is null,
		/// the processor quietly exits without modifying the document. Default is <c>false</c>
		/// </summary>
		[JsonProperty("ignore_missing")]
		bool? IgnoreMissing { get; set; }
	}

	/// <inheritdoc cref="IGsubProcessor" />
	public class GsubProcessor : ProcessorBase, IGsubProcessor
	{
		/// <inheritdoc />
		public Field Field { get; set; }
		/// <inheritdoc />
		public Field TargetField { get; set; }
		/// <inheritdoc />
		public string Pattern { get; set; }
		/// <inheritdoc />
		public string Replacement { get; set; }
		/// <inheritdoc />
		public bool? IgnoreMissing { get; set; }
		protected override string Name => "gsub";
	}

	/// <inheritdoc cref="IGsubProcessor" />
	public class GsubProcessorDescriptor<T>
		: ProcessorDescriptorBase<GsubProcessorDescriptor<T>, IGsubProcessor>, IGsubProcessor
		where T : class
	{
		protected override string Name => "gsub";

		Field IGsubProcessor.Field { get; set; }
		Field IGsubProcessor.TargetField { get; set; }
		string IGsubProcessor.Pattern { get; set; }
		string IGsubProcessor.Replacement { get; set; }
		bool? IGsubProcessor.IgnoreMissing { get; set; }

		/// <inheritdoc cref="IGsubProcessor.Field" />
		public GsubProcessorDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		/// <inheritdoc cref="IGsubProcessor.Field" />
		public GsubProcessorDescriptor<T> Field(Expression<Func<T, object>> objectPath) =>
			Assign(a => a.Field = objectPath);

		/// <inheritdoc cref="IGsubProcessor.TargetField" />
		public GsubProcessorDescriptor<T> TargetField(Field field) => Assign(a => a.TargetField = field);

		/// <inheritdoc cref="IGsubProcessor.TargetField" />
		public GsubProcessorDescriptor<T> TargetField(Expression<Func<T, object>> objectPath) =>
			Assign(a => a.TargetField = objectPath);

		/// <inheritdoc cref="IGsubProcessor.Pattern" />
		public GsubProcessorDescriptor<T> Pattern(string pattern) => Assign(a => a.Pattern = pattern);

		/// <inheritdoc cref="IGsubProcessor.Replacement" />
		public GsubProcessorDescriptor<T> Replacement(string replacement) => Assign(a => a.Replacement = replacement);

		/// <inheritdoc cref="IGsubProcessor.IgnoreMissing" />
		public GsubProcessorDescriptor<T> IgnoreMissing(bool? ignoreMissing = true) => Assign(a => a.IgnoreMissing = ignoreMissing);
	}
}
