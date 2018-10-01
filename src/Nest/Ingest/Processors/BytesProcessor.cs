using Newtonsoft.Json;
using System;
using System.Linq.Expressions;

namespace Nest
{
	/// <summary>
	/// URL-decodes a string
	/// </summary>
	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(ProcessorJsonConverter<BytesProcessor>))]
	public interface IBytesProcessor : IProcessor
	{
		/// <summary> The field to convert bytes from </summary>
		[JsonProperty("field")]
		Field Field { get; set; }

		/// <summary> The field to assign the converted value to, by default <see cref="Field"/> is updated in-place </summary>
		[JsonProperty("target_field")]
		Field TargetField { get; set; }

		/// <summary>
		/// If <c>true</c> and <see cref="Field"/> does not exist or is null,
		/// the processor quietly exits without modifying the document. Default is <c>false</c>
		/// </summary>
		[JsonProperty("ignore_missing")]
		bool? IgnoreMissing { get; set; }
	}

	/// <inheritdoc cref="IBytesProcessor"/>
	public class BytesProcessor : ProcessorBase, IBytesProcessor
	{
		protected override string Name => "bytes";

		/// <inheritdoc />
		[JsonProperty("field")]
		public Field Field { get; set; }

		/// <inheritdoc />
		[JsonProperty("target_field")]
		public Field TargetField { get; set; }

		/// <inheritdoc />
		[JsonProperty("ignore_missing")]
		public bool? IgnoreMissing { get; set; }
	}

	/// <inheritdoc cref="IBytesProcessor"/>
	public class BytesProcessorDescriptor<T>
		: ProcessorDescriptorBase<BytesProcessorDescriptor<T>, IBytesProcessor>, IBytesProcessor
		where T : class
	{
		protected override string Name => "bytes";

		Field IBytesProcessor.Field { get; set; }
		Field IBytesProcessor.TargetField { get; set; }
		bool? IBytesProcessor.IgnoreMissing { get; set; }

		/// <inheritdoc cref="IBytesProcessor.Field"/>
		public BytesProcessorDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		/// <inheritdoc cref="IBytesProcessor.Field"/>
		public BytesProcessorDescriptor<T> Field(Expression<Func<T, object>> objectPath) => Assign(a => a.Field = objectPath);

		/// <inheritdoc cref="IBytesProcessor.TargetField"/>
		public BytesProcessorDescriptor<T> TargetField(Field field) => Assign(a => a.TargetField = field);

		/// <inheritdoc cref="IBytesProcessor.TargetField"/>
		public BytesProcessorDescriptor<T> TargetField(Expression<Func<T, object>> objectPath) => Assign(a => a.TargetField = objectPath);

		/// <inheritdoc cref="IBytesProcessor.IgnoreMissing"/>
		public BytesProcessorDescriptor<T> IgnoreMissing(bool? ignoreMissing = true) => Assign(a => a.IgnoreMissing = ignoreMissing);
	}
}
