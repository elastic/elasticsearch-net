using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(PipelineJsonConverter))]
	public interface IPipeline
	{
		[JsonProperty("description")]
		string Description { get; set; }

		[JsonProperty("processors")]
		IEnumerable<IProcessor> Processors { get; set; }

		[JsonProperty("on_failure")]
		IEnumerable<IProcessor> OnFailure { get; set; }
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
		IEnumerable<IProcessor> IPipeline.Processors { get; set; }
		IEnumerable<IProcessor> IPipeline.OnFailure { get; set; }

		/// <inheritdoc/>
		public PipelineDescriptor Description(string description) => Assign(a => a.Description = description);

		/// <inheritdoc/>
		public PipelineDescriptor Processors(IEnumerable<IProcessor> processors) => Assign(a => a.Processors = processors.ToListOrNullIfEmpty());

		/// <inheritdoc/>
		public PipelineDescriptor Processors(Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> selector) =>
			Assign(a => a.Processors = selector?.Invoke(new ProcessorsDescriptor())?.Value);

		/// <inheritdoc/>
		public PipelineDescriptor OnFailure(IEnumerable<IProcessor> processors) => Assign(a => a.OnFailure = processors.ToListOrNullIfEmpty());

		/// <inheritdoc/>
		public PipelineDescriptor OnFailure(Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> selector) =>
			Assign(a => a.OnFailure = selector?.Invoke(new ProcessorsDescriptor())?.Value);
	}
}
