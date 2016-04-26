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
		string IProcessor.Name => this.Name;
		protected abstract string Name { get; }

		public IEnumerable<IProcessor> OnFailure { get; set; }
	}

	public abstract class ProcessorDescriptorBase<TProcessorDescriptor, TProcessorInterface> : DescriptorBase<TProcessorDescriptor, TProcessorInterface>, IProcessor
		where TProcessorDescriptor : ProcessorDescriptorBase<TProcessorDescriptor, TProcessorInterface>, TProcessorInterface
		where TProcessorInterface : class, IProcessor
	{
		IEnumerable<IProcessor> IProcessor.OnFailure { get; set; }
		string IProcessor.Name => this.Name;
		protected abstract string Name { get; }

		/// <inheritdoc/>
		public TProcessorDescriptor OnFailure(IEnumerable<IProcessor> processors) => Assign(a => a.OnFailure = processors.ToListOrNullIfEmpty());

		/// <inheritdoc/>
		public TProcessorDescriptor OnFailure(Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> selector) =>
			Assign(a => a.OnFailure = selector?.Invoke(new ProcessorsDescriptor())?.Value);
	}
}
