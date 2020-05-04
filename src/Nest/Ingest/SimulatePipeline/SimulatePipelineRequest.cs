// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	[MapsApi("ingest.simulate.json")]
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

	public partial class SimulatePipelineDescriptor
	{
		IEnumerable<ISimulatePipelineDocument> ISimulatePipelineRequest.Documents { get; set; }
		IPipeline ISimulatePipelineRequest.Pipeline { get; set; }

		public SimulatePipelineDescriptor Pipeline(Func<PipelineDescriptor, IPipeline> pipeline) =>
			Assign(pipeline, (a, v) => a.Pipeline = v?.Invoke(new PipelineDescriptor()));

		public SimulatePipelineDescriptor Documents(IEnumerable<ISimulatePipelineDocument> documents) => Assign(documents, (a, v) => a.Documents = v);

		public SimulatePipelineDescriptor Documents(Func<SimulatePipelineDocumentsDescriptor, IPromise<IList<ISimulatePipelineDocument>>> selector) =>
			Assign(selector, (a, v) => a.Documents = v?.Invoke(new SimulatePipelineDocumentsDescriptor())?.Value);
	}
}
