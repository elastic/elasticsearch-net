using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IPutPipelineRequest : IPipeline { }

	public partial class PutPipelineRequest
	{
		public string Description { get; set; }
		public IEnumerable<IProcessor> Processors { get; set; }
		public IEnumerable<IProcessor> OnFailure { get; set; }
	}

	[DescriptorFor("IngestPutPipeline")]
	public partial class PutPipelineDescriptor
	{
		string IPipeline.Description { get; set; }
		IEnumerable<IProcessor> IPipeline.Processors { get; set; }
		IEnumerable<IProcessor> IPipeline.OnFailure { get; set; }

		/// <inheritdoc/>
		public PutPipelineDescriptor Description(string description) => Assign(a => a.Description = description);

		/// <inheritdoc/>
		public PutPipelineDescriptor Processors(IEnumerable<IProcessor> processors) => Assign(a => a.Processors = processors.ToListOrNullIfEmpty());

		/// <inheritdoc/>
		public PutPipelineDescriptor Processors(Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> selector) =>
			Assign(a => a.Processors = selector?.Invoke(new ProcessorsDescriptor())?.Value);

		/// <inheritdoc/>
		public PutPipelineDescriptor OnFailure(IEnumerable<IProcessor> processors) => Assign(a => a.OnFailure = processors.ToListOrNullIfEmpty());

		/// <inheritdoc/>
		public PutPipelineDescriptor OnFailure(Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> selector) =>
			Assign(a => a.OnFailure = selector?.Invoke(new ProcessorsDescriptor())?.Value);
	}
}
