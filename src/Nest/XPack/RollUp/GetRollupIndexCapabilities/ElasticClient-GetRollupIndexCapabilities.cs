using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;

namespace Nest
{
	public partial interface IElasticClient
	{
		/// <summary>
		/// </summary>
		GetRollupIndexCapabilitiesResponse GetRollupIndexCapabilities(
			IndexName index,
			Func<GetRollupIndexCapabilitiesDescriptor, IGetRollupIndexCapabilitiesRequest> selector = null
		);

		/// <inheritdoc cref="GetRollupIndexCapabilities(IndexName, System.Func{Nest.GetRollupIndexCapabilitiesDescriptor,Nest.IGetRollupIndexCapabilitiesRequest})" />
		GetRollupIndexCapabilitiesResponse GetRollupIndexCapabilities(IGetRollupIndexCapabilitiesRequest request);

		/// <inheritdoc cref="GetRollupIndexCapabilities(IndexName, System.Func{Nest.GetRollupIndexCapabilitiesDescriptor,Nest.IGetRollupIndexCapabilitiesRequest})" />
		Task<GetRollupIndexCapabilitiesResponse> GetRollupIndexCapabilitiesAsync(
			IndexName index,
			Func<GetRollupIndexCapabilitiesDescriptor, IGetRollupIndexCapabilitiesRequest> selector = null,
			CancellationToken cancellationToken = default
		);

		/// <inheritdoc cref="GetRollupIndexCapabilities(IndexName, System.Func{Nest.GetRollupIndexCapabilitiesDescriptor,Nest.IGetRollupIndexCapabilitiesRequest})" />
		Task<GetRollupIndexCapabilitiesResponse> GetRollupIndexCapabilitiesAsync(IGetRollupIndexCapabilitiesRequest request,
			CancellationToken ct = default
		);
	}

	public partial class ElasticClient
	{
		/// <inheritdoc cref="GetRollupIndexCapabilities(IndexName, System.Func{Nest.GetRollupIndexCapabilitiesDescriptor,Nest.IGetRollupIndexCapabilitiesRequest})" />
		public GetRollupIndexCapabilitiesResponse GetRollupIndexCapabilities(
			IndexName index,
			Func<GetRollupIndexCapabilitiesDescriptor, IGetRollupIndexCapabilitiesRequest> selector = null
		) =>
			GetRollupIndexCapabilities(selector.InvokeOrDefault(new GetRollupIndexCapabilitiesDescriptor(index)));

		/// <inheritdoc cref="GetRollupIndexCapabilities(IndexName, System.Func{Nest.GetRollupIndexCapabilitiesDescriptor,Nest.IGetRollupIndexCapabilitiesRequest})" />
		public GetRollupIndexCapabilitiesResponse GetRollupIndexCapabilities(IGetRollupIndexCapabilitiesRequest request) =>
			DoRequest<IGetRollupIndexCapabilitiesRequest, GetRollupIndexCapabilitiesResponse>(request, request.RequestParameters);

		/// <inheritdoc cref="GetRollupIndexCapabilities(IndexName, System.Func{Nest.GetRollupIndexCapabilitiesDescriptor,Nest.IGetRollupIndexCapabilitiesRequest})" />
		public Task<GetRollupIndexCapabilitiesResponse> GetRollupIndexCapabilitiesAsync(
			IndexName index,
			Func<GetRollupIndexCapabilitiesDescriptor, IGetRollupIndexCapabilitiesRequest> selector = null,
			CancellationToken cancellationToken = default
		) =>
			GetRollupIndexCapabilitiesAsync(selector.InvokeOrDefault(new GetRollupIndexCapabilitiesDescriptor(index)), cancellationToken);

		/// <inheritdoc cref="GetRollupIndexCapabilities(IndexName, System.Func{Nest.GetRollupIndexCapabilitiesDescriptor,Nest.IGetRollupIndexCapabilitiesRequest})" />
		public Task<GetRollupIndexCapabilitiesResponse> GetRollupIndexCapabilitiesAsync(
			IGetRollupIndexCapabilitiesRequest request,
			CancellationToken ct = default
		) =>
			DoRequestAsync<IGetRollupIndexCapabilitiesRequest, GetRollupIndexCapabilitiesResponse, GetRollupIndexCapabilitiesResponse>
				(request, request.RequestParameters, ct);
	}
}
