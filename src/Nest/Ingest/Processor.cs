using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IProcessor
	{
		string Name { get; }

		[JsonProperty("on_failure")]
		IEnumerable<IProcessor> OnFailure { get; set; }
	}

	public abstract class ProcessorBase : IProcessor
	{
		public IEnumerable<IProcessor> OnFailure { get; set; }
		protected abstract string Name { get; }
		string IProcessor.Name => Name;
	}

	public abstract class ProcessorDescriptorBase<TProcessorDescriptor, TProcessorInterface>
		: DescriptorBase<TProcessorDescriptor, TProcessorInterface>, IProcessor
		where TProcessorDescriptor : ProcessorDescriptorBase<TProcessorDescriptor, TProcessorInterface>, TProcessorInterface
		where TProcessorInterface : class, IProcessor
	{
		protected abstract string Name { get; }
		string IProcessor.Name => Name;
		IEnumerable<IProcessor> IProcessor.OnFailure { get; set; }

		/// <inheritdoc />
		public TProcessorDescriptor OnFailure(IEnumerable<IProcessor> processors) => Assign(a => a.OnFailure = processors.ToListOrNullIfEmpty());

		/// <inheritdoc />
		public TProcessorDescriptor OnFailure(Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> selector) =>
			Assign(a => a.OnFailure = selector?.Invoke(new ProcessorsDescriptor())?.Value);
	}
}
