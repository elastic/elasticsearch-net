using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Nest
{
	public partial interface ISimulatePipelineRequest
	{
		[JsonProperty("pipeline")]
		IPipeline Pipeline { get; set; }

		[JsonProperty("docs")]
		IEnumerable<ISimulatePipelineDocument> Documents { get; set; }
	}

	public partial class SimulatePipelineRequest
	{
		public IPipeline Pipeline { get; set; }

		public IEnumerable<ISimulatePipelineDocument> Documents { get; set; }
	}

	[DescriptorFor("IngestSimulate")]
	public partial class SimulatePipelineDescriptor
	{
		IPipeline ISimulatePipelineRequest.Pipeline { get; set; }

		IEnumerable<ISimulatePipelineDocument> ISimulatePipelineRequest.Documents { get; set; }

		public SimulatePipelineDescriptor Pipeline(Func<PipelineDescriptor, IPipeline> pipeline) =>
			Assign(a => a.Pipeline = pipeline?.Invoke(new PipelineDescriptor()));

		public SimulatePipelineDescriptor Documents(IEnumerable<ISimulatePipelineDocument> documents) => Assign(a => a.Documents = documents);

		public SimulatePipelineDescriptor Documents(Func<SimulatePipelineDocumentsDescriptor, IPromise<IList<ISimulatePipelineDocument>>> selector) =>
			Assign(a => a.Documents = selector?.Invoke(new SimulatePipelineDocumentsDescriptor())?.Value);
	}
}
