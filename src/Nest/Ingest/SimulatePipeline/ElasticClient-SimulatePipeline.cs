// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <inheritdoc />
		SimulatePipelineResponse SimulatePipeline(Func<SimulatePipelineDescriptor, ISimulatePipelineRequest> selector);

		/// <inheritdoc />
		SimulatePipelineResponse SimulatePipeline(ISimulatePipelineRequest request);

		/// <inheritdoc />
		Task<SimulatePipelineResponse> SimulatePipelineAsync(Func<SimulatePipelineDescriptor, ISimulatePipelineRequest> selector,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<SimulatePipelineResponse> SimulatePipelineAsync(ISimulatePipelineRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		public SimulatePipelineResponse SimulatePipeline(Func<SimulatePipelineDescriptor, ISimulatePipelineRequest> selector) =>
			SimulatePipeline(selector?.Invoke(new SimulatePipelineDescriptor()));

		public SimulatePipelineResponse SimulatePipeline(ISimulatePipelineRequest request) =>
			DoRequest<ISimulatePipelineRequest, SimulatePipelineResponse>(request, request.RequestParameters);

		public Task<SimulatePipelineResponse> SimulatePipelineAsync(
			Func<SimulatePipelineDescriptor, ISimulatePipelineRequest> selector,
			CancellationToken ct = default
		) => SimulatePipelineAsync(selector?.Invoke(new SimulatePipelineDescriptor()), ct);

		public Task<SimulatePipelineResponse> SimulatePipelineAsync(ISimulatePipelineRequest request, CancellationToken ct = default) =>
			DoRequestAsync<ISimulatePipelineRequest, SimulatePipelineResponse>(request, request.RequestParameters, ct);
	}
}
