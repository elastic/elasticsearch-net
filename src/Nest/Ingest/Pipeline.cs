using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(PipelineJsonConverter))]
	public interface IPipeline
	{
		[JsonProperty("description")]
		string Description { get; set; }

		[JsonProperty("on_failure")]
		IEnumerable<IProcessor> OnFailure { get; set; }

		[JsonProperty("processors")]
		IEnumerable<IProcessor> Processors { get; set; }
	}

	public class Pipeline : IPipeline
	{
		public string Description { get; set; }

		public IEnumerable<IProcessor> OnFailure { get; set; }

		public IEnumerable<IProcessor> Processors { get; set; }
	}

	public class PipelineDescriptor : DescriptorBase<PipelineDescriptor, IPipeline>, IPipeline
	{
		string IPipeline.Description { get; set; }
		IEnumerable<IProcessor> IPipeline.OnFailure { get; set; }
		IEnumerable<IProcessor> IPipeline.Processors { get; set; }

		/// <inheritdoc />
		public PipelineDescriptor Description(string description) => Assign(description, (a, v) => a.Description = v);

		/// <inheritdoc />
		public PipelineDescriptor Processors(IEnumerable<IProcessor> processors) => Assign(processors.ToListOrNullIfEmpty(), (a, v) => a.Processors = v);

		/// <inheritdoc />
		public PipelineDescriptor Processors(Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> selector) =>
			Assign(selector, (a, v) => a.Processors = v?.Invoke(new ProcessorsDescriptor())?.Value);

		/// <inheritdoc />
		public PipelineDescriptor OnFailure(IEnumerable<IProcessor> processors) => Assign(processors.ToListOrNullIfEmpty(), (a, v) => a.OnFailure = v);

		/// <inheritdoc />
		public PipelineDescriptor OnFailure(Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> selector) =>
			Assign(selector, (a, v) => a.OnFailure = v?.Invoke(new ProcessorsDescriptor())?.Value);
	}
}
