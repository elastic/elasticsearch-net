using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Processes elements in an array of unknown length.
	/// All processors can operate on elements inside an array, but if all elements of
	/// an array need to be processed in the same way, defining a processor for each
	/// element becomes cumbersome and tricky because it is likely that the number of
	/// elements in an array is unknown. For this reason the foreach processor exists.
	/// By specifying the field holding array elements and a processor that defines what
	/// should happen to each element, array fields can easily be preprocessed.
	/// </summary>
	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(ProcessorJsonConverter<ForeachProcessor>))]
	public interface IForeachProcessor : IProcessor
	{
		/// <summary>
		/// The array field
		/// </summary>
		[JsonProperty("field")]
		Field Field { get; set; }

		/// <summary>
		/// The processor to execute against each field
		/// </summary>
		[JsonProperty("processor")]
		IProcessor Processor { get; set; }

		/// <summary>
		/// If <c>true</c> and <see cref="Field" /> does not exist or is null,
		/// the processor quietly exits without modifying the document. Default is <c>false</c>
		/// </summary>
		[JsonProperty("ignore_missing")]
		bool? IgnoreMissing { get; set; }
	}

	/// <inheritdoc cref="IForeachProcessor"/>
	public class ForeachProcessor : ProcessorBase, IForeachProcessor
	{
		/// <inheritdoc />
		public Field Field { get; set; }
		/// <inheritdoc />
		public IProcessor Processor { get; set; }
		/// <inheritdoc />
		public bool? IgnoreMissing { get; set; }
		protected override string Name => "foreach";
	}

	/// <inheritdoc cref="IForeachProcessor"/>
	public class ForeachProcessorDescriptor<T>
		: ProcessorDescriptorBase<ForeachProcessorDescriptor<T>, IForeachProcessor>, IForeachProcessor
		where T : class
	{
		protected override string Name => "foreach";

		Field IForeachProcessor.Field { get; set; }
		IProcessor IForeachProcessor.Processor { get; set; }
		bool? IForeachProcessor.IgnoreMissing { get; set; }

		/// <inheritdoc cref="IForeachProcessor.Field"/>
		public ForeachProcessorDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		/// <inheritdoc cref="IForeachProcessor.Field"/>
		public ForeachProcessorDescriptor<T> Field(Expression<Func<T, object>> objectPath) =>
			Assign(a => a.Field = objectPath);

		/// <inheritdoc cref="IForeachProcessor.Processor"/>
		public ForeachProcessorDescriptor<T> Processor(Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> selector) =>
			Assign(a => a.Processor = selector?.Invoke(new ProcessorsDescriptor())?.Value?.FirstOrDefault());

		/// <inheritdoc cref="IForeachProcessor.IgnoreMissing" />
		public ForeachProcessorDescriptor<T> IgnoreMissing(bool? ignoreMissing = true) => Assign(a => a.IgnoreMissing = ignoreMissing);
	}
}
