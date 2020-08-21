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
		GetPipelineResponse GetPipeline(Func<GetPipelineDescriptor, IGetPipelineRequest> selector = null);

		/// <inheritdoc />
		GetPipelineResponse GetPipeline(IGetPipelineRequest request);

		/// <inheritdoc />
		Task<GetPipelineResponse> GetPipelineAsync(Func<GetPipelineDescriptor, IGetPipelineRequest> selector = null,
			CancellationToken ct = default
		);

		/// <inheritdoc />
		Task<GetPipelineResponse> GetPipelineAsync(IGetPipelineRequest request, CancellationToken ct = default);
	}


	public partial class ElasticClient
	{
		/// <inheritdoc />
		public GetPipelineResponse GetPipeline(Func<GetPipelineDescriptor, IGetPipelineRequest> selector = null) =>
			GetPipeline(selector.InvokeOrDefault(new GetPipelineDescriptor()));

		/// <inheritdoc />
		public GetPipelineResponse GetPipeline(IGetPipelineRequest request) =>
			DoRequest<IGetPipelineRequest, GetPipelineResponse>(request, request.RequestParameters);

		/// <inheritdoc />
		public Task<GetPipelineResponse> GetPipelineAsync(
			Func<GetPipelineDescriptor, IGetPipelineRequest> selector = null,
			CancellationToken ct = default
		) =>
			GetPipelineAsync(selector.InvokeOrDefault(new GetPipelineDescriptor()), ct);

		/// <inheritdoc />
		public Task<GetPipelineResponse> GetPipelineAsync(IGetPipelineRequest request, CancellationToken ct = default) =>
			DoRequestAsync<IGetPipelineRequest, GetPipelineResponse>(request, request.RequestParameters, ct);
	}
}
