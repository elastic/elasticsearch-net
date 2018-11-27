using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public partial interface ISimulatePipelineRequest
	{
		[DataMember(Name ="docs")]
		IEnumerable<ISimulatePipelineDocument> Documents { get; set; }

		[DataMember(Name ="pipeline")]
		IPipeline Pipeline { get; set; }
	}

	public partial class SimulatePipelineRequest
	{
		public IEnumerable<ISimulatePipelineDocument> Documents { get; set; }
		public IPipeline Pipeline { get; set; }
	}

	[DescriptorFor("IngestSimulate")]
	public partial class SimulatePipelineDescriptor
	{
		IEnumerable<ISimulatePipelineDocument> ISimulatePipelineRequest.Documents { get; set; }
		IPipeline ISimulatePipelineRequest.Pipeline { get; set; }

		public SimulatePipelineDescriptor Pipeline(Func<PipelineDescriptor, IPipeline> pipeline) =>
			Assign(a => a.Pipeline = pipeline?.Invoke(new PipelineDescriptor()));

		public SimulatePipelineDescriptor Documents(IEnumerable<ISimulatePipelineDocument> documents) => Assign(a => a.Documents = documents);

		public SimulatePipelineDescriptor Documents(Func<SimulatePipelineDocumentsDescriptor, IPromise<IList<ISimulatePipelineDocument>>> selector) =>
			Assign(a => a.Documents = selector?.Invoke(new SimulatePipelineDocumentsDescriptor())?.Value);
	}
}
