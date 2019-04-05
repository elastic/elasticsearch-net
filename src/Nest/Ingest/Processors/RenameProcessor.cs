using System;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Renames an existing field. If the field doesn’t exist or the new name is already used, an exception will be thrown.
	/// </summary>
	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(ProcessorJsonConverter<RenameProcessor>))]
	public interface IRenameProcessor : IProcessor
	{
		/// <summary>
		/// The field to be renamed. Supports template snippets.
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
		/// The new name of the field. Supports template snippets.
		/// </summary>
		[JsonProperty("target_field")]
		Field TargetField { get; set; }
	}

	/// <inheritdoc cref="IRenameProcessor" />
	public class RenameProcessor : ProcessorBase, IRenameProcessor
	{
		/// <inheritdoc />
		public Field Field { get; set; }

		/// <inheritdoc />
		public bool? IgnoreMissing { get; set; }

		/// <inheritdoc />
		public Field TargetField { get; set; }

		protected override string Name => "rename";
	}

	/// <inheritdoc cref="IRenameProcessor" />
	public class RenameProcessorDescriptor<T>
		: ProcessorDescriptorBase<RenameProcessorDescriptor<T>, IRenameProcessor>, IRenameProcessor
		where T : class
	{
		protected override string Name => "rename";
		Field IRenameProcessor.Field { get; set; }
		bool? IRenameProcessor.IgnoreMissing { get; set; }
		Field IRenameProcessor.TargetField { get; set; }

		/// <inheritdoc cref="IRenameProcessor.Field" />
		public RenameProcessorDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		/// <inheritdoc cref="IRenameProcessor.Field" />
		public RenameProcessorDescriptor<T> Field(Expression<Func<T, object>> objectPath) =>
			Assign(objectPath, (a, v) => a.Field = v);

		/// <inheritdoc cref="IRenameProcessor.TargetField" />
		public RenameProcessorDescriptor<T> TargetField(Field field) => Assign(field, (a, v) => a.TargetField = v);

		/// <inheritdoc cref="IRenameProcessor.TargetField" />
		public RenameProcessorDescriptor<T> TargetField(Expression<Func<T, object>> objectPath) =>
			Assign(objectPath, (a, v) => a.TargetField = v);

		/// <inheritdoc cref="IRenameProcessor.IgnoreMissing" />
		public RenameProcessorDescriptor<T> IgnoreMissing(bool? ignoreMissing = true) => Assign(a => a.IgnoreMissing = ignoreMissing);
	}
}
