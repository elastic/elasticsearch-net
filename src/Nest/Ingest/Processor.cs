using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary> Ingest pipelines are composed of one or more processors </summary>
	public interface IProcessor
	{
		/// <summary> The name of the processor, will be used as the key when persisting the processor on the pipeline </summary>
		string Name { get; }

		/// <summary>
		/// If a processor fails, call these processors instead. Read more about handling failures here:
		/// https://www.elastic.co/guide/en/elasticsearch/reference/current/handling-failure-in-pipelines.html
		/// </summary>
		[JsonProperty("on_failure")]
		IEnumerable<IProcessor> OnFailure { get; set; }

		/// <summary> A painless script predicate that can control whether this processor should be executed or not </summary>
		[JsonProperty("if")]
		string If { get; set; }

		/// <summary>
		/// A tag is simply a string identifier of the specific instantiation of a certain processor
		/// in a pipeline. The tag field does not affect the processor’s behavior, but is very useful
		/// for bookkeeping and tracing errors to specific processors./
		/// </summary>
		[JsonProperty("tag")]
		string Tag { get; set; }

		/// <summary> When a failure happens, ignore it and proceed to the next processor </summary>
		[JsonProperty("ignore_failue")]
		bool? IgnoreFailure { get; set; }
	}

	/// <inheritdoc cref="IProcessor"/>
	public abstract class ProcessorBase : IProcessor
	{
		/// <inheritdoc cref="IProcessor.If"/>
		public string If { get; set; }
		/// <inheritdoc cref="IProcessor.Tag"/>
		public string Tag { get; set; }
		/// <inheritdoc cref="IProcessor.IgnoreFailure"/>
		public bool? IgnoreFailure { get; set; }
		/// <inheritdoc cref="IProcessor.OnFailure"/>
		public IEnumerable<IProcessor> OnFailure { get; set; }
		protected abstract string Name { get; }
		string IProcessor.Name => Name;
	}

	/// <inheritdoc cref="IProcessor"/>
	public abstract class ProcessorDescriptorBase<TProcessorDescriptor, TProcessorInterface>
		: DescriptorBase<TProcessorDescriptor, TProcessorInterface>, IProcessor
		where TProcessorDescriptor : ProcessorDescriptorBase<TProcessorDescriptor, TProcessorInterface>, TProcessorInterface
		where TProcessorInterface : class, IProcessor
	{
		protected abstract string Name { get; }
		string IProcessor.Name => Name;
		IEnumerable<IProcessor> IProcessor.OnFailure { get; set; }
		string IProcessor.If { get; set; }
		string IProcessor.Tag { get; set; }
		bool? IProcessor.IgnoreFailure { get; set; }

		/// <inheritdoc cref="IProcessor.OnFailure"/>
		public TProcessorDescriptor OnFailure(IEnumerable<IProcessor> processors) => Assign(a => a.OnFailure = processors.ToListOrNullIfEmpty());

		/// <inheritdoc cref="IProcessor.OnFailure"/>
		public TProcessorDescriptor OnFailure(Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> selector) =>
			Assign(a => a.OnFailure = selector?.Invoke(new ProcessorsDescriptor())?.Value);

		/// <inheritdoc cref="IProcessor.If"/>
		public TProcessorDescriptor If(string painlessPredicate) => Assign(a => a.If = painlessPredicate);

		/// <inheritdoc cref="IProcessor.Tag"/>
		public TProcessorDescriptor Tag(string tag) => Assign(a => a.Tag = tag);

		/// <inheritdoc cref="IProcessor.IgnoreFailure"/>
		public TProcessorDescriptor IgnoreFailure(bool? ignoreFailure = true) => Assign(a => a.IgnoreFailure = ignoreFailure);

	}

}
