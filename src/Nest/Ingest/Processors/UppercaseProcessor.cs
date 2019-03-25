using System;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Converts a string to its uppercase equivalent.
	/// </summary>
	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(ProcessorJsonConverter<UppercaseProcessor>))]
	public interface IUppercaseProcessor : IProcessor
	{
		/// <summary>
		/// The field to make uppercase
		/// </summary>
		[JsonProperty("field")]
		Field Field { get; set; }

		/// <summary>
		/// If <c>true</c> and <see cref="Field" /> does not exist or is null,
		/// the processor quietly exits without modifying the document. Default is <c>false</c>
		/// </summary>
		[JsonProperty("ignore_missing")]
		bool? IgnoreMissing { get; set; }

		/// <summary>
		/// The field to assign the converted value to, by default field is updated in-place
		/// </summary>
		[JsonProperty("target_field")]
		Field TargetField { get; set; }
	}

	public class UppercaseProcessor : ProcessorBase, IUppercaseProcessor
	{
		/// <inheritdoc />
		public Field Field { get; set; }

		/// <inheritdoc />
		public bool? IgnoreMissing { get; set; }

		/// <inheritdoc />
		public Field TargetField { get; set; }

		protected override string Name => "uppercase";
	}

	public class UppercaseProcessorDescriptor<T>
		: ProcessorDescriptorBase<UppercaseProcessorDescriptor<T>, IUppercaseProcessor>, IUppercaseProcessor
		where T : class
	{
		protected override string Name => "uppercase";

		Field IUppercaseProcessor.Field { get; set; }
		bool? IUppercaseProcessor.IgnoreMissing { get; set; }
		Field IUppercaseProcessor.TargetField { get; set; }

		/// <inheritdoc cref="IUppercaseProcessor.Field" />
		public UppercaseProcessorDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		/// <inheritdoc cref="IUppercaseProcessor.Field" />
		public UppercaseProcessorDescriptor<T> Field(Expression<Func<T, object>> objectPath) =>
			Assign(a => a.Field = objectPath);

		/// <inheritdoc cref="IUppercaseProcessor.TargetField" />
		public UppercaseProcessorDescriptor<T> TargetField(Field field) => Assign(a => a.TargetField = field);

		/// <inheritdoc cref="IUppercaseProcessor.TargetField" />
		public UppercaseProcessorDescriptor<T> TargetField(Expression<Func<T, object>> objectPath) =>
			Assign(a => a.TargetField = objectPath);

		/// <inheritdoc cref="IUppercaseProcessor.IgnoreMissing" />
		public UppercaseProcessorDescriptor<T> IgnoreMissing(bool? ignoreMissing = true) => Assign(a => a.IgnoreMissing = ignoreMissing);
	}
}
