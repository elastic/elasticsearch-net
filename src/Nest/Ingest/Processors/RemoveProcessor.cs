using System;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Removes existing fields. If one field doesn't exist, an exception will be thrown.
	/// </summary>
	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(ProcessorJsonConverter<RemoveProcessor>))]
	public interface IRemoveProcessor : IProcessor
	{
		// TODO: Change to Fields in 7.x
		/// <summary>
		/// fields to be removed. Supports template snippets.
		/// </summary>
		[JsonProperty("field")]
		Field Field { get; set; }

		/// <summary>
		/// If <c>true</c> and <see cref="Field" /> does not exist or is null,
		/// the processor quietly exits without modifying the document. Default is <c>false</c>
		/// </summary>
		[JsonProperty("ignore_missing")]
		bool? IgnoreMissing { get; set; }
	}

	/// <inheritdoc cref="IRemoveProcessor" />
	public class RemoveProcessor : ProcessorBase, IRemoveProcessor
	{
		/// <inheritdoc />
		public Field Field { get; set; }

		/// <inheritdoc />
		public bool? IgnoreMissing { get; set; }

		protected override string Name => "remove";
	}

	/// <inheritdoc cref="IRemoveProcessor" />
	public class RemoveProcessorDescriptor<T>
		: ProcessorDescriptorBase<RemoveProcessorDescriptor<T>, IRemoveProcessor>, IRemoveProcessor
		where T : class
	{
		protected override string Name => "remove";

		Field IRemoveProcessor.Field { get; set; }
		bool? IRemoveProcessor.IgnoreMissing { get; set; }

		/// <inheritdoc cref="IRemoveProcessor.Field" />
		public RemoveProcessorDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		/// <inheritdoc cref="IRemoveProcessor.Field" />
		public RemoveProcessorDescriptor<T> Field(Expression<Func<T, object>> objectPath) => Assign(objectPath, (a, v) => a.Field = v);

		/// <inheritdoc cref="IRemoveProcessor.IgnoreMissing" />
		public RemoveProcessorDescriptor<T> IgnoreMissing(bool? ignoreMissing = true) => Assign(ignoreMissing, (a, v) => a.IgnoreMissing = v);
	}
}
